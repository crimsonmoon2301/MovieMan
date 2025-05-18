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
        private OracleConnection connection;
        private OracleDataAdapter adapter;
        private OracleDataAdapter adapter1;
        private DataTable transactionTable;
        private DataTable transactdetTable;
        private OracleCommandBuilder builder;
        private OracleCommandBuilder builder1;

        public Transactions()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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


            string connectionString = "User Id=kursadarbs;Password=artis;Data Source=localhost:1521/XE";

            try
            {
                connection = new OracleConnection(connectionString);
                adapter = new OracleDataAdapter("SELECT * FROM TRANSACTIONS", connection);
                adapter1 = new OracleDataAdapter("SELECT * FROM TRANSACT_DETAILS", connection);
                builder = new OracleCommandBuilder(adapter);
                builder1 = new OracleCommandBuilder(adapter1);


                transactionTable = new DataTable();
                transactdetTable = new DataTable();
                adapter.Fill(transactionTable);
                adapter1.Fill(transactdetTable);

                dataGridView1.DataSource = transactionTable;
                dataGridView2.DataSource = transactdetTable;
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
            refrsh_btn.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible = true;

                title_label.Text = "Refresh data";
                desc_label.Text = "Use this if the view is bugged.";
            };
            refrsh_btn.MouseLeave += ClearHoverLabels;

            edit_btn.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible = true;

                title_label.Text = "Configure an entry";
                desc_label.Text = "Adjust a entry for a transaction";
            };
            edit_btn.MouseLeave += ClearHoverLabels;

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
                desc_label.Text = "Use this if you want to sort data in a specific way. It sorts from Z - A by default.";
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

        private void edit_btn_Click(object sender, EventArgs e)
        {
            EditTransaction f = new EditTransaction();
            f.MdiParent = EmployeeForm.ActiveForm;
            f.Show();
        }
    }
}
