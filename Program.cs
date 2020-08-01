using EntellectUniCupChallenge.CNN;
using EntellectUniCupChallenge.CNN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntellectUniCupChallenge
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
    

            //test cnn
            bool[,] grid = new bool[,] { 
                                            {false,false,false,false,false,false,false},
                                            {false,true,false,false,false,false,false},
                                            {false,false,false,false,false,false,false},
                                            {false,false,false,false,true,false,false},
                                            {false,false,false,true,true,false,false},
                                            {false,false,false,false,false,false,false},
                                            {false,false,false,false,false,false,false},
                                           
                                        };

            bool[,] filter = new bool[,] {
                                            { false, true, false},
                                            { true, true, true},
                                            { false, true, false},
                                         };
            DataLayer inputGrid = new DataLayer(7, 7, grid);
            DataLayer inputFilter = new DataLayer(3, 3, filter);

            ConvolutionHandler handler = new ConvolutionHandler(7, 7, inputGrid, inputFilter);
            handler.ConvolveFilters();
            Console.WriteLine(handler.ToString());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
