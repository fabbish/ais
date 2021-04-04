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
    public partial class selling : Form
    {
        DataTable dt;
        public selling()
        {
            InitializeComponent();
            dt = OleDbOperator.GetOledb("sales");
            dataGridView1.DataSource = dt;
            if(User.getInstance().Pos == "manager")
            {
                dt.DefaultView.RowFilter = String.Format("worker = '" + User.getInstance().Name + "'");
            }
                
        }

        public void filter()
        {
            if (User.getInstance().Pos == "manager")
            {
                if (checkBox1.Checked)
                    dt.DefaultView.RowFilter = String.Format("worker = '" + User.getInstance().Name + "' AND dateSale >= '" + dateTimePicker1.Value 
                        + "' AND dateSale <= '" + dateTimePicker2.Value + "'");
                else
                    dt.DefaultView.RowFilter = String.Format("worker = '" + User.getInstance().Name + "'");
            }
            else
            {
                if (checkBox1.Checked)
                    dt.DefaultView.RowFilter = String.Format("dateSale >= '" + dateTimePicker1.Value + "' AND dateSale <= '" + dateTimePicker2.Value + "'");
                else
                    dt.DefaultView.RowFilter = "";
            }
           
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            checkout f = new checkout();
            f.ShowDialog();
            dt = OleDbOperator.GetOledb("sales");
            dataGridView1.DataSource = dt;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            object amount;
            if(checkBox1.Checked)
            {
                if (User.getInstance().Pos == "manager")
                    amount = dt.Compute("Sum(price)", String.Format("worker = '" + User.getInstance().Name + "' AND dateSale >= '" + dateTimePicker1.Value
                            + "' AND dateSale <= '" + dateTimePicker2.Value + "'"));
                else
                    amount = dt.Compute("Sum(price)", String.Format("dateSale >= '" + dateTimePicker1.Value +
                        "' AND dateSale <= '" + dateTimePicker2.Value + "'"));
            }
            else
                amount = dt.Compute("Sum(price)", "");
            MessageBox.Show($"Сумма продаж за выбранный период составляет {amount} рублей");
        }
    }
}
