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
    public partial class providers : Form
    {
        DataTable dt;
        public providers()
        {
            InitializeComponent();
            dt = OleDbOperator.GetOledb("providers");
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].HeaderText = "Поставщик";
            dataGridView1.Columns[2].HeaderText = "Номер телефона";
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = (int)dataGridView1.Rows[index].Cells[0].Value;


                if (OleDbOperator.DeleteOledb("providers", id) != 1)
                    MessageBox.Show("Ошибка выполнения запроса!");
                else
                {
                    MessageBox.Show("Поставщик удален!");
                    dt = OleDbOperator.GetOledb("providers");
                    dataGridView1.DataSource = dt;
                }
            }
            else
            {
                MessageBox.Show("Выберите поставщика!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new_provider f = new new_provider();
            f.ShowDialog();
            dt = OleDbOperator.GetOledb("providers");
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dt.DefaultView.Sort = String.Format("provider asc");
        }
    }
}
