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

namespace Database_tables
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private DataTable dtCustomers = new DataTable();
        private DataTable dtOrders = new DataTable();
        private DataTable dtOrderDetails = new DataTable();
        private DataTable dtEmployees = new DataTable();
        ConnectionForm connectionForm;
        private void RBLoad_table_Customers_CheckedChanged(object sender, EventArgs e)
        {
            sqlDACustomers.Fill(dtCustomers);
            dgvTable.DataSource = dtCustomers;
            TabPage selectedTabPage = tabControl1.SelectedTab;
            if (selectedTabPage != null)
            {
                selectedTabPage.Text = "Table Customers";
            }
        }

        private void RB_table_Orders_CheckedChanged(object sender, EventArgs e)
        {
            sqlDAOrders.Fill(dtOrders);
            dgvTable.DataSource = dtOrders;
            TabPage selectedTabPage = tabControl1.SelectedTab;
            if (selectedTabPage != null)
            {
                selectedTabPage.Text = "Table Orders";
            }
        }

        private void RBOrderDetails_CheckedChanged(object sender, EventArgs e)
        {
            sqlDAOrderDetails.Fill(dtOrderDetails);
            dgvTable.DataSource = dtOrderDetails;
            TabPage selectedTabPage = tabControl1.SelectedTab;
            if (selectedTabPage != null)
            {
                selectedTabPage.Text = "Table OrderDetails";
            }
        }

        private void RB_table_Employees_CheckedChanged(object sender, EventArgs e)
        {
            sqlDAEmployees.Fill(dtEmployees);
            dgvTable.DataSource = dtEmployees;
            TabPage selectedTabPage = tabControl1.SelectedTab;
            if (selectedTabPage != null)
            {
                selectedTabPage.Text = "Table Employees";
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                sqlDACustomers.Update(dtCustomers);
                sqlDAOrders.Update(dtOrders);
                sqlDAOrderDetails.Update(dtOrderDetails);
                sqlDAEmployees.Update(dtEmployees);

                MessageBox.Show("Data in the tables has been updated");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 515)
                {
                    MessageBox.Show("Error: Cannot insert NULL value into a column with NOT NULL constraint.");
                }
                else
                {
                    MessageBox.Show($"Database error: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int selectedIndex = dgvTable.CurrentCell.RowIndex;

            if (selectedIndex >= 0)
            {
                try
                {
                    DataRowView selectedRow = (DataRowView)dgvTable.Rows[selectedIndex].DataBoundItem;
                    selectedRow.Row.Delete();

                    if (dgvTable.DataSource == dtCustomers)
                        sqlDACustomers.Update(dtCustomers);
                    else if (dgvTable.DataSource == dtOrders)
                        sqlDAOrders.Update(dtOrders);
                    else if (dgvTable.DataSource == dtOrderDetails)
                        sqlDAOrderDetails.Update(dtOrderDetails);
                    else if (dgvTable.DataSource == dtEmployees)
                        sqlDAEmployees.Update(dtEmployees);

                    MessageBox.Show("Row deleted successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting row: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTable.DataSource is DataTable dataTable)
                {
                    DataRow newRow = dataTable.NewRow();
                    dataTable.Rows.Add(newRow);

                    if (dgvTable.DataSource == dtCustomers)
                        sqlDACustomers.Update(dtCustomers);
                    else if (dgvTable.DataSource == dtOrders)
                        sqlDAOrders.Update(dtOrders);
                    else if (dgvTable.DataSource == dtOrderDetails)
                        sqlDAOrderDetails.Update(dtOrderDetails);
                    else if (dgvTable.DataSource == dtEmployees)
                        sqlDAEmployees.Update(dtEmployees);

                    MessageBox.Show("The row has been successfully added, and the changes have been saved");
                }
                else
                {
                    MessageBox.Show("Incorrect data source. Unable to add a new row.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void connectToTheDatabase_Click(object sender, EventArgs e)
        {
            connectionForm = new ConnectionForm();
            connectionForm.ShowDialog();
        }
    }
}
