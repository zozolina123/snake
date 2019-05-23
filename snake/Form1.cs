using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Graphics desen;
        Random rnd = new Random();
        Pen negru = new Pen(Color.Black);
        SolidBrush negruf = new SolidBrush(Color.Black);
        SolidBrush rosu = new SolidBrush(Color.Red);
        Point head,foodLoc;
        Point[] body = new Point[100];
        int directie,foodSet,l,dirChanged;
        public void init()
        {
            dirChanged = 0;
            directie = 1;
            desen = this.CreateGraphics();
            head.X = this.Width / 2;
            head.Y = this.Height / 2;
            desen.FillRectangle(negruf, head.X, head.Y, 20, 20);
            body[0] = head;
            body[0].X = head.X - 20;
            desen.DrawRectangle(negru, body[0].X, body[0].Y , 20, 20);
            setFood();
            timer1.Enabled = true;
        }

        public void stergere() {
            desen.Clear(this.BackColor);
        }

        public void miscare() {
            desen = this.CreateGraphics();
            for (int i = l; i >= 1; i--)
            {
                body[i] = body[i - 1];
                desen.DrawRectangle(negru, body[i].X, body[i].Y, 20, 20);
            }
            body[0] = head;
            desen.DrawRectangle(negru, body[0].X, body[0].Y, 20, 20);
            if (directie == 1)
                head.Y -= 20;
            else if (directie == 2)
                head.X += 20;
            else if (directie == 3)
                head.Y += 20;
            else head.X -= 20;
            desen.FillRectangle(negruf, head.X, head.Y, 20, 20);
           
        }

        public void setFood() {
            foodLoc.X = rnd.Next(0,  29);
            foodLoc.Y = rnd.Next(0, 29);
            foodLoc.X *= 20;
            foodLoc.Y *= 20;
            foodSet = 1;
        }

        public void drawFood(Point foodLoc) {
            desen = this.CreateGraphics();
            desen.FillRectangle(rosu, foodLoc.X, foodLoc.Y, 20, 20);
        }
       
        private void timer1_Tick(object sender, EventArgs e)
        {
            dirChanged = 0;
            stergere();
            miscare();
            if (foodSet == 0)
            {
                setFood();
            }
            drawFood(foodLoc);
            if (foodLoc == head)
            {
                setFood();
                l++;
                timer1.Interval -= timer1.Interval / 50;
            }
            bool ok = true;
            for (int i = 0; i <= l; i++) {
                if (head == body[i])
                    ok = false;
            }
            if (!ok) timer1.Enabled = false;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
                init();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (dirChanged == 0)
            {
                if (e.KeyValue == 38 && directie != 3)
                {
                    dirChanged = 1;
                    directie = 1;
                }
                else if (e.KeyValue == 39 && directie != 4)
                {
                    dirChanged = 1;
                    directie = 2;
                }
                else if (e.KeyValue == 40 && directie != 1)
                {
                    dirChanged = 1;
                    directie = 3;
                }
                else if (directie != 2)
                {
                    dirChanged = 1;
                    directie = 4;
                }
            }
        }


    }
}
