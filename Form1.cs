using EntellectUniCupChallenge.CNN;
using EntellectUniCupChallenge.CNN.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            // This will get the project directory
            string workingDirectory = Environment.CurrentDirectory;
            string projectRoot = Directory.GetParent(workingDirectory).Parent.FullName;
            string outputPath = new Uri(Path.Combine(projectRoot, "Output")).LocalPath;
            string openFilePath = new Uri(Path.Combine(projectRoot, "Data")).LocalPath;

            OpenFileDialog open = new OpenFileDialog
            {
                // txt filters  
                Filter = "Image Files(*.INPUT;)|*.INPUT;"
            };
            open.InitialDirectory = openFilePath;

            if (open.ShowDialog() == DialogResult.OK)
            {
                // store string 
                WorldFileName = open.FileName;
                // make the world 
                WorldBuilder myworld = new WorldBuilder(open.FileName);
              
                List<Coordinate> coTest = myworld.GetBlockedCoords();
                rtxFileDisplay.Text = myworld.toString();

                List<DataLayer> filters = new List<DataLayer>();
                var shapeIds = myworld.GetMapShapeList();
                RetriveShapes retrieve = new RetriveShapes();
                foreach(var shape in shapeIds)
                {
                    filters.Add(retrieve.DataLayer((int)shape.ID));
                }

                ConvolutionHandler handler = new ConvolutionHandler(GridRows: (int)myworld.GetMapHeight(),GridCols: (int)myworld.GetMapWidth(), OccupiedCoordinates: myworld.GetBlockedCoords(),filters: filters);
                handler.ConvolveFilters();


                Console.WriteLine(handler.ToString());

                var fileBits = open.FileName.Split(new char[] { '\\', '/' });
                string newName = fileBits[fileBits.Length - 1];

                handler.GenerateOutput(outputPath, $"{newName}_output.txt");
            }
        }
    }
}
