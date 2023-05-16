using System.Collections.Generic;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static lab6.Form1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace lab6
{
    public partial class Form1 : Form
    {
        private List<Figure> figures = new List<Figure>(); 
        public int objectSize = 30;
        private bool Cntrl;

        Color color = Color.Black;
        Color red = Color.Red;
        Color green = Color.Green;
        Color blue = Color.Blue;
        Color black = Color.Black;
        int colorIndex = 0;

        int selectedFigure = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; 
            foreach (Figure figure in figures)
            {
                figure.SelfDraw(e.Graphics); // Метод круга для отрисовки 
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

            if (!Cntrl)
            {
                foreach (Figure figure in figures) // снятие выделения со всех объектов
                {
                    figure.setCondition(false);
                }

                switch (selectedFigure)
                {
                    case 0:
                        Circle newcircle = new Circle(e.X, e.Y, objectSize, color);
                        newcircle.setCondition(false);
                        figures.Add(newcircle);
                        break;
                    case 1:
                        Square newsquare = new Square(e.X, e.Y, objectSize, color);
                        newsquare.setCondition(false);
                        figures.Add((newsquare));
                        break;
                    case 2:
                        Triangle newtriangle = new Triangle(e.X, e.Y, objectSize, color);
                        newtriangle.setCondition(false);
                        figures.Add((newtriangle));
                        break;
                    case 3:
                        Line newsection = new Line(e.X, e.Y, objectSize, color);
                        newsection.setCondition(false);
                        figures.Add((newsection));
                        break;
                }
                Refresh();
            }
            else if (Cntrl) // Выделение кругов, если зажат cntrl
            {
                foreach (Figure figure in figures)
                {
                    if (figure.MouseCheck(e))
                    {
                        figure.setCondition(true);
                        break;
                    }
                }
                Refresh();
            }

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            objectSize = trackBar1.Value;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            checkBox_Cntrl.Checked = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                checkBox_Cntrl.Checked = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                DelFigures();
            }
            else if (e.KeyCode == Keys.W)
            {
                foreach (Figure figure in figures)
                {
                    if (figure.selected && ((figure.coords.Y - figure.rad) > 0))
                    {
                        figure.coords.Y -= 3;
                    }
                }
                Refresh();
            }
            else if (e.KeyCode == Keys.S)
            {
                foreach (Figure figure in figures)
                {
                    if (figure.selected && ((figure.coords.Y + figure.rad) < (int)this.ClientSize.Height))
                    {
                        figure.coords.Y += 3;
                    }
                }
                Refresh();
            }
            else if (e.KeyCode == Keys.A)
            {
                foreach (Figure figure in figures)
                {
                    if (figure.selected && ((figure.coords.X - figure.rad) > 0))
                    {
                        figure.coords.X -= 3;
                    }
                }
                Refresh();
            }
            else if (e.KeyCode == Keys.D)
            {
                foreach (Figure figure in figures)
                {
                    if (figure.selected && ((figure.coords.X + figure.rad) < (int)this.ClientSize.Width))
                    {
                        figure.coords.X += 3;
                    }
                }
                Refresh();
            }

            else if (e.KeyCode == Keys.Oemplus)
            {
                DoBigger();
            }
            else if (e.KeyCode == Keys.OemMinus)
            {
                DoSmaller();
            }
        }

        private void checkBox_Cntrl_CheckedChanged(object sender, EventArgs e)
        {
            Cntrl = checkBox_Cntrl.Checked;
            foreach (Figure figure in figures)
            {
                figure.fcntrl = Cntrl;
            }
        }

        private void btn_doBigger_Click(object sender, EventArgs e)
        {
            DoBigger();
        }

        private void btn_doSmaller_Click(object sender, EventArgs e)
        {
            DoSmaller();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            DelFigures();
        }

        private void DoBigger()
        {
            foreach (Figure figure in figures)
            {
                if (figure.selected && figure.rad <= 95)
                {
                    figure.rad += 5;
                }
            }
            Refresh();
        }

        private void DoSmaller()
        {
            foreach (Figure figure in figures)
            {
                if (figure.selected && figure.rad > 10)
                {
                    figure.rad -= 5;
                }
            }
            Refresh();
        }
        
        void DelFigures()
        {
            for (int i = 0; i < figures.Count; i++)
            {
                if (figures[i].selected == true)
                {
                    figures.Remove(figures[i]);
                    i--;
                }
            }
            Refresh();
        }

        private void btn_Select_Click(object sender, EventArgs e) // выделения всех объектов
        {
            foreach (Figure figure in figures)
            {
                figure.setCondition(true);
            }
            Refresh();
        }

        private void btn_unSelect_Click(object sender, EventArgs e) // снятие выделения 
        {
            foreach (Figure figure in figures)
            {
                figure.setCondition(false);
            }
            Refresh();
        }

        private void btn_Color_Click(object sender, EventArgs e)
        {
            if (colorIndex < 3)
                colorIndex++;
            else
                colorIndex = 0;
            switch (colorIndex)
            {
                case 0:
                    color = red;
                    break;
                case 1:
                    color = green;
                    break;
                case 2:
                    color = blue;
                    break;
                case 3:
                    color = black;
                    break;
            }

            btn_Color.BackColor = color;
            foreach (Figure figure in figures)
            {
                if (figure.selected)
                    figure.colorF = color;
            }
        }

        private void btn_circle_Click(object sender, EventArgs e)
        {
            selectedFigure = 0;
        }

        private void btn_square_Click(object sender, EventArgs e)
        {
            selectedFigure = 1;
        }

        private void btn_triangle_Click(object sender, EventArgs e)
        {
            selectedFigure = 2;
        }

        private void btn_line_Click(object sender, EventArgs e)
        {
            selectedFigure = 3;
        }

        void control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.S || e.KeyCode == Keys.A || e.KeyCode == Keys.D)
            {
                e.IsInputKey = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                control.PreviewKeyDown += new PreviewKeyDownEventHandler(control_PreviewKeyDown);
            }
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            KeyPreview = true;
        }
    }

    public class Figure
    {
        public Point coords;
        public int rad;
        public bool selected = false;
        public bool fcntrl = false;

        public Color colorT = Color.Red;
        public Color colorF = Color.Purple;

        public void setCondition(bool cond) // метод переключения выделения
        {
            selected = cond;
        }
        public virtual void SelfDraw(Graphics g) // Метод для отрисовки самого себя
        {
        }
        public virtual bool MouseCheck(MouseEventArgs e) // Проверка объекта на попадание в него курсора
        {
            return false;
        }

    }
    public class Circle : Figure// класс круга
    {
        public Circle(int x, int y, int radius, Color color) // конструктор по умолчанию
        {
            coords.X = x;
            coords.Y = y;
            rad = radius;
            colorF = color;
        }
        public override void SelfDraw(Graphics g) // Метод для отрисовки самого себя
        {
            if (selected == true)
                g.DrawEllipse(new Pen(colorT, 3), coords.X - rad, coords.Y - rad, rad * 2, rad * 2);
            else
                g.DrawEllipse(new Pen(colorF, 3), coords.X - rad, coords.Y - rad, rad * 2, rad * 2);
        }
        public override bool MouseCheck(MouseEventArgs e) // Проверка объекта на попадание в него курсора
        {
            if (fcntrl)
            {
                if (Math.Pow(e.X - coords.X, 2) + Math.Pow(e.Y - coords.Y, 2) <= Math.Pow(rad, 2) && !selected)
                {
                    selected = true;
                    return true;
                }
            }
            return false;
        }

    }

    public class Square : Figure // класс квадрата
    {
        public Square(int x, int y, int radius, Color color) // конструктор по умолчанию
        {
            coords.X = x;
            coords.Y = y;
            rad = radius;
            colorF = color;
        }
        public override void SelfDraw(Graphics g) // Метод для отрисовки самого себя
        {
            if (selected == true)
                g.DrawRectangle(new Pen(colorT, 3), coords.X - rad, coords.Y - rad, rad * 2, rad * 2);
            else
                g.DrawRectangle(new Pen(colorF, 3), coords.X - rad, coords.Y - rad, rad * 2, rad * 2);

        }
        public override bool MouseCheck(MouseEventArgs e) // Проверка объекта на попадание в него курсора
        {
            if (fcntrl)
            {
                if (Math.Pow(e.X - coords.X, 2) + Math.Pow(e.Y - coords.Y, 2) <= Math.Pow(rad, 2) && !selected)
                {
                    selected = true;
                    return true;
                }
            }
            return false;
        }
    }

    public class Triangle : Figure // класс треугольника
    {
        public Triangle(int x, int y, int radius, Color color) // конструктор по умолчанию
        {
            coords.X = x;
            coords.Y = y;
            rad = radius;
            colorF = color;
        }
        public override void SelfDraw(Graphics g) // Метод для отрисовки самого себя
        {
            Point point1 = new Point(coords.X, coords.Y - rad);
            Point point2 = new Point(coords.X + rad, coords.Y + rad);
            Point point3 = new Point(coords.X - rad, coords.Y + rad);
            Point[] curvePoints = { point1, point2, point3 };

            if (selected == true)
                g.DrawPolygon(new Pen(colorT, 3), curvePoints);
            else
                g.DrawPolygon(new Pen(colorF, 3), curvePoints);
        }
        public override bool MouseCheck(MouseEventArgs e) // Проверка объекта на попадание в него курсора
        {
            if (fcntrl)
            {
                if (Math.Pow(e.X - coords.X, 2) + Math.Pow(e.Y - coords.Y, 2) <= Math.Pow(rad, 2) && !selected)
                {
                    selected = true;
                    return true;
                }
            }
            return false;
        }
    }

    public class Line : Figure // класс отрезка
    {
        public Line(int x, int y, int radius, Color color) // конструктор по умолчанию
        {
            coords.X = x;
            coords.Y = y;
            rad = radius;
            colorF = color;
        }
        public override void SelfDraw(Graphics g) // Метод для отрисовки самого себя
        {
            Point point1 = new Point(coords.X - rad, coords.Y);
            Point point2 = new Point(coords.X + rad, coords.Y);
            Point[] curvePoints = { point1, point2 };

            if (selected == true)
                g.DrawPolygon(new Pen(colorT, 3), curvePoints);
            else
                g.DrawPolygon(new Pen(colorF, 3), curvePoints);
        }
        public override bool MouseCheck(MouseEventArgs e) // Проверка объекта на попадание в него курсора
        {
            if (fcntrl)
            {
                if (Math.Pow(e.X - coords.X, 2) + Math.Pow(e.Y - coords.Y, 2) <= Math.Pow(rad, 2) && !selected)
                {
                    selected = true;
                    return true;
                }
            }
            return false;
        }
    }

}
