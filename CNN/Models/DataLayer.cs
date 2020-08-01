using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntellectUniCupChallenge.CNN.Models
{
   public class DataLayer
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        public bool[,] OccupiedLocations { get; set; }

        public DataLayer(int rows, int cols )
        {
            Rows = rows;
            Cols = cols;
            OccupiedLocations = new bool[Rows, Cols];
        }

        public DataLayer(int rows, int cols, bool[,] IsOccupied)
        {
            Rows = rows;
            Cols = cols;
            this.OccupiedLocations = IsOccupied;
        }

        public void SetOccupiedLocation(int row, int col,bool value)
        {
            if (IsInRange(row, col))
            {
                this.OccupiedLocations[row, col] = value;
            }
        }
        public bool GetOccupiedLocation(int row, int col)
        {
            if (IsInRange(row, col))
            {
                return this.OccupiedLocations[row, col];
            }
            else
            {
                throw new Exception("Error in DataLayer: Out of Range");
            }
        }

        public bool IsInRange(int row, int col)
        {
            return row >= 0 && row < this.Rows && col >= 0 && col < this.Cols;
        }
    }
}
