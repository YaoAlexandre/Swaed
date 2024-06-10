namespace Swaed.Helpers
{
    enum UserRole
    {
        Admin,
        Volunteer,
        Organization
    }
    public static class UserRoles
    {
        public static string[] GetRoles()
        {
            return Enum.GetNames(typeof(UserRole));
        }
    }
}
