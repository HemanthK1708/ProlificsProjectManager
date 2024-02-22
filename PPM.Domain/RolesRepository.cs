using PPM.Model;
using PPM.DAL;
namespace PPM.Domain
{
    public class RolesRepository : IEntity<Role>
    {
        public static List<Role> rolesList = new List<Role>();
        RoleDAL roleDAL = new();
        public void Add(Role role)
        {
            //rolesList.Add(roles);
            roleDAL.Add(role);

        }

        public bool IsValidRole(int roleId)
        {
            // bool result = rolesList.Exists(r => r.RoleId == roleId);
            bool result = roleDAL.IsValidRole(roleId);
            return result;
        }

        public List<Role> ViewAll()
        {
            var roles = roleDAL.ViewAll();
            return roles;

        }

        public Role ViewByID(int roleId)
        {
            // var role = rolesList.FirstOrDefault(r => r.RoleId == roleId);
            var role = roleDAL.ViewByID(roleId);
            return role;
        }

        public void DeleteByID(int roleId)
        {
            // rolesList.RemoveAll(r => r.RoleId == roleId);
            roleDAL.DeleteByID(roleId);
        }

        public bool RoleProjectCount(int roleId)
        {
            bool result = roleDAL.RoleProjectCount(roleId);
            return result;
        }

    }
}