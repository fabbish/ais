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
    public partial class checkout : Form
    {
        List<string> makeList = new List<string>();
        public checkout()
        {
            InitializeComponent();
            
        }

        private void checkout_Load(object sender, EventArgs e)
        {
            foreach (Auto a in Auto.autos)
                makeList.Add(a.makeAuto);

            makeList = makeList.Distinct().ToList();
            comboBox1.DataSource = makeList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Equals(""))
                MessageBox.Show("Введите ФИО");
            else
            {
                string conString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=AIS.mdb";
                OleDbConnection con = new OleDbConnection(conString);
                con.Open();
                string query = "INSERT INTO sales (make_car, model_car, price, customer, dateSale, worker)"
                    + "VALUES (@make_auto, @model_auto, @price, @client, @dateSale, @worker)";
                OleDbCommand cmd = new OleDbCommand(query, con);

                cmd.Parameters.AddWithValue("@make_auto", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@model_auto", comboBox2.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(textBox2.Text));
                cmd.Parameters.AddWithValue("@client", textBox1.Text);
                cmd.Parameters.AddWithValue("@dateSale", DateTime.Today);
                cmd.Parameters.AddWithValue("@worker", User.getInstance().Name);
                if (cmd.ExecuteNonQuery() != 1)
                    MessageBox.Show("Ошибка!");
                else
                {
                    Sale.sales.Add(new Sale
                    {
                        id = Sale.sales[Sale.sales.Count - 1].id + 1,
                        makeAuto = comboBox1.Text,
                        modelAuto = comboBox2.Text,
                        price = Convert.ToDecimal(textBox2.Text),
                        customer = textBox1.Text,
                        dateSale = DateTime.Today,
                        worker = User.getInstance().Name
                    });
                    MessageBox.Show("Данные о продаже добавлены");
                }  
                con.Close();
                int indx = Client.clients.FindIndex(c => c.fullname == textBox1.Text);
                if(indx == -1)
                {
                    new_client f = new new_client(textBox1.Text);
                    f.ShowDialog();
                }
                this.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> modelList = new List<string>();
            if (comboBox1.SelectedItem != null)
            {
                foreach (Auto a in Auto.autos)
                {
                    if (a.makeAuto.ToUpper() == comboBox1.SelectedItem.ToString().ToUpper())
                        modelList.Add(a.modelAuto);
                }
                comboBox2.DataSource = modelList;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue != null)
                foreach (Auto a in Auto.autos)
                {
                    if (a.makeAuto.ToUpper() == comboBox1.SelectedItem.ToString().ToUpper() &&
                        a.modelAuto.ToUpper() == comboBox2.SelectedItem.ToString().ToUpper())
                    {
                        textBox2.Text = a.priceAuto.ToString();
                        pictureBox1.Image = new Bitmap(a.photoAuto);
                        break;
                    }     
                }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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
