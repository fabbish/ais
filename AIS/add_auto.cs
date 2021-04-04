using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIS
{
    public partial class add_auto : Form
    {
        public add_auto()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {
                try
                {
                    pictureBox1.Image = new Bitmap(open_dialog.FileName);
                    pictureBox1.ImageLocation = open_dialog.FileName;
                }
                catch
                {
                    DialogResult result = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox2.Text.Equals("")) || (textBox4.Text.Equals("")) || (textBox5.Text.Equals("")) || (richTextBox1.Text.Equals("")) || (pictureBox1.ImageLocation == null))
                MessageBox.Show("Введите все данные");
            else
            {
                if (Auto.autos.Exists(a => a.makeAuto == textBox5.Text && a.modelAuto == textBox4.Text))
                {
                    MessageBox.Show("Такое авто уже есть в списке!");
                }
                else
                {
                    Auto.autos.Add(new Auto
                    {
                        id = Auto.autos[Auto.autos.Count - 1].id + 1,
                        makeAuto = textBox5.Text,
                        modelAuto = textBox4.Text,
                        priceAuto = Convert.ToDecimal(textBox2.Text),
                        descriptionAuto = richTextBox1.Text,
                        photoAuto = pictureBox1.ImageLocation
                    });
                    if (OleDbOperator.AddAuto() != 1)
                        MessageBox.Show("Ошибка выполнения запроса!");
                    else
                    {
                        MessageBox.Show("Данные об автомобиле добавлены!");
                    }
                }
                this.Close();
            }
        }
        private void add_auto_FormClosing(Object sender, FormClosingEventArgs e)
        {
            this.Close();
        }



        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }
    }
}
