namespace Assessment.BLL.Helper
{
    public class EnumHelper
    {
        public enum Difficulty
        {
            Junior = 1,
            medior = 2,
            Senior = 3
        };

        public enum PermissionsUser
        {
            GDPR,
            Admin,
            Owner,
            Read,
            Write
        };

    }
}
