using System.Diagnostics.CodeAnalysis;
using PPM.DAL;
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

        ProjectDAL projectDAL = new();
        EmployeeDAL employeeDAL = new();
        EmployeeProjectDAL employeeProjectDAL = new();

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
                Console.ForegroundColor = ConsoleColor.Magenta;
                System.Console.WriteLine("\nException Occured : " + ex.Message);
                Console.ResetColor();
            }

            System.Console.WriteLine("\n--------------------------------------------------------");

            // Console.Clear();
            return choice;
        }

        int projectId, employeeId;

        public void AddEmployeeToProject()
        {
            // To check that there is atleast an employee/project inside the lists.

            bool projectCount = projectDAL.ProjectCount();
            bool employeeCount = employeeDAL.EmployeeCount();


            if (projectCount == false)
            {
                if (employeeCount == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\n--   EMPLOYEES does not exists, Please enter a valid PROJECTS  !!!   --\n");
                    Console.ResetColor();
                    return;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("\n--   PROJECTS does not exists, Please enter a valid PROJECTS  !!!   --\n");
                Console.ResetColor();
                return;
            }


            System.Console.WriteLine("The available projects are : ");
            projectConsole.ViewProjects();
            System.Console.WriteLine();

            while (true)
            {
                try
                {
                    System.Console.Write("Enter the Project ID : ");
                    projectId = int.Parse(Console.ReadLine());
                    System.Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    System.Console.WriteLine("\nException Occured : " + ex.Message);
                    Console.ResetColor();
                    continue;
                }

                bool result = projectRepository.IsValidProject(projectId);
                if (result == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(
                        "\n--   Project Does Not Exist, Please enter a valid PROJECT ID !!!   --\n"
                    );
                    Console.ResetColor();
                    continue;
                }

                break;
            }

            System.Console.WriteLine("The available employees are : ");
            employeeConsole.ViewEmployee();
            System.Console.WriteLine();

            while (true)
            {
                try
                {
                    System.Console.Write("Enter the Employee ID : ");
                    employeeId = int.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    System.Console.WriteLine("\nException Occured : " + ex.Message);
                    Console.ResetColor();
                    continue;
                }
            }


            var valid = employeeRepository.IsValidEmployee(employeeId);
            if (valid == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(
                    "\n--   Employee Does Not Exist, Please enter a VALID EMPLOYEE ID !!!   --\n"
                );
                Console.ResetColor();
                // continue;
            }

            // var ValidEmployeeProject = EmployeeProjectRepository.employeeProjectList.Any(
            //     p => p.ProjectId == projectId && p.EmployeeID == employeeId
            // );

            var ValidEmployeeProject = employeeProjectRepository.ValidEmployeeProject(projectId, employeeId);
            if (ValidEmployeeProject)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(
                    "\n--   Employee exists in the project, Please enter a VALID EMPLOYEE ID !!!   --\n"
                );
                Console.ResetColor();
            }
            else
            {
                employeeProjectRepository.AddEmployeeToProject(projectId, employeeId);
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("\n--   Employee added successfully !!!   --\n");
                Console.ResetColor();
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

            // if (ProjectRepository.projectList.Count == 0)
            // {
            //     System.Console.WriteLine(
            //         "\n--   Project does not Exist, Please enter a valid PROJECTS !!!   --\n"
            //     );
            //     return;
            // }
            // else if (EmployeeRepository.employeeList.Count == 0)
            // {
            //     System.Console.WriteLine(
            //         "\n--   Employee does not Exist, Please enter Employees !!!   --\n"
            //     );
            //     return;
            // }
            // else if (EmployeeProjectRepository.employeeProjectList.Count == 0)
            // {
            //     System.Console.WriteLine(
            //         "\n--   Employee to Project does not Exist, Please enter Employees into Project !!!   --\n"
            //     );
            //     return;
            // }
            bool projectCount = projectDAL.ProjectCount();
            bool employeeCount = employeeDAL.EmployeeCount();
            bool employeeProjectCount = employeeProjectDAL.EmployeeProjectCount();

            int projectId = 0, employeeId = 0;

            Console.ForegroundColor = ConsoleColor.Red;
            if (projectCount == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("\n--   PROJECTS does not exists, Please enter a valid PROJECTS  !!!   --\n");
                Console.ResetColor();
                return;
            }
            else if (employeeCount == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("\n--   EMPLOYEES does not exists, Please enter a valid PROJECTS  !!!   --\n");
                Console.ResetColor();
                return;
            }
            else if (employeeProjectCount == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("\n--   Employee to Project does not Exist, Please enter Employees into Project !!!   --\n");
                Console.ResetColor();
                return;
            }
            Console.ResetColor();

            

            System.Console.WriteLine("The available projects are : ");
            projectConsole.ViewProjects();
            System.Console.WriteLine();

            while (true)
            {
                try
                {
                    System.Console.Write("Enter the Project ID : ");
                    projectId = int.Parse(Console.ReadLine());
                    System.Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    System.Console.WriteLine("\nException Occured : " + ex.Message);
                    Console.ResetColor();
                }


                bool result = projectRepository.IsValidProject(projectId);
                if (result == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(
                        "\n--   Project Does Not Exist, Please enter a valid PROJECT ID !!!   --\n"
                    );
                    Console.ResetColor();
                    continue;
                }
                break;
            }

            System.Console.WriteLine("The available employees in the given project are : ");
            ViewEmployeeInProject(projectId);
            if(employeeProjectDAL.EmployeeInProjectCount(projectId) == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("\n--   EMPLOYEES does not exists, Please enter a valid PROJECTS  !!!   --\n");
                Console.ResetColor();
                return;
            }
            System.Console.WriteLine();



            while (true)
            {
                try
                {
                    System.Console.Write("Enter the Employee ID : ");
                    employeeId = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    System.Console.WriteLine("\nException Occured : " + ex.Message);
                    Console.ResetColor();
                }

                var validEmployee = employeeRepository.IsValidEmployee(employeeId);
                if (validEmployee == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\n--   Employee Does Not Exist, Please enter a new EMPLOYEE ID !!!   --\n");
                    Console.ResetColor();
                    continue;
                }

                // var ValidEmployeeProject = EmployeeProjectRepository.employeeProjectList.Any(
                //     p => p.ProjectId == projectId && p.EmployeeID == employeeId
                // );

                var ValidEmployeeProject = employeeProjectRepository.ValidEmployeeProject(projectId, employeeId);

                if (ValidEmployeeProject == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(
                        "\n--   Employee does not exist in the project, Enter a valid employee !!!   --\n"
                    );
                    Console.ResetColor();
                    continue;
                }
                break;
            }

            employeeProjectRepository.RemoveEmployeeFromProject(projectId, employeeId);

            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("\n--   Employee removed successfully !!!   --\n");
            Console.ResetColor();
        }

        public void Default()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("\n--   Please enter a valid choice !!!   --\n");
            Console.ResetColor();
        }

        public void ViewEmployeeInProject(int projectId)
        {
            var employeeInProject = employeeProjectRepository.ViewEmployeeInProject(projectId);

            foreach (var item in employeeInProject)
            {
                System.Console.WriteLine(
                    $"Employee ID : {item.EmployeeID}, Employee First Name : {item.FirstName}, Employee Role ID : {item.RoleId}"
                );
            }
        }
    }
}
