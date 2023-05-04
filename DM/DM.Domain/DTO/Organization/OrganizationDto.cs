namespace DM.Domain.Models
{
    public class OrganizationDto : BaseDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Inn { get; set; }
        public string Ogrn { get; set; }
        public string Kpp { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
