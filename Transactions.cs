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
                Loader.LoadTransactions();

                // Set the data sources without any filtering or additional logic
                dataGridView1.DataSource = Loader.TransactionTable;
                dataGridView2.DataSource = Loader.TransactionDetailsTable;

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

            sortgrp_box.MouseEnter += (s,e) =>
            {
                title_label.Visible = true;
                desc_label.Visible = true;

                title_label.Text = "Sorting section";
                desc_label.Text = "Use this if you want to sort data in a specific way.";
            };
            sortgrp_box.MouseLeave += ClearHoverLabels;

            outputgrp_box.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible = true;

                title_label.Text = "Output section";
                desc_label.Text = "You can select your output type of the transactions made here.";
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

                if (dataGridView1.Columns.Contains("ID_TRANSACTION"))
                    dataGridView1.Columns["ID_TRANSACTION"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to refresh: " + ex.Message);
            }
        }

        private void outputgrp_box_Enter(object sender, EventArgs e)
        {

        }
    }
}
