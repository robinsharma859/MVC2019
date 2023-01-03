using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using Newtonsoft.Json;
namespace BussinessLayer
{
    public class UserRegistrationBussinessService
    {
        private readonly List<UserRegistration> UserRegistrationList = null;
        private SqlConnection SqlConnection = null;
        private SqlCommand SqlCommand;
        private SqlDataAdapter SqlDataAdapter;
        private readonly string connection = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;

        public UserRegistrationBussinessService()
        {
            UserRegistrationList = new List<UserRegistration>();
        }

        public IEnumerable<UserRegistration> UserRegistationList()
        {
            try
            {

                //SqlConnection = new SqlConnection(connection);
                //SqlCommand = new SqlCommand("select * from UserRegistration", SqlConnection);
                //SqlConnection.Open();
                //SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();
                //while (sqlDataReader.Read())
                //{
                //    UserRegistration userRegistrations = new UserRegistration()
                //    {
                //        Name = sqlDataReader["Name"].ToString(),
                //        Addresss = sqlDataReader["Address"].ToString(),
                //        Country = sqlDataReader["Country"].ToString(),
                //        PinCode = sqlDataReader["PinCode"].ToString(),
                //        Age = Convert.ToInt32(sqlDataReader["Age"]),
                //        Phone = sqlDataReader["Phone"].ToString(),
                //        Gender = sqlDataReader["Gender"].ToString()
                //    };
                //    UserRegistrationList.Add(userRegistrations);
                //}
                var dataSet = SQLService.Instance.ReadData("select * from UserRegistration");
                string record = JsonConvert.SerializeObject(dataSet, Formatting.Indented);

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    UserRegistration userRegistrations = new UserRegistration()
                    {
                        RegistrationID = (int)row["RegistrationID"],
                        Name = row["Name"].ToString(),
                        Address = row["Address"].ToString(),
                        Country = row["Country"].ToString(),
                        PinCode = row["PinCode"].ToString(),
                        Age = Convert.ToInt32(row["Age"]),
                        Phone = row["Phone"].ToString(),
                        Gender = row["Gender"].ToString()
                    };
                    UserRegistrationList.Add(userRegistrations);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured while fetching User Regitration List" + ex.Message);
            }
            finally
            {
                if (SqlConnection != null)
                {
                    SqlConnection.Dispose();
                }

            }
            return UserRegistrationList.AsEnumerable();

        }
        public void AddUserRegistration(string query, bool isProc, Dictionary<string, object> paramaters)

        {
            try
            {
                SQLService.Instance.InsetUpdateRecords(query, isProc, paramaters);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error occured while Inserting User Regitration List" + ex.Message);
            }
        }
        public IEnumerable<UserRegistration> UserRegistationListWherCondition(int userRegistrationId)
        {
            try
            {

                //SqlConnection = new SqlConnection(connection);
                //SqlCommand = new SqlCommand("select * from UserRegistration", SqlConnection);
                //SqlConnection.Open();
                //SqlDataReader sqlDataReader = SqlCommand.ExecuteReader();
                //while (sqlDataReader.Read())
                //{
                //    UserRegistration userRegistrations = new UserRegistration()
                //    {
                //        Name = sqlDataReader["Name"].ToString(),
                //        Addresss = sqlDataReader["Address"].ToString(),
                //        Country = sqlDataReader["Country"].ToString(),
                //        PinCode = sqlDataReader["PinCode"].ToString(),
                //        Age = Convert.ToInt32(sqlDataReader["Age"]),
                //        Phone = sqlDataReader["Phone"].ToString(),
                //        Gender = sqlDataReader["Gender"].ToString()
                //    };
                //    UserRegistrationList.Add(userRegistrations);
                //}
                var dataSet = SQLService.Instance.ReadData($"select * from UserRegistration  where RegistrationId = {userRegistrationId}");
                string record = JsonConvert.SerializeObject(dataSet, Formatting.Indented);

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    UserRegistration userRegistrations = new UserRegistration()
                    {
                        RegistrationID = (int)row["RegistrationID"],
                        Name = row["Name"].ToString(),
                        Address = row["Address"].ToString(),
                        Country = row["Country"].ToString(),
                        PinCode = row["PinCode"].ToString(),
                        Age = Convert.ToInt32(row["Age"]),
                        Phone = row["Phone"].ToString(),
                        Gender = row["Gender"].ToString()
                    };
                    UserRegistrationList.Add(userRegistrations);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured while fetching User Regitration List" + ex.Message);
            }
            finally
            {
                if (SqlConnection != null)
                {
                    SqlConnection.Dispose();
                }

            }
            return UserRegistrationList.AsEnumerable();
        }
    }
}
