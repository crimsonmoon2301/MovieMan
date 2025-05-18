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
    public partial class ConfigureCustomer: Form
    {
        private DataRowView boundRowView;
        private int rowIndex;

        public ConfigureCustomer(int rowIndex)
        {
            InitializeComponent();
            this.rowIndex = rowIndex;
            boundRowView = (DataRowView)Loader.CustomerTable.DefaultView[rowIndex];
            radioButton1.Checked = false;
            radioButton2.Checked = false;
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
        private void ConfigureCustomer_Load(object sender, EventArgs e)
        {
            LoadCustomerData();
            txtName.Text = boundRowView["NAME"].ToString();
            txtSurname.Text = boundRowView["SURNAME"].ToString();
            txtEmail.Text = boundRowView["EMAIL"].ToString();
            txtPhone.Text = boundRowView["PHONE"].ToString();

            radioButton2.Checked = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                boundRowView.BeginEdit();

                boundRowView["NAME"] = txtName.Text;
                boundRowView["SURNAME"] = txtSurname.Text;
                boundRowView["EMAIL"] = txtEmail.Text;
                boundRowView["PHONE"] = txtPhone.Text;

                boundRowView.EndEdit();
            }
            else if (radioButton1.Checked)
            {
                boundRowView.Delete();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                DataRowView drv = dataGridView1.CurrentRow.DataBoundItem as DataRowView;
                if (drv != null)
                {
                    boundRowView = drv;
                    txtName.Text = boundRowView["NAME"].ToString();
                    txtSurname.Text = boundRowView["SURNAME"].ToString();
                    txtEmail.Text = boundRowView["EMAIL"].ToString();
                    txtPhone.Text = boundRowView["PHONE"].ToString();
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
            if (radioButton1.Checked)
            {
                txtName.Enabled = false;
                txtSurname.Enabled = false;
                txtEmail.Enabled = false;
                txtPhone.Enabled = false;

                button1.Enabled = true;
            }
            if (!radioButton2.Checked && !radioButton1.Checked)
            {
                txtName.Enabled = false;
                txtSurname.Enabled = false;
                txtEmail.Enabled = false;
                txtPhone.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            bool editing = radioButton2.Checked;

            txtName.Enabled = editing;
            txtSurname.Enabled = editing;
            txtEmail.Enabled = editing;
            txtPhone.Enabled = editing;

            button1.Enabled = editing || radioButton2.Checked;
        }
    }
}
