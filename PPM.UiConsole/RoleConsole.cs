using PPM.DAL;
using PPM.Domain;
using PPM.Model;
using System.Data;
using System.Text.RegularExpressions;

namespace PPM.UiConsole
{
    public class RoleConsole
    {
        RolesRepository rolesRepository = new RolesRepository();

        public int RoleModule()
        {
            int choice = 0;

            System.Console.WriteLine("--------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("--                    Role Module                     --");
            Console.ResetColor();
            System.Console.WriteLine("---------------------------------------------------------");

            System.Console.WriteLine("--           Enter 1. to Add Role                     --");
            System.Console.WriteLine("--           Enter 2. to View All Roles               --");
            System.Console.WriteLine("--           Enter 3. to View Roles by ID             --");
            System.Console.WriteLine("--           Enter 4. to Delete Roles                 --");
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

            return choice;
        }

        public void AddRole()
        {
            int roleId;
            while (true)
            {
                try
                {
                    System.Console.Write("Enter Role ID : ");
                    roleId = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    System.Console.WriteLine("\nException Occured : " + ex.Message);
                    Console.ResetColor();
                    System.Console.WriteLine();
                    continue;
                }
                bool result = rolesRepository.IsValidRole(roleId);
                if (result)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(
                        "\n--   RoleID exists, Please enter a valid ROLE ID !!!   --\n"
                    );
                    Console.ResetColor();
                    continue;
                }
                break;
            }
            System.Console.Write("Enter the Role Name : ");
            string roleName = Console.ReadLine();

            Role roles = new Role { RoleId = roleId, RoleName = roleName };

            rolesRepository.Add(roles);
        }

        public void ViewRole()
        {
            var roles = rolesRepository.ViewAll();
            foreach (Role role in roles)
            {
                System.Console.WriteLine($"Role Id : {role.RoleId}, Role Name : {role.RoleName}");
            }
        }

        public void ViewRoleByID()
        {
            try
            {
                System.Console.Write("Enter the Role ID to be searched : ");
                int roleId = int.Parse(Console.ReadLine());
                System.Console.WriteLine();

                var viewRole = rolesRepository.ViewByID(roleId);
                if (viewRole.RoleId == roleId)
                {
                    System.Console.WriteLine(
                        $"Role Id : {viewRole.RoleId}, Role Name : {viewRole.RoleName}"
                    );
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\n--   Role does not Exist, Please enter a valid Role ID !!!   --\n");
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

        public void DeleteRoleByID()
        {
            System.Console.WriteLine("The available roles are : ");
            ViewRole();

            int roleId;
            if (RoleDAL.rolesList.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("\n--   Roles does not Exist, Please enter valid Roles !!!   --\n");
                Console.ResetColor();

                return;
            }

            // System.Console.WriteLine("The available roles are : ");
            // ViewRole();
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write("\nEnter the Role ID to be removed : ");
                roleId = int.Parse(Console.ReadLine());
                Console.ResetColor();

                bool result = rolesRepository.IsValidRole(roleId);
                if (result == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\n--   Role does not Exist, Please enter a valid Role ID !!!   --\n");
                    Console.ResetColor();
                }
                // else if (EmployeeRepository.employeeList.Any(r => r.RoleId == roleId))
                else if (rolesRepository.RoleProjectCount(roleId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\n--   The selected role cannot be removed as it contains active employees !!!   --\n");
                    Console.ResetColor();
                }
                else
                {
                    rolesRepository.DeleteByID(roleId);

                    Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("\n--   Role removed successfully !!!   --\n");
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

        public void Default()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("\n--   Please enter a valid choice !!!   --\n");
            Console.ResetColor();
        }
    }
}
