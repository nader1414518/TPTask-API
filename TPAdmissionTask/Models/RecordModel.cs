namespace TPAdmissionTask.Models
{
    public class RecordModel
    {
        public int RecordId { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? ImgUrl { get; set; }
    }
}
