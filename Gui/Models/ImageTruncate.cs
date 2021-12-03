using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.Models
{
    static class ImageTruncate
    {
        /// <summary>
        /// removes the url from image and saves the image name
        /// </summary>
        /// <param name="input"></param>
        /// <returns>image name</returns>
        public static string ImageTrun(string input)
        {
            string output = input.Replace("http://users.du.se/~v19ponbo/Project/", "");
            return output;
        }
    }
}
