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
    public partial class AddCustomer: Form
    {
        public AddCustomer()
        {
            InitializeComponent();
        }

        private void AddCustomer_Load(object sender, EventArgs e)
        {
            // Clear all input fields when form loads
            txtName.Text = string.Empty;
            txtSurname.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtNumber.Text = string.Empty;

            // Make sure we have the customer data loaded
            try
            {
                if (Loader.CustomerTable == null)
                {
                    Loader.LoadCustomers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customer data: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtSurname.Text))
                {
                    MessageBox.Show("Name and Surname are required fields.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get the next sequence value for ID_CUSTOMER
                int newCustomerId = GetNextCustomerId();

                if (newCustomerId <= 0)
                {
                    MessageBox.Show("Could not generate a customer ID. Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create a new row in the CustomerTable
                DataRow newRow = Loader.CustomerTable.NewRow();

                // Set values for the new customer including the ID
                newRow["ID_CUSTOMER"] = newCustomerId;
                newRow["NAME"] = txtName.Text;
                newRow["SURNAME"] = txtSurname.Text;
                newRow["EMAIL"] = txtEmail.Text;
                newRow["PHONE"] = txtNumber.Text;

                // Add the row to the table
                Loader.CustomerTable.Rows.Add(newRow);

                // Save changes to the database
                Loader.CustomerAdapter.Update(Loader.CustomerTable);

                MessageBox.Show("Customer added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
