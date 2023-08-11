using ParkingOnBoard.Context;
using ParkingOnBoard.Models;
namespace ParkingOnBoard.Services;

public class CityService
{
    private readonly ParkingOnBoardContext _context;
    private readonly ExceptionsClass _exceptionsClass;

    public CityService()
    {
        _context = new ParkingOnBoardContext();
        _exceptionsClass = new ExceptionsClass();
    }

    public void ManageCities()
    {
        try
        {
            Console.WriteLine("Choose what operation would you like to do: ");
            Console.WriteLine("1. Add a new city.\n");
            Console.WriteLine("The list of all the cities: ");
            GetAllCities();             
            Console.WriteLine("Answer: ");
            int input = int.Parse(Console.ReadLine()!);

            switch (input)
            {
                case 1:
                    Console.WriteLine("Enter city name: ");
                    string name = Console.ReadLine()!;
                    AddACity(name);
                    break;
                default:
                    Console.WriteLine("Please enter the correct input asked!");
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error : {e.Message}");
        }
    }
    public void AddACity(string name)
    {
        try
        {
            /*//Name Should not be null or whitespace
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("City name should not be null or whitespace!");
            }

            //Should not be a number
            foreach (char c in name)
            {
                if (Char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric. Please enter a valid street name!");
                }
            }

            //Should not exist in the db
            var cityExists = _context.Cities.Any(n => n.CityName == name);
            if (cityExists)
            {
                throw new Exception($"The city with the name {name} already exists in the database!");
            }*/

            _exceptionsClass.ExceptionHandlerCities(name);

            var city = new City()
            {
                CityName = name
            };

            _context.Cities.Add(city);
            _context.SaveChanges();

            Console.WriteLine($"The city with the name {city.CityName} was added successfully to the database!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    public void GetAllCities()
    {
        var cities = _context.Cities.ToList();
        if( cities.Count == 0)
            Console.WriteLine("There are no cities available!");
        
        foreach (var city in cities)
        {
            Console.WriteLine($"{city.CityName}");
        }
    }
}
