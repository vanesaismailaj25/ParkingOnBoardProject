using Microsoft.EntityFrameworkCore;
using ParkingOnBoard.Services;
using ParkingOnBoard.Context;

namespace ParkingOnBoard;
public class Program
{
    static void Main(string[] args)
    { 
        using (var _context = new ParkingOnBoardContext())
        {
            _context.Database.Migrate();
        }

        RunService.RunProgram();
    }
}







