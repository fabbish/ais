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
    public partial class client_buys : Form
    {
        public client_buys(string name)
        {
            InitializeComponent();
            string listItem;
            foreach (Sale sale in Sale.sales)
            {
                if (sale.customer == name)
                {
                    listItem = "Автомобиль: " + sale.makeAuto.ToString() + " " + sale.modelAuto.ToString()
                        + "  Стоимость: " + sale.price.ToString() + " руб.  Дата покупки: " + sale.dateSale.ToShortDateString();
                    listView1.Items.Add(listItem);
                }
                    
            }
            label1.Text = name;
        }
    }
}
