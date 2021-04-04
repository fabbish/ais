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
    public partial class auto_client : Form
    {
        DataTable dt;
        List<string> makeList = new List<string>();
        public auto_client()
        {
            InitializeComponent();
            richTextBox1.ReadOnly = true;
            dt = OleDbOperator.GetOledb("auto");
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].HeaderText = "Марка авто";
            dataGridView1.Columns[2].HeaderText = "Модель авто";
            dataGridView1.Columns[3].HeaderText = "Цена";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            foreach (Auto a in Auto.autos)
                makeList.Add(a.makeAuto);

            makeList = makeList.Distinct().ToList();
            comboBox1.DataSource = makeList;
            comboBox1.SelectedIndex = -1;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                string imagePath = dt.Rows[dataGridView1.CurrentRow.Index][5].ToString();
                pictureBox1.Image = new Bitmap(@imagePath);
                richTextBox1.Text = dt.Rows[dataGridView1.CurrentRow.Index][4].ToString();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                filterAutoTable();
            }
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
            comboBox2.SelectedIndex = -1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = String.Format("make_auto like '{0}%'", textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string makeAuto = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string modelAuto = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            int price = Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value);
            Order f = new Order(makeAuto, modelAuto, price);
            f.ShowDialog();
        }

        private void filterAutoTable()
        {
            List<string> filterParts = new List<string>();
            if (comboBox1.SelectedIndex >= 0)
                filterParts.Add("make_auto LIKE '%" + comboBox1.Text + "%'");
            if (comboBox2.SelectedIndex >= 0)
                filterParts.Add("model_auto LIKE '%" + comboBox2.Text + "%'");
            if (textBox2.Text != "")
                filterParts.Add("price <= " + Convert.ToDecimal(textBox2.Text));
            string filter = string.Join(" AND ", filterParts);
            dt.DefaultView.RowFilter = filter;
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender; // приводим отправителя к элементу типа CheckBox
            if (checkBox.Checked == true)
            {
                filterAutoTable();
            }
            else
            {
                dt.DefaultView.RowFilter = "";
            }
        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string imagePath = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            if (imagePath.Contains(".jpg"))
                pictureBox1.Image = new Bitmap(@imagePath);
            else
                pictureBox1.Image = new Bitmap("C:\\Users\\dim90\\Desktop\\auto\\non.jpg");
            richTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                filterAutoTable();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                filterAutoTable();
            }
        }
    }
}