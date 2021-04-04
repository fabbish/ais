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
    public partial class Supplies : Form
    {
        public Supplies()
        {
            InitializeComponent();
        }

        private void Supply_Load(object sender, EventArgs e)
        {
            DataTable dt = OleDbOperator.GetOledb("Supply");
            dataGridView1.DataSource = dt;
            Supply.supplies = dt.AsEnumerable()
                .Select(dr => {
                    return new Supply
                    {
                        id = int.Parse(dr["Id"].ToString()),
                        provider = (string)dr["Provider"],
                        makeAuto = (string)dr["make_auto"],
                        modelAuto = (string)dr["model_auto"],
                        countAuto = int.Parse(dr["countAuto"].ToString()),
                        dateSupply = (DateTime)(dr["DateSupply"]),
                        price = (decimal)(dr["price"]),
                    };
                }).ToList();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Поставщик";
            dataGridView1.Columns[2].HeaderText = "Марка авто";
            dataGridView1.Columns[3].HeaderText = "Модель авто";
            dataGridView1.Columns[4].HeaderText = "Количество";
            dataGridView1.Columns[5].HeaderText = "Дата поставки";
            dataGridView1.Columns[6].HeaderText = "Цена за шт.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_Supply f = new Add_Supply();
            f.ShowDialog();
            DataTable dt = OleDbOperator.GetOledb("Supply");
            dataGridView1.DataSource = dt;
        }
    }
}
