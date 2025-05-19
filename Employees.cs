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
    public partial class Employees: Form
    {
        public Employees()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

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

        private void Employees_Load(object sender, EventArgs e)
        {
            
            title_label.Visible = false;
            desc_label.Visible = false;

            title_label.AutoSize = true;
            desc_label.TextAlign = ContentAlignment.TopLeft;
            title_label.MaximumSize = new Size(groupBox1.ClientSize.Width - 15, 0);
            desc_label.MaximumSize = new Size(groupBox1.ClientSize.Width - 15, 0);
            desc_label.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            SetupButtonHover();
            LoadEmployeeData();
        }

        private void SetupButtonHover()
        {
            add_btn.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible = true;

                title_label.Text = "Add new Employee";
                desc_label.Text = "Queue new employee to the database.";
            };
            add_btn.MouseLeave += ClearHoverLabels;

            struct_grpbox.MouseEnter += (s, e) =>
            {
                title_label.Visible= true;
                desc_label.Visible= true;

                title_label.Text = "Change layout type";
                desc_label.Text = "You can change the employee layout in this section.";
            };
            struct_grpbox.MouseLeave += ClearHoverLabels;

            filtr_txtbox.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible= true;

                title_label.Text = "Filter function";
                desc_label.Text = "You can look up custom data by need.";
            };
            filtr_txtbox.MouseLeave += ClearHoverLabels;

            flatlist_radio.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible= true;

                title_label.Text = "Show employee list on flat list";
                desc_label.Text = "Shows data on data view grid. Used by default.";
            };
            flatlist_radio.MouseLeave += ClearHoverLabels;

            hiarch_radio.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible= true;

                title_label.Text = "Show employee list hiarchically";
                desc_label.Text = "Shows data in treeview. Will reload the form.";
            };
            hiarch_radio.MouseLeave += ClearHoverLabels;

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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            AddEmployee f = new AddEmployee();
            f.MdiParent = EmployeeForm.ActiveForm;
            f.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Loader.Adapter.Update(Loader.EmployeeTable);
                MessageBox.Show("Changes saved successfully.");

                RefreshEmployeeTable(); // see step 2
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save changes: " + ex.Message);
            }
        }
        private void RefreshEmployeeTable()
        {
            try
            {
                Loader.LoadEmployees(); // reload from DB
                dataGridView1.DataSource = Loader.EmployeeTable;

                if (dataGridView1.Columns.Contains("ID_EMPLOYEE"))
                    dataGridView1.Columns["ID_EMPLOYEE"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to refresh: " + ex.Message);
            }
        }

        private void title_label_Click(object sender, EventArgs e)
        {

        }
    }
}
