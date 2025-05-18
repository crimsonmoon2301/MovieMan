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
    public partial class ConfigureCustomer: Form
    {
        public ConfigureCustomer()
        {
            InitializeComponent();
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
        }
    }
}
