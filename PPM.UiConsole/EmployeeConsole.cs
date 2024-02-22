using PPM.DAL;
using PPM.Domain;
using PPM.Model;
using System.Text.RegularExpressions;

namespace PPM.UiConsole
{
    public class EmployeeConsole
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();
        RolesRepository rolesRepository = new RolesRepository();
        EmployeeProjectDAL employeeProjectDAL = new();
        RoleDAL roleDAL = new();

        public int EmployeeModule()
        {
            int choice = 0;

            System.Console.WriteLine("--------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("--                   Employee Module                   --");
            Console.ResetColor();
            System.Console.WriteLine("---------------------------------------------------------");


            System.Console.WriteLine("--           Enter 1. to Add Employee                 --");
            System.Console.WriteLine("--           Enter 2. to View All Employees           --");
            System.Console.WriteLine("--           Enter 3. to View Employee by ID          --");
            System.Console.WriteLine("--           Enter 4. to Delete Employee              --");
            System.Console.WriteLine("--           Enter 5. to Return to Main Menu          --");

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
        int employeeId, roleId;
        string mobile, email;
        public void AddEmployee()
        {
            bool choice = true;

            if (roleDAL.RoleCount() == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("\n--   Roles does not exists, Please enter a valid ROLES  !!!   --\n");
                Console.ResetColor();
                return;
            }

            while (choice)
            {
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
                        System.Console.WriteLine();
                        Console.ResetColor();
                        continue;
                    }

                    var valid = employeeRepository.IsValidEmployee(employeeId);
                    if (valid)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("\n--   Employee aldready exists, Please enter a new EMPLOYEE ID !!!   --\n");
                        Console.ResetColor();
                        continue;
                    }
                    break;

                }

                System.Console.Write("Enter the First Name : ");
                string firstName = Console.ReadLine();

                System.Console.Write("Enter the Last Name : ");
                string lastName = Console.ReadLine();

                while (true)
                {
                    System.Console.Write("Enter the Email : ");
                    string employeeEmail = Console.ReadLine();

                    if (IsValidEmail(employeeEmail))
                    {
                        email = employeeEmail;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("\nEnter a valid email !!!\n");
                        Console.ResetColor();
                        continue;
                    }
                    break;
                }

                while (true)
                {
                    System.Console.Write("Enter the Mobile No. : ");
                    string phoneNumber = Console.ReadLine();

                    if (IsValidPhoneNumber(phoneNumber))
                    {
                        mobile = phoneNumber;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("\nPlease enter a valid number !!! \n");
                        Console.ResetColor();
                        continue;
                    }
                    break;
                }
                System.Console.Write("Enter the Address : ");
                string address = Console.ReadLine();

                while (true)
                {
                    try
                    {
                        System.Console.Write("Enter the RoleId : ");
                        roleId = int.Parse(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        System.Console.WriteLine("\nException Occured : " + ex.Message);
                        System.Console.WriteLine();
                        Console.ResetColor();
                        continue;
                    }
                    if (rolesRepository.IsValidRole(roleId) == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("\n--   RoleID does not exists, Please enter a valid ROLE ID  !!!   --\n");
                        Console.ResetColor();
                        continue;
                    }
                    break;
                }

                Employee employee = new Employee
                {
                    EmployeeID = employeeId,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Mobile = mobile,
                    Address = address,
                    RoleId = roleId
                };

                employeeRepository.Add(employee);

                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("\n--   Employee added successfully !!!   --\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write("Do you want to add more employees y/n : ");
                string c = Console.ReadLine();
                System.Console.WriteLine();
                Console.ResetColor();

                if (c == "n" || c == "N" || c == "no" || c == "No" || c == "NO")
                {
                    choice = false;
                }
            }
        }

        public void ViewEmployee()
        {
            var employees = employeeRepository.ViewAll();

            foreach (Employee employee in employees)
            {
                System.Console.WriteLine($"Employee ID : {employee.EmployeeID}, First Name : {employee.FirstName}, Last Name : {employee.LastName}, Email : {employee.Email}, Mobile : {employee.Mobile}, Address : {employee.Address}, RoleID : {employee.RoleId}");
            }
        }

        public void ViewEmployeeByID()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write("Enter the Employee ID to be searched : ");
                int employeeId = int.Parse(Console.ReadLine());
                Console.ResetColor();

                var viewEmployee = employeeRepository.ViewByID(employeeId);
                if (viewEmployee.EmployeeID == employeeId)
                {
                    System.Console.WriteLine($"Employee ID : {viewEmployee.EmployeeID}, First Name : {viewEmployee.FirstName}, Last Name : {viewEmployee.LastName}, Email : {viewEmployee.Email}, Mobile : {viewEmployee.Mobile}, Address : {viewEmployee.Address}, RoleID : {viewEmployee.RoleId}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\n--   Employee does not Exist, Please enter a valid Employee ID !!!   --\n");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                System.Console.WriteLine("\nException Occured : " + ex.Message);
                Console.ResetColor();
            }

        }

        public void DeleteEmployeeByID()
        {
            System.Console.WriteLine("The available employees are : ");
            ViewEmployee();

            if (EmployeeDAL.employeeList.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("\n--   Employee does not Exist, Please enter Employees !!!   --\n");
                Console.ResetColor();
                return;
            }

            try
            {
                System.Console.WriteLine();
                int employeeId;

                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write("Enter the Employee ID to be removed : ");
                employeeId = int.Parse(Console.ReadLine());
                Console.ResetColor();

                bool result = employeeRepository.IsValidEmployee(employeeId);

                Console.ForegroundColor = ConsoleColor.Red;
                if (result == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\n--   Employee does not Exist, Please enter a valid Employee ID !!!   --\n");
                    Console.ResetColor();
                }
                // else if (EmployeeProjectRepository.employeeProjectList.Any(r => r.EmployeeID == employeeId))
                else if (employeeProjectDAL.EmployeeInProject(employeeId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\n--  The Employee cannot be deleted as he in enrolled in the project !!! --");
                    Console.ResetColor();
                }
                else
                {
                    employeeRepository.DeleteByID(employeeId);
                    Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("\n--   Employee removed successfully !!!   --\n");
                }
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                System.Console.WriteLine("\nException Occured : " + ex.Message);
                Console.ResetColor();
            }

        }

        public void Default()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("\n--   Please enter a valid choice !!!   --\n");
            Console.ResetColor();
        }

        public static bool IsValidPhoneNumber(string mobileNumber)
        {
            if (mobileNumber != null)
            {
                string phoneExpression = @"^(\+91[\-\s]?)?[0]?(91)?[789]\d{9}$";
                return Regex.IsMatch(mobileNumber, phoneExpression);
            }
            else
            {
                return false;
            }
        }

        public bool IsValidEmail(string empemail)
        {
            if (empemail != null)
            {
                string emailExpression =
                    @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                return Regex.IsMatch(empemail, emailExpression);
            }
            else
            {
                return false;
            }
        }
    }
}
