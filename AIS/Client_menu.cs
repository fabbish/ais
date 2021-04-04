using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIS
{
    public partial class Client_menu : Form
    {
        public Client_menu()
        {
            InitializeComponent();
            label1.Text = User.getInstance().Name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            auto_client f = new auto_client();
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client_buys f = new client_buys(User.getInstance().Name);
            f.ShowDialog();
        }

      
        private void Client_menu_FormClosing(Object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void changeUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            authorization f = new authorization();
            f.Show();
            this.Close();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
