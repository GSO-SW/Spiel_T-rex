using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace TREX._2._0
{
    class Kakteen
    {
        public  int Standardhöhe = 258;
        public  int Kakteen_geschwindigkeit = -6;
        public  Bitmap[] Kakteeen;
        public  List<Rectangle> rectangles = new List<Rectangle>();
         
            
        public Kakteen()
        {
            Kakteeen = new Bitmap[] { Properties.Resources.Kaktus, Properties.Resources.dead};   //fügt die Images in die bitmap hinzu

            rectangles.Add(new Rectangle(500, Standardhöhe,  30, 40));  //fügt die pposition der rectamgels hinzu
            rectangles.Add(new Rectangle(2500, Standardhöhe, 20, 40));
            rectangles.Add(new Rectangle(4500, Standardhöhe, 10, 40));
       
        }
    }
}
