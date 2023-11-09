using PPM.Domain;
using PPM.Model;

namespace PPM.UiConsole
{
    public class EmployeeProjectConsole
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();
        ProjectRepository projectRepository = new ProjectRepository();
        EmployeeConsole employeeConsole = new();
        ProjectConsole projectConsole = new();

        EmployeeProjectRepository employeeProjectRepository = new EmployeeProjectRepository();

        public int EmployeeToProjectModule()
        {
            int choice = 0;

            System.Console.WriteLine("--------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("--              Employee To Project Module             --");
            Console.ResetColor();
            System.Console.WriteLine("---------------------------------------------------------");

            System.Console.WriteLine("--           Enter 1. to Add Employee to Project      --");
            System.Console.WriteLine("--           Enter 2. to Delete Employee from Project --");
            System.Console.WriteLine("--           Enter 3. to View Project Details         --");
            System.Console.WriteLine("--           Enter 4. to Return to Main Menu          --");

            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write("\nEnter your choice : ");
                choice = int.Parse(Console.ReadLine());
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception Occured : " + ex.Message);
            }

            System.Console.WriteLine("\n--------------------------------------------------------");

            // Console.Clear();
            return choice;
        }

        public void AddEmployeeToProject()
        {
            // To check that there is atleast an employee/project inside the lists.
            if (
                ProjectRepository.projectList.Count == 0
                && EmployeeRepository.employeeList.Count == 0
            )
            {
                System.Console.WriteLine(
                    "\n--   PROJECTS/EMPLOYEES does not exists, Please enter a valid PROJECTS/EMPLOYEES  !!!   --\n"
                );
                return;
            }

            int projectId,
                employeeId;
            System.Console.WriteLine("The available projects are : ");
            projectConsole.ViewProjects();
            System.Console.WriteLine();
            while (true)
            {
                System.Console.Write("Enter the Project ID : ");
                projectId = int.Parse(Console.ReadLine());
                System.Console.WriteLine();

                bool result = projectRepository.IsValidProject(projectId);

                if (result == false)
                {
                    System.Console.WriteLine(
                        "\n--   Project Does Not Exist, Please enter a valid PROJECT ID !!!   --\n"
                    );
                    continue;
                }

                break;
            }

            System.Console.WriteLine("The available employees are : ");
            employeeConsole.ViewEmployee();
            System.Console.WriteLine();

           
                System.Console.Write("Enter the Employee ID : ");
                employeeId = int.Parse(Console.ReadLine());

                var valid = employeeRepository.IsValidEmployee(employeeId);
                if (valid == false)
                {
                    System.Console.WriteLine(
                        "\n--   Employee Does Not Exist, Please enter a VALID EMPLOYEE ID !!!   --\n"
                    );
                    // continue;
                }

                var ValidEmployeeProject = EmployeeProjectRepository.employeeProjectList.Any(
                    p => p.ProjectId == projectId && p.EmployeeID == employeeId
                );

                if (ValidEmployeeProject)
                {
                    System.Console.WriteLine(
                        "\n--   Employee exists in the project, Please enter a VALID EMPLOYEE ID !!!   --\n"
                    );
                }
                else
                {
                    employeeProjectRepository.AddEmployeeToProject(projectId, employeeId);
                    System.Console.WriteLine("\n--   Employee added successfully !!!   --\n");
                }

            
        }

        public void ViewEmployeeProject()
        {
            var employeeProjectList = employeeProjectRepository.ViewEmployeeProject();

            foreach (var item in employeeProjectList)
            {
                System.Console.WriteLine(
                    $"Project ID : {item.ProjectId}, Project Name: {item.ProjectName}, Employee ID : {item.EmployeeID}, Employee First Name : {item.FirstName}, Employee Role ID : {item.RoleId}"
                );
            }
        }

        public void RemoveEmployeeFromProject()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (ProjectRepository.projectList.Count == 0)
            {
                System.Console.WriteLine(
                    "\n--   Project does not Exist, Please enter a valid PROJECTS !!!   --\n"
                );
                return;
            }
            else if (EmployeeRepository.employeeList.Count == 0)
            {
                System.Console.WriteLine(
                    "\n--   Employee does not Exist, Please enter Employees !!!   --\n"
                );
                return;
            }
            else if (EmployeeProjectRepository.employeeProjectList.Count == 0)
            {
                System.Console.WriteLine(
                    "\n--   Employee to Project does not Exist, Please enter Employees into Project !!!   --\n"
                );
                return;
            }
            Console.ResetColor();

            int projectId,
                employeeId;

            System.Console.WriteLine("The available projects are : ");
            projectConsole.ViewProjects();
            System.Console.WriteLine();

            while (true)
            {
                System.Console.Write("Enter the Project ID : ");
                projectId = int.Parse(Console.ReadLine());
                System.Console.WriteLine();

                bool result = projectRepository.IsValidProject(projectId);
                if (result == false)
                {
                    System.Console.WriteLine(
                        "\n--   Project Does Not Exist, Please enter a valid PROJECT ID !!!   --\n"
                    );
                    continue;
                }
                break;
            }

            System.Console.WriteLine("The available employees in the given project are : ");
            employeeProjectRepository.ViewEmployeeInProject(projectId);
            System.Console.WriteLine();

            while (true)
            {
                System.Console.Write("Enter the Employee ID : ");
                employeeId = int.Parse(Console.ReadLine());

                var valid = employeeRepository.IsValidEmployee(employeeId);
                if (valid == false)
                {
                    System.Console.WriteLine(
                        "\n--   Employee Does Not Exist, Please enter a new EMPLOYEE ID !!!   --\n"
                    );
                    continue;
                }

                var ValidEmployeeProject = EmployeeProjectRepository.employeeProjectList.Any(
                    p => p.ProjectId == projectId && p.EmployeeID == employeeId
                );

                if (ValidEmployeeProject == false)
                {
                    System.Console.WriteLine(
                        "\n--   Employee does not exist in the project, Enter a valid employee !!!   --\n"
                    );
                    continue;
                }
                break;
            }

            employeeProjectRepository.RemoveEmployeeFromProject(projectId, employeeId);

            System.Console.WriteLine("\n--   Employee removed successfully !!!   --\n");
        }

        public void Default()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("\n--   Please enter a valid choice !!!   --\n");
            Console.ResetColor();
        }
    }
}
