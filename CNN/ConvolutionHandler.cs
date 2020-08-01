using EntellectUniCupChallenge.CNN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntellectUniCupChallenge.CNN
{
    public class ConvolutionHandler
    {
        DataLayer InputGrid { get; set; }
        DataLayer OutputGrid { get; set; }
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
                this.OutputGrid.SetOccupiedLocation(c.Row, c.Col, true);
            }
        }

        public void ConvolveFilters()
        {
            bool blnNextFilter = false;
            foreach(var filter in Filters)
            {
                //create an ouput filter
                DataLayer outputFilter = new DataLayer(filter.Rows, filter.Cols);
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
                                outputFilter.SetOccupiedLocation(filterRow, filterCol, blnCanPlace);
                                

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
                                    bool blnOutputValue = filter.GetOccupiedLocation(filterRow, filterCol);
                                     this.OutputGrid.SetOccupiedLocation(inputLayerRowOffset + filterRow, inputLayerColOffset + filterCol,blnOutputValue);
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


    }
}
