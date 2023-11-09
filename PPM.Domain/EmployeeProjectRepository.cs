using PPM.Model;

namespace PPM.Domain
{
    public class EmployeeProjectRepository : IEmployeeProject
    {
        public static List<EmployeeProject> employeeProjectList = new List<EmployeeProject>();

        public void AddEmployeeToProject(int projectId, int employeeId)
        {
            EmployeeProject employeeProject = new EmployeeProject();
            var ProjectDetail = ProjectRepository.projectList.FirstOrDefault(
                project => project.ProjectId == projectId
            );

            var EmployeeDetail = EmployeeRepository.employeeList.FirstOrDefault(
                employee => employee.EmployeeID == employeeId
            );

            if (ProjectDetail != null)
            {
                employeeProject.ProjectId = ProjectDetail.ProjectId;
                employeeProject.ProjectName = ProjectDetail.ProjectName;
            }

            if (EmployeeDetail != null)
            {
                employeeProject.EmployeeID = EmployeeDetail.EmployeeID;
                employeeProject.FirstName = EmployeeDetail.FirstName;
                employeeProject.RoleId = EmployeeDetail.RoleId;
            }

            employeeProjectList.Add(employeeProject);
        }

        public void RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            int ProjectToRemove = employeeProjectList.FindIndex(
                project => project.ProjectId == projectId && project.EmployeeID == employeeId
            );

            if (ProjectToRemove >= 0)
            {
                employeeProjectList.RemoveAt(ProjectToRemove);
            }
        }

        public List<EmployeeProject> ViewEmployeeProject()
        {
            return employeeProjectList;
        }

        public void ViewEmployeeInProject(int projectId)
        {
            var employeeInProject = employeeProjectList.FindAll(
                employeeProject => employeeProject.ProjectId == projectId
            );

            foreach (var item in employeeInProject)
            {
                System.Console.WriteLine(
                    $"Employee ID : {item.EmployeeID}, Employee First Name : {item.FirstName}, Employee Role ID : {item.RoleId}"
                );
            }
        }

        public List<EmployeeProject> ViewEmployeeProjects()
        {
            return employeeProjectList;
        }
    }
}
