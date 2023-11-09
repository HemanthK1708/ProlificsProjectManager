using PPM.Model;

namespace PPM.Domain
{
    public class ProjectRepository : IEntity<Project>
    {
        public static List<Project> projectList = new List<Project>();

        public void Add(Project project)
        {
           projectList.Add(project);
        }

        public bool IsValidProject(int projectId)
        {
            bool result = projectList.Exists(p => p.ProjectId == projectId);
            return result;
        }

        public List<Project> ViewAll()
        {
          return projectList;
        }


        public Project ViewByID(int projectId)
        {
            Project projectByID = projectList.FirstOrDefault(p => p.ProjectId == projectId);

            return projectByID;
        }

        public void DeleteByID(int projectId)
        {
            projectList.RemoveAll(item => item.ProjectId == projectId);
        }
    }
}