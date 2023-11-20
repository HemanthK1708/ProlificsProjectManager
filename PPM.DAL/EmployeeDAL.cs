using PPM.Model;
using System.Data;
using System.Data.SqlClient;

namespace PPM.DAL;

public class EmployeeDAL
{
    public static List<Employee> employeeList = new List<Employee>();

    string connectionString = "Server = RHJ-9F-D217\\SQLEXPRESS; Database = ProlificsProjectManager; Integrated Security=SSPI;";

    public void Add(Employee employee)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd =
                $"INSERT INTO Employee (EmployeeId, FirstName, LastName, Email, Mobile, Address, RoleId) VALUES (@EmployeeId, @FirstName, @LastName, @Email, @Mobile, @Address, @RoleId)";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeID);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@Mobile", employee.Mobile);
                command.Parameters.AddWithValue("@Address", employee.Address);
                command.Parameters.AddWithValue("@RoleId", employee.RoleId);

                command.ExecuteNonQuery();
            }
        }
    }

    public List<Employee> ViewAll()
    {
        employeeList.Clear();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"SELECT * FROM Employee";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Employee employee = new()
                        {
                            EmployeeID = Convert.ToInt32(dataReader["EmployeeId"]),
                            FirstName = dataReader["FirstName"].ToString(),
                            LastName = dataReader["LastName"].ToString(),
                            Email = dataReader["Email"].ToString(),
                            Mobile = dataReader["Mobile"].ToString(),
                            Address = dataReader["Address"].ToString(),
                            RoleId = Convert.ToInt32(dataReader["RoleId"])
                        };

                        employeeList.Add(employee);
                    }
                }
            }
            return employeeList;
        }
    }

    public Employee ViewById(int employeeId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string cmd = $"SELECT * FROM Employee WHERE EmployeeId = {employeeId}";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                Employee employee = new();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        employee.EmployeeID = Convert.ToInt32(dataReader["EmployeeId"]);
                        employee.FirstName = dataReader["FirstName"].ToString();
                        employee.LastName = dataReader["LastName"].ToString();
                        employee.Email = dataReader["Email"].ToString();
                        employee.Mobile = dataReader["Mobile"].ToString();
                        employee.Address = dataReader["Address"].ToString();
                        employee.RoleId = Convert.ToInt32(dataReader["RoleId"]);
                    }

                }

                command.ExecuteNonQuery();
                return employee;
            }
        }
    }

    public void DeleteByID(int employeeId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"DELETE Employee WHERE EmployeeId = {employeeId}";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public bool IsValidEmployee(int employeeId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"SELECT COUNT(*) FROM Employee WHERE EmployeeId = '{employeeId}';";
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

    public bool EmployeeCount()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"SELECT COUNT(*) FROM Employee;";
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