using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
// Для рефлексии
using PluginInterface;
using System.Reflection;

namespace WinFormsPaint
{
    /*
     * Крутые фичи:
     * Респонсив смена толщины кисти
     * Возможность выбирать между залитым и нет кругом
     * То же самое со звездочками
     * Динамическая генерация палитры
     * Оптимизированное решения на кейсах (окно) 
     * Валидация всех входных данных
     * Кнопка эскейп в форме отменяет последнее действие
     * Передача метаданных в битмап для трансформации плагинами
     * 
     * Идеи:
     * IDEA: Толщина и поворот звезды        
     * IDEA: Прямоугольник как инструмент
     * IDEA: Нормальные иконки для окон
     * 
     * Обязательно:
     * TODO: Фильтры
     */

    public partial class Form1 : Form
    {
        #region Всякий мусор
        public enum SelectedToolType
        {
            Brush, StarFill, StarOutline, Line, RoundFill, RoundOutline, Eraser
        }
        private ColorDialog MyColorDialog = new ColorDialog
        {
            AllowFullOpen = true,   // Разрешаем выбать абсолютно любой цвет              
            ShowHelp = false        // Разрешаем открыть справку
        };
        #endregion

        #region Параметры
        public static SelectedToolType SelectedTool = SelectedToolType.Brush;              // Выбраный инструмент
        public static int BrushSize = 5;                                                   // Размер кисти
        public static Color AnotherPickedColor = ColorTranslator.FromHtml("#FFFFFF00");    // Цвет "Другого цвета"
        public static Color PickedColor = ColorTranslator.FromHtml("#FF000000");           // Цвет по умолчанию, как вариант - (Color)new ColorConverter().ConvertFromString   
        public static Dictionary<string, IPlugin> pluginsList = new Dictionary<string, IPlugin>(); //Словарь рефлексии
        #endregion

        #region Загрузка формы
        public Form1()
        {
            InitializeComponent();
            foreach (Control c in this.Controls) if (c is MdiClient) c.BackColor = ColorTranslator.FromHtml("#FF274970"); // Костыль для назначения цвета MDI контроллеру   
            // Обновляем список плагинов
            FindPlugins();
        }

        // Генерация цветов при загрузке
        private void Form1_Load(object sender, EventArgs e)
        {
            // Проходим по массиву элементов палитры и их цветов
            ToolStripMenuItem[] PaletteItems = new ToolStripMenuItem[] { PaletteColor, PaletteRed, PaletteGreen, PaletteBlue, PaletteBlack, PaletteAnother };
            string[] PaletteColors = new string[] { "#FF000000", "#FFFF0000", "#FF008000", "#FF0000FF", "#FF000000", "#FFFFFF00" };
            for (int i = 0; i < PaletteItems.Length; i++) { DrawBorderedCircle(PaletteItems[i], ColorTranslator.FromHtml(PaletteColors[i])); }
            // Устанавливаем события изменения цвета кисти
            PaletteRed.Click += (object sndr, EventArgs eargs) => { PickedColor = ColorTranslator.FromHtml(PaletteColors[1]); DrawBorderedCircle(PaletteColor, PickedColor); };
            PaletteGreen.Click += (object sndr, EventArgs eargs) => { PickedColor = ColorTranslator.FromHtml(PaletteColors[2]); DrawBorderedCircle(PaletteColor, PickedColor); };
            PaletteBlue.Click += (object sndr, EventArgs eargs) => { PickedColor = ColorTranslator.FromHtml(PaletteColors[3]); DrawBorderedCircle(PaletteColor, PickedColor); };
            PaletteBlack.Click += (object sndr, EventArgs eargs) => { PickedColor = ColorTranslator.FromHtml(PaletteColors[4]); DrawBorderedCircle(PaletteColor, PickedColor); };
            //
            int[] DefaultBrushSizes = new int[] { 1, 4, 5, 6, 8, 10, 12, 14, 18, 24, 30, 36, 48, 60, 72, 96 };
            for (int j = 0; j < DefaultBrushSizes.Length; j++)
            {
                // Без этого не работает, не понимаю, нахрена компилятор помнит итератор, я же потерял ссылку, ну да ладно, подумаешь пара часиков ушло на решение
                int fuck_my_brain = new int();
                fuck_my_brain = j;
                // А теперь к делу
                int resolution = 100;
                var thisitem = new ToolStripMenuItem()
                {
                    Text = $"{DefaultBrushSizes[j]} px",
                    Name = $"SizeMenuItem_{j}",
                    Image = new Bitmap(resolution, resolution)
                };
                BrushThickness.DropDownItems.Add(thisitem);
                thisitem.Click += (object sndr, EventArgs eargs) => { BrushSize = DefaultBrushSizes[fuck_my_brain]; BrushThickness.Text = $"Толщина: {BrushSize}px"; };
                int currentsize = DefaultBrushSizes[j];
                #region Отрисовываем кружочек в размер кисти
                Graphics.FromImage(thisitem.Image).FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#FF293541")), (resolution - currentsize) / 2, (resolution - currentsize) / 2, currentsize, currentsize);
                #endregion
            }
        }

