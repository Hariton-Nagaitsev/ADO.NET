using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Database_tables
{
    public partial class ConnectionForm : Form
    {
        private SqlConnection connection;
        private string connectionString;
        public ConnectionForm()
        {
            InitializeComponent();
        }
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string login = textBoxUserName.Text;
                string password = textBoxPassword.Text;

                connectionString = ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;
                connectionString = connectionString.Replace("Integrated Security=False;", $"User ID={login};Password={password};Integrated Security=False;");

                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    MessageBox.Show("Connection established.");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Error opening connection: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message);
            }
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
                MessageBox.Show("Connection closed.");
            }
            else
            {
                MessageBox.Show("Connection is already closed or was never established.");
            }
        }
    }
}
