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
            LoadMovieData();

            // Populate Genre ComboBox
            genre_combbox.Items.Clear();
            genre_combbox.Items.Add("All");

            var genres = Loader.MovieTypeTable.AsEnumerable()
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
            var formats = Loader.MovieTypeTable.AsEnumerable()
                .Select(row => row.Field<string>("FORMAT"))
                .Distinct()
                .Where(f => !string.IsNullOrEmpty(f))
                .OrderBy(f => f)
                .ToList();

            formt_combbox.Items.Clear();
            formt_combbox.Items.Add("All"); // optional
            formt_combbox.Items.AddRange(formats.ToArray());
            formt_combbox.SelectedIndex = 0;


            if (dataGridView1.Columns.Contains("ID_MOVIE"))
                dataGridView1.Columns["ID_MOVIE"].Visible = false;
            if (dataGridView2.Columns.Contains("ID_MOVIETYPE"))
                dataGridView2.Columns["ID_MOVIETYPE"].Visible = false;
            if (dataGridView2.Columns.Contains("ID_MOVIE"))
                dataGridView2.Columns["ID_MOVIE"].Visible = false;

            ReplaceMovieIdWithComboBox();
            NormalizeColumnHeaders();

        }
        private void ReplaceMovieIdWithComboBox()
        {
            // Remove the original column if it exists
            if (dataGridView2.Columns.Contains("ID_MOVIE"))
            {
                dataGridView2.Columns.Remove("ID_MOVIE");
            }

            // Create combo box column
            DataGridViewComboBoxColumn movieComboBox = new DataGridViewComboBoxColumn();
            movieComboBox.Name = "ID_MOVIE";
            movieComboBox.HeaderText = "Movie Title";

            // Use Loader.MovieTable as the data source
            movieComboBox.DataSource = Loader.MovieTable;
            movieComboBox.DisplayMember = "NAME";         // what the user sees
            movieComboBox.ValueMember = "ID_MOVIE";        // actual value stored in data

            movieComboBox.DataPropertyName = "ID_MOVIE";   // binds to ID_MOVIE in MovieTypeTable

            movieComboBox.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton; // looks clean
            movieComboBox.FlatStyle = FlatStyle.Flat;

            // Add to grid
            dataGridView2.Columns.Insert(0, movieComboBox);
        }

        private void LoadMovieData()
        {
            try
            {
                Loader.LoadMovies();

                // Set the data sources without any filtering or additional logic
                dataGridView1.DataSource = Loader.MovieTable;
                dataGridView2.DataSource = Loader.MovieTypeTable;

                // Basic combo box setup
                genre_combbox.Items.Clear();
                genre_combbox.Items.Add("All");
                genre_combbox.SelectedIndex = 0;

                formt_combbox.Items.Clear();
                formt_combbox.Items.Add("All");
                formt_combbox.SelectedIndex = 0;

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in LoadMovieData: " + ex.Message);
            }
        }

        private void SetupButtonHover()
        {
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
                dataGridView2.DataSource = Loader.MovieTypeTable;
                dataGridView1.DataSource = Loader.MovieTable;
            }
            else
            {
                // Filter Movie_type by genre
                DataView filteredTypeView = new DataView(Loader.MovieTypeTable);
                filteredTypeView.RowFilter = $"GENRE = '{selectedGenre.Replace("'", "''")}'";
                dataGridView2.DataSource = filteredTypeView;

                // Get matching movie IDs
                var matchingIDs = Loader.MovieTypeTable.AsEnumerable()
                                 .Where(row => row.Field<string>("GENRE") == selectedGenre)
                                 .Select(row => row["ID_MOVIE"].ToString())
                                 .ToList();

                DataView filteredMovieView = new DataView(Loader.MovieTable);
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
                dataGridView2.DataSource = Loader.MovieTypeTable;
                dataGridView1.DataSource = Loader.MovieTable;
            }
            else
            {
                // Filter Movie_type by format
                DataView filteredTypeView = new DataView(Loader.MovieTypeTable);
                filteredTypeView.RowFilter = $"FORMAT = '{selectedFormat.Replace("'", "''")}'";
                dataGridView2.DataSource = filteredTypeView;

                // Get matching movie IDs as strings
                var matchingIDs = Loader.MovieTypeTable.AsEnumerable()
                    .Where(row => row.Field<string>("FORMAT") == selectedFormat)
                    .Select(row => row["ID_MOVIE"].ToString())
                    .ToList();

                DataView filteredMovieView = new DataView(Loader.MovieTable);
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
                    Loader.MovieAdapter.Update((DataTable)dataGridView1.DataSource);
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

        private void savech_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Loader.MovieAdapter.Update(Loader.MovieTable);
                Loader.MovieTypeAdapter.Update(Loader.MovieTypeTable);
                MessageBox.Show("Changes saved successfully.");

                RefreshMovieTables(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save changes: " + ex.Message);
            }
        }

        private void RefreshMovieTables()
        {
            try
            {
                Loader.LoadMovies(); // reload from DB
                dataGridView1.DataSource = Loader.MovieTable;
                dataGridView2.DataSource = Loader.MovieTypeTable;

                if (dataGridView1.Columns.Contains("ID_MOVIE"))
                    dataGridView1.Columns["ID_MOVIE"].Visible = false;
                NormalizeColumnHeaders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to refresh: " + ex.Message);
            }
        }
        private void NormalizeColumnHeaders()
        {
            Dictionary<string, string> headerMappings = new Dictionary<string, string>()
            {
                 { "NAME", "Title" },
                 { "DIRECTOR", "Director" },
                 { "PRICE", "Price (EUR)" },
                 { "ORIGIN_OF_CREATION", "Movie Origin" },
                 { "ADDRESS", "Address" },
                 { "CITY", "City" },
                 { "ZIP", "ZIP Code" },
                 { "COUNTRY", "Country" },
                {"RELEASE_YEAR", "Release Year"},
                {"GENRE", "Genre"},
                {"FORMAT", "Format"},
                {"DURATION", "Duration (Minutes)" }
                
        // Add more mappings as needed
            };

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (headerMappings.ContainsKey(column.Name))
                {
                    column.HeaderText = headerMappings[column.Name];
                }
            }
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                if (headerMappings.ContainsKey(column.Name))
                {
                    column.HeaderText = headerMappings[column.Name];
                }
            }
        }
    }
}