        #endregion

        #region Метод отрисовки круга с обводкой
        private void DrawBorderedCircle(ToolStripMenuItem victim, Color color, int W = 512, int H = 512)
        {
            victim.Image = new Bitmap(W, W); // УСтанавливаем иконке битмап
            Graphics.FromImage(victim.Image).FillEllipse(new SolidBrush(color), W / 8, H / 8, W - W / 4, H - H / 4); // wh / 4, wh / 2
            Graphics.FromImage(victim.Image).DrawEllipse(new Pen(ColorTranslator.FromHtml("#FF293541"), 32), W / 8, H / 8, W - W / 4, H - H / 4);
        }
        #endregion

        #region Выбор другого цвета
        private void PaletteAnother_Click(object sender, EventArgs e)
        {
            MyColorDialog.Color = AnotherPickedColor; // Устанавливаем изначальный цвет колорпикера
            // Когда юзер соглашается с выбранным цветом
            if (MyColorDialog.ShowDialog() == DialogResult.OK)
            {
                AnotherPickedColor = MyColorDialog.Color; PickedColor = AnotherPickedColor; // Запоминаем цвет
                // Элемент выпадающего списка
                DrawBorderedCircle(PaletteAnother, AnotherPickedColor);
                DrawBorderedCircle(PaletteColor, PickedColor);
            }
        }
        #endregion

        #region Изменение размера кисти через текстовое поле
        private void OnBrushSizeChanged(object sender, EventArgs e)
        {
            BrushSizeTextBox.Text = string.Join("", BrushSizeTextBox.Text.Where(c => char.IsDigit(c)));
            if (int.TryParse(BrushSizeTextBox.Text, out int temporaryinteger))
                if (temporaryinteger > 0 && temporaryinteger < 501)
                {
                    BrushSize = temporaryinteger;
                    BrushThickness.Text = $"Толщина: {BrushSize}px";
                }
        }
        #endregion

        #region Выбор инструмента
        private void SelectTool(object sender, EventArgs e)
        {
            switch ((sender as ToolStripMenuItem).Name)
            {
                case "BrushTool": { SelectedTool = SelectedToolType.Brush; } break;
                case "LineTool": { SelectedTool = SelectedToolType.Line; } break;
                case "RoundTool":
                    {
                        if (SelectedTool == SelectedToolType.RoundOutline)
                        {
                            SelectedTool = SelectedToolType.RoundFill;
                            RoundTool.Image = global::WinFormsPaint.Properties.Resources.circle_fill;
                        }
                        else
                        {
                            SelectedTool = SelectedToolType.RoundOutline;
                            RoundTool.Image = global::WinFormsPaint.Properties.Resources.circle_outline;
                        }
                    }
                    break;
                case "StarTool":
                    {
                        if (SelectedTool == SelectedToolType.StarOutline)
                        {
                            SelectedTool = SelectedToolType.StarFill;
                            StarTool.Image = global::WinFormsPaint.Properties.Resources.star_half;
                        }
                        else
                        {
                            SelectedTool = SelectedToolType.StarOutline;
                            StarTool.Image = global::WinFormsPaint.Properties.Resources.star;
                        }
                    }
                    break;
                case "EraserTool": { SelectedTool = SelectedToolType.Eraser; } break;
            }
            foreach (ToolStripMenuItem tsmi in menuStrip2.Items)
                tsmi.Font = new Font("Century Gothic", 10.2f, FontStyle.Regular);
            (sender as ToolStripMenuItem).Font = new Font("Century Gothic", 10.2f, FontStyle.Bold);
        }
        #endregion

