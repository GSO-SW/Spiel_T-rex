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
        
        List<Rectangle> rectangles;
        Kakteen Kakteeeen = new Kakteen();
        Charakter Charakter = new Charakter();

        int Standardhöhe = 258;
        int Sprungkraft = 0;
        int Höhe = 258;
        int Score = 0;
        int hindernis_geschwindigkeit = -6;
        bool springen = false;

        #endregion
        public Spiel()
        {
            InitializeComponent();
            SpielMusik.Play();
           
            Kakteeeen.Kakteeen = new Bitmap[] { Properties.Resources.Kaktus }; //fügt die Images in die bitmap hinzu
            rectangles = new List<Rectangle>();
            DoubleBuffered = true;
            rectangles.Add(new Rectangle(500, Standardhöhe, 40, 40));  //fügt die pposition der rectamgels hinzu
            rectangles.Add(new Rectangle(700, Standardhöhe, 40, 40));
            rectangles.Add(new Rectangle(1600, Standardhöhe, 40, 40));
            rectangles.Add(new Rectangle(1100, Standardhöhe, 40, 40));
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
            graphics.DrawImage(Charakter.T_rex, 20, Höhe, Charakter.T_rex.Width, Charakter.T_rex.Height);
            graphics.DrawImage(Kakteeeen.Kakteeen[0], rectangles[0]);
            graphics.DrawImage(Kakteeeen.Kakteeen[0], rectangles[1]);
            graphics.DrawImage(Kakteeeen.Kakteeen[0], rectangles[2]);
            graphics.DrawImage(Kakteeeen.Kakteeen[0], rectangles[3]);
         
        }
        //Timer 
        private void GameEvent(object sender, EventArgs e)
        {
            for (int i = rectangles.Count - 1; i >= 0; i--)       // wir zöhlen um einas ab 
            {
                Point point;
                if (rectangles[i].X + rectangles[i].Width < 0)   //ist die koordinate kleiner null
                {
                    Random Rand = new Random();
                    point = new Point(this.Width + Rand.Next(210, 520), rectangles[i].Y);  // wir erstellen einen neuen punkt mit den aktualisierten Daten
                }
                else
                    point = new Point(rectangles[i].X + hindernis_geschwindigkeit, rectangles[i].Y);  // wir erstellen einen neuen punkt mit den aktualisierten Daten

                rectangles.Add(new Rectangle(point, rectangles[i].Size));                                                  // ertstellen ein rectangle und fügen das in die liste hinzu  
                rectangles.RemoveAt(i);                                                                                    // entfernt das alte rectangle 
                Score++;

                if (Score >= 300)
                {
                    hindernis_geschwindigkeit = -9;
                  
                }
                if (Score >= 1100)
                {
                    hindernis_geschwindigkeit = -12;
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
            Höhe += Sprungkraft;             // Höhe = höhe + sprungkraft
            Sprungkraft += 3;               // Sprungkraft = Sprungkraft + 3

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
               
                
                if (Score >= 600)
                {
                    Sprungkraft = -40;
                }
                if (Score>1000)
                {
                    Sprungkraft = -30;
                }
            }
        }
        private void Kollision()
        {

            bool Überprüfen = false;

            Rectangle rect = new Rectangle(20, Höhe, Charakter.T_rex.Width, Charakter.T_rex.Height);
            for (int i = 0; i < rectangles.Count; i++)
            {
                if (rect.IntersectsWith(rectangles[i]))
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

        private void Timer1_Tick(object sender, EventArgs e)
        {

        }

        private void Spiel_KeyUp(object sender, KeyEventArgs e)
        {
            
            
        }
    }
}
