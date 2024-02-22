using System;
using Microsoft.Win32;
using System.Text.RegularExpressions;

using PPM.Domain;
using PPM.UiConsole;

namespace PPM.Main
{
    public class Program
    {
        static EmployeeConsole employeeConsole = new();
        static EmployeeProjectConsole employeeProjectConsole = new();
        static RoleConsole roleConsole = new();
        static ProjectConsole projectConsole = new();
        static UIConsole uIConsole = new();

        public static void Main(string[] args)
        {
            Console.Clear();
            UIConsole.Title();

            bool choice = true;
            do
            {
                bool projectLoop = true,
                    roleLoop = true,
                    employeeLoop = true,
                    employeeToProjectLoop = true;
                int switchChoice = uIConsole.MenuDriven();
                switch (switchChoice)
                {
                    case 1:
                        do
                        {
                            int projectChoice = projectConsole.ProjectModule();
                            switch (projectChoice)
                            {
                                case 1:
                                    projectConsole.AddProject();
                                    break;
                                case 2:
                                    projectConsole.ViewProjects();
                                    break;

                                case 3:
                                    projectConsole.ViewProjectByID();
                                    break;

                                case 4:
                                    projectConsole.DeleteProjectByID();
                                    break;

                                case 5:
                                    do
                                    {
                                        int employeeToProjectChoice =
                                            employeeProjectConsole.EmployeeToProjectModule();
                                        switch (employeeToProjectChoice)
                                        {
                                            case 1:
                                                employeeProjectConsole.AddEmployeeToProject();
                                                break;

                                            case 2:
                                                employeeProjectConsole.RemoveEmployeeFromProject();
                                                break;

                                            case 3:
                                                employeeProjectConsole.ViewEmployeeProject();
                                                break;
                                            case 4:
                                                employeeToProjectLoop = false;
                                                break;
                                            default:
                                                employeeProjectConsole.Default();
                                                break;
                                        }
                                    } 
                                    while (employeeToProjectLoop);
                                    
                                    break;

                                case 6:
                                    projectLoop = false;
                                    break;

                                default:
                                    projectConsole.Default();
                                    break;
                            }
                        } while (projectLoop);
                        break;

                    case 2:
                        do
                        {
                            int employeeChoice = employeeConsole.EmployeeModule();
                            switch (employeeChoice)
                            {
                                case 1:
                                    employeeConsole.AddEmployee();
                                    break;

                                case 2:
                                    employeeConsole.ViewEmployee();
                                    break;

                                case 3:
                                    employeeConsole.ViewEmployeeByID();
                                    break;

                                case 4:
                                    employeeConsole.DeleteEmployeeByID();
                                    break;

                                case 5:
                                    employeeLoop = false;
                                    break;

                                default:
                                    employeeConsole.Default();
                                    break;
                            }
                        } while (employeeLoop);
                        break;

                    case 3:
                        do
                        {
                            int roleChoice = roleConsole.RoleModule();
                            switch (roleChoice)
                            {
                                case 1:
                                    roleConsole.AddRole();
                                    break;

                                case 2:
                                    roleConsole.ViewRole();
                                    break;

                                case 3:
                                    roleConsole.ViewRoleByID();
                                    break;

                                case 4:
                                    roleConsole.DeleteRoleByID();
                                    break;

                                case 5:
                                    roleLoop = false;
                                    break;

                                default:
                                    roleConsole.Default();
                                    break;
                            }
                        } while (roleLoop);
                        break;

                    case 4:
                        uIConsole.SaveState();
                        break;

                    case 5:
                        uIConsole.MenuExit();
                        choice = false;
                        break;

                    default:
                        uIConsole.Default();
                        break;
                }
            } while (choice);
        }
    }
}
