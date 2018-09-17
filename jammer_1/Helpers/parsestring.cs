using Jammer_1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jammer_1.Helpers
{
    public class parsestring
    {
        string thestring;
        List<Instrument> instrumentlist = new List<Instrument>();
    List<Genre> genrelist = new List<Genre>();
        public List<Genre> Genrelist { get { return genrelist; } }
        public List <Instrument> Instrumentlist { get { return instrumentlist; } }
        public parsestring(string x)
        {
            thestring = x;
            parseme();
        }

        void parseme()
        {
            char firstdelimiter = ';';
            char seconddelimieter = ',';
            string[] instruments = thestring.Split(firstdelimiter);
            foreach (string i in instruments)
            {
                var instrument_string = i.Split(seconddelimieter);
                Instrument newinstrument = new Instrument();

                for (int x = 0; x < instrument_string.Length; x++)
                {
                    if (instrument_string[x] != "") { 
                    if (x == 0)
                    {
                        newinstrument.Instrument_name = instrument_string[x];
                    }
                    if (x == 1)
                    {
                        newinstrument.Skill_rating = instrument_string[x];
                    }
                    }
                    //do something with the element
                }
                instrumentlist.Add(newinstrument);

            }
        }
        public List<Genre> parsegenre(string input)
        {
            char seconddelimieter = ',';
            string[] genres = input.Split(seconddelimieter);
            for (int x = 0; x < genres.Length; x++)
            {
                if (genres[x] != "") {
                Genre newgenre = new Genre();
                newgenre.Name = genres[x];
                genrelist.Add(newgenre);
                }
            }
            return genrelist;
        }
        public static List<string> parselocation(string location)
        {
            char delimiter = ',';
            List<string> long_lat = new List<string>();
            string[] split_location = location.Split(delimiter);
            for (int x = 0; x < split_location.Length; x++)
            {
                if (split_location[x] != "") { long_lat.Add(split_location[x]); }

            }
            return long_lat;
        }
    }
}
