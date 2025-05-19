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
    public partial class quickadd: Form
    {
        public quickadd()
        {
            InitializeComponent();
        }
        private void LoadFormats()
        {
            try
            {
                // Load unique formats into combo box from existing data
                if (Loader.MovieTypeTable != null)
                {
                    var formats = Loader.MovieTypeTable.AsEnumerable()
                        .Select(row => row.Field<string>("FORMAT"))
                        .Distinct()
                        .Where(f => !string.IsNullOrEmpty(f))
                        .OrderBy(f => f)
                        .ToList();

                    formatMov_cmb.DataSource = formats;
                    formatMov_cmb.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading formats: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            quickAddMovie();
            quickAddEmployee();
            quickAddCustomer();
            quickAddTransaction();
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void quickadd_Load(object sender, EventArgs e)
        {
            txtnameCustomer.Text = string.Empty;
            txtsurnameCustomer.Text =  string.Empty;
            txtemailCustom.Text = string.Empty;
            txtPhoneCustom.Text = string.Empty;

            txtnameEmp.Text = string.Empty;
            txtsurnameEmp.Text = string.Empty;
            txtposEmp.Text = string.Empty;
            txtsalEmp.Text = string.Empty;
            txthireEmp.Text = string.Empty;
            txtTitlemov.Text = string.Empty;
            txtdirectMov.Text = string.Empty;
            txtpriceMov.Text = string.Empty;
            txtoriginMov.Text = string.Empty;

            txtReleaseMov.Text = string.Empty;
            txtgenreMov.Text = string.Empty;
            txtdurMov.Text = string.Empty;
            txtquantityTrans.Text = string.Empty;
            txtdateTrans.Text = string.Empty;
            
            // Load data and bind to combo boxes
            try
            {
                // Load employees if not loaded
                if (Loader.EmployeeTable == null || Loader.EmployeeTable.Rows.Count == 0)
                    Loader.LoadEmployees();

                managedemp_cmb.DataSource = Loader.EmployeeTable.Copy();
                managedemp_cmb.DisplayMember = "NAME";
                managedemp_cmb.ValueMember = "MANAGER_ID";

                emptrans_cmb.DataSource = Loader.EmployeeTable.Copy();
                emptrans_cmb.DisplayMember = "NAME";
                emptrans_cmb.ValueMember = "ID_EMPLOYEE";

                // Load customers
                if (Loader.CustomerTable == null || Loader.CustomerTable.Rows.Count == 0)
                    Loader.LoadCustomers();

                customertrans_cmb.DataSource = Loader.CustomerTable.Copy();
                customertrans_cmb.DisplayMember = "NAME";
                customertrans_cmb.ValueMember = "ID_CUSTOMER";

                // Load movies
                if (Loader.MovieTable == null || Loader.MovieTable.Rows.Count == 0)
                    Loader.LoadMovies();

                movietrans_cmb.DataSource = Loader.MovieTable.Copy();
                LoadFormats();
                movietrans_cmb.DisplayMember = "NAME";
                movietrans_cmb.ValueMember = "ID_MOVIE";

                if (Loader.TransactionTable == null || Loader.TransactionTable.Rows.Count == 0)
                    Loader.LoadTransactions();

                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }
        
        private int quickAddCustomer()
        {
            try
            {
                if (radioButton7.Checked)
                    return -1;

                if (string.IsNullOrWhiteSpace(txtnameCustomer.Text) ||
                    string.IsNullOrWhiteSpace(txtsurnameCustomer.Text) ||
                    string.IsNullOrWhiteSpace(txtemailCustom.Text) ||
                    string.IsNullOrWhiteSpace(txtPhoneCustom.Text))
                {
                    MessageBox.Show("Please fill in all customer fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                int customerId = GetNextCustomerId();
                if (customerId == 0) return -1;

                DataRow newCustomer = Loader.CustomerTable.NewRow();
                newCustomer["ID_CUSTOMER"] = customerId;
                newCustomer["NAME"] = txtnameCustomer.Text.Trim();
                newCustomer["SURNAME"] = txtsurnameCustomer.Text.Trim();
                newCustomer["EMAIL"] = txtemailCustom.Text.Trim();
                newCustomer["PHONE"] = txtPhoneCustom.Text.Trim();

                Loader.CustomerTable.Rows.Add(newCustomer);
                Loader.CustomerAdapter.Update(Loader.CustomerTable);
                Loader.LoadCustomers(); // Refresh combo

                return customerId;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer: " + ex.Message);
                return -1;
            }
        }
        private int quickAddEmployee()
        {
            try
            {
                if (radioButton8.Checked)
                    return -1;

                if (string.IsNullOrWhiteSpace(txtnameEmp.Text) ||
                    string.IsNullOrWhiteSpace(txtsurnameEmp.Text) ||
                    string.IsNullOrWhiteSpace(txtposEmp.Text) ||
                    string.IsNullOrWhiteSpace(txthireEmp.Text) ||
                    string.IsNullOrWhiteSpace(txtsalEmp.Text))
                {
                    MessageBox.Show("Please fill in all employee fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                int empId = GetNextEmployeeId();
                if (empId == 0) return -1;

                DataRow newEmp = Loader.EmployeeTable.NewRow();
                newEmp["ID_EMPLOYEE"] = empId;
                newEmp["NAME"] = txtnameEmp.Text.Trim();
                newEmp["SURNAME"] = txtsurnameEmp.Text.Trim();
                newEmp["POSITION"] = txtposEmp.Text.Trim();
                newEmp["HIRE_DATE"] = DateTime.Parse(txthireEmp.Text.Trim());
                newEmp["SALARY"] = decimal.Parse(txtsalEmp.Text.Trim());

                if (managedemp_cmb.SelectedItem != null)
                    newEmp["MANAGER_ID"] = Convert.ToInt32(managedemp_cmb.SelectedValue);

                Loader.EmployeeTable.Rows.Add(newEmp);
                Loader.Adapter.Update(Loader.EmployeeTable);
                Loader.LoadEmployees(); // Refresh combo

                return empId;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding employee: " + ex.Message);
                return -1;
            }
        }
        private int quickAddMovie()
        {
            try
            {
                if (radioButton9.Checked)
                    return -1;

                if (string.IsNullOrWhiteSpace(txtTitlemov.Text) ||
                    string.IsNullOrWhiteSpace(txtdirectMov.Text) ||
                    string.IsNullOrWhiteSpace(txtpriceMov.Text) ||
                    string.IsNullOrWhiteSpace(txtoriginMov.Text) ||
                    string.IsNullOrWhiteSpace(txtReleaseMov.Text) ||
                    string.IsNullOrWhiteSpace(txtgenreMov.Text) ||
                    string.IsNullOrWhiteSpace(txtdurMov.Text) ||
                    formatMov_cmb.SelectedItem == null)
                {
                    MessageBox.Show("Please fill in all movie fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                int movieId = GetNextMovieId();
                int movieTypeId = GetNextMovieTypeId();
                if (movieId == 0 || movieTypeId == 0) return -1;

                DataRow newMovie = Loader.MovieTable.NewRow();
                newMovie["ID_MOVIE"] = movieId;
                newMovie["NAME"] = txtTitlemov.Text.Trim();
                newMovie["DIRECTOR"] = txtdirectMov.Text.Trim();
                newMovie["PRICE"] = decimal.Parse(txtpriceMov.Text.Trim());
                newMovie["ORIGIN_OF_CREATION"] = txtoriginMov.Text.Trim();

                Loader.MovieTable.Rows.Add(newMovie);

                DataRow newType = Loader.MovieTypeTable.NewRow();
                newType["ID_MOVIETYPE"] = movieTypeId;
                newType["ID_MOVIE"] = movieId;
                newType["RELEASE_YEAR"] = int.Parse(txtReleaseMov.Text.Trim());
                newType["GENRE"] = txtgenreMov.Text.Trim();
                newType["DURATION"] = int.Parse(txtdurMov.Text.Trim());
                newType["FORMAT"] = formatMov_cmb.SelectedItem.ToString();

                Loader.MovieTypeTable.Rows.Add(newType);

                Loader.MovieAdapter.Update(Loader.MovieTable);
                Loader.MovieTypeAdapter.Update(Loader.MovieTypeTable);
                Loader.LoadMovies(); // Refresh combo

                return movieId;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding movie: " + ex.Message);
                return -1;
            }
        }
        private void quickAddTransaction()
        {
            try
            {
                if (radioButton10.Checked)
                    return;

                if (customertrans_cmb.SelectedItem == null)
                {
                    MessageBox.Show("Please select a customer.");
                    return;
                }
                if (emptrans_cmb.SelectedItem == null)
                {
                    MessageBox.Show("Please select an employee.");
                    return;
                }
                if (movietrans_cmb.SelectedItem == null)
                {
                    MessageBox.Show("Please select a movie.");
                    return;
                }

                // Access values from DataRowView instead of dynamic
                var customerId = ((DataRowView)customertrans_cmb.SelectedItem)["ID_CUSTOMER"].ToString();
                var employeeId = ((DataRowView)emptrans_cmb.SelectedItem)["ID_EMPLOYEE"].ToString();
                var movieId = ((DataRowView)movietrans_cmb.SelectedItem)["ID_MOVIE"].ToString();

                DateTime purchaseDate;
                if (!DateTime.TryParse(txtdateTrans.Text.Trim(), out purchaseDate))
                {
                    MessageBox.Show("Invalid date format. Please use yyyy-MM-dd.");
                    return;
                }

                int quantity;
                if (!int.TryParse(txtquantityTrans.Text.Trim(), out quantity) || quantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity (positive number).");
                    return;
                }

                int transactionId = GetNextTransactionId();
                int detailId = GetNextDetailsId();
                var customerRow = customertrans_cmb.SelectedItem as DataRowView;
                var employeeRow = emptrans_cmb.SelectedItem as DataRowView;
                var movieRow = movietrans_cmb.SelectedItem as DataRowView;

                if (customerRow == null || employeeRow == null || movieRow == null)
                {
                    MessageBox.Show("One or more selections are invalid.");
                    return;
                }

                if (!customerRow.Row.Table.Columns.Contains("ID_CUSTOMER") ||
                    !employeeRow.Row.Table.Columns.Contains("ID_EMPLOYEE") ||
                    !movieRow.Row.Table.Columns.Contains("ID_MOVIE"))
                {
                    MessageBox.Show("Expected ID columns not found in the selected items.");
                    return;
                }

               
                DataRow transactionRow = Loader.TransactionTable.NewRow();
                transactionRow["ID_TRANSACTIONS"] = transactionId;
                transactionRow["ID_CUSTOMER"] = customerId;
                transactionRow["ID_EMPLOYEE"] = employeeId;
                transactionRow["PURCHASE_DATE"] = purchaseDate;
                Loader.TransactionTable.Rows.Add(transactionRow);

                var builder = new OracleCommandBuilder(Loader.TransactionAdapter);
                Loader.TransactionAdapter.Update(Loader.TransactionTable);

                DataRow detailsRow = Loader.TransactionDetailsTable.NewRow();
                detailsRow["ID_DETAILS"] = detailId;
                detailsRow["ID_TRANSACTIONS"] = transactionId;
                detailsRow["ID_MOVIE"] = movieId;
                detailsRow["QUANTITY"] = quantity;
                Loader.TransactionDetailsTable.Rows.Add(detailsRow);

                var builder2 = new OracleCommandBuilder(Loader.TransactionDetailsAdapter);
                Loader.TransactionDetailsAdapter.Update(Loader.TransactionDetailsTable);

                MessageBox.Show("Transaction successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n\nStack trace:\n{ex.StackTrace}");
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
            {
                txtnameCustomer.Enabled = false;
                txtsurnameCustomer.Enabled = false;
                txtemailCustom.Enabled = false;
                txtPhoneCustom.Enabled = false;
            }
            else
            {
                txtnameCustomer.Enabled = true;
                txtsurnameCustomer.Enabled = true;
                txtemailCustom.Enabled = true;
                txtPhoneCustom.Enabled = true;
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                txtnameEmp.Enabled = false;
                txtsurnameEmp.Enabled = false;
                txtposEmp.Enabled = false;
                txtsalEmp.Enabled = false;
                txthireEmp.Enabled = false;
                managedemp_cmb.Enabled = false;
            }
            else
            {
                txtnameEmp.Enabled = true;
                txtsurnameEmp.Enabled = true;
                txtposEmp.Enabled = true;
                txtsalEmp.Enabled = true;
                txthireEmp.Enabled = true;
                managedemp_cmb.Enabled = true;
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                txtTitlemov.Enabled = false;
                txtdirectMov.Enabled = false;
                txtdurMov.Enabled = false;  
                txtgenreMov.Enabled = false;
                txtdurMov.Enabled = false;
                txtoriginMov.Enabled = false;
                txtReleaseMov.Enabled = false;
                txtpriceMov.Enabled = false;
                formatMov_cmb.Enabled = false;
            }
            else
            {
                txtTitlemov.Enabled = true;
                txtdirectMov.Enabled = true;
                txtdurMov.Enabled = true;
                txtgenreMov.Enabled = true;
                txtdurMov.Enabled = true;
                txtoriginMov.Enabled = true;
                txtReleaseMov.Enabled = true;
                txtpriceMov.Enabled = true;
                formatMov_cmb.Enabled = true;
            }
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked)
            {
                customertrans_cmb.Enabled = false;
                emptrans_cmb.Enabled = false;
                movietrans_cmb.Enabled = false;
                txtquantityTrans.Enabled = false;
                txtdateTrans.Enabled = false;
            }
            else
            {
                customertrans_cmb.Enabled = true;
                emptrans_cmb.Enabled = true;
                movietrans_cmb.Enabled = true;
                txtquantityTrans.Enabled = true;
                txtdateTrans.Enabled = true;
            }
        }
        private int GetNextCustomerId()
        {
            int nextId = 0;
            try
            {
                using (OracleConnection conn = new OracleConnection(Loader.connectionString))
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand("SELECT seq_id_customer.NEXTVAL FROM DUAL", conn))
                    {
                        // Get the next sequence value
                        nextId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting next customer ID: " + ex.Message);
            }
            return nextId;
        }

        private int GetNextEmployeeId()
        {
            int nextId = 0;
            try
            {
                using (OracleConnection conn = new OracleConnection(Loader.connectionString))
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand("SELECT seq_id_employee.NEXTVAL FROM DUAL", conn))
                    {
                        // Get the next sequence value
                        nextId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting next employee ID: " + ex.Message);
            }
            return nextId;
        }

        private int GetNextMovieId()
        {
            int nextId = 0;
            try
            {
                using (OracleConnection conn = new OracleConnection(Loader.connectionString))
                {
                    conn.Open();
                    // Fixed: Use movie sequence instead of employee sequence
                    using (OracleCommand cmd = new OracleCommand("SELECT seq_id_movie.NEXTVAL FROM DUAL", conn))
                    {
                        // Get the next sequence value
                        nextId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting next movie ID: " + ex.Message);
            }
            return nextId;
        }

        private int GetNextMovieTypeId()
        {
            int nextId = 0;
            try
            {
                using (OracleConnection conn = new OracleConnection(Loader.connectionString))
                {
                    conn.Open();
                    // Get next sequence value for movie type
                    using (OracleCommand cmd = new OracleCommand("SELECT seq_id_movietype.NEXTVAL FROM DUAL", conn))
                    {
                        nextId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting next movie type ID: " + ex.Message);
            }
            return nextId;
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
    }
}
