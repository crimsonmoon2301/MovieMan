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
using System.Xml.Linq;

namespace Kursadarbs
{
    public partial class AddNewMovie: Form
    {
        public AddNewMovie()
        {
            InitializeComponent();
        }

        private void AddNewMovie_Load(object sender, EventArgs e)
        {
            LoadFormats();
            // Clear all input fields when form loads
            txtTitle.Text = string.Empty;
            txtDuration.Text = string.Empty;
            txtDirector.Text = string.Empty;
            txtPrice.Text = string.Empty;

            // Make sure we have the customer data loaded
            try
            {
                if (Loader.CustomerTable == null)
                {
                    Loader.LoadCustomers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customer data: " + ex.Message);
            }
        }

        private void LoadFormats()
        {
            try
            {
                // Load unique formats into combo box from existing data
                if (Loader.MovieTypeTable != null)
                {
                    var formats = Loader.MovieTypeTable.AsEnumerable()
                        .Select(row => row.Field<string>("FORMAT"))
                        .Distinct()
                        .Where(f => !string.IsNullOrEmpty(f))
                        .OrderBy(f => f)
                        .ToList();

                    cmbFormat.DataSource = formats;
                    cmbFormat.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading formats: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetNextMovieId()
        {
            int nextId = 0;
            try
            {
                using (OracleConnection conn = new OracleConnection(Loader.connectionString))
                {
                    conn.Open();
                    // Fixed: Use movie sequence instead of employee sequence
                    using (OracleCommand cmd = new OracleCommand("SELECT seq_id_movie.NEXTVAL FROM DUAL", conn))
                    {
                        // Get the next sequence value
                        nextId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting next movie ID: " + ex.Message);
            }
            return nextId;
        }

        private int GetNextMovieTypeId()
        {
            int nextId = 0;
            try
            {
                using (OracleConnection conn = new OracleConnection(Loader.connectionString))
                {
                    conn.Open();
                    // Get next sequence value for movie type
                    using (OracleCommand cmd = new OracleCommand("SELECT seq_id_movietype.NEXTVAL FROM DUAL", conn))
                    {
                        nextId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting next movie type ID: " + ex.Message);
            }
            return nextId;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input fields
                if (!ValidateInput())
                    return;

                // Get the next movie ID from sequence
                int movieId = GetNextMovieId();
                if (movieId == 0)
                {
                    MessageBox.Show("Failed to generate movie ID. Please try again.", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get the next movie type ID from sequence
                int movieTypeId = GetNextMovieTypeId();
                if (movieTypeId == 0)
                {
                    MessageBox.Show("Failed to generate movie type ID. Please try again.", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create new DataRow for Movies table
                DataRow newMovieRow = Loader.MovieTable.NewRow();
                newMovieRow["ID_MOVIE"] = movieId; // Set the generated ID
                newMovieRow["DIRECTOR"] = txtDirector.Text.Trim();
                newMovieRow["NAME"] = txtTitle.Text.Trim();
                newMovieRow["PRICE"] = decimal.Parse(txtPrice.Text.Trim());
                newMovieRow["ORIGIN_OF_CREATION"] = txtOrigin.Text.Trim();

                // Add the row to the DataTable
                Loader.MovieTable.Rows.Add(newMovieRow);

                // Create new DataRow for Movie_Type table
                DataRow newTypeRow = Loader.MovieTypeTable.NewRow();
                newTypeRow["ID_MOVIETYPE"] = movieTypeId; // Set the generated movie type ID
                newTypeRow["ID_MOVIE"] = movieId; // Link to the movie we just created
                newTypeRow["RELEASE_YEAR"] = int.Parse(txtYear.Text.Trim());
                newTypeRow["GENRE"] = txtGenre.Text.Trim();
                newTypeRow["DURATION"] = int.Parse(txtDuration.Text.Trim());
                newTypeRow["FORMAT"] = cmbFormat.SelectedItem.ToString();

                // Add the row to the DataTable
                Loader.MovieTypeTable.Rows.Add(newTypeRow);

                // Use DataAdapter to save changes to the database
                Loader.MovieAdapter.Update(Loader.MovieTable);
                Loader.MovieTypeAdapter.Update(Loader.MovieTypeTable);

                MessageBox.Show("Movie added successfully!", "Success",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the data
                Loader.LoadMovies();

                // Close the form or clear it for next entry
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving movie: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            // Check if name is provided
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter a movie name.", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return false;
            }

            // Check if director is provided
            if (string.IsNullOrWhiteSpace(txtDirector.Text))
            {
                MessageBox.Show("Please enter a director name.", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDirector.Focus();
                return false;
            }

            // Validate price
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Please enter a valid price (greater than or equal to 0).",
                               "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            // Check if origin is provided
            if (string.IsNullOrWhiteSpace(txtOrigin.Text))
            {
                MessageBox.Show("Please enter the origin of creation.", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOrigin.Focus();
                return false;
            }

            // Validate release year
            if (!int.TryParse(txtYear.Text, out int year) || year < 1888 || year > DateTime.Now.Year + 5)
            {
                MessageBox.Show("Please enter a valid release year (1888 - " + (DateTime.Now.Year + 5) + ").",
                               "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtYear.Focus();
                return false;
            }

            // Check if genre is provided
            if (string.IsNullOrWhiteSpace(txtGenre.Text))
            {
                MessageBox.Show("Please enter a genre.", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGenre.Focus();
                return false;
            }

            // Validate duration
            if (!int.TryParse(txtDuration.Text, out int duration) || duration <= 0)
            {
                MessageBox.Show("Please enter a valid duration in minutes (greater than 0).",
                               "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDuration.Focus();
                return false;
            }

            // Check if format is selected
            if (cmbFormat.SelectedItem == null)
            {
                MessageBox.Show("Please select a format.", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbFormat.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input fields first
                if (!ValidateInput())
                    return;

                // Get the next movie ID from sequence
                int movieId = GetNextMovieId();
                if (movieId == 0)
                {
                    MessageBox.Show("Failed to generate movie ID. Please try again.", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get the next movie type ID from sequence
                int movieTypeId = GetNextMovieTypeId();
                if (movieTypeId == 0)
                {
                    MessageBox.Show("Failed to generate movie type ID. Please try again.", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create new DataRow for Movies table
                DataRow newMovieRow = Loader.MovieTable.NewRow();
                newMovieRow["ID_MOVIE"] = movieId; // Set the generated ID
                newMovieRow["DIRECTOR"] = txtDirector.Text.Trim();
                newMovieRow["NAME"] = txtTitle.Text.Trim();
                newMovieRow["PRICE"] = decimal.Parse(txtPrice.Text.Trim());
                newMovieRow["ORIGIN_OF_CREATION"] = txtOrigin.Text.Trim();

                // Add the row to the DataTable
                Loader.MovieTable.Rows.Add(newMovieRow);

                // Create new DataRow for Movie_Type table
                DataRow newTypeRow = Loader.MovieTypeTable.NewRow();
                newTypeRow["ID_MOVIETYPE"] = movieTypeId; // Set the generated movie type ID
                newTypeRow["ID_MOVIE"] = movieId; // Link to the movie we just created
                newTypeRow["RELEASE_YEAR"] = int.Parse(txtYear.Text.Trim());
                newTypeRow["GENRE"] = txtGenre.Text.Trim();
                newTypeRow["DURATION"] = int.Parse(txtDuration.Text.Trim());
                newTypeRow["FORMAT"] = cmbFormat.SelectedItem.ToString();

                // Add the row to the DataTable
                Loader.MovieTypeTable.Rows.Add(newTypeRow);

                // Use DataAdapter to save changes to the database
                Loader.MovieAdapter.Update(Loader.MovieTable);
                Loader.MovieTypeAdapter.Update(Loader.MovieTypeTable);

                MessageBox.Show("Movie added successfully!", "Success",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the data
                Loader.LoadMovies();

                // Close the form or clear it for next entry
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving movie: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
