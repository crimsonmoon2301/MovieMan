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
    public partial class EditTransaction: Form
    {
        public EditTransaction()
        {
            InitializeComponent();
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

        private void EditTransaction_Load(object sender, EventArgs e)
        {
            LoadTransactionData();
        }
    }
}
