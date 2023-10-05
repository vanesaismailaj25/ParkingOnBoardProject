using System.ComponentModel.DataAnnotations;

namespace ParkingOnBoard;
public class ParkingSlot 
{
    [Key]
    public int Id { get; set; }
    public int SlotNumber { get; set; }
    public bool IsClosed { get; set; }
    public bool IsBusy { get; set; }

    public Street Street { get; set; }  
    public int StreetId { get; set; }
}

