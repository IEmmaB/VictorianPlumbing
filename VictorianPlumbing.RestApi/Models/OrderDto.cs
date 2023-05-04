namespace VictorianPlumbing.RestApi.Models
{
    public class OrderDto
    {
        public long Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public List<ItemDto> Items { get; set; }
    }
}
