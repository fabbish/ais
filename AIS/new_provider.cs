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
    public partial class new_provider : Form
    {
        public new_provider()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || !maskedTextBox1.MaskCompleted)
                MessageBox.Show("Заполните все поля");
            else
            {
                string conString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=AIS.mdb";
                OleDbConnection con = new OleDbConnection(conString);
                con.Open();
                string query = "INSERT INTO providers (provider, phone)"
                    + "VALUES (@provider, @phone)";
                OleDbCommand cmd = new OleDbCommand(query, con);

                cmd.Parameters.AddWithValue("@provider", textBox1.Text);
                cmd.Parameters.AddWithValue("@phone", maskedTextBox1.Text);

                if (cmd.ExecuteNonQuery() != 1)
                    MessageBox.Show("Ошибка!");
                MessageBox.Show("Данные о поставщике добавлены!");
                con.Close();
                this.Close();
            }
        }

        private void new_provider_Load(object sender, EventArgs e)
        {

        }
    }
}
