using ParkingOnBoard.Context;
using ParkingOnBoard.Services;
using ParkingOnBoard.Validations;

namespace ParkingOnBoard;

public class StreetService
{
    private readonly ParkingOnBoardContext _context;
    private readonly CityService _cityService;
    private readonly Validation _validations;
    public StreetService()
    {
        _context = new ParkingOnBoardContext();
        _cityService = new CityService();
        _validations = new Validation();
    }
    public void ManageStreets()
    {
        try
        {
            Console.WriteLine("Choose what operation would you like to do: ");
            Console.WriteLine("1. Add a new street. \n2. Close a street. \n3. Validate a street. \n4.Main menu. \nAnswer: ");
            int input = int.Parse(Console.ReadLine()!);

            switch (input)
            {
                case 1:
                    try
                    {
                        Console.WriteLine("The list of all the cities: ");
                        var citiesResult = _cityService.GetAllCities();
                        if (citiesResult.Count > 0)
                        {
                            Console.WriteLine("Enter the name of the city you want to add to the street: ");
                            string cityName = Console.ReadLine()!;
                            _validations.CitiesValidator(cityName);

                            Console.WriteLine($"The list of all the streets of {cityName}: ");
                            GetAllStreets(cityName);


                            Console.WriteLine("Enter the name of the street you want to add: ");
                            string streetName = Console.ReadLine()!;
                            _validations.StreetValidator(streetName);

                            Console.Write("If the street is one sided please type \"false\", if it has two sides please enter \"true\": ");
                            string sides = Console.ReadLine()!;
                            try
                            {
                                bool side = Convert.ToBoolean(sides);
                                Console.Write("Enter total valid car parking slots: ");
                                int totalSlots;

                                if (int.TryParse(Console.ReadLine(), out totalSlots) && totalSlots > 0)
                                {
                                    AddAStreet(cityName, streetName, side, totalSlots);
                                }
                                else
                                {
                                    Console.WriteLine("Could not add a new street! Please insert all the required input!");
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"The value you entered '{sides}' is not a boolean!");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                    break;

                case 2:
                    try
                    {
                        Console.WriteLine("The list of all the cities: ");
                        var citiesResult = _cityService.GetAllCities();
                        if (citiesResult.Count > 0)
                        {
                            Console.WriteLine("Enter the name of the city you want to close the street: ");
                            string cityNameClose = Console.ReadLine()!;

                            _validations.CitiesValidator(cityNameClose);

                            Console.WriteLine("The list of the streets that are not closed: ");
                            //var result = GetAllStreets(cityNameClose); // ketu kusht kur nuk kemi rruge
                            var result = GetAllFreeStreets(cityNameClose);

                            if (result.Count > 0)
                            {
                                Console.WriteLine("Enter the name of the street you want to close: ");
                                string givenStreetName = Console.ReadLine()!;

                                _validations.StreetsValidator(givenStreetName);

                                CloseAStreet(givenStreetName);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                    break;

                case 3:
                    try
                    {
                        Console.WriteLine("The list of all the cities: ");
                        var citiesResults = _cityService.GetAllCities();
                        if (citiesResults.Count > 0)
                        {
                            Console.WriteLine("Enter the name of the city you want to validate the street: ");
                            string cityNameValidate = Console.ReadLine()!;

                            _validations.CitiesValidator(cityNameValidate);

                            Console.WriteLine("The list of the streets that are closed: ");
                            var streetsResult = GetAllBusyStreets(cityNameValidate);

                            if (streetsResult.Count > 0)
                            {
                                Console.WriteLine("Enter the name of the street you want to validate: ");
                                string streetNameValidate = Console.ReadLine()!;

                                _validations.StreetsValidator(streetNameValidate);

                                ValidateAStreet(streetNameValidate);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                    break;

                case 4:
                    Console.WriteLine("Log out!");
                    RunService.Admin();
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

    public void AddAStreet(string cityName, string streetName, bool sides, int totalSlots)
    {
        try
        {
            //

            var city = _context.Cities.SingleOrDefault(c => c.CityName == cityName);

            var street = new Street
            {
                Name = streetName,
                HasTwoSides = sides,
                TotalValidSlots = totalSlots,
                CityName = cityName,
                CityId = city.Id,
                Slots = new List<ParkingSlot>()
            };
            _context.Streets.Add(street);
            _context.SaveChanges();

            for (int i = 1; i <= totalSlots; i++)
            {
                var parkingSlot = new ParkingSlot
                {
                    SlotNumber = i,
                    StreetId = street.Id,
                    StreetName = streetName
                };
                _context.ParkingSlots.Add(parkingSlot);
            }
            _context.SaveChanges();
            Console.WriteLine($"The street with the name '{streetName}' was successfully added to the city {cityName}!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not add to the city. {e.Message}");
        }
    }

    public bool CloseAStreet(string streetName)
    {
        try
        {
            //

            var street = _context.Streets.FirstOrDefault(s => s.Name == streetName);
            if (street == null)
            {
                throw new Exception($"The street with the name {streetName} doesn't exist!");
            }
            else if (street.IsClosed == true)
            {
                throw new Exception($"The street with the name {streetName} is already closed!");
            }
            else
            {
                street.IsClosed = true;
                _context.SaveChanges();
                Console.WriteLine($"The street with the name {streetName} was closed successfully!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not close the street. {e.Message}");
        }
        return true;
    }

    public bool ValidateAStreet(string streetName)
    {
        try
        {
            //

            var street = _context.Streets.FirstOrDefault(sn => sn.Name == streetName);

            if (street == null)
            {
                throw new Exception($"The street with the name {streetName} doesn't exist!");
            }
            else if (street.IsClosed == false)
            {
                throw new Exception($"The street with the name {streetName} is already validated!");
            }
            else
            {
                street!.IsClosed = false;
                _context.Streets.Update(street);
                _context.SaveChanges();
                Console.WriteLine($"Street with the name {streetName} is now validated.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}.");
        }
        return true;
    }

    public List<Street> GetAllStreets(string cityName)
    {
        var streets = _context.Streets.Where(s => s.CityName == cityName).ToList();

        if (streets.Count == 0)
        {
            Console.WriteLine("There are no streets saved!");
        }

        foreach (var street in streets)
        {
            Console.WriteLine($"{street.Name}");
        }
        return streets.ToList();
    }

    public List<Street> GetAllFreeStreets(string cityName)
    {
        var streets = _context.Streets.Where(s => s.CityName == cityName && s.IsClosed == false).ToList();

        if(streets.Count == 0)
        {
            Console.WriteLine("There are no streets saved!");
        }

        foreach(var street in streets)
        {
            Console.WriteLine($"{street.Name}");
        }

        return streets.ToList();
    }

    public List<Street> GetAllBusyStreets(string cityName)
    {
        var streets = _context.Streets.Where(s => s.CityName == cityName && s.IsClosed == true).ToList();

        if (streets.Count == 0)
        {
            Console.WriteLine("There are no streets saved!");
        }

        foreach (var street in streets)
        {
            Console.WriteLine($"{street.Name}");
        }

        return streets.ToList();
    }
}  