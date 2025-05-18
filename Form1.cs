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
    public partial class EmployeeForm: Form
    {
        private OracleConnection connection;
        private OracleDataAdapter adapter;
        private DataTable employeeTable;
        private OracleCommandBuilder builder;


        public EmployeeForm()
        {
            InitializeComponent();
            Load += EmployeeForm_Load;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Employees f = new Employees();
            f.MdiParent = this;
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Movies f = new Movies();
            f.MdiParent = this;
            f.Show();
        }

        private void abtBtn_Click(object sender, EventArgs e)
        {
            string title = "About This Application";
            string message = "Movie Store Manager\n\n" +
                             
                             "This application allows the user to manage movies, customers, " +
                             "transactions, and employees using an Oracle database backend, following a minimalistic, yet intuitive approach. \n" +
                             "\nMade for an programming with database course assignment";

            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Transactions f = new Transactions();
            f.MdiParent = this;
            f.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Customers f = new Customers();
            f.MdiParent = this;
            f.Show();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            quickadd f = new quickadd();
            f.MdiParent = this;
            f.Show();
        }
    }
}
