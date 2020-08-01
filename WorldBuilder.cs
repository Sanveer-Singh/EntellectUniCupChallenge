using EntellectUniCupChallenge.CNN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 
namespace EntellectUniCupChallenge
{
    class WorldBuilder
    {
        // publically accesible  variables that will make a world instance
        public string[] FileLines;
        public string FileName;
        public List<MapPoints> BlockedCellslist;
        //read
        public double MapHeight;
        //read
        public double MapWidth;
        // unique shapes read ( use to read each shapes ID and Co-Ordinates
        public int Shapes;
        // get the number of blocked cells
        public double BlockedCells;
        // shape list including ID and num available 
         public List<MapShape> WorldSHapes = new List<MapShape>();
        // get blocked cells ( line after all shapes ) line shapes +3 [size, num shapes, num cells]



        // a construtor to build the world 
        // to look for errors look for negatives or null lists
        public WorldBuilder(string FileName1)
        {
            // initialize everyithing just for good measure 
            FileName = FileName1;
            BlockedCellslist  = new List<MapPoints>();
            MapHeight = 0;
            MapWidth = 0;
            Shapes = 0;
            BlockedCells = 0;
            FileLines = null;

            // lets properly build the world 
            FileHandler myFilehandler = new FileHandler();
            // lets get the lines
            FileLines = myFilehandler.GetlinesArray(FileName);
            // lets get num shapes
            Shapes = GetNumShapes();
            //lets get width 
            MapWidth = GetMapWidth();
            // lets get map height 
            MapHeight = GetMapHeight();
            // lets get the number of blocked cells 
            BlockedCells = GetNumBlockedCells();
            // lets get a list of blocked cells
            BlockedCellslist = GetMapPoints();
            // get ShapesList
            WorldSHapes = GetMapShapeList();
        

        }

        /// <summary>
        /// coordinates for jarred
        /// </summary>
        /// <returns></returns>
        public List<Coordinate> GetBlockedCoords()
        {
            List<Coordinate> myCos = new List<Coordinate>();
            foreach (MapPoints p in BlockedCellslist )
            {
                Coordinate returner = new Coordinate((int)p.x, (int)p.y);
                myCos.Add(p);
            }

            return myCos;

        }



        // make a function that reads all the map shapes  starts at line 3 
        public List<MapShape> GetMapShapeList()
        {
            List<MapShape> WorldSHapes1 = new List<MapShape>();
            double ID = 0;
            double Number = 0;
            for(int x = 3; x< Shapes+3; x++ )
            {
                ID = GetValueFromPosition(FileLines[x], ',', 0);
                Number = GetValueFromPosition(FileLines[x], ',', 1);
                MapShape temp = new MapShape();
                temp.ID = ID;
                temp.NumAvailable = Number;
                WorldSHapes1.Add(temp);
            }

            return WorldSHapes1;
        }


        // make a function returns a value from from a line of text
        //-2 means general error in function
        //-1 means line length or splitter is incorrectly specified 
        // -3 means conversion error
        protected double GetValueFromPosition(string line, char splitter, int position)
        {
            double answer = -2;
            // do safety checks
            if ((line is null) || (line.Length == 0) || (splitter.ToString() is null))
            {
                // invalid call
                return -1;
            }
            else
            {
                // make a copy so the world doesnt get damaged
                string TempLine = line;
                // split by the splitter 
                string[] words = TempLine.Split(splitter);
                // try catch incase of conversion error
                try
                {
                    // convert the related word to double after trimming white space
                    answer = Convert.ToDouble(words[position].Trim());
                    // answer is done
                }
                catch (Exception ex)
                {
                    // returns -3 for a conversion error
                    answer = -3;
                }

            }
            // returns -2 saying that a problem has occured
            return answer;
        }
        public string getSubstringAtPosition(string line, char splitter, int position)
        {
            string answer = null;
            // do safety checks
            if ((line is null) || (line.Length == 0) || (splitter.ToString() is null))
            {
                // invalid call
                return null;
            }
            else
            {
                // make a copy so the world doesnt get damaged
                string TempLine = line;
                // split by the splitter 
                string[] words = TempLine.Split(splitter);
                // try catch incase of conversion error
                try
                {
                    // convert the related word to double after trimming white space
                    answer = words[position].Trim();
                    // answer is done
                }
                catch (Exception ex)
                {
                    // returns -3 for a conversion error
                    answer = null;
                }

            }
            // returns -2 saying that a problem has occured
            return answer;
        }

        /* get the number of unique shapes (position 1) line 2
         -1 is invalid file lines*/
        public int GetNumShapes()
        {
            int answer = -1;
            if (FileLines is null)
            {
                //return error output here
            }
            else
            {
                answer =(int) GetValueFromPosition(FileLines[1], ',', 0);
            }


            return answer;
        }

        /* get the number of Blocked cells (position 1) line 3
          -1 is invalid file lines*/
        public double GetNumBlockedCells()
        {
            double answer = -1;
            if (FileLines is null)
            {
                //return error output here
            }
            else
            {
                answer = GetValueFromPosition(FileLines[2], ',', 0);
            }


            return answer;
        }
        // get the map width (position 1)line 1 AKA Cols
        //-1 means  invalid file lines
        public double GetMapWidth()
        {
            double answer = -1;
            if (FileLines is null)
            {
                //return error output here
            }
            else
            {
                answer = GetValueFromPosition(FileLines[0], ',', 1);
            }


            return answer;
        }
        // get the map height (position 0) line 1 AKA rows
        //-1 means invalid file lines
        public double GetMapHeight()
        {
            double answer = -1;
            if (FileLines is null)
            {
                //return error output here
            }
            else
            {
                answer = GetValueFromPosition(FileLines[0], ',',0);
            }


            return answer;
        }
        // get all significant map points 
        // null indicates incorrect params
        public List<MapPoints> GetMapPoints()
        {
            List<MapPoints> points = new List<MapPoints>();
            // test if parms are any good
            if (FileLines is null)
            {
                // null indicates incorrect params
                points = null;
            }
            else
            {
             for(int x =Shapes +3;x<FileLines.GetLength(0);x++)
             {
                 // line x contains data for blocked cells 
                 for(int y= 0; y<BlockedCells;y++)
                 {
                       string temp = "";

                        temp = getSubstringAtPosition(FileLines[x], '|', y);
                        double x1, y1 = 0;
                        x1 = GetValueFromPosition(temp, ',', 0);
                        y1 = GetValueFromPosition(temp, ',', 1);
                        MapPoints p = new MapPoints(x1, y1);
                        points.Add(p);
                 }

             }
                
            }
            return points;
        }

        
      
        // make a to-string to make it easy

        public string toString()
        {// default response 
            string answer = "";
            answer += "File name : " + FileName + Environment.NewLine;
            answer += "Number of shapes : " + Convert.ToString(Shapes) + Environment.NewLine;
            answer += "Number of blocked cells : " + Convert.ToString(BlockedCells) + Environment.NewLine;
            //answer += "Number of bases : " + Convert.ToString(NumBases) + Environment.NewLine;
            answer += "map height : " + Convert.ToString(MapHeight) + Environment.NewLine;
            answer += "Map width  :" + Convert.ToString(MapWidth) + Environment.NewLine;
            //answer += "Crates in list :" + Convert.ToString(Crates.Count()) + Environment.NewLine;
           // answer += "CrateBases in List :" + Convert.ToString(Bases.Count()) + Environment.NewLine;

           

            return answer;

        }

    }

}
