using System;
using System.Data.SqlClient;
using DB_Connection.Models;

namespace DB_Connection
{
    class Program
    {
        SqlConnection sqlConnection;

        string connectionString = "Data Source=DESKTOP-PQO8BSH;Initial Catalog=Tokoku;User ID=me;Password=12345678";

        static void Main(string[] args)
        {
            Program program = new Program();

            program.GetById(1);

            Product product = new Product("iPad 6 mini", 20, 6000000);
            
            program.GetAll();
            program.Insert(product);

            Product newProduct = new Product("iPad 9 SE", 59, 8000000);
            program.Update(1, newProduct);
            program.Delete(2);
        }

        void GetAll()
        {
            string query = "SELECT * FROM Products";

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine(sqlDataReader[0] + " - " + sqlDataReader[1] + " - " + sqlDataReader[2] + " - " + sqlDataReader[3]);
                        }
                        Console.WriteLine(" ");
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        void GetById(int id)
        {
            string query = "SELECT * FROM Products WHERE Id = @id";

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@id";
            sqlParameter.Value = id;

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.Add(sqlParameter);
            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine(sqlDataReader[0] + " - " + sqlDataReader[1] + " - " + sqlDataReader[2] + " - " + sqlDataReader[3]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        void Insert(Product product)
        {
            Program program = new Program();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter name = new SqlParameter("@name",product.Name);
                sqlCommand.Parameters.Add(name);

                SqlParameter stock = new SqlParameter("@stock", product.Stock);
                sqlCommand.Parameters.Add(stock);

                SqlParameter price = new SqlParameter("@price", product.Price);
                sqlCommand.Parameters.Add(price);

                try
                {
                    sqlCommand.CommandText = "INSERT INTO Products " +
                        " VALUES (@name,@stock,@price)";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    Console.WriteLine("-------Insert Berhasil-------");
                    program.GetAll();


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
        }

        // Update
        void Update(int productId, Product product)
        {
            Program program = new Program();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter id = new SqlParameter("@id", productId);
                sqlCommand.Parameters.Add(id);

                SqlParameter name = new SqlParameter("@name", product.Name);
                sqlCommand.Parameters.Add(name);

                SqlParameter stock = new SqlParameter("@stock", product.Stock);
                sqlCommand.Parameters.Add(stock);

                SqlParameter price = new SqlParameter("@price", product.Price);
                sqlCommand.Parameters.Add(price);

                try
                {
                    sqlCommand.CommandText = "UPDATE Products " +
                        "SET Name = @name, Stock = @stock, Price = @price " + " WHERE Id = @id; ";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    sqlConnection.Close();
                    Console.WriteLine("-------Update Berhasil-------");
                    program.GetAll();
                    

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
               
            }
        }
        // Delete
        void Delete(int productId)
        {
            Program program = new Program();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter id = new SqlParameter("@id", productId);
                sqlCommand.Parameters.Add(id);


                try
                {
                    sqlCommand.CommandText = "DELETE FROM dbo.Products WHERE Id = @id; ";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    sqlConnection.Close();
                    Console.WriteLine("-------Delete Berhasil-------");
                    program.GetAll();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("-------Delete Gagal-------");
                    Console.WriteLine(ex.InnerException);
                }
                
            }
        }
    }
}

