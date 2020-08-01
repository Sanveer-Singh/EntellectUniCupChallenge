using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            ListofShapes shp = JsonConvert.DeserializeObject<ListofShapes>(File.ReadAllText(Path.Combine(filePath, fileName)));
            Console.WriteLine(shp.ToString());
           
            

        }
    }
}
