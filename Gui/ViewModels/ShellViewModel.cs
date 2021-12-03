using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task4.Models;

namespace Task4.ViewModels
{
    public class ShellViewModel : Screen
    {
        private string _id;
        private string _kurs;
        private string _grupp;
        private string _moment;
        private string _lärare;
        private string _lokal;
        private string _information;
        private string _startTid = DateTime.Now.ToString("t");
        private string _slutTid = DateTime.Now.ToString("t");
        private string _bild;
        private BindableCollection<Lesson> _lesson = new BindableCollection<Lesson>();
        private Lesson _selectedLesson;
        private string _startDatum = DateTime.Now.ToString("yyyy-MM-d");
        private string _slutDatum = DateTime.Now.ToString("yyyy-MM-d");
        private string _search;

        public string Search
        {
            get { return _search; }
            set { _search = value; }
        }
        public string SlutDatum
        {
            get { return _slutDatum; }
            set { _slutDatum = value; }
        }
        public string StartDatum
        {
            get { return _startDatum; }
            set { _startDatum = value; }
        }
        public string Bild
        {
            get { return _bild; }
            set { _bild = value; }
        }
        public Lesson SelectedLesson
        {
            get { return _selectedLesson; }
            set
            {
                _selectedLesson = value;
                NotifyOfPropertyChange(() => SelectedLesson);
            }
        }
        public BindableCollection<Lesson> Lesson
        {
            get { return _lesson; }
            set { _lesson = value; }
        }
        public string SlutTid
        {
            get { return _slutTid; }
            set { _slutTid = value; }
        }
        public string StartTid
        {
            get { return _startTid; }
            set { _startTid = value; }
        }
        public string Information
        {
            get { return _information; }
            set { _information = value; }
        }
        public string Lokal
        {
            get { return _lokal; }
            set { _lokal = value; }
        }
        public string Lärare
        {
            get { return _lärare; }
            set { _lärare = value; }
        }
        public string Moment
        {
            get { return _moment; }
            set { _moment = value; }
        }
        public string Grupp
        {
            get { return _grupp; }
            set { _grupp = value; }
        }
        public string Kurs
        {
            get { return _kurs; }
            set { _kurs = value; }
        }
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string GetBild
        {
            get { return "http://users.du.se/~v19ponbo/Project/" + Bild; }
            set { }
        } // Finns två bilder att välja på: Zoom.jpg eller Sal.png


        /// <summary>
        /// Clears list the BindableCollection
        /// </summary>
        public void ClearList() => Lesson.Clear();

        /// <summary>
        /// Makes Get request and adds all the values to a List
        /// </summary>
        /// <returns></returns>
        public async Task GetLesson()
        {
            ClearList();
            Api api = new Api(ApiRoutes.Library.Get);
            List<Lesson> lesson = await api.GetAll();

            foreach (var i in LessonProcess.Process(lesson))
            {
                string trunImg = ImageTruncate.ImageTrun(i.Bild);
                Lesson.Add(new Lesson() { Id = i.Id, Kurs = i.Kurs, Grupp = i.Grupp, Moment = i.Moment, Lärare = i.Lärare, Lokal = i.Lokal, Information = i.Information, PStartTid = i.PStartTid, PSlutTid = i.PSlutTid, Bild = i.Bild, BildPath = @$"C:\Users\46730\Desktop\Skola\Objektorienterad design och problemlösning\Task4\Bilder\{trunImg}" });
            }
        }

        /// <summary>
        /// Makes a Post request with all the values entered in xaml
        /// </summary>
        /// <returns></returns>
        public async Task AddLesson()
        {
            ClearList();
            ParseDate p = new(StartTid, SlutTid, StartDatum, SlutDatum);

            var body = new Lesson { Kurs = Kurs, Grupp = Grupp, Moment = Moment, Lärare = Lärare, Lokal = Lokal, Information = Information, StartTid = p.LektionStart, SlutTid = p.LektionSlut, Bild = GetBild };

            Api api = new Api(ApiRoutes.Library.Post);
            await api.AddLesson(body);
            await GetLesson();
        }

        /// <summary>
        /// Makes a Delete request with the selectedLesson from the ListBox
        /// </summary>
        public async Task DeleteLesson()
        {
            Api api = new Api(ApiRoutes.Library.Delete);
            await api.DeleteLesson(SelectedLesson.Id);
            await GetLesson();
        }

        /// <summary>
        /// Makes a Put request with the selectedLesson from the ListBox
        /// </summary>
        /// <returns></returns>
        public async Task UpdateLesson()
        {
            Api api = new Api(ApiRoutes.Library.Put);

            ParseDate p = new(StartTid, SlutTid, StartDatum, SlutDatum);
            Lesson lesson = new Lesson { Id = SelectedLesson.Id, Kurs = Kurs, Grupp = Grupp, Moment = Moment, Lärare = Lärare, Lokal = Lokal, Information = Information, StartTid = p.LektionStart, SlutTid = p.LektionSlut, Bild = GetBild };

            await api.Put(SelectedLesson.Id, lesson);
            ClearList();
            await GetLesson();
        }

        /// <summary>
        /// Makes a Get request with the Search value entered in the textBox
        /// </summary>
        /// <returns></returns>
        public async Task SearchLesson()
        {
            ClearList();
            Api api = new Api(ApiRoutes.Library.Search);
            var lesson = await api.Search(Search);

            foreach (var i in lesson)
            {
                Lesson.Add(i);
            }
        }
    }
}
