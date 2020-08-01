using EntellectUniCupChallenge.CNN.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntellectUniCupChallenge.CNN
{
    public class ConvolutionHandler
    {
        DataLayer InputGrid { get; set; }
        List<DataLayer> Filters { get; set; }
        public int GridRows { get; set; }
        public int GridCols { get; set; }
        public List<Coordinate> OccupiedCoordinates { get; private set; }
        public int FilterStride { get; private set; } = 1;

        public ConvolutionHandler(int GridRows, int GridCols,List<Coordinate> OccupiedCoordinates)
        {
            //get the dimensions
            this.GridRows = GridRows;
            this.GridCols = GridCols;

            this.OccupiedCoordinates = OccupiedCoordinates;
            this.Filters = new List<DataLayer>();
            this.InputGrid = new DataLayer(this.GridRows, this.GridCols);
            
            //set the coordinates that are occupied
            foreach(var c in OccupiedCoordinates)
            {
                this.InputGrid.SetOccupiedLocation(c.Row, c.Col, true);
            }
        }
        public ConvolutionHandler(int GridRows, int GridCols, DataLayer inputGrid, DataLayer filter)
        {
            //get the dimensions
            this.GridRows = GridRows;
            this.GridCols = GridCols;

            this.OccupiedCoordinates = new List<Coordinate>();
            this.Filters = new List<DataLayer>();
            this.Filters.Add(filter);
            this.InputGrid = inputGrid;

            //set the coordinates that are occupied
            foreach (var c in OccupiedCoordinates)
            {
                this.InputGrid.SetOccupiedLocation(c.Row, c.Col, true);
            }
        }
        public ConvolutionHandler(int GridRows, int GridCols, DataLayer inputGrid, List<DataLayer> filters)
        {
            //get the dimensions
            this.GridRows = GridRows;
            this.GridCols = GridCols;

            this.OccupiedCoordinates = new List<Coordinate>();
            this.Filters = filters;

            this.InputGrid = inputGrid;


            //set the coordinates that are occupied
            foreach (var c in OccupiedCoordinates)
            {
                this.InputGrid.SetOccupiedLocation(c.Row, c.Col, true);

            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Grid Rows: {this.GridRows}\n");
            sb.Append($"Grid Cols: {this.GridCols}\n\n");
            for(int r = 0; r < this.GridRows; r++)
            {
                for(int c = 0; c < this.GridCols; c++)
                {
                    if(this.InputGrid.GetOccupiedLocation(r,c) == true)
                    {
                        sb.Append('T');
                    }
                    else
                    {
                        sb.Append('F');
                    }
                }
                sb.Append(Environment.NewLine);
            }

            sb.Append(Environment.NewLine);
            foreach(var filter in this.Filters)
            {
                sb.Append(filter.ToString());
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        public void ConvolveFilters()
        {


            bool blnNextFilter = false;
            foreach(var filter in Filters)
            {
              
                //this will track the inputLayerStarting row location
                int inputLayerRowOffset = 0;
                //going to move the filter this many rows
                for (int outputRow = 0; outputRow < this.GridRows; outputRow++)
                {
                    //this will track the inputlayer starting col location
                    int inputLayerColOffset =0;
                    //going to move the filter this many columns
                    for (int outputCol = 0; outputCol < this.GridCols; outputCol++)
                    {

                        bool blnCanPlace = false;
                        //loop through the filter rows
                        for (int filterRow = 0; filterRow < filter.Rows; filterRow++)
                        {
                        

                            //loop through the filter cols
                            for (int filterCol = 0; filterCol < filter.Cols; filterCol++)
                            {
                                blnCanPlace = false;

                                //multiply the filter with the grid
                                bool filterIsOccupied = filter.GetOccupiedLocation(filterRow, filterCol);
                                bool inputGridIsOccupied = this.InputGrid.GetOccupiedLocation(inputLayerRowOffset + filterRow, inputLayerColOffset + filterCol);
                            
                                if(inputGridIsOccupied == true && filterIsOccupied == true)
                                {
                                    blnCanPlace = false;
                                    //move the filter to the next location
                                    break;
                                }
                                else
                                {
                                    blnCanPlace = true;
                                }

                                

                            }
                            if(blnCanPlace == false)
                            {
                                //move to the filter on
                                break;
                            }

                        }
                       
                        if(blnCanPlace == false)
                        {
                            //dont set
                            blnNextFilter = false;
                        }
                        else
                        {
                            //set the output value and move on
                            for (int filterRow = 0; filterRow < filter.Rows; filterRow++)
                            {


                                //loop through the filter cols
                                for (int filterCol = 0; filterCol < filter.Cols; filterCol++)
                                {
                                    //get the filter value
                                    bool blnOutputValue = filter.GetOccupiedLocation(filterRow, filterCol);
                                    int placedRow = inputLayerRowOffset + filterRow;
                                    int placedCol = inputLayerColOffset + filterCol;
                                    //get the grid value
                                    filter.AddPlacedCoordinate(placedRow, placedCol);
                                     this.InputGrid.OROccupiedLocation(placedRow, placedCol,blnOutputValue);
                                }
                            }

                                    blnNextFilter = true;
                            break;
                        }


                        //move to the next column section
                        inputLayerColOffset += this.FilterStride;
                    }

                    if(blnNextFilter == true)
                    {
                        break;
                    }
                    //move to the next row 
                    inputLayerRowOffset += this.FilterStride;
                }

            }
        }

        public void GenerateOutput(string path, string fileName)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            StringBuilder sb = new StringBuilder();
            foreach (var filter in this.Filters)
            {
                sb.Append(filter.ToString());
                sb.Append(Environment.NewLine);
            }
            string fullName = Path.Combine(path, fileName);
            File.WriteAllText(fullName, sb.ToString());
        }

    }
}
