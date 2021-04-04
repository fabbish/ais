using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIS
{
    public partial class add_auto_supply : Form
    {
        public add_auto_supply()
        {
            InitializeComponent();
        }

        private void add_auto_supply_Load(object sender, EventArgs e)
        {
            textBox1.Text = Supply.supplies[Supply.supplies.Count - 1].makeAuto;
            textBox2.Text = Supply.supplies[Supply.supplies.Count - 1].modelAuto;
            textBox3.Text = ((double)(Supply.supplies[Supply.supplies.Count - 1].price) *1.1).ToString();

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
            if ((textBox1.Text.Equals("")) || (textBox2.Text.Equals("")) || (textBox3.Text.Equals("")) || (richTextBox1.Text.Equals("")) || (pictureBox1.ImageLocation == null))
                MessageBox.Show("Введите все данные");
            else
            {
                Auto.autos.Add(new Auto
                {
                    id = Auto.autos[Auto.autos.Count - 1].id + 1,
                    makeAuto = textBox1.Text,
                    modelAuto = textBox2.Text,
                    priceAuto = Convert.ToDecimal(textBox3.Text),
                    descriptionAuto = richTextBox1.Text,
                    photoAuto = pictureBox1.ImageLocation
                });
                if (OleDbOperator.AddAuto() != 1)
                    MessageBox.Show("Ошибка выполнения запроса!");
                else
                {
                    MessageBox.Show("Данные об автомобиле добавлены!");
                    this.Close();
                }
            }
        }
    }
}
