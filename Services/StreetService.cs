using ParkingOnBoard.Context;
using ParkingOnBoard.Services;

namespace ParkingOnBoard;

public class StreetService
{
    private readonly ParkingOnBoardContext _context;
    private readonly CityService _cityService;

    public StreetService()
    {
        _context = new ParkingOnBoardContext();
        _cityService = new CityService();
    }
    public void ManageStreets()
    {
        try
        {
            Console.WriteLine("Choose what operation would you like to do: ");
            Console.WriteLine("1. Add a new street.");
            Console.WriteLine("2. Close a street.");
            Console.WriteLine("3. Validate a street.");
            Console.WriteLine("4. Remove a street.");          
            Console.WriteLine("Answer: ");
            int input = int.Parse(Console.ReadLine()!);

            switch (input)
            {
                case 1:
                    try
                    {
                        Console.WriteLine("The list of all the cities: ");
                        _cityService.GetAllCities();
                        Console.WriteLine("\n");
                        Console.WriteLine("Enter the name of the city you want to add to the street: ");
                        string cityName = Console.ReadLine()!;
                        if (string.IsNullOrWhiteSpace(cityName))
                        {
                            throw new Exception("City name should not be null or whitespace!");
                        }

                        foreach (char c in cityName)
                        {
                            if (Char.IsDigit(c))
                            {
                                throw new Exception("The input you entered was numeric. Please enter a valid city name!");
                            }
                        }

                        var cityExistsAdd = _context.Cities.Any(c => c.CityName == cityName);
                        if (!cityExistsAdd)
                        {
                            throw new Exception($"The city with the name {cityName} doesn't exist in the database!");
                        }
                        
                        Console.WriteLine($"The list of all the streets of {cityName}");
                        GetAllStreets(cityName);

                        Console.WriteLine("Enter the street name: ");
                        string streetName = Console.ReadLine()!;
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
                        }

                        var streetExists = _context.Streets.Any(s => s.Name == streetName);
                        if (!streetExists)
                        {
                            throw new Exception($"The city with the name {streetName} doesn't exist in the database!");
                        }

                        Console.Write("If the street is one sided please type \"false\", if it has two sides please enter \"true\": ");
                        string sidesNr = Console.ReadLine()!;
                        try
                        {
                            bool sides = Convert.ToBoolean(sidesNr);
                            Console.Write("Enter total valid car parking slots: ");
                            int totalSlots;

                            if (int.TryParse(Console.ReadLine(), out totalSlots))
                            {
                                AddAStreet(cityName, streetName, sides, totalSlots);
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"The value you entered '{sidesNr}' is not a boolean!");
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                    break;

                case 2:
                    try
                    {
                        Console.WriteLine("The list of all the cities: ");
                        _cityService.GetAllCities();
                        Console.WriteLine("\n");
                        Console.WriteLine("Enter the name of the city you want to close the street: ");
                        string cityNameClose = Console.ReadLine()!;
                        if (string.IsNullOrWhiteSpace(cityNameClose))
                        {
                            throw new Exception("City name should not be null or whitespace!");
                        }

                        foreach (char c in cityNameClose)
                        {
                            if (Char.IsDigit(c))
                            {
                                throw new Exception("The input you entered was numeric. Please enter a valid city name!");
                            }
                        }

                        var cityExistsClose = _context.Cities.Any(c => c.CityName == cityNameClose);
                        if (!cityExistsClose)
                        {
                            throw new Exception($"The city with the name {cityNameClose} doesn't exist in the database!");
                        }

                        Console.WriteLine("The list of the streets: ");
                        GetAllStreets(cityNameClose);
                        Console.WriteLine("\n");
                        Console.WriteLine("Enter the name of the street you want to close: ");
                        string givenStreetName = Console.ReadLine()!;
                        if (string.IsNullOrWhiteSpace(givenStreetName))
                        {
                            throw new Exception("Street name should not be null or whitespace!");
                        }

                        foreach (char c in givenStreetName)
                        {
                            if (Char.IsDigit(c))
                            {
                                throw new Exception("The input you entered was numeric. Please enter a valid street name!");
                            }
                        }

                        var streetExists = _context.Streets.Any(s => s.Name == givenStreetName);
                        if (!streetExists)
                        {
                            throw new Exception($"The city with the name {givenStreetName} doesn't exist in the database!");
                        }
                        CloseAStreet(givenStreetName);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                    break;

                case 3:
                    try
                    {
                        Console.WriteLine("The list of all the cities: ");
                        _cityService.GetAllCities();
                        Console.WriteLine("\n");
                        Console.WriteLine("Enter the name of the city you want to validate  the street: ");
                        string cityNameValidate = Console.ReadLine()!;
                        if (string.IsNullOrWhiteSpace(cityNameValidate))
                        {
                            throw new Exception("City name should not be null or whitespace!");
                        }

                        foreach (char c in cityNameValidate)
                        {
                            if (Char.IsDigit(c))
                            {
                                throw new Exception("The input you entered was numeric. Please enter a valid city name!");
                            }
                        }

                        var cityExistsValidate = _context.Cities.Any(c => c.CityName == cityNameValidate);
                        if (!cityExistsValidate)
                        {
                            throw new Exception($"The city with the name {cityNameValidate} doesn't exist in the database!");
                        }
                        Console.WriteLine("The list of the streets: ");
                        GetAllStreets(cityNameValidate);
                        Console.WriteLine("\n");
                        Console.WriteLine("Enter the name of the street you want to validate: ");
                        string streetNameValidate = Console.ReadLine()!;
                        if (string.IsNullOrWhiteSpace(streetNameValidate))
                        {
                            throw new Exception("Street name should not be null or whitespace!");
                        }

                        foreach (char c in streetNameValidate)
                        {
                            if (Char.IsDigit(c))
                            {
                                throw new Exception("The input you entered was numeric. Please enter a valid street name!");
                            }
                        }

                        var streetExists = _context.Streets.Any(s => s.Name == streetNameValidate);
                        if (!streetExists)
                        {
                            throw new Exception($"The city with the name {streetNameValidate} doesn't exist in the database!");
                        }

                        ValidateAStreet(streetNameValidate);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                    break;

                case 4:
                    Console.WriteLine("The list of all the cities: ");
                    _cityService.GetAllCities();
                    Console.WriteLine("\n");
                    Console.WriteLine("Enter the name of the city you want to remove the street from: ");
                    string cityNameRemove = Console.ReadLine()!;
                    if (string.IsNullOrWhiteSpace(cityNameRemove))
                    {
                        throw new Exception("City name should not be null or whitespace!");
                    }

                    foreach (char c in cityNameRemove)
                    {
                        if (Char.IsDigit(c))
                        {
                            throw new Exception("The input you entered was numeric. Please enter a valid city name!");
                        }
                    }

                    var cityExists = _context.Cities.Any(c => c.CityName == cityNameRemove);
                    if (!cityExists)
                    {
                        throw new Exception($"The city with the name {cityNameRemove} doesn't exist in the database!");
                    }

                    Console.WriteLine("The list of the streets: ");
                    GetAllStreets(cityNameRemove);
                    Console.WriteLine("\n");
                    Console.WriteLine("Enter the name of the street you want to remove from the list: ");
                    string streetNameRemove = Console.ReadLine()!;
                    if (string.IsNullOrWhiteSpace(streetNameRemove))
                    {
                        throw new Exception("Street name should not be null or whitespace!");
                    }

                    foreach (char c in streetNameRemove)
                    {
                        if (Char.IsDigit(c))
                        {
                            throw new Exception("The input you entered was numeric. Please enter a valid street name!");
                        }
                    }

                    var streetExistsRemove = _context.Streets.Any(s => s.Name == streetNameRemove);
                    if (!streetExistsRemove)
                    {
                        throw new Exception($"The city with the name {streetNameRemove} doesn't exist in the database!");
                    }
                    RemoveAStreet(streetNameRemove);
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

    //Add a street
    public void AddAStreet(string cityName, string streetName, bool sides, int totalSlots)
    {
        try
        {
            //should not be null or whitespace
            if (string.IsNullOrWhiteSpace(cityName))
            {
                throw new ArgumentNullException("City name should not be null or whitespace!");
            }

            //should not be a number
            foreach (char c in cityName)
            {
                if (Char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric. Please enter a valid city name!");
                }
            }

            //name should not be null or whitespace
            if (string.IsNullOrWhiteSpace(streetName))
            {
                throw new ArgumentNullException("Street name should not be null or whitespace!");
            }

            //should not be a number
            foreach (char c in streetName)
            {
                if (Char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric. Please enter a valid street name!");
                }
            }

            //it should not be a negative number
            if (totalSlots < 0)
            {
                throw new Exception("The input you typed was out of range. Please enter a valid slot number!");
            }
            /*
             SigleOrDefault--> will return default value of a data type of generic collection or for the specified condition.
              It will throw an exception if there is more than one element in a collection of the specified condition.
            */

            var city = _context.Cities.SingleOrDefault(c => c.CityName == cityName);
            var cityExists = _context.Cities.Any(c => c.CityName == cityName);

            if (!cityExists || city == null)
            {
                throw new Exception($"The city with the name '{cityName}' does not exist in the database!");
            }

            var streetExists = _context.Streets.Any(s => s.Name == streetName);
            if (streetExists)
            {
                throw new Exception($"The street with the name {streetName} already exists in the database!");
            }
            else
            {
                var street = new Street
                {
                    Name = streetName,
                    HasTwoSides = sides,
                    TotalValidSlots = totalSlots,
                    CityId = city.Id,
                    CityName = cityName,
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
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not add to the city. {e.Message}");
        }
    }

    //Remove a street
    public void RemoveAStreet(string streetName)
    {
        try
        {
            //should not be null or whitespace
            if (string.IsNullOrWhiteSpace(streetName))
            {
                throw new ArgumentNullException("because street name should not be null or whitespace!");
            }

            //should not be a number
            foreach (char c in streetName)
            {
                if (Char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric. Please enter a valid street name!");
                }
            }

            var street = _context.Streets.FirstOrDefault(s => s.Name == streetName);
            var streetExists = _context.Streets.Any(s => s.Name == streetName);
            if (street != null)
            {
                _context.Streets.Remove(street);
                Console.WriteLine($"The street with the name {streetName} was removed successfully from the database!");
            }
            else if (!streetExists)
            {
                Console.WriteLine($"The street with the name {streetName} doesn't exist in the database!");

            }
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not remove the street. {e.Message}");
        }
    }

    //Close a street
    public bool CloseAStreet(string streetName)
    {
        try
        {
            //Name Should not be null or whitespace
            if (string.IsNullOrWhiteSpace(streetName))
            {
                throw new ArgumentNullException("Street name should not be null or whitespace!");
            }

            //Should not be a number

            foreach (char c in streetName)
            {
                if (Char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric. Please enter a valid street name!");
                }
            }

            var street = _context.Streets.FirstOrDefault(s => s.Name == streetName);
            var streetExists = _context.Streets.Any(s => s.Name == streetName);
            if (streetExists && street.IsClosed == false)
            {
                street.IsClosed = true;
                Console.WriteLine($"The street with the name {streetName} was closed successfully!");
            }
            else if (!streetExists)
            {
                Console.WriteLine($"The street with the name {streetName} doesn't exist in the database!");

            }
            else 
            {
                Console.WriteLine($"The street with the name {streetName} is already closed!");
            }
           
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not close the street. {e.Message}");
        }
        return true;
    }

    //Validate a street
    public bool ValidateAStreet(string streetName)
    {
        try
        {
            //Name Should not be null or whitespace
            if (string.IsNullOrWhiteSpace(streetName))
            {
                throw new ArgumentNullException("Street name should not be null or empty!");
            }

            //Should not be a number
            foreach (char c in streetName)
            {
                if (Char.IsDigit(c))
                {
                    throw new Exception("The input you typed was numeric.Please enter a valid street name!");
                }
            }

            var street = _context.Streets.FirstOrDefault(sn => sn.Name == streetName);

            if(street == null)
            {
                throw new Exception($"because the street doesn't exist!");
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
            Console.WriteLine($"Could not close the street {e.Message}.");
        }
        return true;
    }

    //Get all streets
    public void GetAllStreets(string cityName)
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
    }
}