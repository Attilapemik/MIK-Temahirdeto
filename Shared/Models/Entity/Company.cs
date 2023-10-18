namespace PannonBlazor.Shared.Models.Entity
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
