using API.Models;
using System.Data.SqlClient;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string? _connectionString;
        List<Users> listUsers;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServer");
            listUsers = new List<Users>();
        }

        public async Task<IEnumerable<Users>> Get()
        {
            var query = "SELECT * FROM users";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                listUsers.Add(new Users
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Phone = reader.GetString(2)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return listUsers;
        }

        public async Task<IEnumerable<Users>> GetID(int id)
        {
            var query = "SELECT * FROM users WHERE Id=@id";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@id", id);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                listUsers.Add(new Users
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Phone = reader.GetString(2)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return listUsers;
        }

        public async Task<int> Create(Users user)
        {
            var query = "INSERT INTO users (Name, Phone)" +
                        "VALUES (@name, @phone)";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", user.Name);
                        command.Parameters.AddWithValue("@phone", user.Phone);
                        connection.Open();
                        var response = await command.ExecuteNonQueryAsync();
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(int id, Users user)
        {
            var query = "UPDATE users SET Name=@name, Phone=@phone WHERE Id=@id";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@name", user.Name);
                        command.Parameters.AddWithValue("@phone", user.Phone);
                        connection.Open();
                        var response = await command.ExecuteNonQueryAsync();
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Delete(int id)
        {
            var query = "DELETE FROM users WHERE Id=@id";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        connection.Open();
                        var response = await command.ExecuteNonQueryAsync();
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
