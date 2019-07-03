﻿using System;
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
        int Standardhöhe= 261;
        int Sprungkraft = 0;
        int Höhe = 261;
        int Score = 0;
        bool springen = false;
        
        #endregion
        public Spiel()
        {
            InitializeComponent();
            T_rex = Properties.Resources.rennen;
            Kakteen = new Bitmap[] { Properties.Resources._2Kaktus, Properties.Resources.Kaktus };
            rectangles = new List<Rectangle>();
            DoubleBuffered = true;
            rectangles.Add(new Rectangle(500, Standardhöhe, 40, 40));
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

        private void GaameEvent(object sender, EventArgs e)
        {
            Springen();
            Rändern();
        }
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

        private void Spiel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !springen)
            {
                Sprungkraft = -30;
                springen = true;
            }
        }
    }
}
