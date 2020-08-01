using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntellectUniCupChallenge
{
    class FileHandler
    {
        // incase you prefer working with an array
        // returns null if something went wrong
        public string[] GetlinesArray(string FileName)
        {

            string[] line = null;
            if (File.Exists(FileName))
            {
                // a array to hold the lines of the text file 
                // gets all the lines to an array
                line = File.ReadAllLines(FileName);


            }
            else
            {
                // use a null check to check for a problem with file reading
                return null;
            }
            return line;
        }
    }
}
