
using Microsoft.Data.SqlClient;
using myADOProject.Models;
using System.Data;
using System.Xml;

namespace myADOProject.DataAccess
{
    public class CustomerDA
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;

        public static IConfiguration Configuration { get; set; }

        private String GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration= builder.Build();

            return Configuration.GetConnectionString("defaultconnection");
        }

        public List<Customer> GetAll() {

            List<Customer> customerlist = new List<Customer>();
            using(_connection=new SqlConnection(GetConnectionString()))
            {
                _command=_connection.CreateCommand();
                _command.CommandType =CommandType.StoredProcedure;
                _command.CommandText = "sp_GetCustomer";
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while(dr.Read())
                {
                   Customer customer = new Customer();
                    customer.cid =Convert.ToInt32( dr["cid"]);
                    customer.FirstName = dr["Firstname"].ToString();
                    customer.LastName = dr["Lastname"].ToString();
                    customer.DateofBirth =Convert.ToDateTime( dr["DateofBirth"]).Date;
                    customer.Email = dr["Email"].ToString();
                    customer.Salary = dr["Salary"].ToString();
                    customerlist.Add(customer);
                }
                _connection.Close();
            }

            return customerlist;
        }


        public bool Insert(Customer model)
        {
            int id = 0;
            using (_connection=new SqlConnection(GetConnectionString()))
            {
                _command=_connection.CreateCommand();
                _command.CommandType=CommandType.StoredProcedure;
                _command.CommandText = "sp_InsertByIDCustomer";
                _command.Parameters.AddWithValue("@FirstName", model.FirstName);
                _command.Parameters.AddWithValue("@LastName", model.LastName);
                _command.Parameters.AddWithValue("@DateofBirth", model.DateofBirth);
                _command.Parameters.AddWithValue("@Email", model.Email);
                _command.Parameters.AddWithValue("@Salary", model.Salary);
                _connection.Open();
                id=_command.ExecuteNonQuery();
                _connection.Close();

            }
            return id > 0 ? true : false;
        }

     //----------------------------------------------------------------------------------------------------


        public Customer GetbyId(int id)
        {

            Customer customer = new Customer();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "sp_GetByIDCustomer";
                _command.Parameters.AddWithValue("@custid", id);
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    customer.cid = Convert.ToInt32(dr["cid"]);
                    customer.FirstName = dr["Firstname"].ToString();
                    customer.LastName = dr["Lastname"].ToString();
                    customer.DateofBirth = Convert.ToDateTime(dr["DateofBirth"]).Date;
                    customer.Email = dr["Email"].ToString();
                    customer.Salary = dr["Salary"].ToString();
                }
                _connection.Close();
            }

            return customer;
        }


        //------------------------------------------------update------------------------------------------------


        public bool Update(Customer model)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "sp_updateByIdCustomer";
                _command.Parameters.AddWithValue("@ctmid", model.cid);
                _command.Parameters.AddWithValue("@firstname", model.FirstName);
                _command.Parameters.AddWithValue("@lastname", model.LastName);
                _command.Parameters.AddWithValue("@dateofbirth", model.DateofBirth);
                _command.Parameters.AddWithValue("@email", model.Email);
                _command.Parameters.AddWithValue("@salary", model.Salary);
                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();

            }
            return id > 0 ? true : false;
        }


        //---------------------------------------------------------------------------------------------
        public bool Delete(int id)
        {
            int did = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "sp_DeleteByIdCustomer";
                _command.Parameters.AddWithValue("@custmid", id);
                _connection.Open();
                 did=_command.ExecuteNonQuery();
               
                _connection.Close();
            }

            return did>0? true : false;
        }











    }
}
