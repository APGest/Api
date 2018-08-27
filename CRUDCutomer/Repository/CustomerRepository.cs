using CRUDCustomer.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDCutomer.Repository
{
    public class CustomerRepository
    {
        public List<Customer> ReadAll()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection("Server=ANDRZEJPC\\SQLEXPRESS; Database = Customers; Integrated Security=true;"))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT Id,Name,SurName,PhoneNumber,Address FROM dbo.Customer";
                cmd.Connection = conn;
                conn.Open();
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        customers.Add(new Customer()
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            Name = reader["Name"].ToString(),
                            SurName = reader["SurName"].ToString(),
                            PhoneNumber = int.Parse(reader["PhoneNumber"].ToString()),
                            Address = reader["Address"].ToString(),
                        });
                    }
                }
                return customers;
            }
        }

        public Customer Read(int id)
        {
            using (SqlConnection conn = new SqlConnection("Server=ANDRZEJPC\\SQLEXPRESS; Database = Customers; Integrated Security=true;"))
            {
                Customer customer = new Customer();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = (@"SELECT Id,Name,SurName,PhoneNumber,Address FROM Customer WHERE Customer.Id=@Id;");

                cmd.Parameters.AddWithValue("@Id", id);

                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customer = new Customer
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        SurName = reader["SurName"].ToString(),
                        PhoneNumber = int.Parse(reader["PhoneNumber"].ToString()),
                        Address = reader["Address"].ToString(),
                    };
                }
                conn.Close();
                return customer;
            }
        }

        public void Create(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection("Server=ANDRZEJPC\\SQLEXPRESS; Database = Customers; Integrated Security=true;"))
            {
                String query = "INSERT INTO Customer (Name,SurName,PhoneNumber,Address) VALUES (@Name,@SurName,@PhoneNumber,@Address)";

                using (SqlCommand command = new SqlCommand(query, conn))
                {

                    command.Parameters.AddWithValue("@Name", customer.Name);
                    command.Parameters.AddWithValue("@SurName", customer.SurName);
                    command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    command.Parameters.AddWithValue(@"Address", customer.Address);

                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(Customer customer, int id)
        {
            using (SqlConnection conn = new SqlConnection("Server=ANDRZEJPC\\SQLEXPRESS; Database = Customers; Integrated Security=true;"))
            {
                String query = "UPDATE Customer SET Name=@Name, SurName=@SurName, PhoneNumber=@PhoneNumber, Address=@Address WHERE Id=@Id";
                              

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Id", customer.Id);
                    command.Parameters.AddWithValue("@Name", customer.Name);
                    command.Parameters.AddWithValue("@SurName", customer.SurName);
                    command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", customer.Address);

                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection("Server=ANDRZEJPC\\SQLEXPRESS; Database = Customers; Integrated Security=true;"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();

                using (SqlCommand delete = new SqlCommand("DELETE FROM Customer WHERE Id=@Id", conn))
                {
                    delete.Parameters.AddWithValue("@Id", id);
                    delete.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}

