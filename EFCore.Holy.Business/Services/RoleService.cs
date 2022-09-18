using EFCore.Holy.Business.Handling;

namespace EFCore.Holy.Business.Services
{
    public static class RoleService
    {
        public static bool IsValidRole(int id)
        {
            List<int> levels = new() { 1, 2, 3 };

            return levels.Contains(id);
        }
        public static bool IsShepherd(int id)
        {
            if (!IsValidRole(id))
                throw new HttpException(400, Error.InvalidRole);

            return id == 3;
        }
        public static bool IsTreasurer(int id)
        {
            if (!IsValidRole(id))
                throw new HttpException(400, Error.InvalidRole);

            return id == 2;
        }
        public static bool IsSecretary(int id)
        {
            if (!IsValidRole(id))
                throw new HttpException(400, Error.InvalidRole);

            return id == 1;
        }
    }
}
