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
    public partial class Add_Supply : Form
    {
        public Add_Supply()
        {
            InitializeComponent();
            DataTable dt = OleDbOperator.GetOledb("providers");
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "provider";
            comboBox1.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("") || textBox2.Text.Equals("")
                || textBox3.Text.Equals("") || textBox4.Text.Equals("") || comboBox1.SelectedIndex == -1)
                MessageBox.Show("Заполните все поля");
            else
            {
                Supply.supplies.Add(new Supply
                {
                    id = Supply.supplies[Supply.supplies.Count - 1].id + 1,
                    provider = comboBox1.Text,
                    makeAuto = textBox1.Text,
                    modelAuto = textBox2.Text,
                    countAuto = int.Parse(textBox3.Text),
                    dateSupply = DateTime.Today,
                    price = Convert.ToDecimal(textBox4.Text),
                });
                if (OleDbOperator.AddSupply() != 1)
                    MessageBox.Show("Ошибка выполнения запроса!");
                else
                {
                    MessageBox.Show("Данные о поставке добавлены!");
                    
                    if (Auto.autos.Exists(a => a.makeAuto == Supply.supplies[Supply.supplies.Count - 1].makeAuto
                    && a.modelAuto == Supply.supplies[Supply.supplies.Count - 1].modelAuto))
                        this.Close();
                    else
                    {
                        add_auto_supply f = new add_auto_supply();
                        MessageBox.Show("Вам необходимо добавить фото и описание авто!");
                        f.ShowDialog();
                    }
                    this.Close();
                }
            }

        }


        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void Add_Supply_Load(object sender, EventArgs e)
        {

        }
    }
}
