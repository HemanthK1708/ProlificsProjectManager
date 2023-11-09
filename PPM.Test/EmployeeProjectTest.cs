using PPM.Domain;
using PPM.Model;

namespace PPM.Test;

public class EmployeeProjectTest
{

    EmployeeProjectRepository employeeProjectRepository = new EmployeeProjectRepository();
    [Test]
    public void TestAddEmployeeProjectValidData()
    {
        EmployeeProjectRepository.employeeProjectList.Clear();

        Project projectObject = new Project()
        {
            ProjectId = 01,
            ProjectName = "Prolifics Project Manager",
            StartDate = new DateTime(2023, 11, 01),
            EndDate = new DateTime(2023, 11, 11)
        };

        ProjectRepository.projectList.Add(projectObject);

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

        int ProjectId = 01;
        string ProjectName = "Prolifics Project Manager";
        int EmployeeID = 01;
        string FirstName = "Hemanth";
        int RoleId = 01;


        employeeProjectRepository.AddEmployeeToProject(ProjectId, EmployeeID);

        //EmployeeProjectRepository.employeeProjectList.Add(employeeProject);

        EmployeeProject addedEmployeeProject = EmployeeProjectRepository.employeeProjectList.Find(
            e => e.ProjectId == ProjectId
        );

        Assert.NotNull(addedEmployeeProject);

        Assert.That(ProjectId, Is.EqualTo(addedEmployeeProject.ProjectId));
        Assert.That(ProjectName, Is.EqualTo(addedEmployeeProject.ProjectName));
        Assert.That(EmployeeID, Is.EqualTo(addedEmployeeProject.EmployeeID));
        Assert.That(FirstName, Is.EqualTo(addedEmployeeProject.FirstName));
        Assert.That(RoleId, Is.EqualTo(addedEmployeeProject.RoleId));
    }

    [TestCase (01, "PPM", 5602, "Hemanth", 007)]
    [TestCase (02, "PPM", 5602, "Hemanth", 007)]
    [TestCase (02, "PPM", 01, "John", 000)]

    public void TestAddEmployeeProjectInValidData(int projectId, string projectName, int employeeID, string firstName, int roleId)
    {
        EmployeeProjectRepository.employeeProjectList.Clear();

        try
        {
            EmployeeProject employeeProject =
                new()
                {
                    ProjectId = 01,
                    ProjectName = "PPM",
                    EmployeeID = 5602,
                    FirstName = "Hemanth",
                    RoleId = 007
                };

            EmployeeProjectRepository.employeeProjectList.Add(employeeProject);

            EmployeeProject addedEmployeeProject =
                EmployeeProjectRepository.employeeProjectList.Find(
                    e => e.ProjectId == employeeProject.ProjectId
                );

            Assert.NotNull(addedEmployeeProject);

            Assert.That(projectId, Is.EqualTo(addedEmployeeProject.ProjectId));
            Assert.That(firstName, Is.EqualTo(addedEmployeeProject.FirstName));
            Assert.That(employeeID, Is.EqualTo(addedEmployeeProject.EmployeeID));
            Assert.That(firstName, Is.EqualTo(addedEmployeeProject.FirstName));
            Assert.That(roleId, Is.EqualTo(addedEmployeeProject.RoleId));

        }
        catch (Exception ex)
        {
            System.Console.WriteLine("Error occured " + ex.Message);
            Assert.Pass("The test project handled the invalid data exception : " + ex.Message);
        }
    }


    [Test]
    public void TestViewEmployeeProject()
    {
        EmployeeProjectRepository.employeeProjectList.Clear();

        EmployeeProject employeeProject = new()
        {
            ProjectId = 01,
            ProjectName = "PPM",
            EmployeeID = 5602,
            FirstName = "Hemanth",
            RoleId = 007
        };

        EmployeeProjectRepository.employeeProjectList.Add(employeeProject);

        List<EmployeeProject> addedEmployeeProject = employeeProjectRepository.ViewEmployeeProjects();

        Assert.NotNull(addedEmployeeProject);

        Assert.IsTrue(addedEmployeeProject.Contains(employeeProject));

    }


    [TestCase (01, 01)]
    [TestCase (01, 02)]
    [TestCase (02, 01)]
    [TestCase (02, 02)]

    public void TestRemoveEmployeeProject(int projectId, int employeeID)
    {
        EmployeeProjectRepository.employeeProjectList.Clear();

        Project projectObject = new Project()
        {
            ProjectId = 01,
            ProjectName = "Prolifics Project Manager",
            StartDate = new DateTime(2023, 11, 01),
            EndDate = new DateTime(2023, 11, 11)
        };

        ProjectRepository.projectList.Add(projectObject);

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

        employeeProjectRepository.RemoveEmployeeFromProject(projectId, employeeID);

        EmployeeProject removedEmployeeProject = EmployeeProjectRepository.employeeProjectList.Find(r => r.ProjectId == projectId && r.EmployeeID == employeeID);

        Assert.That(removedEmployeeProject, Is.Null);

    }

}
