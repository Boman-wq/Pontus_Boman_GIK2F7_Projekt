using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.Models
{
    class ParseDate
    {
        public DateTime LektionStart;
        public DateTime LektionSlut;
        /// <summary>
        /// Parse the date from string to DateTime and adds one hour
        /// </summary>
        /// <param name="startTid"></param>
        /// <param name="slutTid"></param>
        /// <param name="startDatum"></param>
        /// <param name="slutDatum"></param>

        // När jag gör en post request till api:n tar den bort en eller två timmar from det datumet jag skickar in.
        public ParseDate(string startTid, string slutTid, string startDatum, string slutDatum)
        {
            string s = startDatum + "t" + startTid;
            string g = slutDatum + "t" + slutTid;

            DateTime ParseLektionStart;
            DateTime ParseLektionSlut;

            DateTime.TryParse(s, out ParseLektionStart);
            DateTime.TryParse(g, out ParseLektionSlut);

            //Tar man bort radarna nedanför blir det fel timmar i databasen.
            LektionSlut = ParseLektionSlut.AddHours(1);
            LektionStart = ParseLektionStart.AddHours(1);
        }
    }
}
