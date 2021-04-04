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
    public partial class new_client : Form
    {
        public new_client(string name)
        {
            InitializeComponent();
            if (name == "")
                textBox1.ReadOnly = false;
            else
                textBox1.Text = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.MaskCompleted && textBox1.Text != "")
            {
                Client.clients.Add(new Client
                {
                    id = Client.clients[Client.clients.Count - 1].id + 1,
                    fullname = textBox1.Text,
                    phone = maskedTextBox1.Text
                });
                if (OleDbOperator.AddClient() != 1)
                    MessageBox.Show("Ошибка выполнения запроса!");
                else
                {
                    MessageBox.Show("Данные о клиенте добавлены!");
                }
                this.Close();
            }
            else
                MessageBox.Show("Заполните все поля!");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void new_client_Load(object sender, EventArgs e)
        {

        }
    }
}
