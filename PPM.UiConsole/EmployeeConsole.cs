using PPM.Domain;
using PPM.Model;
using System.Text.RegularExpressions;

namespace PPM.UiConsole
{
    public class EmployeeConsole
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();
        RolesRepository rolesRepository = new RolesRepository();
        public int EmployeeModule()
        {

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

            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.Write("\nEnter your choice : ");
            int choice = int.Parse(Console.ReadLine());
            Console.ResetColor();

            System.Console.WriteLine("\n--------------------------------------------------------");

            // Console.Clear();
            return choice;
        }
        public void AddEmployee()
        {
            bool choice = true;

            if (RolesRepository.rolesList.Count == 0)
            {
                System.Console.WriteLine("\n--   Roles does not exists, Please enter a valid ROLES  !!!   --\n");
                return;
            }
            while (choice)
            {
                int employeeId, roleId;
                string mobile,
                    email;
                while (true)
                {
                    System.Console.Write("Enter the Employee ID : ");
                    employeeId = int.Parse(Console.ReadLine());

                    var valid = employeeRepository.IsValidEmployee(employeeId);
                    if (valid)
                    {
                        System.Console.WriteLine("\n--   Employee aldready exists, Please enter a new EMPLOYEE ID !!!   --\n");
                        continue;
                    }
                    break;

                    //string cmd = "SELECT COUNT(*) FROM Project W"
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
                        System.Console.WriteLine("\nEnter a valid email !!!\n");
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
                        System.Console.WriteLine("\nPlease enter a valid number !!! \n");
                        continue;
                    }
                    break;
                }
                System.Console.Write("Enter the Address : ");
                string address = Console.ReadLine();

                while (true)
                {
                    System.Console.Write("Enter the RoleId : ");
                    roleId = int.Parse(Console.ReadLine());
                    if (rolesRepository.IsValidRole(roleId) == false)
                    {
                        System.Console.WriteLine("\n--   RoleID does not exists, Please enter a valid ROLE ID  !!!   --\n");
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
            System.Console.Write("Enter the Employee ID to be searched : ");
            int employeeId = int.Parse(Console.ReadLine());


            var viewEmployee = employeeRepository.ViewByID(employeeId);
            if (viewEmployee != null)
            {
                System.Console.WriteLine($"Employee ID : {viewEmployee.EmployeeID}, First Name : {viewEmployee.FirstName}, Last Name : {viewEmployee.LastName}, Email : {viewEmployee.Email}, Mobile : {viewEmployee.Mobile}, Address : {viewEmployee.Address}, RoleID : {viewEmployee.RoleId}");
            }
            else
            {
                System.Console.WriteLine("\n--   Employee does not Exist, Please enter a valid Employee ID !!!   --\n");
            }
        }

        public void DeleteEmployeeByID()
        {
            if (EmployeeRepository.employeeList.Count == 0)
            {
                System.Console.WriteLine("\n--   Employee does not Exist, Please enter Employees !!!   --\n");
                return;
            }

            System.Console.WriteLine("The available employees are : ");
            ViewEmployee();
            System.Console.WriteLine();
            int employeeId;

            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.Write("Enter the Employee ID to be removed : ");
            employeeId = int.Parse(Console.ReadLine());
            Console.ResetColor();

            bool result = employeeRepository.IsValidEmployee(employeeId);
            if (result == false)
            {
                System.Console.WriteLine("\n--   Employee does not Exist, Please enter a valid Employee ID !!!   --\n");
            }
            else if (EmployeeProjectRepository.employeeProjectList.Any(r => r.EmployeeID == employeeId))
            {
                System.Console.WriteLine("\nThe Employee cannot be deleted as he in enrolled in the project.");
            }
            else
            {
                employeeRepository.DeleteByID(employeeId);
                System.Console.WriteLine("\n--   Employee removed successfully !!!   --\n");
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
