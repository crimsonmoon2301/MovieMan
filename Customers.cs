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
                dataGridView1.DataSource = Loader.CustomerTable;

                // Hide any columns you want
                if (dataGridView1.Columns.Contains("ID_CUSTOMER"))
                    dataGridView1.Columns["ID_CUSTOMER"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

            sortgrp_box.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible = true;

                title_label.Text = "Sorting section";
                desc_label.Text = "Use this if you want to sort data in a specific way. It sorts from Z - A by default.";
            };
            sortgrp_box.MouseLeave += ClearHoverLabels;

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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to refresh: " + ex.Message);
            }
        }
    }
}
