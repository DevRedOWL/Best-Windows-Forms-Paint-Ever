using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsPaint
{
    public partial class CanvasSize : Form
    {
        #region Загрузка формы
        public CanvasSize()
        {
            InitializeComponent();
        }
        #endregion

        #region Инкапсуляция полей ввода
        public int CanvasWidth
        {
            get {
                if (int.TryParse(string.Join("", textBox1.Text.Where(c => char.IsDigit(c))), out int temporaryinteger))
                    if (temporaryinteger > 0 && temporaryinteger < 10000)
                        return temporaryinteger;
                    else return 500;
                else return 500;
            }
            set
            {
                textBox1.Text = $"{value}";
            }
        }
        public int CanvasHeight
        {
            get {
                if (int.TryParse(string.Join("", textBox2.Text.Where(c => char.IsDigit(c))), out int temporaryinteger))
                    if (temporaryinteger > 0 && temporaryinteger < 10000)
                        return temporaryinteger;
                    else return 500;
                else return 500;
            }
            set
            {
                textBox2.Text = $"{value}";
            }
        }
        #endregion

        #region Валидация ввода
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(string.Join("", textBox1.Text.Where(c => char.IsDigit(c))), out int temporaryinteger))
                if (temporaryinteger > 0 && temporaryinteger < 10000)
                    button1.Enabled = true;
                else button1.Enabled = false;
            else button1.Enabled = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(string.Join("", textBox2.Text.Where(c => char.IsDigit(c))), out int temporaryinteger))
                if (temporaryinteger > 0 && temporaryinteger < 10000)
                    button1.Enabled = true;
                else button1.Enabled = false;
            else button1.Enabled = false;
        }
        #endregion
    }

}
