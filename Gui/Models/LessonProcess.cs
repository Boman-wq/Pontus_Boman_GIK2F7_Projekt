using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Net;

namespace Task4.Models
{
    static class LessonProcess
    {
        public static BindableCollection<Lesson> Process(List<Lesson> lesson)
        {
            BindableCollection<Lesson> bindLesson = new();
            List<Lesson> tempList = new();
            try
            {
                foreach (var i in lesson)
                {
                    using WebClient wc = new();
                    string trunImg = ImageTruncate.ImageTrun(i.Bild);
                    wc.DownloadFileAsync(new Uri(i.Bild), @$"C:\Users\46730\Desktop\Skola\Objektorienterad design och problemlösning\Task4\Bilder\{trunImg}");
                }
            }
            catch
            { }
            foreach (var i in lesson)
            {
                string trunImg = ImageTruncate.ImageTrun(i.Bild);
                tempList.Add(new Lesson() { Id = i.Id, Kurs = i.Kurs, Grupp = i.Grupp, Moment = i.Moment, Lärare = i.Lärare, Lokal = i.Lokal, Information = i.Information, PStartTid = i.StartTid.ToString("yyyy-MM-dd HH:mm:ss"), PSlutTid = i.SlutTid.ToString("yyyy-MM-dd HH:mm:ss"), Bild = i.Bild, BildPath = @$"C:\Users\46730\Desktop\Skola\Objektorienterad design och problemlösning\Task4\Bilder\{trunImg}" });
            }
            Sort.Ascending(tempList);

            foreach (var i in tempList)
            {
                string trunImg = ImageTruncate.ImageTrun(i.Bild);
                bindLesson.Add(new Lesson() { Id = i.Id, Kurs = i.Kurs, Grupp = i.Grupp, Moment = i.Moment, Lärare = i.Lärare, Lokal = i.Lokal, Information = i.Information, PStartTid = i.PStartTid, PSlutTid = i.PSlutTid, Bild = i.Bild, BildPath = @$"C:\Users\46730\Desktop\Skola\Objektorienterad design och problemlösning\Task4\Bilder\{trunImg}" });
            }
            return bindLesson;
        }
    }
}
