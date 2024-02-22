using PPM.Model;
using System.Data;
using System.Data.SqlClient;

namespace PPM.DAL;

public class ProjectDAL

{
    public static List<Project> projectList = new List<Project>();

    string connectionString = "Server = RHJ-9F-D217\\SQLEXPRESS; Database = ProlificsProjectManager; Integrated Security=SSPI;";

    public void AddProject(Project project)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd =
                $"INSERT INTO Project (ProjectId, ProjectName, StartDate, EndDate) VALUES (@ProjectId, @ProjectName, @StartDate, @EndDate)";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                command.Parameters.AddWithValue("@ProjectId", project.ProjectId);
                command.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                command.Parameters.AddWithValue("@StartDate", project.StartDate);
                command.Parameters.AddWithValue("@EndDate", project.EndDate);

                command.ExecuteNonQuery();
            }
        }
    }

    public List<Project> ViewAll()
    {
        projectList.Clear();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"SELECT * FROM Project";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Project project = new()
                        {
                            ProjectId = Convert.ToInt32(dataReader["ProjectId"]),
                            ProjectName = dataReader["ProjectName"].ToString(),
                            StartDate = Convert.ToDateTime(dataReader["StartDate"]),
                            EndDate = Convert.ToDateTime(dataReader["EndDate"])
                        };

                        projectList.Add(project);
                    }
                }
            }
            return projectList;
        }
    }

    public Project ViewByID(int projectId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string cmd = $"SELECT * FROM Project WHERE ProjectId = {projectId}";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                Project project = new();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        project.ProjectId = Convert.ToInt32(dataReader["ProjectId"]);
                        project.ProjectName = dataReader["ProjectName"].ToString();
                        project.StartDate = Convert.ToDateTime(dataReader["StartDate"]);
                        project.EndDate = Convert.ToDateTime(dataReader["EndDate"]);

                    }

                }
                command.ExecuteNonQuery();
                return project;
            }
        }
    }

    public void DeleteByID(int projectId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"DELETE Project WHERE ProjectId = {projectId}";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public bool IsValidProject(int projectId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"SELECT COUNT(*) FROM Project WHERE ProjectId = '{projectId}';";
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

   
    public bool ProjectCount()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string cmd = $"SELECT COUNT(*) FROM Project;";
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

    public bool ProjectInEmployeeProject(int projectId)
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
