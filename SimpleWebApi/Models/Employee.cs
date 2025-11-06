namespace SimpleWebApi.Models
{
    public class Employee
    {
        public required int Id { get; set; }
        public required int UserId { get; set; }
        public int? ManagerId { get;set; }
        public decimal? PayRate { get; set; }
    }
}
