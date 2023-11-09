using PPM.Domain;
using PPM.Model;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace PPM.UiConsole
{
    public class ProjectConsole
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();
        ProjectRepository projectRepository = new ProjectRepository();
        EmployeeProjectRepository employeeProjectRepository = new EmployeeProjectRepository();
        EmployeeConsole employeeConsole = new();

        string server =
            "Server = RHJ-9F-D217\\SQLEXPRESS; Database = ProlificsProjectManager; Integrated Security=SSPI;";

        public int ProjectModule()
        {
            int choice = 0;

            System.Console.WriteLine("--------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("--                   Project Module                    --");
            Console.ResetColor();
            System.Console.WriteLine("---------------------------------------------------------");

            System.Console.WriteLine("--           Enter 1. to Add Project                  --");
            System.Console.WriteLine("--           Enter 2. to View All Projects            --");
            System.Console.WriteLine("--           Enter 3. to View Project by ID           --");
            System.Console.WriteLine("--           Enter 4. to Delete Project               --");
            System.Console.WriteLine("--           Enter 5. for Employee to Project Module  --");
            System.Console.WriteLine("--           Enter 6. to Return to Main Menu          --");

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

            return choice;
        }

        public void AddProject()
        {
            int projectId;
            DateTime endDate, projectEndDate;
            while (true)
            {
                System.Console.Write("Enter the Project ID : ");
                projectId = int.Parse(Console.ReadLine());

                using (SqlConnection connection = new SqlConnection(server))
                {
                    connection.Open();
                    string cmd = $"SELECT COUNT(*) FROM Project WHERE ProjectId = '{projectId}';";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        int count = (int)command.ExecuteScalar();
                        if (count != 0)
                        {
                            System.Console.WriteLine("\n--   Project Exists, Please enter a valid PROJECT ID !!!   --\n");
                            continue;
                        }
                    }
                }

                // bool result = projectRepository.IsValidProject(projectId);
                // if (result) { 
                //      System.Console.WriteLine(
                //         "\n--   Project Exists, Please enter a valid PROJECT ID !!!   --\n"
                //     );
                //     continue;
                // }
                
                break;
            }

            System.Console.Write("Enter the Project Name : ");
            string projectName = Console.ReadLine();

            System.Console.Write("Enter the project start date : ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            while (true)
            {
                System.Console.Write("Enter the project end date : ");
                projectEndDate = DateTime.Parse(Console.ReadLine());

                if (IsValidEndDate(startDate, projectEndDate))
                {
                    endDate = projectEndDate;
                }
                else
                {
                    System.Console.WriteLine(
                        "Project End date should not be less than start date ! "
                    );
                    continue;
                }
                break;
            }
            Project project = new Project
            {
                ProjectId = projectId,
                ProjectName = projectName,
                StartDate = startDate,
                EndDate = endDate
            };
            projectRepository.Add(project);

            System.Console.WriteLine("\n--   Project added successfully !!!   --\n");

            // Adding Employee to project.
            bool choice = true;
            while (choice)
            {
                System.Console.Write("Do you want to add Employees into the project ? (y/n) : ");
                string request = Console.ReadLine();

                if (
                    request == "y"
                    || request == "yes"
                    || request == "Yes"
                    || request == "Y"
                    || request == "YES"
                )
                {
                    if (EmployeeRepository.employeeList.Count == 0)
                    {
                        System.Console.WriteLine(
                            "\n--   Employee does not Exist, Please enter Employees !!!   --\n"
                        );
                        return;
                    }
                    System.Console.WriteLine();
                    System.Console.WriteLine("The available employees are : ");
                    employeeConsole.ViewEmployee();
                    System.Console.WriteLine();
                    int employeeId;
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        System.Console.Write("Enter the Employee ID : ");
                        employeeId = int.Parse(Console.ReadLine());
                        Console.ResetColor();

                        var valid = employeeRepository.IsValidEmployee(employeeId);
                        if (valid == false)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            System.Console.WriteLine(
                                "\n--   Employee Does Not Exist, Please enter a VALID EMPLOYEE ID !!!   --\n"
                            );
                            Console.ResetColor();
                            continue;
                        }

                        var ValidEmployeeProject =
                            EmployeeProjectRepository.employeeProjectList.Any(
                                p => p.ProjectId == projectId && p.EmployeeID == employeeId
                            );

                        if (ValidEmployeeProject)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            System.Console.WriteLine(
                                "\n--   Employee exists in the project, Enter a valid employee !!!   --\n"
                            );
                            Console.ResetColor();
                            continue;
                        }

                        employeeProjectRepository.AddEmployeeToProject(projectId, employeeId);
                        break;
                    }
                }
                else
                {
                    System.Console.WriteLine("Exitting ....");
                    choice = false;
                }
            }
        }

        public void ViewProjects()
        {
            var projects = projectRepository.ViewAll();
            foreach (Project p in projects)
            {
                System.Console.WriteLine(
                    $"Project ID : {p.ProjectId}, Project Name : {p.ProjectName}, Start Date : {p.StartDate}, End Date : {p.EndDate}"
                );
            }
        }

        public bool IsValidEndDate(DateTime pStartDate, DateTime pEndDate)
        {
            if (pStartDate < pEndDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ViewProjectByID()
        {
            System.Console.Write("Enter the Project ID to be searched : ");
            int projectId = int.Parse(Console.ReadLine());

            var viewProject = projectRepository.ViewByID(projectId);
            if (viewProject != null)
            {
                System.Console.WriteLine(
                    $"Project ID : {viewProject.ProjectId}, Project Name : {viewProject.ProjectName}, Start Date : {viewProject.StartDate}, End Date : {viewProject.EndDate}"
                );
            }
            else
            {
                System.Console.WriteLine(
                    "\n--   Project does not Exist, Please enter a valid PROJECT ID !!!   --\n"
                );
            }
        }

        public void DeleteProjectByID()
        {
            if (ProjectRepository.projectList.Count == 0)
            {
                System.Console.WriteLine(
                    "\n--   Project does not Exist, Please enter a valid PROJECTS !!!   --\n"
                );
                return;
            }

            int projectId;

            System.Console.Write("Enter the Project ID to be deleted : ");
            projectId = int.Parse(Console.ReadLine());

            bool result = projectRepository.IsValidProject(projectId);
            if (result == false)
            {
                System.Console.WriteLine(
                    "\n--   Project does not Exist, Please enter a valid PROJECT ID !!!   --\n"
                );
            }
            else
            {
                projectRepository.DeleteByID(projectId);
                System.Console.WriteLine("\n--   Project removed successfully !!!   --\n");
            }
        }

        public void Default()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("\n--   Please enter a valid choice !!!   --\n");
            Console.ResetColor();
        }
    }
}
