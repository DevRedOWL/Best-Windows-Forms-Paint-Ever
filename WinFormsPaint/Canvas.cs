using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsPaint
{
    public partial class Canvas : Form
    {
        #region Свойства
        public string FilePath = "";                // Путь к картинке
        private int oldX, oldY;                     // Прошлая точка
        public Bitmap bmp;                          // Рабочий битмап
        private int[] MouseStart = new int[2];      // X,Y начала пути
        private int[] MouseEnd = new int[2];        // X,Y конца пути
        private string LastState = "";              // Последнее состояние TODO: Перевести на энам
        GraphicsPath myPath = new GraphicsPath();   // Контур
        Stack<Bitmap> Rollback = new Stack<Bitmap>();   // Откаты
        public int[] GetBmpSize() { return new int[] { bmp.Width, bmp.Height }; }   // Инкапсулируем размеры изображения
        #endregion

        #region Конструкторы
        // Базовый конструктор
        public Canvas()
        {
            // Создаем пустой холст
            InitializeComponent();
            bmp = new Bitmap(PictureBox.Width, ClientSize.Height);
            PictureBox.Image = bmp;
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
            Rollback.Push(new Bitmap(bmp)); // Пушим в стек
        }

        // Конструктор из файла
        public Canvas(String FileName)
        {
            InitializeComponent();
            try
            {
                // Открываем только для чтения файл чтобы можно было записывать
                Image image = Image.FromFile(FileName);
                bmp = new Bitmap(image);
                // Задаем метаданные изображения, ы
                foreach (var pitem in image.PropertyItems)
                    bmp.SetPropertyItem(pitem);
                image.Dispose();
                // Устанавливаем путь и название рисунка
                FilePath = FileName;
                this.Text = FilePath.Split('\\')[FilePath.Split('\\').Length - 1];
                // Создаем пустой холст
                Graphics g = Graphics.FromImage(bmp);
                PictureBox.Width = bmp.Width;
                PictureBox.Height = bmp.Height;
                PictureBox.Image = bmp;
                Rollback.Push(new Bitmap(bmp)); // Пушим в стек
            }
            catch (Exception)
            {
                // Создаем пустой холст
                bmp = new Bitmap(PictureBox.Width, ClientSize.Height);
                PictureBox.Image = bmp;
                Graphics g = Graphics.FromImage(bmp);
                g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
                // Уведомляем об ошибке загрузки
                DialogResult dialogResult = MessageBox.Show("Загрузить файл не удалось, возможно это не изображение.", "Ошибка", MessageBoxButtons.OK);
                Rollback.Push(new Bitmap(bmp)); // Пушим в стек
            }
        }
        #endregion

        #region Свойства устанавливаемых размеров холста
        public int CanvasWidth
        {
            get
            {
                return PictureBox.Width;
            }
            set
            {
                // ЖЫФТОНЭ УПРАВЛЕНИЙЭ ПАМИТЬЮ УТЕЧКА УТЕЧКА 
                // Верните мне мои указатели это же жесть
                PictureBox.Width = value;
                Bitmap tbmp = new Bitmap(value, PictureBox.Width);
                Graphics g = Graphics.FromImage(tbmp);
                g.Clear(Color.White);
                g.DrawImage(bmp, new Point(0, 0));
                bmp = tbmp;
                PictureBox.Image = bmp;
            }
        }
        public int CanvasHeight
        {
            get
            {
                return PictureBox.Height;
            }
            set
            {
                PictureBox.Height = value;
                Bitmap tbmp = new Bitmap(PictureBox.Width, value);
                Graphics g = Graphics.FromImage(tbmp);
                g.Clear(Color.White);
                g.DrawImage(bmp, new Point(0, 0));
                bmp = tbmp;
                PictureBox.Image = bmp;
            }
        }
        #endregion

        #region Методы сохранения
        // Сейв ас
        public void SaveAs()
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                AddExtension = true,
                Filter = "Windows Bitmap (*.bmp)|*.bmp|Файлы JPEG (*.jpg)|*.jpg|Изображения PNG (*.png)|*.png"
            };
            ImageFormat[] ff = { ImageFormat.Bmp, ImageFormat.Jpeg, ImageFormat.Png };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FilePath = dlg.FileName;
                this.Text = FilePath.Split('\\')[FilePath.Split('\\').Length - 1];
                bmp.Save(dlg.FileName, ff[dlg.FilterIndex - 1]);
            }
        }

        // Сохранение без выбора файла
        public void JustSave()
        {
            // Определяем формат для корректной кодировки
            switch ($"{this.Text[this.Text.Length - 3]}{this.Text[this.Text.Length - 2]}{this.Text[this.Text.Length - 1]}")
            {
                case "jpg": case "peg": { bmp.Save(FilePath, ImageFormat.Jpeg); } break;
                case "bmp": { bmp.Save(FilePath, ImageFormat.Bmp); } break;
                case "png": { bmp.Save(FilePath, ImageFormat.Png); } break;
            }
            //bmp.Save(FilePath, ImageFormat.Bmp);
        }
        #endregion

        #region Наброски
        /*
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // Проведение линии
            if (e.Button == MouseButtons.Left)
            {
                if (LastState != "press") { MouseStart[0] = e.X; MouseStart[1] = e.Y; LastState = "press"; }                              
                else {
                    GraphicsPath graphPath = new GraphicsPath();
                    graphPath.AddEllipse(MouseStart[0], MouseStart[1], e.X, e.Y);
                    // Create pen.
                    Pen blackPen = new Pen(Color.Gray, 3);
                    // Draw graphics path to screen.
                    Graphics g = Graphics.FromImage(bmp);
                    g.(blackPen, graphPath);
                    pictureBox1.Invalidate();
                    graphPath.Reset();
                    graphPath.Dispose();
                } // Продолжаем вести линию
            }
            else
            {
                if (LastState == "press")
                {
                    MouseEnd[0] = e.X; MouseEnd[1] = e.Y; LastState = "lost";
                    // Проводим действия над линией
                    Console.WriteLine($"{MouseStart[0]}:{MouseStart[1]} - {MouseEnd[0]}:{MouseEnd[1]}");
                    Graphics g = Graphics.FromImage(bmp);
                    // Ох какая жесть здесь а не матеша, из максимального числа вычитаем минимальное, вот эту разность делим пополам и снова вычитаем из максимального
                    DrawStar(g, Form1.PickedColor, Form1.BrushSize,
                        Math.Max(MouseStart[0], MouseEnd[0]) - (Math.Max(MouseStart[0], MouseEnd[0]) - Math.Min(MouseStart[0], MouseEnd[0])) / 2,
                        Math.Max(MouseStart[1], MouseEnd[1]) - (Math.Max(MouseStart[1], MouseEnd[1]) - Math.Min(MouseStart[1], MouseEnd[1])) / 2
                        // Как то посчитать радиусы 
                    );

                    pictureBox1.Invalidate();
                }
                else { } // Потеряли линию
            }


            
            //Console.WriteLine("Рисую");
            //Graphics g = Graphics.FromImage(bmp);

            //// Метод заливки кружком
            //// g.FillEllipse(new SolidBrush(Form1.PickedColor), e.X - Form1.BrushSize / 4, e.Y-Form1.BrushSize/4, Form1.BrushSize, Form1.BrushSize);
            //// Метод заливки палками
            //// g.DrawLine(new Pen(Form1.PickedColor, Form1.BrushSize), oldX, oldY, e.X, e.Y);
            //// И даже метод заливки звездочками
            //DrawStar(g, Form1.PickedColor, Form1.BrushSize, e.X, e.Y);

            ////
            //oldX = e.X;
            //oldY = e.Y;

            pictureBox1.Invalidate();
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        public void DrawStar(Graphics g, Color color, int n, double x0, double y0, double R = 25, double r = 50)
        {
            //double R = (xe-x0)/2, r = xe-x0;   // радиусы 25 50
            double alpha = 0;        // поворот

            PointF[] points = new PointF[2 * n + 1];
            double a = alpha, da = Math.PI / n, l;
            for (int k = 0; k < 2 * n + 1; k++)
            {
                l = k % 2 == 0 ? r : R;
                points[k] = new PointF((float)(x0 + l * Math.Cos(a)), (float)(y0 + l * Math.Sin(a)));
                a += da;
            }

            g.DrawLines(new Pen(color), points);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            oldX = e.X;
            oldY = e.Y;

        }
        */
        #endregion

        #region Рисовалка
        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            // Штуковина отлавливает путь от нажатия до отпускания левой кнопки мыши
            if (e.Button == MouseButtons.Left)
            {
                if (LastState != "press") { MouseStart[0] = e.X; MouseStart[1] = e.Y; LastState = "press"; } // Если начали рисовать
                else { MouseEnd[0] = e.X; MouseEnd[1] = e.Y; } // Продолжаем вести линию    

                if (Form1.SelectedTool == Form1.SelectedToolType.Brush || Form1.SelectedTool == Form1.SelectedToolType.Eraser)
                {
                    Graphics g = Graphics.FromImage(bmp);
                    if (Form1.BrushSize < 4) // Если мелкая - заливаем палками как в методичке
                        g.DrawLine(new Pen(Form1.SelectedTool == Form1.SelectedToolType.Brush ? Form1.PickedColor : Color.White, Form1.BrushSize), oldX, oldY, e.X, e.Y);
                    else // Иначе кружками
                        g.FillEllipse(new SolidBrush(Form1.SelectedTool == Form1.SelectedToolType.Brush ? Form1.PickedColor : Color.White), e.X - Form1.BrushSize / 4, e.Y - Form1.BrushSize / 4, Form1.BrushSize, Form1.BrushSize);
                    //// И даже есть метод заливки звездочками, но его мы демонстрировать не будем
                    //DrawStar(g, Form1.PickedColor, Form1.BrushSize, e.X, e.Y);      

                    oldX = e.X;
                    oldY = e.Y;
                }
                PictureBox.Invalidate();
            }
            else
            {
                if (LastState == "press")
                {
                    MouseEnd[0] = e.X; MouseEnd[1] = e.Y; LastState = "lost";
                    // Отрисовываем последний путь
                    Graphics g = Graphics.FromImage(bmp);
                    if (Form1.SelectedTool == Form1.SelectedToolType.RoundFill || Form1.SelectedTool == Form1.SelectedToolType.StarFill)
                        g.FillPath(new SolidBrush(Form1.PickedColor), myPath);
                    else
                        g.DrawPath(new Pen(Form1.PickedColor, Form1.BrushSize), myPath);
                    myPath.Reset();
                    Rollback.Push(new Bitmap(bmp)); // Пушим в стек
                }
                else { } // Потеряли линию
            }
        }

        // Что то делает при клике, а что - непонятно
        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            oldX = e.X;
            oldY = e.Y;
        }

        // Распоряжается инструментами
        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            myPath.Reset();
            switch (Form1.SelectedTool)
            {
                case Form1.SelectedToolType.Brush:
                    break;
                case Form1.SelectedToolType.StarOutline:
                    {
                        double alpha = 50;        // поворот

                        int x0 = Math.Max(MouseStart[0], MouseEnd[0]) - (Math.Max(MouseStart[0], MouseEnd[0]) - Math.Min(MouseStart[0], MouseEnd[0])) / 2;
                        int y0 = Math.Max(MouseStart[1], MouseEnd[1]) - (Math.Max(MouseStart[1], MouseEnd[1]) - Math.Min(MouseStart[1], MouseEnd[1])) / 2;
                        int r = (Math.Max(MouseStart[1], MouseEnd[1]) - y0), R = (Math.Max(MouseStart[1], MouseEnd[1]) - y0) / 2;
                        int n = Form1.BrushSize;

                        PointF[] points = new PointF[2 * n + 1];
                        double a = alpha, da = Math.PI / n, l;
                        for (int k = 0; k < 2 * n + 1; k++)
                        {
                            l = k % 2 == 0 ? r : R;
                            points[k] = new PointF((float)(x0 + l * Math.Cos(a)), (float)(y0 + l * Math.Sin(a)));
                            a += da;
                        }
                        myPath.AddLines(points);
                        Pen myPen = new Pen(Form1.PickedColor, 5); // Где 5 - это толщина
                        e.Graphics.DrawPath(myPen, myPath);
                    }
                    break;
                case Form1.SelectedToolType.StarFill:
                    {
                        double alpha = 50;        // поворот

                        int x0 = Math.Max(MouseStart[0], MouseEnd[0]) - (Math.Max(MouseStart[0], MouseEnd[0]) - Math.Min(MouseStart[0], MouseEnd[0])) / 2;
                        int y0 = Math.Max(MouseStart[1], MouseEnd[1]) - (Math.Max(MouseStart[1], MouseEnd[1]) - Math.Min(MouseStart[1], MouseEnd[1])) / 2;
                        int r = (Math.Max(MouseStart[1], MouseEnd[1]) - y0), R = (Math.Max(MouseStart[1], MouseEnd[1]) - y0) / 2;
                        int n = Form1.BrushSize;

                        PointF[] points = new PointF[2 * n + 1];
                        double a = alpha, da = Math.PI / n, l;
                        for (int k = 0; k < 2 * n + 1; k++)
                        {
                            l = k % 2 == 0 ? r : R;
                            points[k] = new PointF((float)(x0 + l * Math.Cos(a)), (float)(y0 + l * Math.Sin(a)));
                            a += da;
                        }
                        myPath.AddLines(points);
                        e.Graphics.FillPath(new SolidBrush(Form1.PickedColor), myPath);
                    }
                    break;
                case Form1.SelectedToolType.Line:
                    {
                        myPath.AddLine(MouseStart[0], MouseStart[1], MouseEnd[0], MouseEnd[1]);
                        Pen myPen = new Pen(Form1.PickedColor, Form1.BrushSize);
                        e.Graphics.DrawPath(myPen, myPath);
                    }
                    break;
                case Form1.SelectedToolType.RoundOutline:
                    {
                        myPath.AddEllipse(MouseStart[0], MouseStart[1], MouseEnd[0] - MouseStart[0], MouseEnd[1] - MouseStart[1]);
                        Pen myPen = new Pen(Form1.PickedColor, Form1.BrushSize);
                        e.Graphics.DrawPath(myPen, myPath);
                    }
                    break;
                case Form1.SelectedToolType.RoundFill:
                    {
                        myPath.AddEllipse(MouseStart[0], MouseStart[1], MouseEnd[0] - MouseStart[0], MouseEnd[1] - MouseStart[1]);
                        e.Graphics.FillPath(new SolidBrush(Form1.PickedColor), myPath);
                    }
                    break;
                case Form1.SelectedToolType.Eraser:
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Событие при закрытии окна
        private void Canvas_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Вы хотите сохранить изменения в файле \"{this.Text}\"?", "Выход", MessageBoxButtons.YesNoCancel);
            switch (dialogResult)
            {
                case DialogResult.Cancel: { e.Cancel = true; } break;
                case DialogResult.Yes: { if (FilePath == "") SaveAs(); else JustSave(); this.Dispose(); } break;
                case DialogResult.No: { this.Dispose(); } break;
            }
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                myPath.Reset();
                if (Rollback.Count > 0)
                {
                    var g = Graphics.FromImage(bmp);
                    g.Clear(Color.White);
                    g.DrawImage(Rollback.Pop(), 0, 0, bmp.Width, bmp.Height);
                    PictureBox.Invalidate();
                }
                if (Rollback.Count == 0)
                    Rollback.Push(new Bitmap(bmp)); // Пушим в стек
            }
        }

        private void Canvas_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region Скейлинг
        public void ScaleBitmapByReclangle(Rectangle TransformationRectangle)
        {
            Graphics g = Graphics.FromImage(bmp);
            var temp = new Bitmap(bmp);
            g.Clear(Color.White);
            g.DrawImage(temp, TransformationRectangle);
            PictureBox.Invalidate();
        }
        #endregion       
    }
}
