using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EntellectUniCupChallenge
{
    class RetriveShapes
    {
        private const string fileName = "shapes_file.json";
        private readonly string filePath = Environment.CurrentDirectory;

        public void GetData()
        {
            var rawJson = File.ReadAllText(Path.Combine(filePath, fileName));
            var response = JsonHelper.ToClass<Shapes>(rawJson);

            Console.WriteLine(response.ToString());
        }
    }
}
