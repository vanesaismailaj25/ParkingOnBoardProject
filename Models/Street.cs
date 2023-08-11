using ParkingOnBoard.Models;
using System.ComponentModel.DataAnnotations;

namespace ParkingOnBoard;
public class Street
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public bool HasTwoSides { get; set; } = false;
    public int TotalValidSlots { get; set; }
    public bool IsClosed { get; set; } = false;
    public List<ParkingSlot> Slots { get; set; } 
    public int CityId { get; set; }
    public string CityName { get; set; } 
}
