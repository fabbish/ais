using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS
{
    class User
    {

        private static User instance;
         string ID { get; set; }//свойство
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string Pos { get; private set; }
        public string Name { get; private set; }

        private User(string ID1, string Login1, string Password1, string Pos1, string Name1)//хранится информация о текущем пользователе
        {
            this.ID = ID1;
            this.Login = Login1;
            this.Password = Password1;
            this.Pos = Pos1;
            this.Name = Name1;
        }

        public static User getInstance(string ID1, string Login1, string password1, string pos1, string name1)
        {
            if (instance == null)
                instance = new User(ID1, Login1, password1, pos1, name1);
            else
            {
                instance.ID = ID1;
                instance.Login = Login1;
                instance.Password = password1;
                instance.Pos = pos1;
                instance.Name = name1;
            }
            return instance;
        }

        public static User getInstance()
        {
            return instance;
        }

    }


    public class Worker
    {
        public static List<Worker> workers;
        public int id { get; set; }
        public string fullname { get; set; }
        public string pos { get; set; }
        public string phone { get; set; }
        public DateTime date { get; set; }

    }

    public class Auto
    {
        public static List<Auto> autos;
        public int id { get; set; }
        public string makeAuto { get; set; }
        public string modelAuto { get; set; }
        public decimal priceAuto { get; set; }
        public string descriptionAuto { get; set; }
        public string photoAuto { get; set; }
    }

    public class Supply
    {
        public static List<Supply> supplies ;
        public int id { get; set; }
        public string provider { get; set; }
        public string makeAuto { get; set; }
        public string modelAuto { get; set; }
        public int countAuto { get; set; }
        public DateTime dateSupply { get; set; }
        public decimal price { get; set; }
    }

    public class Provider
    {
        public static List<Provider> providers;
        public int id { get; set; }
        public string provider { get; set; }
        public string phone { get; set; }
    }

    public class Sale
    {
        public static List<Sale> sales;
        public int id { get; set; }
        public string makeAuto { get; set; }
        public string modelAuto { get; set; }
        public decimal price { get; set; }
        public string customer { get; set; }
        public DateTime dateSale { get; set; }
        public string worker { get; set; }
    }

    public class Client
    {
        public static List<Client> clients;
        public int id { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }

    }
    public static class OleDbOperator
    {
        public const string conString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=AIS.mdb";
        public static int AddAuto()
        {
            OleDbConnection con = new OleDbConnection(conString);
            int index = 0;
            if (Auto.autos.Count != 0)
                index = Auto.autos.Count - 1;
            con.Open();
            string query = "INSERT INTO auto (make_auto, model_auto, price, description, imagelink)" + "VALUES (@make_auto, @model_auto, @price, @description, @imagelink)";
            OleDbCommand cmd = new OleDbCommand(query, con);

            cmd.Parameters.AddWithValue("@make_auto", Auto.autos[index].makeAuto);
            cmd.Parameters.AddWithValue("@model_auto", Auto.autos[index].modelAuto);
            cmd.Parameters.AddWithValue("@price", Auto.autos[index].priceAuto);
            cmd.Parameters.AddWithValue("@description", Auto.autos[index].descriptionAuto);
            cmd.Parameters.AddWithValue("@imagelink", Auto.autos[index].photoAuto);
            int changed = cmd.ExecuteNonQuery();
            con.Close();
            return changed;
        }

        public static int AddSupply()
        {
            OleDbConnection con = new OleDbConnection(conString);
            int index = 0;
            if (Supply.supplies.Count != 0)
                index = Supply.supplies.Count - 1;
            con.Open();
            string query = "INSERT INTO Supply (Provider, make_auto, model_auto, countAuto, DateSupply, price)" 
                + "VALUES (@provider, @make_auto, @model_auto, @countA, @date_supply, @price)";
            OleDbCommand cmd = new OleDbCommand(query, con);

            cmd.Parameters.AddWithValue("@provider", Supply.supplies[index].provider);
            cmd.Parameters.AddWithValue("@make_auto", Supply.supplies[index].makeAuto);
            cmd.Parameters.AddWithValue("@model_auto", Supply.supplies[index].modelAuto);
            cmd.Parameters.AddWithValue("@countA", Supply.supplies[index].countAuto);
            cmd.Parameters.AddWithValue("@date_supply", Supply.supplies[index].dateSupply);
            cmd.Parameters.AddWithValue("@price", Supply.supplies[index].price);

            int changed = cmd.ExecuteNonQuery();
            con.Close();
            return changed;
        }

        public static int AddClient()
        {
            OleDbConnection con = new OleDbConnection(conString);
            int index = 0;
            if (Client.clients.Count != 0)
                index = Client.clients.Count - 1;
            con.Open();
            string query = "INSERT INTO clients (fullname, phone)"
                + "VALUES (@fullname, @phone)";
            OleDbCommand cmd = new OleDbCommand(query, con);

            cmd.Parameters.AddWithValue("@fullname", Client.clients[index].fullname);
            cmd.Parameters.AddWithValue("@phone", Client.clients[index].phone);

            int changed = cmd.ExecuteNonQuery();
            con.Close();
            return changed;
        }

        public static DataTable GetOledb(string table)
        {
            OleDbConnection con = new OleDbConnection(conString);
            con.Open();
            OleDbCommand cmd = new OleDbCommand($"SELECT * FROM {table}", con);
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            con.Close();
            return dt;
        }

        public static int DeleteOledb(string table, int id)
        {
            OleDbConnection con = new OleDbConnection(conString);
            string query = $"DELETE FROM {table} WHERE id = " + id;
            OleDbCommand cmd = new OleDbCommand(query, con);
            con.Open();
            int deleted = cmd.ExecuteNonQuery();
            return deleted;
        }
    }
}

