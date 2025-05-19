using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursadarbs
{
    public partial class AddTransaction : Form
    {
        public AddTransaction()
        {
            InitializeComponent();
        }

        private void AddTransaction_Load(object sender, EventArgs e)
        {
            try
            {
                // Ensure data is loaded before populating ComboBoxes
                EnsureDataLoaded();

                // Populate ComboBoxes manually (same approach as your manager dropdown)
                PopulateCustomerDropdown();
                PopulateEmployeeDropdown();
                PopulateMovieDropdown();

                // Set defaults
                textBox17.Text = DateTime.Now.ToString("yyyy.MM.dd");
                textBox16.Text = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading form: {ex.Message}\n\nStack trace:\n{ex.StackTrace}");
            }
        }

        private void EnsureDataLoaded()
        {
            // Check if data tables are loaded, if not, reload them
            if (Loader.CustomerTable == null || Loader.CustomerTable.Rows.Count == 0 ||
                Loader.EmployeeTable == null || Loader.EmployeeTable.Rows.Count == 0 ||
                Loader.MovieTable == null || Loader.MovieTable.Rows.Count == 0)
            {
                // Show loading message
                this.Cursor = Cursors.WaitCursor;

                // Reload the data (you might need to adjust the method name)
                // This assumes your Loader class has a method to reload data
                Loader.LoadCustomers(); // or whatever method name you use
                Loader.LoadEmployees();
                Loader.LoadMovies();
                Loader.LoadTransactions();
                this.Cursor = Cursors.Default;
            }
        }

        private void PopulateCustomerDropdown()
        {
            comboBox3.Items.Clear();

            // Retry loading if table is empty
            int retryCount = 0;
            while ((Loader.CustomerTable == null || Loader.CustomerTable.Rows.Count == 0) && retryCount < 3)
            {
                System.Threading.Thread.Sleep(100);
                retryCount++;
            }

            if (Loader.CustomerTable != null && Loader.CustomerTable.Rows.Count > 0)
            {
                foreach (DataRow row in Loader.CustomerTable.Rows)
                {
                    string customerName = $"{row["NAME"]} {row["SURNAME"]}";
                    comboBox3.Items.Add(new { Id = row["ID_CUSTOMER"], Name = customerName });
                }
                comboBox3.DisplayMember = "Name";
                comboBox3.ValueMember = "Id";
            }
            else
            {
                comboBox3.Items.Add(new { Id = DBNull.Value, Name = "No customers available" });
                comboBox3.DisplayMember = "Name";
                comboBox3.ValueMember = "Id";
            }
        }

        private void PopulateEmployeeDropdown()
        {
            comboBox4.Items.Clear();

            // Retry loading if table is empty
            int retryCount = 0;
            while ((Loader.EmployeeTable == null || Loader.EmployeeTable.Rows.Count == 0) && retryCount < 3)
            {
                System.Threading.Thread.Sleep(100);
                retryCount++;
            }

            if (Loader.EmployeeTable != null && Loader.EmployeeTable.Rows.Count > 0)
            {
                foreach (DataRow row in Loader.EmployeeTable.Rows)
                {
                    // Same format as your manager dropdown
                    string fullName = $"{row["NAME"]} {row["SURNAME"]}";
                    comboBox4.Items.Add(new { Id = row["ID_EMPLOYEE"], Name = fullName });
                }
                comboBox4.DisplayMember = "Name";
                comboBox4.ValueMember = "Id";
            }
            else
            {
                comboBox4.Items.Add(new { Id = DBNull.Value, Name = "No employees available" });
                comboBox4.DisplayMember = "Name";
                comboBox4.ValueMember = "Id";
            }
        }

        private void PopulateMovieDropdown()
        {
            comboBox5.Items.Clear();

            // Retry loading if table is empty
            int retryCount = 0;
            while ((Loader.MovieTable == null || Loader.MovieTable.Rows.Count == 0) && retryCount < 3)
            {
                System.Threading.Thread.Sleep(100);
                retryCount++;
            }

            if (Loader.MovieTable != null && Loader.MovieTable.Rows.Count > 0)
            {
                foreach (DataRow row in Loader.MovieTable.Rows)
                {
                    // Try different possible column names for movie title
                    string movieTitle = "";
                    if (row.Table.Columns.Contains("NAME"))
                        movieTitle = row["NAME"].ToString();
                    else if (row.Table.Columns.Contains("TITLE"))
                        movieTitle = row["TITLE"].ToString();
                    else if (row.Table.Columns.Contains("MOVIE_NAME"))
                        movieTitle = row["MOVIE_NAME"].ToString();
                    else
                        movieTitle = "Unknown Movie";

                    comboBox5.Items.Add(new { Id = row["ID_MOVIE"], Name = movieTitle });
                }
                comboBox5.DisplayMember = "Name";
                comboBox5.ValueMember = "Id";
            }
            else
            {
                comboBox5.Items.Add(new { Id = DBNull.Value, Name = "No movies available" });
                comboBox5.DisplayMember = "Name";
                comboBox5.ValueMember = "Id";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private int GetNextTransactionId()
        {
            int nextId = 0;
            try
            {
                using (OracleConnection conn = new OracleConnection(Loader.connectionString))
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand("SELECT seq_id_transactions.NEXTVAL FROM DUAL", conn))
                    {
                        nextId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting next transaction ID: " + ex.Message);
            }
            return nextId;
        }
        private int GetNextDetailsId()
        {
            int nextId = 0;
            try
            {
                using (OracleConnection conn = new OracleConnection(Loader.connectionString))
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand("SELECT seq_id_transaction_details.NEXTVAL FROM DUAL", conn))
                    {
                        nextId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting next transaction details ID: " + ex.Message);
            }
            return nextId;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Validate selections first
                if (comboBox3.SelectedItem == null)
                {
                    MessageBox.Show("Please select a customer.");
                    return;
                }
                if (comboBox4.SelectedItem == null)
                {
                    MessageBox.Show("Please select an employee.");
                    return;
                }
                if (comboBox5.SelectedItem == null)
                {
                    MessageBox.Show("Please select a movie.");
                    return;
                }

                // Get the IDs from the anonymous objects
                var customerItem = (dynamic)comboBox3.SelectedItem;
                var employeeItem = (dynamic)comboBox4.SelectedItem;
                var movieItem = (dynamic)comboBox5.SelectedItem;

                var customerId = customerItem.Id.ToString();
                var employeeId = employeeItem.Id.ToString();
                var movieId = movieItem.Id.ToString();

                DateTime purchaseDate;
                if (!DateTime.TryParse(textBox17.Text, out purchaseDate))
                {
                    MessageBox.Show("Invalid date format. Please use yyyy.MM.dd");
                    return;
                }

                int quantity;
                if (!int.TryParse(textBox16.Text, out quantity) || quantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity (positive number).");
                    return;
                }

                // Get next value from sequence before inserting
                int transactionId = GetNextTransactionId(); // This must SELECT from the Oracle sequence

                // Add to TRANSACTIONS table
                DataRow transactionRow = Loader.TransactionTable.NewRow();
                transactionRow["ID_TRANSACTIONS"] = transactionId; // Add this line
                transactionRow["ID_CUSTOMER"] = customerId;
                transactionRow["ID_EMPLOYEE"] = employeeId;
                transactionRow["PURCHASE_DATE"] = purchaseDate;
                Loader.TransactionTable.Rows.Add(transactionRow);

                // Save TRANSACTION to DB
                var builder = new OracleCommandBuilder(Loader.TransactionAdapter);
                Loader.TransactionAdapter.Update(Loader.TransactionTable);


                // Add to TRANSACT_DETAILS table
                DataRow detailsRow = Loader.TransactionDetailsTable.NewRow();
                detailsRow["ID_DETAILS"] = GetNextDetailsId(); // <- add this line
                detailsRow["ID_TRANSACTIONS"] = transactionId;
                detailsRow["ID_MOVIE"] = movieId;
                detailsRow["QUANTITY"] = quantity;
                Loader.TransactionDetailsTable.Rows.Add(detailsRow);

                var builder2 = new OracleCommandBuilder(Loader.TransactionDetailsAdapter);
                Loader.TransactionDetailsAdapter.Update(Loader.TransactionDetailsTable);
                MessageBox.Show("Transaction successfully added.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n\nStack trace:\n{ex.StackTrace}");
            }
        }
    }
}
