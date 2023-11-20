using System.Xml.XPath;
using PPM.DAL;
using PPM.Model;

namespace PPM.Domain
{
    public class EmployeeProjectRepository : IEmployeeProject
    {
        public static List<EmployeeProject> employeeProjectList = new List<EmployeeProject>();

        ProjectDAL projectDAL = new();
        EmployeeDAL employeeDAL = new();
        EmployeeProjectDAL employeeProjectDAL = new();
        public void AddEmployeeToProject(int projectId, int employeeId)
        {
            EmployeeProject employeeProject = new EmployeeProject();
            // var ProjectDetail = ProjectRepository.projectList.FirstOrDefault(
            //     project => project.ProjectId == projectId
            // );

            // var EmployeeDetail = EmployeeRepository.employeeList.FirstOrDefault(
            //     employee => employee.EmployeeID == employeeId
            // );

            // if (ProjectDetail != null)
            // {
            //     employeeProject.ProjectId = ProjectDetail.ProjectId;
            //     employeeProject.ProjectName = ProjectDetail.ProjectName;
            // }

            // if (EmployeeDetail != null)
            // {
            //     employeeProject.EmployeeID = EmployeeDetail.EmployeeID;
            //     employeeProject.FirstName = EmployeeDetail.FirstName;
            //     employeeProject.RoleId = EmployeeDetail.RoleId;
            // }

            Project project = projectDAL.ViewByID(projectId);

            employeeProject.ProjectId = project.ProjectId;
            employeeProject.ProjectName = project.ProjectName;

            Employee employee = employeeDAL.ViewById(employeeId);

            employeeProject.EmployeeID = employee.EmployeeID;
            employeeProject.FirstName = employee.FirstName;
            employeeProject.RoleId = employee.RoleId;

            employeeProjectDAL.AddEmployeeToProject(employeeProject);
            //employeeProjectList.Add(employeeProject);
        }

        public void RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            // int ProjectToRemove = employeeProjectList.FindIndex(
            //     project => project.ProjectId == projectId && project.EmployeeID == employeeId
            // );

            // if (ProjectToRemove >= 0)
            // {
            //     employeeProjectList.RemoveAt(ProjectToRemove);
            // }

            employeeProjectDAL.RemoveEmployeeFromProject(projectId, employeeId);
        }

        public List<EmployeeProject> ViewEmployeeProject()
        {
            var employeeProject = employeeProjectDAL.ViewAll();
            return employeeProject;
        }

        public List<EmployeeProject> ViewEmployeeInProject(int projectId)
        {
            // var employeeInProject = employeeProjectList.FindAll(
            //     employeeProject => employeeProject.ProjectId == projectId
            // );

            var employeeInProject = employeeProjectDAL.ViewEmployeeInProject(projectId);

            return employeeInProject;
            
        }

        public List<EmployeeProject> ViewEmployeeProjects()
        {
            return employeeProjectList;
        }

        public bool ValidEmployeeProject(int projectId, int employeeId)
        {
            return employeeProjectDAL.ValidEmployeeProject(projectId, employeeId);
        }
    }
}
