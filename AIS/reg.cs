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
    public partial class reg : Form
    {
        public reg()
        {
            InitializeComponent();
            
        }

		public Boolean isUserExist()
		{
            string conString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=AIS.mdb";
            OleDbConnection con = new OleDbConnection(conString);
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Users WHERE login ='" + textBox2.Text + "'";
            OleDbCommand cmd = new OleDbCommand(query, con);
            adapter.SelectCommand = cmd;
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
			{
				MessageBox.Show("Логин занят!");
				return true;
			}
			else
			{
				return false;
			}

		}

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            if (isUserExist())
                return;
            string conString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=AIS.mdb";
            OleDbConnection con = new OleDbConnection(conString);
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            string query = "INSERT INTO [Users] ([login], [password], [pos], [name1])" + "VALUES (@login, @password, @pos, @name1)";
            OleDbCommand cmd = new OleDbCommand(query, con);
            if (comboBox1.Text == "Менеджер")
            {
                con.Open();
                cmd.Parameters.AddWithValue("@login", textBox2.Text);
                cmd.Parameters.AddWithValue("@password", textBox3.Text);
                cmd.Parameters.AddWithValue("@pos", "manager");
                cmd.Parameters.AddWithValue("@name1", textBox1.Text);
                if(cmd.ExecuteNonQuery() != 0)
                {
                    MessageBox.Show("Регистрация прошла успешно");
                }
                else
                    MessageBox.Show("Регистрация не удалась");
                con.Close();
                int indx = Worker.workers.FindIndex(w => w.fullname == textBox1.Text);
                if(indx == -1)
                {
                    Reg_new_worker f = new Reg_new_worker(textBox1.Text);
                    f.ShowDialog();
                }
            }
            else
            {
                con.Open();
                cmd.Parameters.AddWithValue("@login", textBox2.Text);
                cmd.Parameters.AddWithValue("@password", textBox3.Text);
                cmd.Parameters.AddWithValue("@pos", "client");
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                if (cmd.ExecuteNonQuery() != 0)
                {
                    MessageBox.Show("Регистрация прошла успешно");
                }
                else
                    MessageBox.Show("Регистрация не удалась");
                con.Close();
                int indx = Client.clients.FindIndex(c => c.fullname == textBox1.Text);
                if (indx == -1)
                {
                    new_client f = new new_client(textBox1.Text);
                    f.ShowDialog();
                }
            }
            this.Close();
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
