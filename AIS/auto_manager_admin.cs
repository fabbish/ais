using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIS
{
    public partial class auto_manager_admin : Form
    {
        DataTable dt;
        public auto_manager_admin()
        {
            InitializeComponent();
            dt = OleDbOperator.GetOledb("auto");
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].HeaderText = "Марка авто";
            dataGridView1.Columns[2].HeaderText = "Модель авто";
            dataGridView1.Columns[3].HeaderText = "Цена";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            if (User.getInstance().Pos != "admin")
            {
                button3.Hide();
                richTextBox1.ReadOnly = true;
                pictureBox1.Click -= pictureBox1_Click;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            add_auto frm = new add_auto();
            frm.ShowDialog();
            dt = OleDbOperator.GetOledb("auto");
            dataGridView1.DataSource = dt;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = (int)dataGridView1.Rows[index].Cells[0].Value;
                
                
                if (OleDbOperator.DeleteOledb("auto", id) != 1)
                    MessageBox.Show("Ошибка выполнения запроса!");
                else
                {
                    MessageBox.Show("Машина удалена из списка!");
                    Auto.autos.RemoveAll(r => r.id == id);
                    dt = OleDbOperator.GetOledb("auto");
                    dataGridView1.DataSource = dt;
                }
            }
            else
            {
                MessageBox.Show("Выберите авто!");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = String.Format("make_auto like '{0}%'", textBox1.Text);
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
                string imagePath = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                if (imagePath.Contains(".jpg") && File.Exists(imagePath))
                    pictureBox1.Image = new Bitmap(@imagePath);
                else
                    pictureBox1.Image = new Bitmap("C:\\Users\\dim90\\Desktop\\auto\\non.jpg");
                richTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                string conString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=AIS.mdb";
                int index = dataGridView1.SelectedRows[0].Index;
                int id = (int)dataGridView1.Rows[index].Cells[0].Value;
                OleDbConnection con = new OleDbConnection(conString);
                string query = "UPDATE auto set description = @descr, imagelink = @imagelink where Id = @id";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                cmd.Parameters.AddWithValue("@descr", richTextBox1.Text);
                cmd.Parameters.AddWithValue("@imagelink", pictureBox1.ImageLocation);
                cmd.Parameters.AddWithValue("@id", id);
                if (cmd.ExecuteNonQuery() == 1)
                    MessageBox.Show("Данные обновлены");
                con.Close();

                int indx = Auto.autos.FindIndex(a => a.id == id);
                if(indx != -1)
                {
                    Auto.autos[indx].photoAuto = pictureBox1.ImageLocation;
                    Auto.autos[indx].descriptionAuto = richTextBox1.Text;
                }
                dt = OleDbOperator.GetOledb("auto");
                dataGridView1.DataSource = dt;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {
                try
                {
                    pictureBox1.Image = new Bitmap(open_dialog.FileName);
                    pictureBox1.ImageLocation = open_dialog.FileName;
                }
                catch
                {
                    DialogResult result = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
