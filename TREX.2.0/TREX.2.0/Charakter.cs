using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
namespace TREX._2._0
{
    class Charakter
    {
        public Image T_rex;
        public int Höhe = 258;
        public int Sprungkraft = 0;
        public int Standardhöhe = 258;
  

        public Charakter()
        {
            T_rex = Properties.Resources.rennen;
        }
       
    }
}
