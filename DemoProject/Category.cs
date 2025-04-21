namespace DemoProject
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public override string ToString()
        {
            return $"Title: {Title}, Code: {Code}, Description: {Description}";
        }
    }
}