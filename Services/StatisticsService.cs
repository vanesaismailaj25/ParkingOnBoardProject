using ParkingOnBoard.Context;

namespace ParkingOnBoard.Services;

public class StatisticsService
{
    private readonly ParkingOnBoardContext _context;
    private readonly CityService _cityService;
    private readonly ExceptionsClass _exceptionClass;
    public StatisticsService()
    {
        _context = new ParkingOnBoardContext();
        _cityService = new CityService();
        _exceptionClass = new ExceptionsClass();
    }

    public void DisplayStatistics()
    {
        try
        {
            Console.WriteLine("Choose what operation would you like to do: ");
            Console.WriteLine("1. Street statistics. \n2. City statistics. ");
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
                        Console.WriteLine("\n");
                        Console.WriteLine("Enter the name of the street you want to calculate: ");
                        string streetName = Console.ReadLine()!;
                        /*
                                                if (string.IsNullOrWhiteSpace(streetName))
                                                {
                                                    throw new Exception("Street name should not be null or whitespace!");
                                                }

                                                foreach (char c in streetName)
                                                {
                                                    if (Char.IsDigit(c))
                                                    {
                                                        throw new Exception("The input you entered was numeric. Please enter a valid street name!");
                                                    }
                                                }*/
                        _exceptionClass.ExceptionHandlerStreets(streetName);
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

                        /*if (string.IsNullOrWhiteSpace(cityName))
                        {
                            throw new Exception("City name should not be null or whitespace!");
                        }

                        foreach (char c in cityName)
                        {
                            if (Char.IsDigit(c))
                            {
                                throw new Exception("The input you entered was numeric. Please enter a valid city name!");
                            }
                        }*/
                        _exceptionClass.ExceptionHandlerCities(cityName);
                        CityStatistics(cityName);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
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
            /*//Name Should not be null or whitespace
            if (string.IsNullOrWhiteSpace(streetName))
            {
                throw new Exception("street name should not be null or empty");
            }

            //Should not be a number
            foreach (char c in streetName)
            {
                if (Char.IsDigit(c))
                {
                    throw new Exception("The input you typed was numeric.Please enter a valid street name.");
                }
            }*/

            _exceptionClass.ExceptionHandlerStreets(streetName);

            var street = _context.Streets.FirstOrDefault(sn => sn.Name == streetName);

            if (street != null)
            {
                int totalSlots = street.TotalValidSlots;
                int occupiedSlots = _context.ParkingSlots.Count(os => os.IsBusy == true && os.StreetName == streetName);
                int invalidSlots = _context.ParkingSlots.Count(sp => sp.IsClosed == true && sp.StreetName == streetName);

                double percentageFree = (totalSlots - occupiedSlots) * 100 / totalSlots;
                double percentageOccupied = occupiedSlots * 100 / totalSlots;
                double percentageInvalid = invalidSlots * 100 / totalSlots;

                Console.WriteLine("Street Statistics:");
                Console.WriteLine($"Percentage of Free Slots: {percentageFree}%.");
                Console.WriteLine($"Percentage of Occupied Slots: {percentageOccupied}%.");
                Console.WriteLine($"Percentage of Invalid Slots: {percentageInvalid}%.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not calculate the street statistics for the street {streetName} {e.Message}.");
        }
    }

    public void CityStatistics(string cityName)
    {
        try
        {
            /*//Name Should not be null or whitespace
            if (string.IsNullOrWhiteSpace(cityName))
            {
                throw new ArgumentNullException("street name should not be null or empty");
            }

            //Should not be a number
            foreach (char c in cityName)
            {
                if (Char.IsDigit(c))
                {
                    throw new Exception("The input you typed was numeric.Please enter a valid city name.");
                }
            }*/

            _exceptionClass.ExceptionHandlerCities(cityName);
            //Should exist in the db
            var city = _context.Cities.FirstOrDefault(cn => cn.CityName == cityName);
            var streets = _context.Streets.Where(s => s.CityName == cityName).ToList();
            var slots = _context.ParkingSlots.ToList();

            foreach (var street in streets)
            {
                Console.WriteLine($"The street: {street.Name}");
            }

            Console.WriteLine("City Statistics:");

            var lessBusyStreets = streets.Where(s => s.Slots.Any() && s.Slots.Count(_slot => _slot.IsBusy) / s.Slots.Count <= 0.25)
                                         .Select(s => s.Name)
                                         .ToList();

            var moreBusyStreets = streets.Where(s => s.Slots.Any() && s.Slots.Count(_slot => _slot.IsBusy) / s.Slots.Count >= 0.75)
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
            Console.WriteLine($"Could not calculate the {cityName} statistics {e.Message}!");
        }
    }
}
