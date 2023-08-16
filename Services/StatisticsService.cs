using Microsoft.EntityFrameworkCore;
using ParkingOnBoard.Context;
using ParkingOnBoard.Validations;

namespace ParkingOnBoard.Services;

public class StatisticsService
{
    private readonly ParkingOnBoardContext _context;
    private readonly CityService _cityService;
    private readonly Validation _validations;
    public StatisticsService()
    {
        _context = new ParkingOnBoardContext();
        _cityService = new CityService();
        _validations = new Validation();

    }

    public void DisplayStatistics()
    {
        try
        {
            Console.WriteLine("Choose what operation would you like to do: ");
            Console.WriteLine("1. Street statistics. \n2. City statistics.\n3.Log out. ");
            int input = int.Parse(Console.ReadLine()!);

            switch (input)
            {
                case 1:
                    try
                    {
                        Console.WriteLine("All the streets: ");
                        var streets = _context.Streets.ToList();
                        foreach (var stre in streets)
                        {
                            Console.WriteLine($"{stre.Name}");
                        }
                        Console.WriteLine("\nEnter the name of the street you want to calculate: ");
                        string streetName = Console.ReadLine()!;

                        StreetStatistics(streetName);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                    break;

                case 2:
                    try
                    {
                        Console.WriteLine("All the cities: ");
                        _cityService.GetAllCities();
                        Console.WriteLine("Enter the name of the city you want to calculate the statistics for: ");
                        string cityName = Console.ReadLine()!;

                        CityStatistics(cityName);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                    break;

                case 3:
                    Console.WriteLine("Log out!");
                    RunService.User();
                    break;

                default:
                    Console.WriteLine("Please enter the correct input asked!");
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
    public void StreetStatistics(string streetName)
    {
        try
        {
            _validations.StreetsValidator(streetName);

            var street = _context.Streets.Include(c => c.Slots).FirstOrDefault(sn => sn.Name == streetName);

            if (street != null)
            {
                int totalSlots = street.Slots.Count();
                int occupiedSlots = street.Slots.Count(os => os.IsBusy == true);
                int invalidSlots = street.Slots.Count(sp => sp.IsClosed == true);

                double percentageFree = (totalSlots - occupiedSlots - invalidSlots) * 100 / totalSlots;
                double percentageOccupied = occupiedSlots * 100 / totalSlots;
                double percentageInvalid = invalidSlots * 100 / totalSlots;

                Console.WriteLine("Street Statistics:");
                Console.WriteLine($"Free Slots: {percentageFree}%.");
                Console.WriteLine($"Occupied Slots: {percentageOccupied}%.");
                Console.WriteLine($"Invalid Slots: {percentageInvalid}%.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    public void CityStatistics(string cityName)
    {
        try
        {
            _validations.CitiesValidator(cityName);

            var city = _context.Cities.Include(c => c.Streets)
                .ThenInclude(s => s.Slots)
                .Where(cn => cn.CityName == cityName)
                .FirstOrDefault();


            Console.WriteLine("City Statistics:");

            var lessBusyStreets = city.Streets.Where(s => s.Slots.Any() && s.Slots.Count(slot => slot.IsBusy) / s.Slots.Count <= 0.25)
                                         .Select(s => s.Name)
                                         .ToList();


            var moreBusyStreets = city.Streets.Where(s => s.Slots.Any() && s.Slots.Count(slot => slot.IsBusy) / s.Slots.Count >= 0.75)
                                         .Select(s => s.Name)
                                         .ToList();

            Console.WriteLine("Streets with maximum 25% busy slots: ");
            foreach (var street in lessBusyStreets)
            {
                Console.WriteLine($"{street}");
            }

            Console.WriteLine("Streets with minimum 75% busy slots: ");
            foreach (var street in moreBusyStreets)
            {
                Console.WriteLine($"{street}");
            }        
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
}
