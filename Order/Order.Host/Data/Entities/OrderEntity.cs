namespace Order.Host.Data.Entities;

public class OrderEntity
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public int CustomerId { get; set; }

    public List<OrderItem>? OrderItems { get; set; }
}