using Microsoft.EntityFrameworkCore;
using ParkingOnBoard.Services;
using ParkingOnBoard.Context;

namespace ParkingOnBoard;
public class Program
{
    static void Main(string[] args)
    {
        RunService.RunProgram();

        using (var _context = new ParkingOnBoardContext())
        {
            _context.Database.Migrate();
        }       
    }
}







