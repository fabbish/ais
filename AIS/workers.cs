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
    public partial class workers : Form
    {
        DataTable dt;
        
        public workers()
        {
            InitializeComponent();
     
        }

        private void workers_Load(object sender, EventArgs e)
        {
            dt = OleDbOperator.GetOledb("workers");
            dataGridView1.DataSource = dt;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Reg_new_worker f = new Reg_new_worker("");
            f.ShowDialog();
            dt = OleDbOperator.GetOledb("workers");
            dataGridView1.DataSource = dt;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = (int)dataGridView1.Rows[index].Cells[0].Value;

                if (OleDbOperator.DeleteOledb("workers", id) != 1)
                    MessageBox.Show("Ошибка выполнения запроса!");
                else
                {
                    MessageBox.Show("Работник уволен!");
                    dt = OleDbOperator.GetOledb("workers");
                    dataGridView1.DataSource = dt;
                }
            }
            else
            {
                MessageBox.Show("Выберите работника!");
            }
        }
        private void workers_FormClosing(Object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked && comboBox1.SelectedIndex != -1)
            {
                dt.DefaultView.RowFilter = String.Format("pos = '" + comboBox1.Text + "'");
            }
            else
                dt.DefaultView.RowFilter = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked && comboBox1.SelectedIndex != -1)
            {
                dt.DefaultView.RowFilter = String.Format("pos = '" + comboBox1.Text + "'");
            }
            else
                dt.DefaultView.RowFilter = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dt.DefaultView.Sort = "date1 asc";
        }
    }
}
