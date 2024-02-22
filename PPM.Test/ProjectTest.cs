using PPM.Model;
using PPM.Domain;

namespace PPM.Test;

public class ProjectTest
{
    ProjectRepository projectRepository = new();
    [Test]
    public void TestAddProjectValidData()
    {
        ProjectRepository.projectList.Clear();
        Project projectObject = new Project()
        {
            ProjectId = 01,
            ProjectName = "Prolifics Project Manager",
            StartDate = new DateTime(2023, 11, 01),
            EndDate = new DateTime(2023, 11, 11)
        };

        projectRepository.Add(projectObject);

        Project AddedProject = ProjectRepository.projectList.FirstOrDefault(
            p => p.ProjectId == projectObject.ProjectId
        );

        Assert.That(AddedProject, Is.Not.Null);

        Assert.That(AddedProject.ProjectId, Is.EqualTo(projectObject.ProjectId));
        Assert.That(AddedProject.ProjectName, Is.EqualTo(projectObject.ProjectName));
        Assert.That(AddedProject.StartDate, Is.EqualTo(projectObject.StartDate));
        Assert.That(AddedProject.EndDate, Is.EqualTo(projectObject.EndDate));
    }

    [Test]
    public void TestAddProjectInValidData()
    {
        ProjectRepository.projectList.Clear();

        try
        {
            Project projectObject = new Project()
            {
                ProjectId = 01,
                ProjectName = "Prolifics Project Manager",
                StartDate = new DateTime(2023, 14, 01),
                EndDate = new DateTime(2023, 11, 11)
            };

            projectRepository.Add(projectObject);
            Assert.Pass("Invalid Data exception not occurred !");
        }
        catch (Exception Ex)
        {
            System.Console.WriteLine("Error occured " + Ex.Message);
            Assert.Fail("The test project handled the invalid data exception : " + Ex.Message);
        }
    }

    [Test]
    public void ViewProjectTest()
    {
        ProjectRepository.projectList.Clear();

        Project projectObject = new Project()
        {
            ProjectId = 1,
            ProjectName = "PPM",
            StartDate = new DateTime(2023, 10, 11),
            EndDate = new DateTime(2023, 11, 11)
        };

        ProjectRepository.projectList.Add(projectObject);

        List<Project> projects = projectRepository.ViewAll();

        Assert.That(projects, Does.Contain(projectObject));
    }


    [TestCase(01)]
    [TestCase(02)]
    [TestCase(03)]

    public void ViewProjectByIDTest(int id)
    {
        ProjectRepository.projectList.Clear();

        Project projectObject = new Project()
        {
            ProjectId = 1,
            ProjectName = "PPM",
            StartDate = new DateTime(2023, 10, 11),
            EndDate = new DateTime(2023, 11, 11)
        };

        ProjectRepository.projectList.Add(projectObject);

        var project = projectRepository.ViewByID(id);

        Assert.IsNotNull(project);
    }
}
