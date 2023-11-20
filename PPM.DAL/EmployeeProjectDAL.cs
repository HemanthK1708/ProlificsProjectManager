using PPM.Model;
using System.Data;
using System.Data.SqlClient;

namespace PPM.DAL;

public class EmployeeProjectDAL
{

    public static List<EmployeeProject> employeeProjectList = new List<EmployeeProject>();

    string connectionString = "Server = RHJ-9F-D217\\SQLEXPRESS; Database = ProlificsProjectManager; Integrated Security=SSPI;";

    public void AddEmployeeToProject(EmployeeProject employeeProject)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd =
                $"INSERT INTO EmployeeProject (ProjectId, ProjectName, EmployeeId, FirstName, RoleId) VALUES (@ProjectId, @ProjectName, @EmployeeId, @FirstName, @RoleId)";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                command.Parameters.AddWithValue("@ProjectId", employeeProject.ProjectId);
                command.Parameters.AddWithValue("@ProjectName", employeeProject.ProjectName);
                command.Parameters.AddWithValue("@EmployeeId", employeeProject.EmployeeID);
                command.Parameters.AddWithValue("@FirstName", employeeProject.FirstName);
                command.Parameters.AddWithValue("@RoleId", employeeProject.RoleId);

                command.ExecuteNonQuery();
            }
        }
    }

    public List<EmployeeProject> ViewAll()
    {
        employeeProjectList.Clear();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"SELECT * FROM EmployeeProject";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        EmployeeProject employeeProject = new()
                        {
                            ProjectId = Convert.ToInt32(dataReader["ProjectId"]),
                            ProjectName = dataReader["ProjectName"].ToString(),
                            EmployeeID = Convert.ToInt32(dataReader["EmployeeId"]),
                            FirstName = dataReader["FirstName"].ToString(),
                            RoleId = Convert.ToInt32(dataReader["RoleId"])

                        };

                        employeeProjectList.Add(employeeProject);
                    }
                }
            }
            return employeeProjectList;
        }
    }

    public void RemoveEmployeeFromProject(int projectId, int employeeId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"DELETE EmployeeProject WHERE ProjectId = {projectId} AND EmployeeId = {employeeId}";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public bool ValidEmployeeProject(int projectId, int employeeId)
    {
        string cmd = $"SELECT COUNT(*) FROM EmployeeProject WHERE ProjectId = {projectId} AND EmployeeId = {employeeId};";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                int result = (int)command.ExecuteScalar();

                if (result != 0)
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

    public List<EmployeeProject> ViewEmployeeInProject(int projectId)
    {
        employeeProjectList.Clear();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"SELECT * FROM EmployeeProject WHERE ProjectId = {projectId}";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        EmployeeProject employeeProject = new()
                        {
                            ProjectId = Convert.ToInt32(dataReader["ProjectId"]),
                            ProjectName = dataReader["ProjectName"].ToString(),
                            EmployeeID = Convert.ToInt32(dataReader["EmployeeId"]),
                            FirstName = dataReader["FirstName"].ToString(),
                            RoleId = Convert.ToInt32(dataReader["RoleId"])

                        };

                        employeeProjectList.Add(employeeProject);
                    }
                }
            }
            return employeeProjectList;
        }
    }


    public bool EmployeeProjectCount()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"SELECT COUNT(*) FROM EmployeeProject;";
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

    public bool EmployeeInProject(int employeeId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"SELECT COUNT(*) FROM EmployeeProject WHERE EmployeeId = {employeeId};";
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
    public bool EmployeeInProjectCount(int projectId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"SELECT COUNT(*) FROM EmployeeProject WHERE ProjectId = {projectId};";
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