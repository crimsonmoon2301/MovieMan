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
    public partial class EditEmployee: Form
    {
       

        public EditEmployee()
        {
            InitializeComponent();
           
        }
        private void LoadEmployeeData()
        {
            try
            {
                Loader.LoadEmployees();
                dataGridView1.DataSource = Loader.EmployeeTable;

                if (dataGridView1.Columns.Contains("ID_EMPLOYEE"))
                    dataGridView1.Columns["ID_EMPLOYEE"].Visible = false;

                if (dataGridView1.Columns.Contains("MANAGER_ID"))
                    dataGridView1.Columns["MANAGER_ID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void EditEmployee_Load(object sender, EventArgs e)
        {
            LoadEmployeeData();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            
        }
    }
}
