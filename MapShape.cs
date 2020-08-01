using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace EntellectUniCupChallenge
{
    class MapShape
    {
        public double ID;
        public double NumAvailable;
        public int capacity;


        public  void GetCapacity()
        {
            ListofShapes shapes = new ListofShapes();
            List < Shapes > myShapes  = shapes.shapes;
            foreach (var s in myShapes )
            {
                if(s.shape_id == ID)
                {
                    capacity = s.capacity;
                }
            }
        }

    }
}
