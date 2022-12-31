using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;
using System.Threading;

namespace BussinessLayer
{
    public class SQLHelper
    {
        private SqlConnection SqlConnection = null;
        private SqlCommand SqlCommand;
        private SqlDataAdapter SqlDataAdapter;
        private DataSet DataSet;
        private readonly string connection = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;
        public SQLHelper()
        {

        }

        public DataSet ReadData(string query, bool isStoreProc = false)
        {
            try
            {

                bool IsConnected = this.RetryConnection();
                if (IsConnected)
                {
                    SqlCommand = new SqlCommand(query, SqlConnection);
                    if (isStoreProc)
                    {
                        SqlCommand.CommandType = CommandType.StoredProcedure;
                    }
                    SqlDataAdapter = new SqlDataAdapter(SqlCommand);
                    DataSet = new DataSet();
                    SqlDataAdapter.Fill(DataSet);
                }
                else { throw new Exception(); }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured while reading data " + ex);
            }
            finally
            {
                if (SqlConnection != null)
                {
                    SqlConnection.Dispose();
                }
            }
            return DataSet;
        }

        public void InsetUpdateRecords(string query, bool isStoreProc = false, Dictionary<string, object> parameters = null)
        {
            try
            {

                bool IsConnected = this.RetryConnection();
                if (IsConnected)
                {
                    SqlCommand = new SqlCommand(query, SqlConnection);
                    if (isStoreProc)
                    {
                        SqlCommand.CommandType = CommandType.StoredProcedure;
                    }
                    if (parameters != null)
                    {
                        foreach (var data in parameters)
                        {
                            SqlCommand.Parameters.AddWithValue(data.Key, data.Value);
                        }
                    }
                    int record = SqlCommand.ExecuteNonQuery();
                    if (record > 0)
                    {
                        Console.WriteLine("Records Inserted Successfully.");
                    }
                    else
                    {
                        throw new Exception("Record is not inserted correctly");
                    }

                }
                else { throw new Exception(); }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured while Inserting  data " + ex);
            }
            finally
            {
                if (SqlConnection != null)
                {
                    SqlConnection.Dispose();
                }
            }

        }

        private bool RetryConnection(int retry = 3, int waitTimeInSeconds = 5)
        {
            bool isConnected = false;
            try
            {
                if (SqlConnection == null || (SqlConnection.State == ConnectionState.Closed))
                {

                    SqlConnection = new SqlConnection(connection);
                    Thread.Sleep(100);
                    SqlConnection.Open();
                }
                if (SqlConnection.State == ConnectionState.Open)
                {
                    isConnected = true;
                    return isConnected;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured while opening the connection " + ex.Message);
                for (int i = 0; i < retry; i++)
                {
                    Console.WriteLine("Retrying connection ..... retry times" + retry);
                    try
                    {
                        if (SqlConnection.State != ConnectionState.Open)
                        {
                            SqlConnection = new SqlConnection(connection);
                            SqlConnection.Open();
                            Thread.Sleep(waitTimeInSeconds);

                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Retrying connection ..... retry times" + retry + Environment.NewLine + exception.Message);
                    }
                }
            }
            return isConnected;
        }


    }

    public sealed class SQLService
    {
        private static SQLHelper SQLHelper = null;
        private static ThreadLocal<SQLHelper> ThreadLocal = null;
        private SQLService()
        {

        }

        public static SQLHelper Instance
        {
            get
            {
                if (ThreadLocal == null)
                {
                    ThreadLocal = new ThreadLocal<SQLHelper>(() => new SQLHelper());
                }
                return ThreadLocal.Value;
            }
        }
    }

}

