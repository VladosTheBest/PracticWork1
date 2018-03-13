using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateTable
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source = CR5-09\SQLEXPRESS;Initial Catalog=VLADOS_BASE; Integrated Security = true;");
        string table = "";
        public Form1()
        {
            InitializeComponent();
            listBox1.ContextMenuStrip = getMenu();
        }
        private ContextMenuStrip getMenu()
        {
            ContextMenuStrip cms = new ContextMenuStrip();
            cms.Items.Add("Delete");
            cms.Items[0].Click += Delete;
            return cms;

        }

        private void Delete(object sender, EventArgs e)
        {
            string query = $"delete from  {table} where id={(listBox1.SelectedItem as string)[0]};";
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
            btSelect_Click(sender, e);
        }

        private void btCreate_Click(object sender, EventArgs e)
        {
            table = tb_tableName.Text;
            string query = $"create table {tb_tableName.Text}(id int primary key identity," +
                 "firstName varchar(25),secondName varchar(25));";
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();

        }

        private void btInsert_Click(object sender, EventArgs e)
        {
            string query = $"insert into  {table} values('{tb_FirstName.Text}','{tbLastName.Text}')";
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
            btSelect_Click(sender, e);
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string query = $"select * from {table}";
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[0] + " " + reader[1] + " " + reader[2] + "\n");
            }
            conn.Close();

        }
    }
}
