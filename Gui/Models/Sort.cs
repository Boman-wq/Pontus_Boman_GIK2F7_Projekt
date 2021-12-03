using Caliburn.Micro;
using System.Collections.Generic;

namespace Task4.Models
{
    public static class Sort
    {
        //FUNGERAR INTE
        //Försökte göra så att jag sortera listan som visas i xaml. Den gick sönder när jag ändrade några klasser.
        public static BindableCollection<Lesson> Ascending(List<Lesson> list)
        {
            BindableCollection<Lesson> re = new();
            list.Sort((a, b) => a.StartTid.CompareTo(b.StartTid));
            foreach(var l in list)
            {
                re.Add(l);
            }
            return re;
        }
    }
}
