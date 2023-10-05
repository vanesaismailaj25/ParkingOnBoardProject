using ParkingOnBoard.Context;
using ParkingOnBoard.Models;
using ParkingOnBoard.Validations;

namespace ParkingOnBoard.Services;

public class CityService
{
    private readonly ParkingOnBoardContext _context;
    private readonly Validation _validations;

    public CityService()
    {
        _context = new ParkingOnBoardContext();
        _validations = new Validation();
    }

    public void ManageCities()
    {
        try
        {
            Console.WriteLine("Choose what operation would you like to do: \n1. Add a new city. \n2.Main menu.");
            Console.WriteLine("The list of all the cities: ");
            GetAllCities();             
            Console.WriteLine("Answer: ");
            int input = int.Parse(Console.ReadLine()!);
            Console.Clear();

            switch (input)
            {
                case 1:
                    Console.WriteLine("Enter city name: ");
                    string name = Console.ReadLine()!;
                    AddACity(name);
                    break;

                case 2:
                    RunService.Admin();
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
            _validations.CityValidatorDBShouldntExist(name);

            var city = new City()
            {
                Name = name
            };

            _context.Cities.Add(city);
            _context.SaveChanges();

            Console.WriteLine($"The city with the name {city.Name} was added successfully to the database!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    public List<City> GetAllCities()
    {
        var cities = _context.Cities.ToList();
        if( cities.Count == 0)
            Console.WriteLine("There are no cities available!");
        
        foreach (var city in cities)
        {
            Console.WriteLine($"{city.Name}");
        }
        return cities.ToList();
    }
}
