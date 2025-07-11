﻿using Oracle.ManagedDataAccess.Client;
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
    public partial class Customers: Form
    {
        public Customers()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void LoadCustomerData()
        {
            try
            {
                Loader.LoadCustomers();
                dataGridView1.DataSource = Loader.CustomerTable.DefaultView;

                // Hide any columns you want
                if (dataGridView1.Columns.Contains("ID_CUSTOMER"))
                    dataGridView1.Columns["ID_CUSTOMER"].Visible = false;

                NormalizeColumnHeaders();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        private void Customers_Load(object sender, EventArgs e)
        {
            title_label.Visible = false;
            desc_label.Visible = false;

            title_label.AutoSize = true;
            desc_label.TextAlign = ContentAlignment.TopLeft;
            title_label.MaximumSize = new Size(groupBox1.ClientSize.Width - 15, 0);
            desc_label.MaximumSize = new Size(groupBox1.ClientSize.Width - 15, 0);
            desc_label.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            SetupButtonHover();

            LoadCustomerData();
        }

        private void SetupButtonHover()
        {
            add_btn.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible = true;

                title_label.Text = "Add new Customer";
                desc_label.Text = "Queue new Customer to the database.";
            };
            add_btn.MouseLeave += ClearHoverLabels;


            filtr_txtbox.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible = true;

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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void refrsh_btn_Click(object sender, EventArgs e)
        {

        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            // First, ensure customers are loaded
            if (Loader.CustomerTable == null)
            {
                Loader.LoadCustomers();
            }
            AddCustomer f = new AddCustomer();
            f.MdiParent = EmployeeForm.ActiveForm;
            f.Show();
            
        }

       
        private void savech_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Loader.CustomerAdapter.Update(Loader.CustomerTable);
                MessageBox.Show("Changes saved successfully.");

                RefreshCustomerTable(); // see step 2
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save changes: " + ex.Message);
            }
        }
        private void RefreshCustomerTable()
        {
            try
            {
                Loader.LoadCustomers(); // reload from DB
                dataGridView1.DataSource = Loader.CustomerTable;

                if (dataGridView1.Columns.Contains("ID_CUSTOMER"))
                    dataGridView1.Columns["ID_CUSTOMER"].Visible = false;

                NormalizeColumnHeaders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to refresh: " + ex.Message);
            }
        }

        private void filtr_txtbox_TextChanged(object sender, EventArgs e)
        {
            if (Loader.CustomerTable == null || Loader.CustomerTable.Columns.Count == 0)
                return;

            string searchText = filtr_txtbox.Text.Trim().Replace("'", "''");

            if (string.IsNullOrEmpty(searchText))
            {
                Loader.CustomerTable.DefaultView.RowFilter = string.Empty;
                return;
            }

            List<string> filterConditions = new List<string>();

            foreach (DataColumn column in Loader.CustomerTable.Columns)
            {
                if (column.DataType == typeof(string))
                {
                    filterConditions.Add($"[{column.ColumnName}] LIKE '%{searchText}%'");
                }
            }

            Loader.CustomerTable.DefaultView.RowFilter = string.Join(" OR ", filterConditions);
        }
    }
}
