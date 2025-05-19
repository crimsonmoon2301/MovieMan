using Oracle.ManagedDataAccess.Client;
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
    public partial class EmployeeHiarchy : Form
    {
        public EmployeeHiarchy()
        {
            InitializeComponent();
            ConfigureTreeView();
        }
        private void ConfigureTreeView()
        {
            // Ensure TreeView is visible and properly configured
            if (treeView1 != null)
            {
                treeView1.Dock = DockStyle.Fill; // Fill the entire form
                treeView1.ShowLines = true;
                treeView1.ShowPlusMinus = true;
                treeView1.ShowRootLines = true;
                treeView1.FullRowSelect = true;
                treeView1.HideSelection = false;
            }
        }

        private void InitializeTree()
        {
            treeView1.Nodes.Clear();
            // Add top-level nodes
            treeView1.Nodes.Add("Employees");
            //treeView1.Nodes.Add("Employees");
            //treeView1.Nodes.Add("Customers");
            treeView1.AfterSelect += TreeView1_AfterSelect;
        }

        private void EmployeeHiarchy_Load(object sender, EventArgs e)
        {
            InitializeTree();
            LoadEmployeesIntoTree(treeView1.Nodes[1]); // Load employees on form load
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Only load if no child nodes yet
            if (e.Node.Text == "Employees" && e.Node.Nodes.Count == 0)
            {
                LoadEmployeesIntoTree(e.Node);
            }
            // You could repeat for Products/Customers if needed
        }

        // This method was missing - it should call LoadEmployeeHierarchy
        private void LoadEmployeesIntoTree(TreeNode employeeNode)
        {
            LoadEmployeeHierarchy(employeeNode);
        }

        private void LoadEmployeeHierarchy(TreeNode root)
        {
            try
            {
                // Check if root node is null
                if (root == null)
                {
                    MessageBox.Show("Root node is null");
                    return;
                }

                // Check if connection string is null
                if (string.IsNullOrEmpty(Loader.connectionString))
                {
                    MessageBox.Show("Connection string is null or empty");
                    return;
                }

                using (var conn = new OracleConnection(Loader.connectionString))
                {
                    conn.Open();
                    string query = "SELECT id_employee, name, surname, position, manager_id FROM Employees";
                    var employees = new Dictionary<int, TreeNode>();
                    var managerMap = new Dictionary<int, List<int>>();
                    var topLevelEmployees = new List<int>(); // For employees with no manager

                    using (var cmd = new OracleCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Add null checks for database fields
                            if (reader["id_employee"] == DBNull.Value) continue;

                            int id = Convert.ToInt32(reader["id_employee"]);
                            string name = reader["name"] == DBNull.Value ? "" : reader["name"].ToString();
                            string surname = reader["surname"] == DBNull.Value ? "" : reader["surname"].ToString();
                            string position = reader["position"] == DBNull.Value ? "" : reader["position"].ToString();
                            bool hasManager = reader["manager_id"] != DBNull.Value;
                            int? managerId = hasManager ? Convert.ToInt32(reader["manager_id"]) : (int?)null;

                            string text = $"{name} {surname} ({position})";
                            var node = new TreeNode(text);
                            employees[id] = node;

                            if (hasManager)
                            {
                                int managerIdValue = managerId.Value;
                                if (!managerMap.ContainsKey(managerIdValue))
                                    managerMap[managerIdValue] = new List<int>();
                                managerMap[managerIdValue].Add(id);
                            }
                            else
                            {
                                topLevelEmployees.Add(id);
                            }
                        }
                    }

                    // Check if we loaded any employees
                    if (employees.Count == 0)
                    {
                        root.Nodes.Add("No employees found");
                        return;
                    }

                    // Recursively build tree
                    void AddChildren(TreeNode parentNode, int parentId)
                    {
                        if (!managerMap.ContainsKey(parentId)) return;
                        foreach (var empId in managerMap[parentId])
                        {
                            var node = employees[empId];
                            parentNode.Nodes.Add(node);
                            AddChildren(node, empId);
                        }
                    }

                    // Add top-level employees (those with no manager)
                    foreach (var empId in topLevelEmployees)
                    {
                        var node = employees[empId];
                        root.Nodes.Add(node);
                        AddChildren(node, empId);
                    }

                    // Expand the root node to show employees
                    root.Expand();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employee hierarchy: " + ex.Message + "\n\nStack Trace: " + ex.StackTrace);
            }
        }

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            // Only load if no child nodes yet
            if (e.Node.Text == "Employees" && e.Node.Nodes.Count == 0)
            {
                LoadEmployeesIntoTree(e.Node);
            }
            // You could repeat for Products/Customers if needed
        }
    }
}
