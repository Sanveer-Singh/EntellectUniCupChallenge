using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EntellectUniCupChallenge
{
    class RetriveShapes
    {
        private const string fileName = "shapes_file.json";
        private readonly string filePath = Environment.CurrentDirectory;
        private ListofShapes listshapes;
        public RetriveShapes()
        {
            listshapes = JsonConvert.DeserializeObject<ListofShapes>(File.ReadAllText(Path.Combine(filePath, fileName)));
        }

        public Boolean[,] DataLayer(int shapeid)
        {
            int bndng_bx = 0;
            List<int[]> coords = new List<int[]>();
            foreach (Shapes shape in listshapes.shapes)
            { 
                if (shape.shape_id == shapeid)
                {
                    bndng_bx = shape.bounding_box;
                    foreach(Rotations rotations in shape.orientations)
                    {
                        foreach(int[] arrk in rotations.cells)
                        {
                            coords.Add(arrk);
                        }
                        break;
                    }
                    break;
                }
            }
            Boolean[,] rect = new Boolean[bndng_bx, bndng_bx];
            for (int r = 0; r < bndng_bx; r++)
            {
                for (int c = 0; c < bndng_bx; c++)
                {
                    rect[r, c] = false;
                }
            }

            for (int r = 0; r < bndng_bx; r++)
            {
                for(int c = 0; c < bndng_bx; c++)
                {
                    foreach(int[] g in coords)
                    {
                        if(r == g[0] && c == g[1])
                        {
                            rect[r, c] = true;
                        }
                    }
                }
            }
            return rect;
        }
    }
}
