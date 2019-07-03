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

    public partial class Spiel : Form
    {
        #region Eigenschaften
        Image T_rex;
        Bitmap[] Kakteen;
        List<Rectangle> rectangles;
        int Standardhöhe = 261;
        int Sprungkraft = 0;
        int Höhe = 261;
        int Score = 0;
        int hindernis_geschwindigkeit = -6;
        bool springen = false;

        #endregion
        public Spiel()
        {
            InitializeComponent();
            T_rex = Properties.Resources.rennen;
            Kakteen = new Bitmap[] { Properties.Resources._2Kaktus, Properties.Resources.Kaktus }; //fügt die Images in die bitmap hinzu
            rectangles = new List<Rectangle>();
            DoubleBuffered = true;
            rectangles.Add(new Rectangle(500, Standardhöhe, 40, 40));  //fügt die pposition der rectamgels hinzu
            rectangles.Add(new Rectangle(700, Standardhöhe, 40, 40));
        }

        private void Spiel_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Pen SchwarzeStift = Pens.Black;
            Graphics graphics = e.Graphics;
            graphics.DrawLine(SchwarzeStift, 0, 300, 100000, 300);
            graphics.DrawImage(T_rex, 20, Höhe, T_rex.Width, T_rex.Height);
            graphics.DrawImage(Kakteen[0], rectangles[0]);
            graphics.DrawImage(Kakteen[1], rectangles[1]);
        }
        //Das ist der Timer 
        private void GameEvent(object sender, EventArgs e)
        {
            for (int i = rectangles.Count - 1; i >= 0; i--)       // wir zöhlen um einas ab 
            {
                Point point;
                if (rectangles[i].X + rectangles[i].Width < 0)   //ist die koordinate kleiner null
                {
                    Random Rand = new Random();
                    point = new Point(this.Width + Rand.Next(10, 120), rectangles[i].Y);  // wir erstellen einen neuen punkt mit den aktualisierten Daten
                }
                else
                    point = new Point(rectangles[i].X + hindernis_geschwindigkeit, rectangles[i].Y);  // wir erstellen einen neuen punkt mit den aktualisierten Daten

                rectangles.Add(new Rectangle(point, rectangles[i].Size));                                                  // ertstellen ein rectangle und fügen das in die liste hinzu  
                rectangles.RemoveAt(i);                                                                                    // entfernt das alte rectangle 
                Score++;

                if (Score >= 300)
                {
                    hindernis_geschwindigkeit = -16;
                }
                if (Score >= 1100)
                {
                    hindernis_geschwindigkeit = -24;
                }
            }
            Springen();
            Rändern();
            Kollision();
        }
        //Bilder sollen Laden
        private void Rändern()
        {
            this.Invalidate();
        }

        private void Springen()
        {
            Höhe += Sprungkraft;             // Höhe = höhe + sprungkraft
            Sprungkraft += 3;               // Sprungkraft = Sprungkraft + 10 

            //Dinosaurier ist gesprungen 
            if (Höhe > Standardhöhe - 3)
            {
                Höhe = Standardhöhe - 3;
                Sprungkraft = 0;
            }
            if (Höhe == Standardhöhe - 3)

                springen = false;
        }
        //wenn Die Space taste gedrückt wird dann soll die Sprungkraft auf -30 steigen 
        private void Spiel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !springen)
            {
                Sprungkraft = -30;
                springen = true;
            }
        }
        private void Kollision()
        {

            bool Überprüfen = false;

            Rectangle rect = new Rectangle(20, Höhe, T_rex.Width, T_rex.Height);
            for (int i = 0; i < rectangles.Count; i++)
            {
                if (rect.IntersectsWith(rectangles[i]))
                {
                    Überprüfen = true;
                    ImageAnimator.StopAnimate(T_rex, new EventHandler(GameEvent));
                }

            }
            if (Überprüfen)
            {
                SpielZähler.Stop();
                MessageBox.Show("Verloren und deine erreichte Punktzahl lautet:" + Score);
                Close();
            }
        }
    }
}
