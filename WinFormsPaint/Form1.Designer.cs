using System.Drawing;

namespace WinFormsPaint
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.PaletteColor = new System.Windows.Forms.ToolStripMenuItem();
            this.PaletteRed = new System.Windows.Forms.ToolStripMenuItem();
            this.PaletteGreen = new System.Windows.Forms.ToolStripMenuItem();
            this.PaletteBlue = new System.Windows.Forms.ToolStripMenuItem();
            this.PaletteBlack = new System.Windows.Forms.ToolStripMenuItem();
            this.PaletteAnother = new System.Windows.Forms.ToolStripMenuItem();
            this.BrushThickness = new System.Windows.Forms.ToolStripMenuItem();
            this.BrushSizeTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.масштабToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScaleUp = new System.Windows.Forms.ToolStripMenuItem();
            this.ScaleDown = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.BrushTool = new System.Windows.Forms.ToolStripMenuItem();
            this.StarTool = new System.Windows.Forms.ToolStripMenuItem();
            this.LineTool = new System.Windows.Forms.ToolStripMenuItem();
            this.RoundTool = new System.Windows.Forms.ToolStripMenuItem();
            this.EraserTool = new System.Windows.Forms.ToolStripMenuItem();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.рисунокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.размерХолстаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.окноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.каскадомToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.слеваНаправоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сверхуВнизToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.упорядочитьЗначкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // PaletteColor
            // 
            this.PaletteColor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PaletteRed,
            this.PaletteGreen,
            this.PaletteBlue,
            this.PaletteBlack,
            this.PaletteAnother});
            this.PaletteColor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PaletteColor.Name = "PaletteColor";
            this.PaletteColor.Size = new System.Drawing.Size(60, 25);
            this.PaletteColor.Text = "Цвет";
            // 
            // PaletteRed
            // 
            this.PaletteRed.Name = "PaletteRed";
            this.PaletteRed.Size = new System.Drawing.Size(216, 26);
            this.PaletteRed.Text = "Красный";
            // 
            // PaletteGreen
            // 
            this.PaletteGreen.Name = "PaletteGreen";
            this.PaletteGreen.Size = new System.Drawing.Size(216, 26);
            this.PaletteGreen.Text = "Зеленый";
            // 
            // PaletteBlue
            // 
            this.PaletteBlue.Name = "PaletteBlue";
            this.PaletteBlue.Size = new System.Drawing.Size(216, 26);
            this.PaletteBlue.Text = "Синий";
            // 
            // PaletteBlack
            // 
            this.PaletteBlack.Name = "PaletteBlack";
            this.PaletteBlack.Size = new System.Drawing.Size(216, 26);
            this.PaletteBlack.Text = "Черный";
            // 
            // PaletteAnother
            // 
            this.PaletteAnother.Name = "PaletteAnother";
            this.PaletteAnother.Size = new System.Drawing.Size(216, 26);
            this.PaletteAnother.Text = "Другой...";
            this.PaletteAnother.Click += new System.EventHandler(this.PaletteAnother_Click);
            // 
            // BrushThickness
            // 
            this.BrushThickness.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BrushSizeTextBox});
            this.BrushThickness.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BrushThickness.Name = "BrushThickness";
            this.BrushThickness.Size = new System.Drawing.Size(138, 25);
            this.BrushThickness.Text = "Толщина: 5px";
            // 
            // BrushSizeTextBox
            // 
            this.BrushSizeTextBox.Name = "BrushSizeTextBox";
            this.BrushSizeTextBox.Size = new System.Drawing.Size(100, 27);
            this.BrushSizeTextBox.ToolTipText = "Размер кисти должен быть от 1 до 500 пикселей";
            this.BrushSizeTextBox.Click += new System.EventHandler(this.OnBrushSizeChanged);
            this.BrushSizeTextBox.TextChanged += new System.EventHandler(this.OnBrushSizeChanged);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.RightToLeftAutoMirrorImage = true;
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 25);
            // 
            // масштабToolStripMenuItem
            // 
            this.масштабToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ScaleUp,
            this.ScaleDown});
            this.масштабToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.масштабToolStripMenuItem.Name = "масштабToolStripMenuItem";
            this.масштабToolStripMenuItem.Size = new System.Drawing.Size(108, 25);
            this.масштабToolStripMenuItem.Text = "Масштаб";
            // 
            // ScaleUp
            // 
            this.ScaleUp.Image = ((System.Drawing.Image)(resources.GetObject("ScaleUp.Image")));
            this.ScaleUp.Name = "ScaleUp";
            this.ScaleUp.Size = new System.Drawing.Size(182, 26);
            this.ScaleUp.Text = "Увеличить";
            this.ScaleUp.Click += new System.EventHandler(this.ScaleUp_Click);
            // 
            // ScaleDown
            // 
            this.ScaleDown.Image = ((System.Drawing.Image)(resources.GetObject("ScaleDown.Image")));
            this.ScaleDown.Name = "ScaleDown";
            this.ScaleDown.Size = new System.Drawing.Size(182, 26);
            this.ScaleDown.Text = "Уменьшить";
            this.ScaleDown.Click += new System.EventHandler(this.ScaleDown_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(177)))), ((int)(((byte)(209)))));
            this.menuStrip2.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PaletteColor,
            this.BrushThickness,
            this.toolStripMenuItem1,
            this.BrushTool,
            this.StarTool,
            this.LineTool,
            this.RoundTool,
            this.EraserTool,
            this.масштабToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 35);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.menuStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip2.Size = new System.Drawing.Size(1211, 35);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // BrushTool
            // 
            this.BrushTool.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BrushTool.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BrushTool.Image = global::WinFormsPaint.Properties.Resources.brush;
            this.BrushTool.Name = "BrushTool";
            this.BrushTool.Size = new System.Drawing.Size(86, 25);
            this.BrushTool.Text = "Перо";
            this.BrushTool.Click += new System.EventHandler(this.SelectTool);
            // 
            // StarTool
            // 
            this.StarTool.ForeColor = System.Drawing.SystemColors.ControlText;
            this.StarTool.Image = global::WinFormsPaint.Properties.Resources.star;
            this.StarTool.Name = "StarTool";
            this.StarTool.Size = new System.Drawing.Size(100, 25);
            this.StarTool.Text = "Звезда";
            this.StarTool.Click += new System.EventHandler(this.SelectTool);
            // 
            // LineTool
            // 
            this.LineTool.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LineTool.Image = global::WinFormsPaint.Properties.Resources.vector;
            this.LineTool.Name = "LineTool";
            this.LineTool.Size = new System.Drawing.Size(91, 25);
            this.LineTool.Text = "Линия";
            this.LineTool.Click += new System.EventHandler(this.SelectTool);
            // 
            // RoundTool
            // 
            this.RoundTool.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RoundTool.Image = ((System.Drawing.Image)(resources.GetObject("RoundTool.Image")));
            this.RoundTool.Name = "RoundTool";
            this.RoundTool.Size = new System.Drawing.Size(105, 25);
            this.RoundTool.Text = "Эллипс";
            this.RoundTool.Click += new System.EventHandler(this.SelectTool);
            // 
            // EraserTool
            // 
            this.EraserTool.ForeColor = System.Drawing.SystemColors.ControlText;
            this.EraserTool.Image = global::WinFormsPaint.Properties.Resources.eraser;
            this.EraserTool.Name = "EraserTool";
            this.EraserTool.Size = new System.Drawing.Size(102, 25);
            this.EraserTool.Text = "Ластик";
            this.EraserTool.Click += new System.EventHandler(this.SelectTool);
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новыйToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.toolStripSeparator1,
            this.сохранитьToolStripMenuItem,
            this.сохранитьКакToolStripMenuItem,
            this.toolStripSeparator2,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(68, 25);
            this.файлToolStripMenuItem.Text = "Файл";
            this.файлToolStripMenuItem.DropDownOpening += new System.EventHandler(this.файлToolStripMenuItem_DropDownOpening);
            // 
            // новыйToolStripMenuItem
            // 
            this.новыйToolStripMenuItem.Name = "новыйToolStripMenuItem";
            this.новыйToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.новыйToolStripMenuItem.Text = "Новый";
            this.новыйToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.открытьToolStripMenuItem.Text = "Открыть...";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(225, 6);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как...";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.сохранитьКакToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(225, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // рисунокToolStripMenuItem
            // 
            this.рисунокToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.размерХолстаToolStripMenuItem});
            this.рисунокToolStripMenuItem.Name = "рисунокToolStripMenuItem";
            this.рисунокToolStripMenuItem.Size = new System.Drawing.Size(91, 25);
            this.рисунокToolStripMenuItem.Text = "Рисунок";
            this.рисунокToolStripMenuItem.DropDownOpening += new System.EventHandler(this.рисунокToolStripMenuItem_DropDownOpening);
            // 
            // размерХолстаToolStripMenuItem
            // 
            this.размерХолстаToolStripMenuItem.Name = "размерХолстаToolStripMenuItem";
            this.размерХолстаToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.размерХолстаToolStripMenuItem.Text = "Размер холста";
            this.размерХолстаToolStripMenuItem.Click += new System.EventHandler(this.размерХолстаToolStripMenuItem_Click);
            // 
            // окноToolStripMenuItem
            // 
            this.окноToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.каскадомToolStripMenuItem,
            this.слеваНаправоToolStripMenuItem,
            this.сверхуВнизToolStripMenuItem,
            this.упорядочитьЗначкиToolStripMenuItem});
            this.окноToolStripMenuItem.Name = "окноToolStripMenuItem";
            this.окноToolStripMenuItem.Size = new System.Drawing.Size(67, 25);
            this.окноToolStripMenuItem.Text = "Окно";
            this.окноToolStripMenuItem.DropDownOpening += new System.EventHandler(this.окноToolStripMenuItem_DropDownOpening);
            // 
            // каскадомToolStripMenuItem
            // 
            this.каскадомToolStripMenuItem.Name = "каскадомToolStripMenuItem";
            this.каскадомToolStripMenuItem.Size = new System.Drawing.Size(253, 26);
            this.каскадомToolStripMenuItem.Text = "Каскадом";
            this.каскадомToolStripMenuItem.Click += new System.EventHandler(this.WindowStyleStripItemSelected);
            // 
            // слеваНаправоToolStripMenuItem
            // 
            this.слеваНаправоToolStripMenuItem.Name = "слеваНаправоToolStripMenuItem";
            this.слеваНаправоToolStripMenuItem.Size = new System.Drawing.Size(253, 26);
            this.слеваНаправоToolStripMenuItem.Text = "Слева направо";
            this.слеваНаправоToolStripMenuItem.Click += new System.EventHandler(this.WindowStyleStripItemSelected);
            // 
            // сверхуВнизToolStripMenuItem
            // 
            this.сверхуВнизToolStripMenuItem.Name = "сверхуВнизToolStripMenuItem";
            this.сверхуВнизToolStripMenuItem.Size = new System.Drawing.Size(253, 26);
            this.сверхуВнизToolStripMenuItem.Text = "Сверху вниз";
            this.сверхуВнизToolStripMenuItem.Click += new System.EventHandler(this.WindowStyleStripItemSelected);
            // 
            // упорядочитьЗначкиToolStripMenuItem
            // 
            this.упорядочитьЗначкиToolStripMenuItem.Name = "упорядочитьЗначкиToolStripMenuItem";
            this.упорядочитьЗначкиToolStripMenuItem.Size = new System.Drawing.Size(253, 26);
            this.упорядочитьЗначкиToolStripMenuItem.Text = "Упорядочить значки";
            this.упорядочитьЗначкиToolStripMenuItem.Click += new System.EventHandler(this.WindowStyleStripItemSelected);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(99, 25);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(225, 26);
            this.оПрограммеToolStripMenuItem.Text = "О программе...";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Crimson;
            this.menuStrip1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.рисунокToolStripMenuItem,
            this.окноToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.окноToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.menuStrip1.Size = new System.Drawing.Size(1211, 35);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1211, 678);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "My Paint For HSE";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ToolStripMenuItem PaletteColor;
        private System.Windows.Forms.ToolStripMenuItem PaletteRed;
        private System.Windows.Forms.ToolStripMenuItem PaletteGreen;
        private System.Windows.Forms.ToolStripMenuItem PaletteBlue;
        private System.Windows.Forms.ToolStripMenuItem PaletteBlack;
        private System.Windows.Forms.ToolStripMenuItem PaletteAnother;
        private System.Windows.Forms.ToolStripMenuItem BrushThickness;
        private System.Windows.Forms.ToolStripTextBox BrushSizeTextBox;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem BrushTool;
        private System.Windows.Forms.ToolStripMenuItem StarTool;
        private System.Windows.Forms.ToolStripMenuItem LineTool;
        private System.Windows.Forms.ToolStripMenuItem RoundTool;
        private System.Windows.Forms.ToolStripMenuItem EraserTool;
        private System.Windows.Forms.ToolStripMenuItem масштабToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ScaleUp;
        private System.Windows.Forms.ToolStripMenuItem ScaleDown;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новыйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem рисунокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem размерХолстаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem окноToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem каскадомToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem слеваНаправоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сверхуВнизToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem упорядочитьЗначкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}

