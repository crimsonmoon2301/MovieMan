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
using System.Xml.Linq;

namespace Kursadarbs
{
    public partial class AddEmployee: Form
    {
        public AddEmployee()
        {
            InitializeComponent();
        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {
            // Clear all input fields when form loads
            txtName.Text = string.Empty;
            txtSurname.Text = string.Empty;
            txtPosition.Text = string.Empty;
            txtSalary.Text = string.Empty;

            txtHireDate.Text = string.Empty;

            try
            {
                if (Loader.EmployeeTable == null)
                {
                    Loader.LoadEmployees();
                }

                // Populate the manager dropdown
                PopulateManagerDropdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employee data: " + ex.Message);
            }
        }
        private void PopulateManagerDropdown()
        {
            // Add a "None" option for employees without a manager
            cmbManager.Items.Add(new { Id = DBNull.Value, Name = "None" });

            // Add each employee as a potential manager
            foreach (DataRow row in Loader.EmployeeTable.Rows)
            {
                string fullName = $"{row["NAME"]} {row["SURNAME"]} ({row["POSITION"]})";
                cmbManager.Items.Add(new { Id = row["ID_EMPLOYEE"], Name = fullName });
            }

            // Set display and value members
            cmbManager.DisplayMember = "Name";
            cmbManager.ValueMember = "Id";

            // Select "None" by default
            cmbManager.SelectedIndex = 0;
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

                // Get the next sequence value for ID_EMPLOYEE
                int newEmployeeId = GetNextEmployeeId();

                if (newEmployeeId <= 0)
                {
                    MessageBox.Show("Could not generate an employee ID. Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate salary
                decimal salary;
                if (!decimal.TryParse(txtSalary.Text, out salary) || salary < 0)
                {
                    MessageBox.Show("Please enter a valid non-negative salary.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create a new row in the EmployeeTable
                DataRow newRow = Loader.EmployeeTable.NewRow();

                // Set values for the new employee including the ID
                newRow["ID_EMPLOYEE"] = newEmployeeId;
                newRow["NAME"] = txtName.Text;
                newRow["SURNAME"] = txtSurname.Text;
                newRow["POSITION"] = txtPosition.Text;

                // Format date as DD.MM.YYYY for Oracle
                DateTime hireDate;
                if (!DateTime.TryParse(txtHireDate.Text, out hireDate))
                {
                    MessageBox.Show("Please enter a valid hire date (e.g., DD.MM.YYYY).", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                newRow["HIRE_DATE"] = hireDate;

                newRow["SALARY"] = salary;

                // Handle manager ID (can be null)
                dynamic selectedManager = cmbManager.SelectedItem;
                if (selectedManager != null && !DBNull.Value.Equals(selectedManager.Id))
                {
                    newRow["MANAGER_ID"] = selectedManager.Id;
                }
                else
                {
                    newRow["MANAGER_ID"] = DBNull.Value;
                }

                // Add the row to the table
                Loader.EmployeeTable.Rows.Add(newRow);
                
                Loader.Adapter.Update(Loader.EmployeeTable);
                // Save changes to the database
                Loader.SaveEmployees();

                MessageBox.Show("Employee added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding employee: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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



        // Add a numeric validation to the salary text box
        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow digits, decimal point, and control keys
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Allow only one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
