using Caliburn.Micro;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Task4.Models
{
    /// <summary>
    /// Routing all the api endpoints
    /// </summary>
    class Api
    {
        public static HttpClient client { get; set; }
        /// <summary>
        /// Base values for doing a request to API
        /// </summary>
        /// <param name="url"></param>
        public Api(string url)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                 new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Put request to API
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lesson"></param>
        /// <returns></returns>
        public async Task Put(string id, Lesson lesson)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(id, lesson);
        }

        /// <summary>
        /// Gets all Lektioner
        /// </summary>
        /// <returns>A list of lesson</returns>
        public async Task<List<Lesson>> GetAll()
        {
            string respons = await client.GetStringAsync(client.BaseAddress);
            List<Lesson> lesson = JsonConvert.DeserializeObject<List<Lesson>>(respons);

            return lesson;
        }
        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="lesson"></param>
        /// <returns></returns>
        public async Task AddLesson(Lesson lesson)
        {
            var json = JsonConvert.SerializeObject(lesson);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync(client.BaseAddress, data);
        }

        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="id">Id to delete</param>
        /// <returns></returns>
        public async Task DeleteLesson(string id)
        {
            await client.DeleteAsync(id);
        }

        /// <summary>
        /// Search request
        /// </summary>
        /// <param name="search">Lärare to search for</param>
        /// <returns></returns>
        public async Task<BindableCollection<Lesson>> Search(string search)
        {
            BindableCollection<Lesson> bindLesson = new();
            string respons = await client.GetStringAsync($"{client.BaseAddress}{search}");
            List<Lesson> lesson = JsonConvert.DeserializeObject<List<Lesson>>(respons);

            foreach (var i in lesson)
            {
                string trunImg = ImageTruncate.ImageTrun(i.Bild);
                bindLesson.Add(new Lesson() { Id = i.Id, Kurs = i.Kurs, Grupp = i.Grupp, Moment = i.Moment, Lärare = i.Lärare, Lokal = i.Lokal, Information = i.Information, PStartTid = i.StartTid.ToString("yyyy-MM-dd HH:mm:ss"), PSlutTid = i.SlutTid.ToString("yyyy-MM-dd HH:mm:ss"), Bild = i.Bild, BildPath = @$"C:\Users\46730\Desktop\Skola\Objektorienterad design och problemlösning\Task4\Bilder\{trunImg}" });
            }
            return bindLesson;
        }
    }
}
