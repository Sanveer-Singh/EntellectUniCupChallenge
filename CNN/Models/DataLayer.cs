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
        public int ID { get; set; }
        public List<Coordinate> PlacedCoordinates { get; set; } = new List<Coordinate>();

        public bool[,] OccupiedLocations { get; set; }

        public DataLayer(int rows, int cols,int ID = -1 )
        {
            Rows = rows;
            Cols = cols;
            this.ID = ID;
            OccupiedLocations = new bool[Rows, Cols];
        }

        public DataLayer(int rows, int cols, bool[,] IsOccupied, int ID = -1)
        {
            Rows = rows;
            Cols = cols;
            this.OccupiedLocations = IsOccupied;
            this.ID = ID;
        }

        public void AddPlacedCoordinate(int row, int col)
        {
            this.PlacedCoordinates.Add(new Coordinate(row, col));
        }

        public void SetOccupiedLocation(int row, int col,bool value)
        {
            if (IsInRange(row, col))
            {
                this.OccupiedLocations[row, col] = value;
            }
        }
        public void OROccupiedLocation(int row, int col, bool value)
        {
            if (IsInRange(row, col))
            {
                this.OccupiedLocations[row, col] |= value;
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
                return true;// throw new Exception("Error in DataLayer: Out of Range");
            }
        }

        public bool IsInRange(int row, int col)
        {
            return row >= 0 && row < this.Rows && col >= 0 && col < this.Cols;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{ID}");
            foreach(var coord in this.PlacedCoordinates)
            {
                sb.Append($"|{coord.ToString()}");
            }
            return sb.ToString();
        }
    }
}
