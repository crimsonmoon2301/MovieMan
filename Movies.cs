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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kursadarbs
{
    public partial class Movies : Form
    {
        private OracleConnection connection;
        private OracleDataAdapter adapter;
        private OracleDataAdapter adapter1;
        private DataTable movieTable;
        private DataTable movietypeTable;
        private OracleCommandBuilder builder;
        private OracleCommandBuilder builder1;


        public Movies()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Movies_Load(object sender, EventArgs e)
        {

            title_label.Visible = false;
            desc_label.Visible = false;

            title_label.AutoSize = true;
            desc_label.TextAlign = ContentAlignment.TopLeft;
            title_label.MaximumSize = new Size(groupBox1.ClientSize.Width - 15, 0);
            desc_label.MaximumSize = new Size(groupBox1.ClientSize.Width - 15, 0);
            desc_label.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            SetupButtonHover();

            string connectionString = "User Id=kursadarbs;Password=artis;Data Source=localhost:1521/XE";

            try
            {
                connection = new OracleConnection(connectionString);
                adapter = new OracleDataAdapter("SELECT * FROM MOVIES", connection);
                builder = new OracleCommandBuilder(adapter);

                movieTable = new DataTable();
                adapter.Fill(movieTable);

                dataGridView1.DataSource = movieTable;

                if (dataGridView1.Columns.Contains("ID_MOVIE"))
                {
                    dataGridView1.Columns["ID_MOVIE"].Visible = false;
                }

                

                // Movie tipi
                adapter1 = new OracleDataAdapter("SELECT * FROM MOVIE_TYPE", connection);
                builder1 = new OracleCommandBuilder(adapter1);

                movietypeTable = new DataTable("Movie_Type"); // Set table name here
                adapter1.Fill(movietypeTable);

                // Now movieTable also needs a name to be referenced in DataRelation
                movieTable.TableName = "Movies";

                // Add to DataSet
                DataSet ds = new DataSet();
                ds.Tables.Add(movieTable);
                ds.Tables.Add(movietypeTable);

                // Set up relation
                DataRelation relation = new DataRelation(
                    "MovieToType",
                    ds.Tables["Movies"].Columns["ID_MOVIE"],
                    ds.Tables["Movie_Type"].Columns["ID_MOVIE"]
                );
                ds.Relations.Add(relation);

                // Bind tables
                dataGridView1.DataSource = ds.Tables["Movies"];
                dataGridView2.DataSource = ds.Tables["Movie_Type"];


                movietypeTable = new DataTable();
                adapter1.Fill(movietypeTable);
                

                if (dataGridView2.Columns.Contains("ID_MOVIETYPE"))
                {
                    dataGridView2.Columns["ID_MOVIETYPE"].Visible = false;
                }
                if (dataGridView2.Columns.Contains("ID_MOVIE"))
                {
                    dataGridView2.Columns["ID_MOVIE"].Visible = false;
                }


                // After adapter1.Fill(movietypeTable);
                var genres = movietypeTable.AsEnumerable()
                    .Select(row => row.Field<string>("GENRE"))
                    .Distinct()
                    .Where(g => !string.IsNullOrEmpty(g))
                    .OrderBy(g => g)
                    .ToList();

                genre_combbox.Items.Clear();
                genre_combbox.Items.Add("All"); // Optional to show everything
                genre_combbox.Items.AddRange(genres.ToArray());
                genre_combbox.SelectedIndex = 0;

                //foarmāti
                // Get unique formats
                var formats = movietypeTable.AsEnumerable()
                    .Select(row => row.Field<string>("FORMAT"))
                    .Distinct()
                    .Where(f => !string.IsNullOrEmpty(f))
                    .OrderBy(f => f)
                    .ToList();

                formt_combbox.Items.Clear();
                formt_combbox.Items.Add("All"); // optional
                formt_combbox.Items.AddRange(formats.ToArray());
                formt_combbox.SelectedIndex = 0;


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
                desc_label.Text = "Adjust a entry for a movie";
            };
            edit_btn.MouseLeave += ClearHoverLabels;

            add_btn.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible = true;

                title_label.Text = "Add new entry";
                desc_label.Text = "Queue new movie entry";
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

            formt_combbox.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible = true;

                title_label.Text = "Sort movies by format";
                desc_label.Text = "Show movies by their available formats. Shows all by default.";
            };
            formt_combbox.MouseLeave += ClearHoverLabels;

            genre_combbox.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible = true;

                title_label.Text = "Sort movies by genre";
                desc_label.Text = "Show movies by their genre. Shows all by default.";
            };
            genre_combbox.MouseLeave += ClearHoverLabels;

            dataGridView1.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible = true;

                title_label.Text = "Did you know?";
                desc_label.Text = "You can double click to see information about the movie!";
            };

            dataGridView2.MouseEnter += (s, e) =>
            {
                title_label.Visible = true;
                desc_label.Visible = true;

                title_label.Text = "Did you know?";
                desc_label.Text = "You can double click to see information about the movie!";
            };
        }

        private void ClearHoverLabels(object sender, EventArgs e)
        {
            title_label.Text = "";
            desc_label.Text = "";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGenre = genre_combbox.SelectedItem.ToString();

            if (selectedGenre == "All")
            {
                // Show all
                dataGridView2.DataSource = movietypeTable;
                dataGridView1.DataSource = movieTable;
            }
            else
            {
                // Filter Movie_type by genre
                DataView filteredTypeView = new DataView(movietypeTable);
                filteredTypeView.RowFilter = $"GENRE = '{selectedGenre.Replace("'", "''")}'";
                dataGridView2.DataSource = filteredTypeView;

                // Get matching movie IDs
                var matchingIDs = movietypeTable.AsEnumerable()
                                 .Where(row => row.Field<string>("GENRE") == selectedGenre)
                                 .Select(row => row["ID_MOVIE"].ToString())
                                 .ToList();

                DataView filteredMovieView = new DataView(movieTable);
                if (matchingIDs.Any())
                {
                    string idFilter = string.Join(",", matchingIDs.Select(id => $"'{id}'"));
                    filteredMovieView.RowFilter = $"ID_MOVIE IN ({idFilter})";
                }
                else
                {
                    filteredMovieView.RowFilter = "1 = 0"; // Show nothing
                }

                dataGridView1.DataSource = filteredMovieView;
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFormat = formt_combbox.SelectedItem.ToString();

            if (selectedFormat == "All")
            {
                dataGridView2.DataSource = movietypeTable;
                dataGridView1.DataSource = movieTable;
            }
            else
            {
                // Filter Movie_type by format
                DataView filteredTypeView = new DataView(movietypeTable);
                filteredTypeView.RowFilter = $"FORMAT = '{selectedFormat.Replace("'", "''")}'";
                dataGridView2.DataSource = filteredTypeView;

                // Get matching movie IDs as strings
                var matchingIDs = movietypeTable.AsEnumerable()
                    .Where(row => row.Field<string>("FORMAT") == selectedFormat)
                    .Select(row => row["ID_MOVIE"].ToString())
                    .ToList();

                DataView filteredMovieView = new DataView(movieTable);
                if (matchingIDs.Any())
                {
                    string idFilter = string.Join(",", matchingIDs.Select(id => $"'{id}'"));
                    filteredMovieView.RowFilter = $"ID_MOVIE IN ({idFilter})";
                }
                else
                {
                    filteredMovieView.RowFilter = "1 = 0";
                }

                dataGridView1.DataSource = filteredMovieView;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddNewMovie f = new AddNewMovie();
            f.MdiParent = EmployeeForm.ActiveForm;
            f.Show();
        }


        // TODO: fix the highlight not working (it selects, but highlight doesn't follow)
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

            object selectedMovieId = dataGridView1.CurrentRow.Cells["ID_MOVIE"].Value;

            
            dataGridView2.ClearSelection(); // Unselect everything first

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["ID_MOVIE"].Value != null &&
                    row.Cells["ID_MOVIE"].Value.ToString() == selectedMovieId.ToString())
                {
                    row.Selected = true;
                    dataGridView2.FirstDisplayedScrollingRowIndex = row.Index;
                }
                else
                {
                    row.Selected = false;
                    adapter.Update((DataTable)dataGridView1.DataSource);
                }
               
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
                return;

            object selectedMovieId = dataGridView2.CurrentRow.Cells["ID_MOVIE"].Value;


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["ID_MOVIE"].Value != null &&
                    row.Cells["ID_MOVIE"].Value.ToString() == selectedMovieId.ToString())
                {
                    row.Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                }
                else
                {
                    row.Selected = false;
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void edit_btn_Click(object sender, EventArgs e)
        {
            ConfigureMovie f = new ConfigureMovie();
            f.MdiParent = EmployeeForm.ActiveForm;
            f.Show();
        }
    }
}