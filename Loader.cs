using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Kursadarbs
{
    public static class Loader
    {
        private static string connectionString = "User Id=kursadarbs;Password=artis;Data Source=localhost:1521/XE";

        // EMPLOYEES
        public static OracleConnection Connection { get; private set; }
        public static OracleDataAdapter Adapter { get; private set; }
        public static OracleCommandBuilder Builder { get; private set; }
        public static DataTable EmployeeTable { get; private set; }


        // CUSTOMERS
        public static OracleConnection CustomerConnection { get; private set; }
        public static OracleDataAdapter CustomerAdapter { get; private set; }
        public static OracleCommandBuilder CustomerBuilder { get; private set; }
        public static DataTable CustomerTable { get; private set; }

        // MOVIES
        public static OracleConnection MovieConnection { get; private set; }
        public static OracleDataAdapter MovieAdapter { get; private set; }
        public static OracleCommandBuilder MovieBuilder { get; private set; }
        public static DataTable MovieTable { get; private set; }

        // MOVIE TYPES
        public static OracleDataAdapter MovieTypeAdapter { get; private set; }
        public static OracleCommandBuilder MovieTypeBuilder { get; private set; }
        public static DataTable MovieTypeTable { get; private set; }

        // DataSet containing related tables
        public static DataSet MovieDataSet { get; private set; }


        // TRANSACTIONS 
        public static OracleConnection TransactionConnection { get; private set; }
        public static OracleDataAdapter TransactionAdapter { get; private set; }
        public static OracleCommandBuilder TransactionBuilder { get; private set; }
        public static DataTable TransactionTable { get; private set; }
        public static OracleDataAdapter TransactionDetailsAdapter { get; private set; }
        public static OracleCommandBuilder TransactionDetailsBuilder { get; private set; }
        public static DataTable TransactionDetailsTable { get; private set; }


        public static void LoadTransactions()
        {
           
            try
            {
                TransactionConnection = new OracleConnection(connectionString);
                TransactionAdapter = new OracleDataAdapter("SELECT * FROM TRANSACTIONS", connectionString);

                TransactionDetailsAdapter = new OracleDataAdapter("SELECT * FROM TRANSACT_DETAILS", connectionString);

                TransactionTable = new DataTable("Transactions");
                TransactionAdapter.Fill(TransactionTable);
                TransactionDetailsTable = new DataTable("Transaction_Details");
                TransactionDetailsAdapter.Fill(TransactionDetailsTable);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in loading transactions: {ex.Message}\nStack Trace: {ex.StackTrace}");
                throw;
            }

        }
        // Movies and Movie Types
        public static void LoadMovies()
        {
            try
            {
                // Clean up existing resources to avoid issues
                if (MovieConnection != null)
                {
                    if (MovieConnection.State == ConnectionState.Open)
                        MovieConnection.Close();
                    MovieConnection.Dispose();
                }

                MovieConnection = new OracleConnection(connectionString);

                // Create and fill Movies table
                MovieAdapter = new OracleDataAdapter("SELECT * FROM MOVIES", MovieConnection);
                MovieTable = new DataTable("Movies");
                MovieAdapter.Fill(MovieTable);

                // Create and fill MovieType table
                MovieTypeAdapter = new OracleDataAdapter("SELECT * FROM MOVIE_TYPE", MovieConnection);
                MovieTypeTable = new DataTable("Movie_Type");
                MovieTypeAdapter.Fill(MovieTypeTable);

                // Create dataset and add tables
                MovieDataSet = new DataSet();
                MovieDataSet.Tables.Add(MovieTable);
                MovieDataSet.Tables.Add(MovieTypeTable);

                // We'll skip the relation for now to simplify troubleshooting
            }
            catch (Exception ex)
            {
                // Detailed error information
                throw new Exception($"Error in LoadMovies: {ex.Message}\nStack Trace: {ex.StackTrace}");
            }
        }


        public static void LoadEmployees()
        {
            try
            {
                Connection = new OracleConnection(connectionString);
                Adapter = new OracleDataAdapter("SELECT * FROM EMPLOYEES", Connection);
                Builder = new OracleCommandBuilder(Adapter);

                EmployeeTable = new DataTable();
                Adapter.Fill(EmployeeTable);
            }
            catch (OracleException ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error: " + ex.Message);
            }
        }

        public static void LoadCustomers()
        {
            try
            {
                CustomerConnection = new OracleConnection(connectionString);
                CustomerAdapter = new OracleDataAdapter("SELECT * FROM CUSTOMERS", CustomerConnection);
                CustomerBuilder = new OracleCommandBuilder(CustomerAdapter);
                CustomerTable = new DataTable();
                CustomerAdapter.Fill(CustomerTable);
            }
            catch (OracleException ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error: " + ex.Message);
            }
        }

        public static void SaveEmployees()
        {
            if (EmployeeTable != null && Adapter != null)
            {
                Adapter.Update(EmployeeTable);
            }
        }
    }
}
