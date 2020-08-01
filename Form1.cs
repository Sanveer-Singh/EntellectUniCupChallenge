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
                rtxFileDisplay.Text = myworld.toString();
                myworld.

            }
        }
    }
}
