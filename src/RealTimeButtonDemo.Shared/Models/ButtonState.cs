namespace RealTimeButtonDemo.Shared.Models;

public class ButtonState
{
    public int Id { get; set; }
    public string RoomId { get; set; } = string.Empty;
    public string Color { get; set; } = "#007bff";
    public string LastClickedBy { get; set; } = string.Empty;
    public DateTime LastClickedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}