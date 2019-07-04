using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TREX._2._0
{
    public partial class Menue : Form
    {
        public Menue()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawString("TREX SPIEL", new Font("Comic Sans MS", 24), new SolidBrush(Color.Black), new Point(150,60));
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            Spiel spiel = new Spiel();
            spiel.ShowDialog();
        }

        private void Menue_Load(object sender, EventArgs e)
        {

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}
