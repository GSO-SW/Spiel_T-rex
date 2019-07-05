using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace TREX._2._0
{

    public partial class Spiel : Form
    {

        #region Eigenschaften
        SoundPlayer SpielMusik = new SoundPlayer(Properties.Resources.Sound);
        Kakteen Kakteeeen = new Kakteen();
        Charakter Charakter = new Charakter();

        public   int Score = 0;
        bool springen = false;

        #endregion
        public Spiel()
        {
            InitializeComponent();
            SpielMusik.Play();
           
            DoubleBuffered = true;
          
            if (ImageAnimator.CanAnimate(Charakter.T_rex))
            {
                ImageAnimator.Animate(Charakter.T_rex, new EventHandler(Timer1_Tick));
            }
        }

        private void Spiel_Load(object sender, EventArgs e)
        {
           
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Pen GrünerStift = Pens.Green;
            Graphics graphics = e.Graphics;
            ImageAnimator.UpdateFrames();
            graphics.DrawString("Score:" + Score, new Font("Comic Sans MS", 10.0f), Brushes.Black, new Point(5, 10));
     //     graphics.DrawLine(GrünerStift, 0, 300, 100000, 300);
            graphics.DrawImage(Charakter.T_rex, 20, Charakter.Höhe, Charakter.T_rex.Width, Charakter.T_rex.Height);
            graphics.DrawImage(Kakteeeen.Kakteeen[0], Kakteeeen.rectangles[0]);
            graphics.DrawImage(Kakteeeen.Kakteeen[0], Kakteeeen.rectangles[1]);
            graphics.DrawImage(Kakteeeen.Kakteeen[0], Kakteeeen.rectangles[2]);
            graphics.DrawImage(Kakteeeen.Kakteeen[0], Kakteeeen.rectangles[3]);
         
        }
        //Timer 
        private void GameEvent(object sender, EventArgs e)
        {
            for (int i = Kakteeeen.rectangles.Count - 1; i >= 0; i--)       // wir zöhlen um einas ab 
            {
                Point point;
                if (Kakteeeen.rectangles[i].X + Kakteeeen.rectangles[i].Width < 0)   //ist die koordinate kleiner null
                {
                    Random Rand = new Random();
                    point = new Point(this.Width + Rand.Next(210, 520), Kakteeeen.rectangles[i].Y);  // wir erstellen einen neuen punkt mit den aktualisierten Daten
                }
                else
                    point = new Point(Kakteeeen.rectangles[i].X + Kakteeeen.Kakteen_geschwindigkeit, Kakteeeen.rectangles[i].Y);  // wir erstellen einen neuen punkt mit den aktualisierten Daten

                Kakteeeen.rectangles.Add(new Rectangle(point, Kakteeeen.rectangles[i].Size));                                                  // ertstellen ein rectangle und fügen das in die liste hinzu  
                Kakteeeen.rectangles.RemoveAt(i);                                                                                    // entfernt das alte rectangle 
                Score++;

                if (Score >= 300)
                {
                    Kakteeeen.Kakteen_geschwindigkeit = -9;
                  
                }
                if (Score >= 1100)
                {
                    Kakteeeen.Kakteen_geschwindigkeit = -12;
                }
            }
            Springen();
            Rändern();
            Kollision();
        }
        //Die Bilder laden
        private void Rändern()
        {
            this.Invalidate();
        }

        private void Springen()
        {
            Charakter.Höhe += Charakter.Sprungkraft;             // Höhe = höhe + sprungkraft
            Charakter.Sprungkraft += 3;               // Sprungkraft = Sprungkraft + 3

            //Dinosaurier ist gesprungen 
            if (Charakter.Höhe > Kakteeeen.Standardhöhe - 3)
            {
                Charakter.Höhe = Kakteeeen.Standardhöhe - 3;
                Charakter.Sprungkraft = 0;
            }
            if (Charakter.Höhe == Kakteeeen.Standardhöhe - 3)

                springen = false;
        }
        //wenn Die Space taste gedrückt wird dann soll die Sprungkraft auf -30 steigen 
        private void Spiel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !springen)
            {
                Charakter.Sprungkraft = -30;
                springen = true;
               
                
                if (Score >= 600)
                {
                    Charakter.Sprungkraft = -40;
                }
                if (Score>1000)
                {
                    Charakter.Sprungkraft = -30;
                }
            }
        }
        private void Kollision()
        {

            bool Überprüfen = false;

            Rectangle rect = new Rectangle(20, Charakter.Höhe, Charakter.T_rex.Width, Charakter.T_rex.Height);
            for (int i = 0; i < Kakteeeen.rectangles.Count; i++)
            {
                if (rect.IntersectsWith(Kakteeeen.rectangles[i]))
                {
                    Überprüfen = true;
                    ImageAnimator.StopAnimate(Charakter.T_rex, new EventHandler(Timer1_Tick));
                }
            }
            if (Überprüfen)
            {
                SpielZähler.Stop();
                Charakter.T_rex = Properties.Resources.dead;
                SpielMusik.Stop();
                MessageBox.Show("Verloren und deine erreichte Punktzahl lautet:" + Score);
                Close();
            }
        }

        public void Timer1_Tick(object sender, EventArgs e)
        {

        }

        private void Spiel_KeyUp(object sender, KeyEventArgs e)
        {
            
            
        }
    }
}
