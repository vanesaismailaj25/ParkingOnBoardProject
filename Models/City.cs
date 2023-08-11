using System.ComponentModel.DataAnnotations;

namespace ParkingOnBoard.Models;

public class City
{
    [Key]
    public int Id { get; set; }
    public string CityName { get; set; }
    public ICollection<Street> Streets { get; set; }
}
