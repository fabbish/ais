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
    public partial class clients : Form
    {
        public clients()
        {
            InitializeComponent();
            dataGridView1.DataSource = Client.clients;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "ФИО";
            dataGridView1.Columns[2].HeaderText = "Номер телефона";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                client_buys f = new client_buys(name);
                f.ShowDialog();
            }
            else
                MessageBox.Show("Выберите одного покупателя!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new_client f = new new_client("");
            f.ShowDialog();
            dataGridView1.DataSource = Client.clients;
            dataGridView1.Refresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
