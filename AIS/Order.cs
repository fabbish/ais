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
    public partial class Order : Form
    {
        public Order(string make,string model, int price)
        {
            InitializeComponent();
            textBox1.Text = make;
            textBox2.Text = model;
            textBox3.Text = price + " Р.";
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
        }

        private void Order_Load(object sender, EventArgs e)
        {
            DataTable dt = OleDbOperator.GetOledb("workers");
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "fullname";
            comboBox1.ValueMember = "Id";
            comboBox1.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=AIS.mdb";
            OleDbConnection con = new OleDbConnection(conString);
            con.Open();
            string query = "INSERT INTO sales (make_car, model_car, price, customer, dateSale, worker)" + "Values (@make_car, @model_car, @price, @client, @dateSale, @worker)";
            OleDbCommand cmd = new OleDbCommand(query, con);
            cmd.Parameters.AddWithValue("@make_car", textBox1.Text);
            cmd.Parameters.AddWithValue("@model_car", textBox2.Text);
            cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(textBox3.Text.Substring(0, textBox3.Text.Length - 3)));
            cmd.Parameters.AddWithValue("@client", User.getInstance().Name);
            cmd.Parameters.AddWithValue("@dateSale", DateTime.Now.ToShortDateString());
            cmd.Parameters.AddWithValue("@worker", comboBox1.Text);
            if (cmd.ExecuteNonQuery() != 1)
                MessageBox.Show("Ошибка выполнения запроса!");
            else
            {
                Sale.sales.Add(new Sale
                {
                    id = Sale.sales[Sale.sales.Count - 1].id + 1,
                    makeAuto = textBox1.Text,
                    modelAuto = textBox2.Text,
                    price = Convert.ToDecimal(textBox3.Text.Substring(0, textBox3.Text.Length - 3)),
                    customer = User.getInstance().Name,
                    dateSale = DateTime.Today,
                    worker = comboBox1.Text
                });
                MessageBox.Show("Заказ успешно оформлен!");
                this.Close();
            }
            con.Close();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }
    }
}
