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
    public partial class Manager_menu : Form
    {
        public Manager_menu()
        {
            InitializeComponent();
            label1.Text = User.getInstance().Name;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            auto_manager_admin f = new auto_manager_admin();
            f.ShowDialog();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            selling f = new selling();
            f.ShowDialog();
        }

        private void Manager_menu_FormClosing(Object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Supplies f = new Supplies();
            f.ShowDialog();
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            workers f = new workers();
            f.ShowDialog();
        }


        private void button5_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            checkout f = new checkout();
            f.ShowDialog();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void changeUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            authorization f = new authorization();
            f.Show();
            this.Close();
        }

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clients f = new clients();
            f.ShowDialog();
        }

        private void поставщикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            providers f = new providers();
            f.ShowDialog();
        }
    }
}
