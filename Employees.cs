using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursadarbs
{
    public partial class Employees: Form
    {
        public Employees()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void LoadEmployeeData()
        {
            try
            {
                Loader.LoadEmployees();
                dataGridView1.DataSource = Loader.EmployeeTable.DefaultView;

                if (dataGridView1.Columns.Contains("ID_EMPLOYEE"))
                    dataGridView1.Columns["ID_EMPLOYEE"].Visible = false;

                NormalizeColumnHeaders();

                ReplaceManagerIdWithDropdown();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ReplaceManagerIdWithDropdown()
        {
            if (!Loader.EmployeeTable.Columns.Contains("MANAGER_ID"))
                return;

            // Get unique non-null MANAGER_IDs as objects
            var uniqueManagers = Loader.EmployeeTable
                .AsEnumerable()
                .Select(row => row["MANAGER_ID"])
                .Where(val => val != DBNull.Value)
                .Distinct()
                .ToList();

            if (uniqueManagers.Count > 20)
                return;

            // Build a list of ID_EMPLOYEE + Full Name
            DataTable managerList = Loader.EmployeeTable.DefaultView.ToTable(true, "ID_EMPLOYEE", "NAME", "SURNAME");
            managerList.Columns["ID_EMPLOYEE"].ColumnName = "ManagerID";

            managerList.Columns.Add("FullName", typeof(string));
            foreach (DataRow row in managerList.Rows)
            {
                string name = row["NAME"] != DBNull.Value ? row["NAME"].ToString() : "";
                string surname = row["SURNAME"] != DBNull.Value ? row["SURNAME"].ToString() : "";
                row["FullName"] = $"{name} {surname}".Trim();
            }

            // Remove MANAGER_ID column from DataGridView (not the DataTable!)
            if (dataGridView1.Columns.Contains("MANAGER_ID"))
                dataGridView1.Columns.Remove("MANAGER_ID");

            // Add ComboBoxColumn
            DataGridViewComboBoxColumn comboColumn = new DataGridViewComboBoxColumn
            {
                Name = "MANAGER_ID",
                HeaderText = "Managed By",
                DataPropertyName = "MANAGER_ID",
                DataSource = managerList,
                ValueMember = "ManagerID",
                DisplayMember = "FullName",
                DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
                FlatStyle = FlatStyle.Flat
            };

            dataGridView1.Columns.Add(comboColumn);
        }
        private void Employees_Load(object sender, EventArgs e)
        {
            
            title_label.Visible = false;
            desc_label.Visible = false;

            title_label.AutoSize = true;
            desc_label.TextAlign = ContentAlignment.TopLeft;
            title_label.MaximumSize = new Size(groupBox1.ClientSize.Width - 15, 0);
            desc_label.MaximumSize = new Size(groupBox1.ClientSize.Width - 15, 0);
            desc_label.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            SetupButtonHover();
            LoadEmployeeData();
        }

        private void SetupButtonHover()
        {
            add_btn.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible = true;

                title_label.Text = "Add new Employee";
                desc_label.Text = "Queue new employee to the database.";
            };
            add_btn.MouseLeave += ClearHoverLabels;

            struct_grpbox.MouseEnter += (s, e) =>
            {
                title_label.Visible= true;
                desc_label.Visible= true;

                title_label.Text = "Change layout type";
                desc_label.Text = "You can change the employee layout in this section.";
            };
            struct_grpbox.MouseLeave += ClearHoverLabels;

            filtr_txtbox.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible= true;

                title_label.Text = "Filter function";
                desc_label.Text = "You can look up custom data by need.";
            };
            filtr_txtbox.MouseLeave += ClearHoverLabels;
            

            savech_btn.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible = true;

                title_label.Text = "Save changes";
                desc_label.Text = "Apply changes to the database.";
            };
            savech_btn.MouseLeave += ClearHoverLabels;

        }

        private void ClearHoverLabels(object sender, EventArgs e)
        {
            title_label.Text = "";
            desc_label.Text = "";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Loader.EmployeeTable == null || Loader.EmployeeTable.Columns.Count == 0)
                return;

            string searchText = filtr_txtbox.Text.Trim().Replace("'", "''");

            if (string.IsNullOrEmpty(searchText))
            {
                Loader.EmployeeTable.DefaultView.RowFilter = string.Empty;
                return;
            }

            List<string> filterConditions = new List<string>();

            foreach (DataColumn column in Loader.EmployeeTable.Columns)
            {
                if (column.DataType == typeof(string))
                {
                    // Wrap column name in brackets to prevent issues with special characters
                    filterConditions.Add($"[{column.ColumnName}] LIKE '%{searchText}%'");
                }
            }

            Loader.EmployeeTable.DefaultView.RowFilter = string.Join(" OR ", filterConditions);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            AddEmployee f = new AddEmployee();
            f.MdiParent = EmployeeForm.ActiveForm;
            f.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Loader.Adapter.Update(Loader.EmployeeTable);
                MessageBox.Show("Changes saved successfully.");

                RefreshEmployeeTable(); // see step 2
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save changes: " + ex.Message);
            }
        }
        private void RefreshEmployeeTable()
        {
            try
            {
                Loader.LoadEmployees(); // reload from DB
                dataGridView1.DataSource = Loader.EmployeeTable;

                if (dataGridView1.Columns.Contains("ID_EMPLOYEE"))
                    dataGridView1.Columns["ID_EMPLOYEE"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to refresh: " + ex.Message);
            }
        }

        private void title_label_Click(object sender, EventArgs e)
        {

        }
        private void NormalizeColumnHeaders()
        {
            Dictionary<string, string> headerMappings = new Dictionary<string, string>()
             {
                { "NAME", "Name" },
                { "SURNAME", "Surname" },
                { "EMAIL", "E-mail" },
                { "PHONE", "Contact" },
                { "ADDRESS", "Address" },
                { "CITY", "City" },
                { "ZIP", "ZIP Code" },
                { "COUNTRY", "Country" },
                { "POSITION", "Position" },
                { "SALARY", "Salary" },
                { "HIRE_DATE", "Hire Date" },
                { "MANAGER_ID", "Managed By" },
                { "ID_EMPLOYEE", "Employee ID" },
        // Add more mappings as needed
            };

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (headerMappings.ContainsKey(column.Name))
                {
                    column.HeaderText = headerMappings[column.Name];
                }
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            var form = new EmployeeHiarchy();
            form.MdiParent = EmployeeForm.ActiveForm;
            form.Show();
        }
    }
}
