using PPM.Model;

namespace PPM.Domain
{
    public class EmployeeRepository : IEntity<Employee>
    {
        public static List<Employee> employeeList = new List<Employee>();
        public void Add(Employee employee)
        {
            employeeList.Add(employee);
        }

        public bool IsValidEmployee(int employeeId)
        {
            bool result = employeeList.Exists(e => e.EmployeeID == employeeId);
            return result;
        }

        public List<Employee> ViewAll()
        {
            return employeeList;
        }

        public Employee ViewByID(int employeeId)
        {
            Employee employeeByID = employeeList.FirstOrDefault(p => p.EmployeeID == employeeId);
            return employeeByID;
        }

        public void DeleteByID(int employeeId)
        {
            employeeList.RemoveAll(item => item.EmployeeID == employeeId);
        }



    }
}