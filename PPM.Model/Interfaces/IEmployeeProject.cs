namespace PPM.Model
{
    public interface IEmployeeProject
    {
        public void AddEmployeeToProject(int projectId, int employeeId);

        public void RemoveEmployeeFromProject(int projectId, int employeeId);

        public List<EmployeeProject> ViewEmployeeProject();

        public List<EmployeeProject> ViewEmployeeInProject(int projectId);

        public List<EmployeeProject> ViewEmployeeProjects();
    }
}