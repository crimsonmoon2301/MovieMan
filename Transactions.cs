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
    public partial class Transactions: Form
    {
        
        public Transactions()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadTransactionData()
        {
            try
            {
                Loader.LoadCustomers();
                Loader.LoadEmployees();
                Loader.LoadMovies();         // ✅ Make sure this comes first
                Loader.LoadTransactions();   // Then load transaction data

                dataGridView1.DataSource = Loader.TransactionTable;
                dataGridView2.DataSource = Loader.TransactionDetailsTable;

                NormalizeColumnHeaders();
                ReplaceMovieIdWithComboBox();
                ReplaceCustomerAndEmployeeWithComboBoxes();

                if (dataGridView1.Columns.Contains("ID_DETAILS"))
                    dataGridView1.Columns["ID_DETAILS"].Visible = false;
                if (dataGridView1.Columns.Contains("ID_TRANSACTIONS"))
                    dataGridView1.Columns["ID_TRANSACTIONS"].Visible = false;

                if (dataGridView2.Columns.Contains("ID_DETAILS"))
                    dataGridView2.Columns["ID_DETAILS"].Visible = false;
                if (dataGridView2.Columns.Contains("ID_TRANSACTIONS"))
                    dataGridView2.Columns["ID_TRANSACTIONS"].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Load transaction Data: " + ex.Message);
            }
        }

        private void Transactions_Load(object sender, EventArgs e)
        {
            title_label.Visible = false;
            desc_label.Visible = false;

            title_label.AutoSize = true;
            desc_label.TextAlign = ContentAlignment.TopLeft;
            title_label.MaximumSize = new Size(groupBox2.ClientSize.Width - 15, 0);
            desc_label.MaximumSize = new Size(groupBox2.ClientSize.Width - 15, 0);
            desc_label.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            SetupButtonHover();

            try
            {
                LoadTransactionData();
                ReplaceMovieIdWithComboBox();
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error: " + ex.Message);
            }
        }
        private void ReplaceCustomerAndEmployeeWithComboBoxes()
        {
            // Remove existing columns if they exist
            if (dataGridView1.Columns.Contains("ID_CUSTOMER"))
                dataGridView1.Columns.Remove("ID_CUSTOMER");

            if (dataGridView1.Columns.Contains("ID_EMPLOYEE"))
                dataGridView1.Columns.Remove("ID_EMPLOYEE");

            // === Customer ComboBox Column ===
            DataGridViewComboBoxColumn customerComboBox = new DataGridViewComboBoxColumn();
            customerComboBox.Name = "ID_CUSTOMER";
            customerComboBox.HeaderText = "Customer Name";
            customerComboBox.DataSource = Loader.CustomerTable;
            customerComboBox.DisplayMember = "NAME";            // What user sees
            customerComboBox.ValueMember = "ID_CUSTOMER";       // Stored value
            customerComboBox.DataPropertyName = "ID_CUSTOMER";  // Bind to source table
            customerComboBox.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            customerComboBox.FlatStyle = FlatStyle.Flat;
            dataGridView1.Columns.Insert(0, customerComboBox);

            // === Employee ComboBox Column ===
            DataGridViewComboBoxColumn employeeComboBox = new DataGridViewComboBoxColumn();
            employeeComboBox.Name = "ID_EMPLOYEE";
            employeeComboBox.HeaderText = "Employee Name";
            employeeComboBox.DataSource = Loader.EmployeeTable;
            employeeComboBox.DisplayMember = "NAME";            // What user sees
            employeeComboBox.ValueMember = "ID_EMPLOYEE";       // Stored value
            employeeComboBox.DataPropertyName = "ID_EMPLOYEE";  // Bind to source table
            employeeComboBox.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            employeeComboBox.FlatStyle = FlatStyle.Flat;
            dataGridView1.Columns.Insert(1, employeeComboBox);
        }
        private void ReplaceMovieIdWithComboBox()
        {
            if (dataGridView2.Columns.Contains("ID_MOVIE"))
            {
                dataGridView2.Columns.Remove("ID_MOVIE");
            }

            DataGridViewComboBoxColumn movieComboBox = new DataGridViewComboBoxColumn();
            movieComboBox.Name = "ID_MOVIE";
            movieComboBox.HeaderText = "Movie Title";
            movieComboBox.DataSource = Loader.MovieTable;
            movieComboBox.DisplayMember = "NAME";
            movieComboBox.ValueMember = "ID_MOVIE";
            movieComboBox.DataPropertyName = "ID_MOVIE";
            movieComboBox.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            movieComboBox.FlatStyle = FlatStyle.Flat;

            dataGridView2.Columns.Insert(0, movieComboBox);
        }
        private void SetupButtonHover()
        {
            add_btn.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible = true;

                title_label.Text = "Add new entry";
                desc_label.Text = "Queue new transaction entry";
            };
            add_btn.MouseLeave += ClearHoverLabels;

            savech_btn.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible = true;

                title_label.Text = "Save changes";
                desc_label.Text = "Apply changes to the database.";
            };
            savech_btn.MouseLeave += ClearHoverLabels;

            

            outputgrp_box.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible = true;

                title_label.Text = "Output section";
                desc_label.Text = "You can generate a summary here.";
            };
        }

        private void ClearHoverLabels(object sender, EventArgs e)
        {
            title_label.Text = "";
            desc_label.Text = "";
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            AddTransaction f = new AddTransaction();
            f.MdiParent = EmployeeForm.ActiveForm;
            f.Show();
        }

        private void savech_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Loader.MovieAdapter.Update(Loader.MovieTable);
                Loader.MovieTypeAdapter.Update(Loader.MovieTypeTable);
                MessageBox.Show("Changes saved successfully.");

                RefreshTransactionTables();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save changes: " + ex.Message);
            }
        }
        private void RefreshTransactionTables()
        {
            try
            {
                Loader.LoadMovies(); // reload from DB
                dataGridView1.DataSource = Loader.TransactionTable;
                dataGridView2.DataSource = Loader.TransactionDetailsTable;

                if (dataGridView1.Columns.Contains("ID_TRANSACTIONS"))
                    dataGridView1.Columns["ID_TRANSACTIONS"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to refresh: " + ex.Message);
            }
        }
        private void NormalizeColumnHeaders()
        {
            Dictionary<string, string> headerMappings = new Dictionary<string, string>()
            {
                { "ID_TRANSACTION", "Transaction ID" },
                { "DATE", "Transaction Date" },
                { "CUSTOMER_NAME", "Customer" },
                { "TOTAL", "Total (EUR)" },
                { "ID_MOVIE", "Movie" },
                { "PURCHASE_DATE", "Purchase date"},
                { "QUANTITY", "Quantity" },
                { "PRICE_PER_UNIT", "Unit Price (EUR)" }
        // Add any more mappings relevant to your columns
            };

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (headerMappings.TryGetValue(column.Name, out string headerText))
                {
                    column.HeaderText = headerText;
                }
            }

            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                if (headerMappings.TryGetValue(column.Name, out string headerText))
                {
                    column.HeaderText = headerText;
                }
            }
        }
        private void outputgrp_box_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Pārliecināmies, ka visas nepieciešamās tabulas ir ielādētas
                if (Loader.TransactionTable == null ||
                    Loader.TransactionDetailsTable == null ||
                    Loader.CustomerTable == null ||
                    Loader.EmployeeTable == null)
                {
                    MessageBox.Show("Not all data is loaded. (Transactions, Customers, Employees).");
                    return;
                }

                // 1. Kopā pārdotās filmas
                int totalMoviesSold = Loader.TransactionDetailsTable.AsEnumerable()
                    .Where(row => row["QUANTITY"] != DBNull.Value)
                    .Sum(row => Convert.ToInt32(row["QUANTITY"]));

                // 2. Transakciju skaits
                int transactionCount = Loader.TransactionTable.Rows.Count;

                // 3. Klientu skaits
                int customerCount = Loader.CustomerTable.Rows.Count;

                // 4. Darbinieku skaits
                int employeeCount = Loader.EmployeeTable.Rows.Count;

                // 5. Parādām visu kā MessageBox
                string summary = $"  Summary:\n\n" +
                                 $"- Sold movie count: {totalMoviesSold}\n" +
                                 $"- Transaction count: {transactionCount}\n" +
                                 $"- Customer count: {customerCount}\n" +
                                 $"- Employee count: {employeeCount}";

                MessageBox.Show(summary, "Kopsavilkums", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kļūda kopsavilkuma aprēķinā: " + ex.Message);
            }
        }
    }
}
