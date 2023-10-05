using ParkingOnBoard.Models;
using System.ComponentModel.DataAnnotations;

namespace ParkingOnBoard;
public class Street
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public bool HasTwoSides { get; set; } = false;
    public bool IsClosed { get; set; } = false;

    public ICollection<ParkingSlot> Slots { get; set; } 
    public City City { get; set; }
    public int CityId { get; set; } 
}