        #region Призыв диалоговых окошечек
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Canvas frmChild = new Canvas
            {
                MdiParent = this
            };
            frmChild.Show();
        }

        private void размерХолстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasSize cs = new CanvasSize
            {
                CanvasWidth = ((Canvas)ActiveMdiChild).CanvasWidth,
                CanvasHeight = ((Canvas)ActiveMdiChild).CanvasHeight
            };
            if (cs.ShowDialog() == DialogResult.OK)
            {
                ((Canvas)ActiveMdiChild).CanvasWidth = cs.CanvasWidth;
                ((Canvas)ActiveMdiChild).CanvasHeight = cs.CanvasHeight;
            }
        }
        #endregion

        #region Заблокированные пункты меню
        private void файлToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            сохранитьToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
            сохранитьКакToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
        }

        private void рисунокToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            размерХолстаToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
        }

        private void окноToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            каскадомToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
            слеваНаправоToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
            сверхуВнизToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
            упорядочитьЗначкиToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
        }

        private void плагиныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (object item in (sender as ToolStripDropDownItem).DropDownItems)
            {
                if (item is ToolStripDropDownItem)
                    (item as ToolStripDropDownItem).Enabled = !(ActiveMdiChild == null);
            }
        }
        #endregion

        #region Панель "окно"
        private void WindowStyleStripItemSelected(object sender, EventArgs e)
        {
            switch ((sender as ToolStripMenuItem).Name)
            {
                case "каскадомToolStripMenuItem": { LayoutMdi(MdiLayout.Cascade); } break;
                case "слеваНаправоToolStripMenuItem": { LayoutMdi(MdiLayout.TileVertical); } break;
                case "сверхуВнизToolStripMenuItem": { LayoutMdi(MdiLayout.TileHorizontal); } break;
                case "упорядочитьЗначкиToolStripMenuItem": { LayoutMdi(MdiLayout.ArrangeIcons); } break;
            }
        }
        #endregion

        #region Масштабирование
        private void ScaleDown_Click(object sender, EventArgs e)
        {
            Rectangle compressionRectangle = new Rectangle(0, 0, (ActiveMdiChild as Canvas).GetBmpSize()[0] / 2, (ActiveMdiChild as Canvas).GetBmpSize()[1] / 2);
            (ActiveMdiChild as Canvas).ScaleBitmapByReclangle(compressionRectangle);
        }

        private void ScaleUp_Click(object sender, EventArgs e)
        {
            Rectangle expansionRectangle = new Rectangle(0, 0, (ActiveMdiChild as Canvas).GetBmpSize()[0] * 2, (ActiveMdiChild as Canvas).GetBmpSize()[1] * 2);
            (ActiveMdiChild as Canvas).ScaleBitmapByReclangle(expansionRectangle);
        }
        #endregion

        #region Манипуляции с файлом
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Файлы JPEG (*.jpeg, *.jpg)|*.jpeg;*.jpg" +
                        "|Изображение PNG (*.png)|*.png" +
                        "|Windows Bitmap (*.bmp)|*.bmp" +
                        "|Все файлы (*.*)|*.*"
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Canvas frmChild = new Canvas(dlg.FileName)
                {
                    MdiParent = this
                };
                frmChild.Show();
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((Canvas)ActiveMdiChild).SaveAs();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((Canvas)ActiveMdiChild).FilePath == "") ((Canvas)ActiveMdiChild).SaveAs(); else ((Canvas)ActiveMdiChild).JustSave();
        }
        #endregion

        #region Рефлек$$ия
        void FindPlugins()
        {
            /** 
             * То что код 1 в 1 слизан с источника в виде вордовского файла еще не значит, что я не понял, как оно работает.
             * Кстати, классная технология, давно искал способы как бы безопасно (ну или почти (если позднее связывание вообще 
             * можно назвать безопасным (ахах, зацените каламбур "типа-безопасно" (гы, рефлексирую над рефлексией)))).
             * Скобочная последовательность сверху, между прочим, правильная.
             **/

            // папка с плагинами
            string folder = System.AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(folder);

            // dll-файлы в этой папке
            string[] files = System.IO.Directory.GetFiles(folder, "*.dll");

            foreach (string file in files)
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);

                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("PluginInterface.IPlugin");

                        if (iface != null)
                        {
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                            Console.WriteLine(plugin + plugin.Name + plugin.GetType());

                            if (plugin != null)
                                pluginsList.Add(plugin.Name, plugin);

                            var newItem = new ToolStripMenuItem() { Text = plugin.Name };
                            newItem.Click += new System.EventHandler((sender, e) =>
                            {
                                if (ActiveMdiChild != null)
                                {
                                    plugin.Transform((ActiveMdiChild as Canvas).bmp);
                                    (ActiveMdiChild as Canvas).PictureBox.Image = (ActiveMdiChild as Canvas).bmp;
                                }

                            });
                            плагиныToolStripMenuItem.DropDownItems.Add(newItem);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при загрузке плагина\n" + ex.Message + '\n' + ex.StackTrace);
                }

            #region Отрывок кода для отладки плагина
            //var debugPlugin = new ToolStripMenuItem() { Text = "Отладочный плагин" };
            //debugPlugin.Click += new System.EventHandler((sender, e) =>
            //{
            //    if (ActiveMdiChild != null)
            //    {
            //        Graphics graphics = Graphics.FromImage((ActiveMdiChild as Canvas).bmp);


            //        (ActiveMdiChild as Canvas).PictureBox.Image = (ActiveMdiChild as Canvas).bmp;
            //    }

            //});
            //плагиныToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
            //плагиныToolStripMenuItem.DropDownItems.Add(debugPlugin);
            #endregion
        }

        #endregion
    }

    #region Попытка добавить трекбар в меню [Безуспешно]
    //[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
    //public class TrackBarMenuItem : ToolStripControlHost
    //{
    //    private TrackBar trackBar;

    //    public TrackBarMenuItem() : base(new TrackBar())
    //    {
    //        this.trackBar = this.Control as TrackBar;
    //    }
    //}
    #endregion
    #region Исследование представлений геоданных [Успешно]
    /*
    public void testFlex()
    {
        Image image = Image.FromFile(@"C:\Users\vdape\Desktop\england-london-bridge.jpg");
        var bitmap = new Bitmap(image);
        // Задаем
        foreach (var pitem in image.PropertyItems)
            bitmap.SetPropertyItem(pitem);
        image.Dispose();


        foreach (var item in bitmap.PropertyItems)
        {
            if (item.Id == 0x0002)
                Console.WriteLine(item.Value[0]);
        }

        // TODO: Навешивать геоданные устройства, если гео изображения нет
        foreach (var l1 in new int[] { 0x0001, 0x0002, 0x0003, 0x0004, 0x0005, 0x0006 })
        {
            Console.Write(l1 + ": ");
            var propertyValue = bitmap.GetPropertyItem(l1).Value;
            if (propertyValue != null)
            {
                foreach (var l2 in propertyValue)
                {
                    Console.Write(l2); Console.Write(' ');
                }
                Console.WriteLine();

                if (propertyValue.Length > 8)
                {
                    Console.WriteLine("D: " + BitConverter.ToUInt32(propertyValue, 0) + '\n');
                    Console.WriteLine("M: " + BitConverter.ToUInt32(propertyValue, 8) + '\n');
                    Console.WriteLine("S: " + ((double)BitConverter.ToUInt32(propertyValue, 16) / 100) + '\n');
                }
                else if (propertyValue.Length == 8)
                {
                    Console.WriteLine("A: " + BitConverter.ToUInt32(propertyValue, 0) + '\n');
                }
                else
                {
                    Console.WriteLine((char)propertyValue[0]);
                }
            }

        }

        // Console.WriteLine(image.GetPropertyItem(0x0002).Value.ToString() + " " + image.GetPropertyItem(0x0004).Value.ToString());
    }
    */
    #endregion
}
