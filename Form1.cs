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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RetriveShapes retriveshape = new RetriveShapes();
            Boolean[,] DataLayer = retriveshape.DataLayer(22);
            String str = "";
            for(int r = 0; r < Math.Sqrt(DataLayer.Length); r++)
            {
                for (int c = 0; c < Math.Sqrt(DataLayer.Length); c++)
                {
                    if(DataLayer[r,c] == true)
                    {
                        str += "T";
                    }
                    else { str += "F"; }
                }
                str += "\n";
            }
            Console.WriteLine(str);
        }
    }
}
