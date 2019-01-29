using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SQLConnect
{
    public partial class Form1 : Form
    {
        private MySqlConnection Connection;
        private MySqlCommand Command = new MySqlCommand();
        private MySqlDataReader DataReader;

        public Form1()
        {
            InitializeComponent();
        }

        public void CreateTable()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            try
            {
                Command = new MySqlCommand("CREATE TABLE IF NOT EXISTS TEST_TABLE (" +
                                        "IDX INT NOT NULL primary key AUTO_INCREMENT, " +
                                        "A CHAR(1) NOT NULL , " +
                                        "B CHAR(1) NOT NULL )", Connection);

                Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FAILED CREATE TABLE : " + ex.Message);
            }
            Connection.Close();
        }

        public void TEST_InsertDB()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            try
            {
                Command = new MySqlCommand("INSERT INTO TEST_TABLE" + "(A, B)" + "VALUES (@A, @B)", Connection);
                Command.Parameters.AddWithValue("@A", "A");
                Command.Parameters.AddWithValue("@B", "B");
                Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            Connection.Close();
        }

        private void CreateDatabase()
        {
            Connection.Open();
            string cmd = "CREATE DATABASE IF NOT EXISTS `TEST`;";
            Command = new MySqlCommand(cmd, Connection);
            Command.ExecuteNonQuery();
            Connection.Close();
        }

        public void ConnectDB()
        {
            Connection = null;
            try
            {
                string connectionString = "Data Source='IP'; port='PORT'; Initial Catalog=TEST; user id='ID'; password='PASSWORD'; protocol=tcp; Character Set=utf8; Connection timeout=15; Connection lifetime=0; Max Pool Size=100; Min Pool Size=10; allow user variables=true";
                Connection = new MySqlConnection(connectionString);
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        private void btnCreateTable_Click(object sender, EventArgs e)
        {
            ConnectDB();
            CreateDatabase();
            CreateTable();
            TEST_InsertDB();
        }
    }
}
