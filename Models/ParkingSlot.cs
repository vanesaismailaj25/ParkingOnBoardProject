using System.ComponentModel.DataAnnotations;

namespace ParkingOnBoard;
public class ParkingSlot 
{
    [Key]
    public int Id { get; set; }
    public int SlotNumber { get; set; }
    public bool IsClosed { get; set; } = false;
    public bool IsBusy { get; set; } = false;
    public Street Street { get; set; }  
    public int StreetId { get; set; }
    public string StreetName { get; set;}
}

