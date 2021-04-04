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
    public partial class authorization : Form
    {
        public static string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=AIS.mdb";
        private OleDbConnection con;
        public authorization()
        {
            InitializeComponent();
            DataTable dt = OleDbOperator.GetOledb("auto");
            Auto.autos = dt.AsEnumerable()
                .Select(dr => {
                    return new Auto
                    {
                        id = int.Parse(dr["Id"].ToString()),
                        makeAuto = (string)dr["make_auto"],
                        modelAuto = (string)dr["model_auto"],
                        priceAuto = (decimal)(dr["price"]),
                        descriptionAuto = (string)dr["description"],
                        photoAuto = (string)dr["imagelink"]
                    };
                }).ToList();

            dt = OleDbOperator.GetOledb("sales");
            Sale.sales = dt.AsEnumerable()
                .Select(dr => {
                    return new Sale
                    {
                        id = int.Parse(dr["id"].ToString()),
                        makeAuto = dr["make_car"].ToString(),
                        modelAuto = dr["model_car"].ToString(),
                        price = Convert.ToDecimal(dr["price"]),
                        customer = dr["customer"].ToString(),
                        dateSale = Convert.ToDateTime(dr["DateSale"]),
                        worker = dr["worker"].ToString()
                    };
                }).ToList();

            dt = OleDbOperator.GetOledb("clients");
            Client.clients = dt.AsEnumerable()
                .Select(dr => {
                    return new Client
                    {
                        id = int.Parse(dr["id"].ToString()),
                        fullname = dr["fullname"].ToString(),
                        phone = dr["phone"].ToString()
                    };
                }).ToList();

            dt = OleDbOperator.GetOledb("workers");
            Worker.workers = dt.AsEnumerable()
               .Select(dr => {
                   return new Worker
                   {
                       id = int.Parse(dr["id"].ToString()),
                       fullname = dr["fullname"].ToString(),
                       pos = dr["pos"].ToString(),
                       phone = dr["phone"].ToString(),
                       date = Convert.ToDateTime(dr["date1"])
                   };
               }).ToList();
        }

        private void authorization_FormClosing(Object sender, FormClosingEventArgs e)
        {
            con.Close();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = new OleDbConnection(connectionString);
            con.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Users WHERE login ='" + textBox1.Text + "' and password ='" + textBox2.Text + "'";
            OleDbCommand cmd = new OleDbCommand(query, con);
            adapter.SelectCommand = cmd;
            adapter.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                for (int j = 1; j <= 2500; j++)
                    progressBar1.PerformStep();
                this.Hide();
                User usr = User.getInstance(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), dt.Rows[0][4].ToString());
                string pos = dt.Rows[0][3].ToString();
                switch (pos)
                {
                    case "admin":
                        Admin_menu a = new Admin_menu();
                        a.Show();
                        break;
                    case "manager":
                        Manager_menu m = new Manager_menu();
                        m.Show();
                        break;
                    case "client":
                        Client_menu c = new Client_menu();
                        c.Show();
                        break;
                    default:
                        break;
                }
            }
            else
                MessageBox.Show("Неправильный логин или пароль");

            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reg f = new reg();
            f.ShowDialog();
        }
    }
}
