using PPM.Model;
namespace PPM.Domain
{
    public class RolesRepository : IEntity<Role>
    {
        public static List<Role> rolesList = new List<Role>();

        public void Add(Role roles)
        {
           rolesList.Add(roles);
        }

        public bool IsValidRole(int roleId)
        {
            bool result = rolesList.Exists(r => r.RoleId == roleId);
            return result;
        }

        public List<Role> ViewAll()
        {
            return rolesList;

        }

        public Role ViewByID(int roleId)
        {
            var role = rolesList.FirstOrDefault(r => r.RoleId == roleId);

            return role;
        }

        public void DeleteByID(int roleId)
        {
            rolesList.RemoveAll(r => r.RoleId == roleId);
        }

    }
}