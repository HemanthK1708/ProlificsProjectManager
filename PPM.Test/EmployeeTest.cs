using PPM.Domain;
using PPM.Model;

namespace PPM.Test;

public class EmployeeTest
{
    EmployeeRepository employeeRepository = new();
    [Test]
    public void TestAddEmployeeValidData()
    {
        EmployeeRepository.employeeList.Clear();

        Employee employee = new Employee
        {

            EmployeeID = 01,
            FirstName = "Hemanth",
            LastName = "Kumar",
            Email = "hk@prolifics",
            Mobile = "9492135389",
            Address = "Vijayawada",
            RoleId = 01
        };

        employeeRepository.Add(employee);

        Employee addedEmployee = EmployeeRepository.employeeList.Find(
            e => e.EmployeeID == employee.EmployeeID
        );

        Assert.NotNull(addedEmployee);

        Assert.That(addedEmployee.EmployeeID, Is.EqualTo(employee.EmployeeID));
        Assert.That(addedEmployee.FirstName, Is.EqualTo(employee.FirstName));
        Assert.That(addedEmployee.LastName, Is.EqualTo(employee.LastName));
        Assert.That(addedEmployee.Email, Is.EqualTo(employee.Email));
        Assert.That(addedEmployee.Mobile, Is.EqualTo(employee.Mobile));
        Assert.That(addedEmployee.Address, Is.EqualTo(employee.Address));
        Assert.That(addedEmployee.RoleId, Is.EqualTo(employee.RoleId));
    }

    [TestCase(01)]
    [TestCase(02)]
    public void TestAddEmployeeInvalidData(int id)
    {
        EmployeeRepository.employeeList.Clear();

        try
        {
            Employee employee = new Employee
            {

                EmployeeID = 01,
                FirstName = "Hemanth",
                LastName = "Kumar",
                Email = "hk@prolifics",
                Mobile = "9492135389",
                Address = "Vijayawada",
                RoleId = 01
            };

            employeeRepository.Add(employee);

            Employee addedEmployee = EmployeeRepository.employeeList.Find(e => e.EmployeeID == employee.EmployeeID);

            Assert.NotNull(addedEmployee);

            Assert.That(id, Is.EqualTo(addedEmployee.EmployeeID));
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("Error occured " + ex.Message);
            Assert.Fail("The test project handled the invalid data exception : " + ex.Message);
        }
    }

    [Test]
    public void ViewEmployeeTest()
    {
        EmployeeRepository.employeeList.Clear();

        Employee employeeObject = new Employee
        {
            EmployeeID = 01,
            FirstName = "Hemanth",
            LastName = "Kumar",
            Email = "hk@prolifics.com",
            Mobile = "9492135389",
            Address = "Vijayawada",
            RoleId = 01
        };

        EmployeeRepository.employeeList.Add(employeeObject);

        List<Employee> projects = employeeRepository.ViewAll();

        Assert.IsTrue(projects.Contains(employeeObject));
        //Assert.IsTrue(projects.Contains(addedEmployee));

        //Employee addedEmployee = EmployeeRepository.employeeList.Find(e => e.ID == 3);
    }

    [TestCase(01)]
    [TestCase(02)]
    [TestCase(03)]
    public void ViewEmployeeByIDTest(int id)
    {
        EmployeeRepository.employeeList.Clear();

        Employee employeeObject = new Employee
        {
            EmployeeID = 01,
            FirstName = "Hemanth",
            LastName = "Kumar",
            Email = "hk@prolifics.com",
            Mobile = "9492135389",
            Address = "Vijayawada",
            RoleId = 01
        };

        EmployeeRepository.employeeList.Add(employeeObject);

        var employee = employeeRepository.ViewByID(id);

        Assert.IsNotNull(employee);
    }
}
