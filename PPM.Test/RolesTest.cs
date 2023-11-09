using System.Data.Common;
using PPM.Domain;
using PPM.Model;

namespace PPM.Test;

public class RolesTest
{
    RolesRepository rolesRepository = new RolesRepository();
    [TestCase(01, "Developer")]
    [TestCase(02, "Tester")]
    public void TestAddRoleValidData(int id, string name)
    {
        RolesRepository.rolesList.Clear();

        Role roles = new Role()
        {
            RoleId = id,
            RoleName = name
        };
        // Role roleObject = new() { RoleId = id, RoleName = name };

        rolesRepository.Add(roles);

        Role role = RolesRepository.rolesList.Find(r => r.RoleId == id);

        Assert.NotNull(role);

        Assert.That(id, Is.EqualTo(role.RoleId));
        Assert.That(name, Is.EqualTo(role.RoleName));
    }

    [TestCase(02, "Developer")]
    [TestCase(03, "Tester")]
    [TestCase(01, "Delivery")]
    public void TestRoleInValidData(int id, string name)
    {
        RolesRepository.rolesList.Clear();

        try
        {
            Role roles = new Role()
            {
                RoleId = 01,
                RoleName = "Delivery"
            };

            rolesRepository.Add(roles);

            Role addedRole = RolesRepository.rolesList.Find(r => r.RoleId == roles.RoleId);

            Assert.NotNull(addedRole);

            Assert.That(id, Is.EqualTo(addedRole.RoleId));
            Assert.That(name, Is.EqualTo(addedRole.RoleName));
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("Error occured " + ex.Message);
            Assert.Fail("The test project handled the invalid data exception : " + ex.Message);
        }
    }

    [Test]
    public void TestViewRole()
    {
        RolesRepository.rolesList.Clear();

        Role roleObject = new() { RoleId = 01, RoleName = "Delivery" };

        RolesRepository.rolesList.Add(roleObject);

        List<Role> roles = rolesRepository.ViewAll();

        Assert.NotNull(roles);
        Assert.IsTrue(roles.Contains(roleObject));

    }


    [TestCase(01)]
    [TestCase(02)]
    [TestCase(03)]
    public void ViewRoleByIDTest(int id)
    {
        Role roles = new Role()
        {
            RoleId = 01,
            RoleName = "Delivery"
        };

        rolesRepository.Add(roles);

        var role = rolesRepository.ViewByID(id);

        Assert.IsNotNull(id);
    }
}
