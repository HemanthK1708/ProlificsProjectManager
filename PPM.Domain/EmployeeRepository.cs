using PPM.DAL;
using PPM.Model;

namespace PPM.Domain
{
    public class EmployeeRepository : IEntity<Employee>
    {
        public static List<Employee> employeeList = new List<Employee>();

        EmployeeDAL employeeDAL = new();

        public void Add(Employee employee)
        {
            employeeDAL.Add(employee);
            //employeeList.Add(employee);
        }

        public bool IsValidEmployee(int employeeId)
        {
            // bool result = employeeList.Exists(e => e.EmployeeID == employeeId);
            bool result = employeeDAL.IsValidEmployee(employeeId);
            return result;
        }

        public List<Employee> ViewAll()
        {
            var empList = employeeDAL.ViewAll();
            return empList;
        }

        public Employee ViewByID(int employeeId)
        {
            // Employee employeeByID = employeeList.FirstOrDefault(p => p.EmployeeID == employeeId);
            Employee employeeByID = employeeDAL.ViewById(employeeId);
            return employeeByID;
        }

        public void DeleteByID(int employeeId)
        {
            // employeeList.RemoveAll(item => item.EmployeeID == employeeId);
            employeeDAL.DeleteByID(employeeId);
        }



    }
}