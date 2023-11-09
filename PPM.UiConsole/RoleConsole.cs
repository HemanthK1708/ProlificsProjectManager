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
                System.Console.WriteLine("Exception occured : " + ex.Message);
            }

            System.Console.WriteLine("\n--------------------------------------------------------");

            return choice;
        }

        public void AddRole()
        {
            int roleId;
            while (true)
            {
                System.Console.Write("Enter Role ID : ");
                roleId = int.Parse(Console.ReadLine());
                bool result = rolesRepository.IsValidRole(roleId);
                if (result)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
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
            System.Console.Write("Enter the Role ID to be searched : ");
            int roleId = int.Parse(Console.ReadLine());

            var viewRole = rolesRepository.ViewByID(roleId);
            if (viewRole != null)
            {
                System.Console.WriteLine(
                    $"Role Id : {viewRole.RoleId}, Role Name : {viewRole.RoleName}"
                );
            }
            else
            {
                System.Console.WriteLine(
                    "\n--   Role does not Exist, Please enter a valid Role ID !!!   --\n"
                );
            }
        }

        public void DeleteRoleByID()
        {
            int roleId;

            if (RolesRepository.rolesList.Count == 0)
            {
                System.Console.WriteLine(
                    "\n--   Roles does not Exist, Please enter valid Roles !!!   --\n"
                );
                return;
            }

            System.Console.WriteLine("The available roles are : ");
            ViewRole();

            System.Console.Write("\nEnter the Role ID to be removed : ");
            roleId = int.Parse(Console.ReadLine());

            bool result = rolesRepository.IsValidRole(roleId);
            if (result == false)
            {
                System.Console.WriteLine(
                    "\n--   Role does not Exist, Please enter a valid Role ID !!!   --\n"
                );
            }
            else if (EmployeeRepository.employeeList.Any(r => r.RoleId == roleId))
            {
                System.Console.WriteLine(
                    "The selected role cannot be removed as it contains active employees !!!"
                );
            }
            else
            {
                rolesRepository.DeleteByID(roleId);
                System.Console.WriteLine("\n--   Role removed successfully !!!   --\n");
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
