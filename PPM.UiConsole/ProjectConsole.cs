using PPM.DAL;
using PPM.Domain;
using PPM.Model;
using System.Data.SqlClient;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace PPM.UiConsole
{
    public class ProjectConsole
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();
        ProjectRepository projectRepository = new ProjectRepository();
        EmployeeProjectRepository employeeProjectRepository = new EmployeeProjectRepository();
        EmployeeConsole employeeConsole = new();
        EmployeeDAL employeeDAL = new();
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
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("--           Enter 5. for Employee to Project Module  --");
            Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Magenta;
                System.Console.WriteLine("\nException Occured : " + ex.Message);
                Console.ResetColor();
            }

            System.Console.WriteLine("\n--------------------------------------------------------");

            return choice;
        }

        DateTime endDate, projectEndDate, startDate;
        public void AddProject()
        {
            int projectId = 0;
            while (true)
            {
                try
                {

                    System.Console.Write("Enter the Project ID : ");
                    projectId = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    System.Console.WriteLine("\nException Occured : " + ex.Message);
                    System.Console.WriteLine();
                    Console.ResetColor();
                    continue;

                }

                bool result = projectRepository.IsValidProject(projectId);
                if (result)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(
                        "\n--   Project Exists, Please enter a valid PROJECT ID !!!   --\n"
                    );
                    Console.ResetColor();
                    continue;
                }

                break;
            }

            System.Console.Write("Enter the Project Name : ");
            string projectName = Console.ReadLine();


            while (true)
            {
                try
                {

                    System.Console.Write("Enter the project start date : ");
                    startDate = DateTime.Parse(Console.ReadLine());

                    System.Console.Write("Enter the project end date : ");
                    projectEndDate = DateTime.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    System.Console.WriteLine("\nException Occured : " + ex.Message);
                    System.Console.WriteLine();
                    Console.ResetColor();
                    continue;
                }

                if (IsValidEndDate(startDate, projectEndDate))
                {
                    endDate = projectEndDate;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\n--   Project End date should not be less than start date !!!   --\n");
                    Console.ResetColor();
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

            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("\n--   Project added successfully !!!   --\n");
            Console.ResetColor();

            // Adding Employee to project.
            bool choice = true;
            while (choice)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write("Do you want to add Employees into the project ? (y/n) : ");
                string request = Console.ReadLine();
                Console.ResetColor();

                if (
                    request == "y"
                    || request == "yes"
                    || request == "Yes"
                    || request == "Y"
                    || request == "YES"
                )
                {
                    // if (EmployeeRepository.employeeList.Count == 0)
                    bool employeeCount = employeeDAL.EmployeeCount();
                    if (employeeCount == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("\n--   Employee does not Exist, Please enter Employees !!!   --\n");
                        Console.ResetColor();
                        return;
                    }
                    System.Console.WriteLine();
                    System.Console.WriteLine("The available employees are : ");
                    employeeConsole.ViewEmployee();
                    System.Console.WriteLine();
                    int employeeId = 0;

                    while (true)
                    {
                        try
                        {

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            System.Console.Write("Enter the Employee ID : ");
                            employeeId = int.Parse(Console.ReadLine());
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            System.Console.WriteLine("\nException Occured : " + ex.Message);
                            System.Console.WriteLine();
                            Console.ResetColor();

                        }


                        var valid = employeeRepository.IsValidEmployee(employeeId);
                        if (valid == false)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            System.Console.WriteLine("\n--   Employee Does Not Exist, Please enter a VALID EMPLOYEE ID !!!   --\n");
                            Console.ResetColor();
                            continue;
                        }

                        // var ValidEmployeeProject =
                        //     EmployeeProjectRepository.employeeProjectList.Any(
                        //         p => p.ProjectId == projectId && p.EmployeeID == employeeId
                        //     );

                        var ValidEmployeeProject = employeeProjectRepository.ValidEmployeeProject(projectId, employeeId);

                        if (ValidEmployeeProject)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            System.Console.WriteLine("\n--   Employee exists in the project, Enter a valid employee !!!   --\n");
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
            try
            {

                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write("Enter the Project ID to be searched : ");
                int projectId = int.Parse(Console.ReadLine());
                System.Console.WriteLine();
                Console.ResetColor();

                var viewProject = projectRepository.ViewByID(projectId);
                if (viewProject.ProjectId == projectId)
                {
                    System.Console.WriteLine(
                        $"Project ID : {viewProject.ProjectId}, Project Name : {viewProject.ProjectName}, Start Date : {viewProject.StartDate}, End Date : {viewProject.EndDate}"
                    );
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\n--   Project does not Exist, Please enter a valid PROJECT ID !!!   --\n");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                System.Console.WriteLine("\nException Occured : " + ex.Message);
                System.Console.WriteLine();
                Console.ResetColor();

            }
        }

        public void DeleteProjectByID()
        {
            ViewProjects();
            if (ProjectDAL.projectList.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("\n--   Project does not Exist, Please enter a valid PROJECTS !!!   --\n");
                Console.ResetColor();
                return;
            }

            try
            {

                int projectId;

                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write("\nEnter the Project ID to be deleted : ");
                projectId = int.Parse(Console.ReadLine());
                Console.ResetColor();

                bool result = projectRepository.IsValidProject(projectId);
                if (result)
                {
                    if (projectRepository.ProjectInEmployeeProject(projectId))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("\n--   Project cannot be removed as it contains active employees !!!   --\n");
                        Console.ResetColor();
                    }
                    else
                    {
                        projectRepository.DeleteByID(projectId);
                        Console.ForegroundColor = ConsoleColor.Green;
                        System.Console.WriteLine("\n--   Project removed successfully !!!   --\n");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\n--   Project does not Exist, Please enter a valid PROJECT ID !!!   --\n");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                System.Console.WriteLine("\nException Occured : " + ex.Message);
                System.Console.WriteLine();
                Console.ResetColor();

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
