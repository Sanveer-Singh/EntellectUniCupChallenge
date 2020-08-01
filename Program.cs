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

            bool[,] tempFilter1 = new bool[,] {
                                            { false, true, false},
                                            { true, true, true},
                                            { false, true, false},
                                         };
            bool[,] tempFilter2 = new bool[,] {
                                            {  true, },
                                            {  true, },
                                            {  true, },
                                         };
            DataLayer inputGrid = new DataLayer(7, 7, grid);
            DataLayer filter1 = new DataLayer(3, 3, tempFilter1);
            DataLayer filter2 = new DataLayer(3, 1, tempFilter2);

            List<DataLayer> filters = new List<DataLayer>();
            filters.Add(filter1);
            filters.Add(filter2);

            ConvolutionHandler handler = new ConvolutionHandler(7, 7, inputGrid, filters);
            handler.ConvolveFilters();
            Console.WriteLine(handler.ToString());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
