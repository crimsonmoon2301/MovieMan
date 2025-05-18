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
    public partial class ConfigureMovie: Form
    {
        public ConfigureMovie()
        {
            InitializeComponent();
        }
        private void LoadMovieData()
        {
            try
            {
                Loader.LoadMovies();

                // Set the data sources without any filtering or additional logic
                dataGridView1.DataSource = Loader.MovieTable;
                dataGridView2.DataSource = Loader.MovieTypeTable;

                if (dataGridView1.Columns.Contains("ID_MOVIE"))
                    dataGridView1.Columns["ID_MOVIE"].Visible = false;
                if (dataGridView2.Columns.Contains("ID_MOVIETYPE"))
                    dataGridView2.Columns["ID_MOVIETYPE"].Visible = false;
                if (dataGridView2.Columns.Contains("ID_MOVIE"))
                    dataGridView2.Columns["ID_MOVIE"].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in LoadMovieData: " + ex.Message);
            }
        }
        private void ConfigureMovie_Load(object sender, EventArgs e)
        {
            LoadMovieData();
        }
    }
}
