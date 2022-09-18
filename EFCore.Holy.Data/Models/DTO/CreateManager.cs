namespace EFCore.Holy.Data.Models.DTO
{
    public class CreateManager
    {
        public int IdChurch { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
}
