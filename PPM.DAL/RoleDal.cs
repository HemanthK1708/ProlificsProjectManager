using PPM.Model;
using System.Data.SqlClient;

namespace PPM.DAL;

public class RoleDAL
{
    public static List<Role> rolesList = new List<Role>();

    string connectionString = "Server = RHJ-9F-D217\\SQLEXPRESS; Database = ProlificsProjectManager; Integrated Security=SSPI;";

    public void Add(Role role)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"INSERT INTO Role (RoleId, RoleName) VALUES (@RoleId, @RoleName)";

            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                command.Parameters.AddWithValue("@RoleId", role.RoleId);
                command.Parameters.AddWithValue("@RoleName", role.RoleName);

                command.ExecuteNonQuery();
            }
        }
    }

    public List<Role> ViewAll()
    {
        rolesList.Clear();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"SELECT * FROM Role";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Role role = new()
                        {
                            RoleId = Convert.ToInt32(dataReader["RoleId"]),
                            RoleName = dataReader["RoleName"].ToString(),
                        };

                        rolesList.Add(role);
                    }
                }
            }
            return rolesList;
        }
    }

    public Role ViewByID(int roleId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string cmd = $"SELECT * FROM Role WHERE RoleId = {roleId}";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                Role role = new();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        role.RoleId = Convert.ToInt32(dataReader["RoleId"]);
                        role.RoleName = dataReader["RoleName"].ToString();
                    }

                }
                command.ExecuteNonQuery();
                return role;
            }
        }
    }

    public void DeleteByID(int roleId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"DELETE Role WHERE RoleId = {roleId}";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public bool IsValidRole(int roleId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"SELECT COUNT(*) FROM Role WHERE RoleId = '{roleId}';";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                int count = (int)command.ExecuteScalar();
                if (count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    public bool RoleCount()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"SELECT COUNT(*) FROM Role;";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                int count = (int)command.ExecuteScalar();
                if (count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

      public bool RoleProjectCount(int roleId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"SELECT COUNT(*) FROM Employee WHERE RoleId = {roleId};";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                int count = (int)command.ExecuteScalar();
                if (count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
