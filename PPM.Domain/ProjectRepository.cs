using PPM.Model;
using PPM.DAL;
namespace PPM.Domain
{
    public class ProjectRepository : IEntity<Project>
    {
        public static List<Project> projectList = new List<Project>();

        ProjectDAL projectDAL = new ProjectDAL();
        public void Add(Project project)
        {
            projectDAL.AddProject(project);
            //projectList.Add(project);
        }

        public bool IsValidProject(int projectId)
        {
            bool result = projectDAL.IsValidProject(projectId);
            return result;
        }

        public List<Project> ViewAll()
        {
            var projectL = projectDAL.ViewAll();
            return projectL;
        }

        public Project ViewByID(int projectId)
        {
            // Project projectByID = projectList.FirstOrDefault(p => p.ProjectId == projectId);
            Project projectByID = projectDAL.ViewByID(projectId);
            return projectByID;
        }

        public void DeleteByID(int projectId)
        {
            // projectList.RemoveAll(item => item.ProjectId == projectId);
            projectDAL.DeleteByID(projectId);
        }

        public bool ProjectInEmployeeProject(int projectId)
        {
            return projectDAL.ProjectInEmployeeProject(projectId);
        }
    }
}
