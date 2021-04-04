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
    public partial class Reg_new_worker : Form
    {
        public Reg_new_worker(string name)
        {
            InitializeComponent();
            if(name != "")
            {
                textBox1.ReadOnly = true;
                textBox1.Text = name;
                AddButton.Text = "Сохранить";
            }
            
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox1.Text != "" && maskedTextBox1.MaskCompleted)
            {
                string conString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=AIS.mdb";
                OleDbConnection con = new OleDbConnection(conString);
                string countStr = "SELECT COUNT(*) FROM workers";
                con.Open();
                OleDbCommand cmd = new OleDbCommand(countStr, con);
                int id = (int)cmd.ExecuteScalar() + 1;
                string query = "INSERT INTO workers (fullname, pos, phone, date1)" + "VALUES (@fullname, @pos, @phone, @date1)";
                cmd = new OleDbCommand(query, con);
                cmd.Parameters.AddWithValue("@fullname", textBox1.Text);
                cmd.Parameters.AddWithValue("@pos", comboBox1.Text);
                cmd.Parameters.AddWithValue("@phone", maskedTextBox1.Text);
                cmd.Parameters.AddWithValue("@date1", DateTime.Today);
                if (cmd.ExecuteNonQuery() != 1)
                    MessageBox.Show("Ошибка выполнения запроса!");
                else
                {
                    Worker.workers.Add(new Worker
                    {
                        id = Worker.workers[Worker.workers.Count - 1].id + 1,
                        fullname = textBox1.Text,
                        pos = comboBox1.Text,
                        phone = maskedTextBox1.Text,
                        date = DateTime.Today
                    }); 
                    MessageBox.Show("Данные о работнике добавлены!");
                }

                con.Close();
                this.Close();
            }
            else
                MessageBox.Show("Заполните все поля!");
            
        }

     
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
    }
}
