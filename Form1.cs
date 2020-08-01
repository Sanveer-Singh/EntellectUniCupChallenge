using EntellectUniCupChallenge.CNN;
using EntellectUniCupChallenge.CNN.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntellectUniCupChallenge
{
    public partial class Form1 : Form
    {
        // global variable because I am lazy now 
        string WorldFileName = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenInput_Click(object sender, EventArgs e)
        {
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog
            {
                // txt filters  
                Filter = "Image Files(*.INPUT;)|*.INPUT;"
            };
            if (open.ShowDialog() == DialogResult.OK)
            {
                // store string 
                WorldFileName = open.FileName;
                // make the world 
                WorldBuilder myworld = new WorldBuilder(open.FileName);
              
                List<Coordinate> coTest = myworld.GetBlockedCoords();
                rtxFileDisplay.Text = myworld.toString();

                List<DataLayer> filters = new List<DataLayer>();
                List<MapShape> SansShapes = myworld.GetMapShapeList();
                List<MapShape> shapeIds = new List<MapShape>();
                // initiialize the capacity 
                foreach (MapShape s in SansShapes )
                {
                    s.GetCapacity();
                    shapeIds.Add(s);
                }
                shapeIds = (from s in shapeIds orderby s.capacity descending select s).ToList();

                RetriveShapes retrieve = new RetriveShapes();
                foreach(var shape in shapeIds)
                {
                    filters.Add(retrieve.DataLayer((int)shape.ID));
                }

                ConvolutionHandler handler = new ConvolutionHandler(GridRows: (int)myworld.GetMapHeight(),GridCols: (int)myworld.GetMapWidth(), OccupiedCoordinates: myworld.GetBlockedCoords(),filters: filters);
                handler.ConvolveFilters();
                Console.WriteLine(handler.ToString());

            }
        }
    }
}
