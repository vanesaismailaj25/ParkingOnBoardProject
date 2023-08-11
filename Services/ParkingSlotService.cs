using ParkingOnBoard.Context;
using ParkingOnBoard.Services;

namespace ParkingOnBoard;

public class ParkingSlotService
{
    private readonly ParkingOnBoardContext _context;
    private readonly StreetService _streetService;
    private readonly CityService _cityService;

    public ParkingSlotService()
    {
        _context = new ParkingOnBoardContext();
        _streetService = new StreetService();
        _cityService = new CityService();
    }

    public void ManageParkingSlots()
    {
        try
        {
            Console.WriteLine("Choose what operation would you like to do: ");
            Console.WriteLine("1. Add a new parking slot to a street.");
            Console.WriteLine("2. Remove a parking slot from a street.");
            Console.WriteLine("3. Close a parking slot.");
            Console.WriteLine("4. Validate a parking slot.");           
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
                        Console.WriteLine("Enter the name of the city you want to add the parking slot: ");
                        string cityName = Console.ReadLine()!;
                        if(string.IsNullOrWhiteSpace(cityName))
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

                        var cityExists = _context.Cities.Any(c => c.CityName == cityName);
                        if (!cityExists)
                        {
                            throw new Exception($"The city with the name {cityName} doesn't exist in the database!");
                        }

                        Console.WriteLine($"The list of all the streets of {cityName}");
                        _streetService.GetAllStreets(cityName);
                        Console.WriteLine("\n");
                        Console.WriteLine("Enter the name of the street where you want to add the parking slot: ");
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

                        var streetExist = _context.Streets.Any(s => s.Name == streetName);
                        if (!streetExist)
                        {
                            throw new Exception($"The city with the name {streetName} doesn't exist in the database!");
                        }

                        Console.WriteLine("All the parking slots: ");
                        GetAllParkingSlots(streetName);
                        Console.WriteLine("\n");
                        Console.WriteLine("Enter the number of the slot you want to add: ");
                        int slotNumber;

                        if (int.TryParse(Console.ReadLine(), out slotNumber))
                        {
                            AddSlotToTheStreet(streetName, slotNumber);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input for slot number!");
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
                        _cityService.GetAllCities();
                        Console.WriteLine("\n");
                        Console.WriteLine("Enter the name of the city you want to remove the parking slot from: ");
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

                        var cityExistsRemove = _context.Cities.Any(c => c.CityName == cityNameRemove);
                        if (!cityExistsRemove)
                        {
                            throw new Exception($"The city with the name {cityNameRemove} doesn't exist in the database!");
                        }

                        Console.WriteLine($"The list of all the streets of {cityNameRemove}");
                        _streetService.GetAllStreets(cityNameRemove);
                        Console.WriteLine("\n");
                        Console.WriteLine("Enter the name of the street you want to remove the parking slot from: ");
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
                        Console.WriteLine("All the parking slots: ");
                        GetAllParkingSlots(streetNameRemove);
                        Console.WriteLine("\n");
                        Console.WriteLine("Enter the number of the slot you want to remove: ");
                        int slotNumberRemove;

                        if (int.TryParse(Console.ReadLine(), out slotNumberRemove))
                        {
                            RemoveASlotFromStreet(streetNameRemove, slotNumberRemove);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input for the slot number!");
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                    
                break;

                case 3:
                    Console.WriteLine("The list of all the cities: ");
                    _cityService.GetAllCities();
                    Console.WriteLine("\n");
                    Console.WriteLine("Enter the name of the city you want to remove the parking slot from: ");
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

                    Console.WriteLine($"The list of all the streets of {cityNameClose}");
                    _streetService.GetAllStreets(cityNameClose);
                    Console.WriteLine("\n");
                    Console.WriteLine("Enter the name of the street you want to close a parking slot: ");
                    string streetNameClose = Console.ReadLine()!;
                    if (string.IsNullOrWhiteSpace(streetNameClose))
                    {
                        throw new Exception("Street name should not be null or whitespace!");
                    }

                    foreach (char c in streetNameClose)
                    {
                        if (Char.IsDigit(c))
                        {
                            throw new Exception("The input you entered was numeric. Please enter a valid street name!");
                        }
                    }

                    var streetExists = _context.Streets.Any(s => s.Name == streetNameClose);
                    if (!streetExists)
                    {
                        throw new Exception($"The city with the name {streetNameClose} doesn't exist in the database!");
                    }
                    Console.WriteLine("All the parking slots that are not closed already: ");
                    GetAllParkingSlots(streetNameClose);
                    Console.WriteLine("\n");
                    Console.WriteLine("Enter the number of the slot you want to close: ");
                    int slotNumberClose;

                    if (int.TryParse(Console.ReadLine(), out slotNumberClose))
                    {
                        CloseAParkingSlot(streetNameClose, slotNumberClose);                      
                    }
                    else
                    {
                        Console.WriteLine("Invalid input of the slot number!");
                    }
                    break;

                case 4:
                    Console.WriteLine("The list of all the cities: ");
                    _cityService.GetAllCities();
                    Console.WriteLine("\n");
                    Console.WriteLine("Enter the name of the city you want to remove the parking slot from: ");
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

                    Console.WriteLine($"The list of all the streets of {cityNameValidate}");
                    _streetService.GetAllStreets(cityNameValidate);
                    Console.WriteLine("\n");
                    Console.WriteLine("Enter the name of the street you want to validate a parking slot on: ");
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

                    var streetExistsValidate = _context.Streets.Any(s => s.Name == streetNameValidate);
                    if (!streetExistsValidate)
                    {
                        throw new Exception($"The city with the name {streetNameValidate} doesn't exist in the database!");
                    }
                    Console.WriteLine("The parking slots that will be validated: ");
                    GetAllParkingSlots(streetNameValidate);
                    Console.WriteLine("\n");
                    Console.WriteLine("Enter the slot number to validate: ");
                    int slotNumberValidate;

                    if (int.TryParse(Console.ReadLine(), out slotNumberValidate))
                    {
                        ValidateAParkingSlot(streetNameValidate, slotNumberValidate);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input of the slot number!");
                    }
                    break;

                default:
                    Console.WriteLine("Please enter the correct input asked!");
                    break;
            }
        }
        catch(Exception e)
        {
            Console.WriteLine($"Error : {e.Message}");
        } 
    }
  
    public void AddSlotToTheStreet(string streetName,int slotNumber)
    {
        try
        {
            //it should not be null or whitespace
            if(string.IsNullOrWhiteSpace(streetName))
            {
                throw new ArgumentNullException("Street name should not be null or whitespace!");
            }

            //it should not be a number
            foreach(char c in streetName)
            {
                if (Char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric. Please enter a valid street name!");
                }
            }

            //if the number is negative 
            if(slotNumber < 0)
            {
                throw new Exception("The input you entered was out of range. Please enter a valid slot number!");
            }

            var street = _context.Streets.SingleOrDefault(s => s.Name.Trim() == streetName.Trim()); 
            if (street == null)
            {
                throw new Exception($"The street with the name {streetName} does not exist in the database!");
            }
          
            var slotExists = _context.ParkingSlots.Any(s => s.SlotNumber == slotNumber);
            if (!slotExists)
            {
                throw new Exception($"The slot with the number {slotNumber} already exists in the database!");
            }
           
                ParkingSlot slot = new ParkingSlot
                {
                    SlotNumber = slotNumber,
                    StreetId = street.Id,
                    StreetName = streetName                   
                };
                _context.ParkingSlots.Add(slot);
                _context.SaveChanges();

                Console.WriteLine($"The parking lot was added succesfully to the street:{streetName}.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not add the parking slot. {e.Message}");
        }
    }

    public void RemoveASlotFromStreet(string streetName, int slotNumber)
    {
        try
        {
            //it should no be null or whitespace
            if (string.IsNullOrWhiteSpace(streetName))
            {
                throw new ArgumentNullException("Street name should not be null or whitespace!");
            }

            //it should not be a number
            foreach(char c in streetName)
            {
                if (Char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric. Please enter a valid street name!");
                }
            }
            //it shouldn't be a negative nr
            if (slotNumber < 0)
            {
                throw new Exception("The input you entered was out of range. Please enter a valid slot number!");
            }

            var slot = _context.ParkingSlots.FirstOrDefault(sn => sn.SlotNumber == slotNumber && sn.Street.Name.ToLower().Trim() == streetName.ToLower().Trim());
            if (slot != null)
            {
                _context.ParkingSlots.Remove(slot);
            }
            _context.SaveChanges();

            Console.WriteLine($"The slot with the number {slotNumber} was removed successfully from the '{streetName}' street!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not remove the parking slot. {e.Message}");
        }
    }

    public void CloseAParkingSlot(string streetName, int slotNumber)
    {
        try
        {
            //it should not be null or whitespace
            if (string.IsNullOrEmpty(streetName))
            {
                throw new ArgumentNullException("Street name should not be null or whitespace!");
            }

            //it should not be a number
            foreach(char c in streetName)
            {
                if (Char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric. Please enter a valid street name!");
                }
            }
           
            //it shouldn't be a negative nr
            if (slotNumber < 0)
            {
                throw new Exception("The input you entered was out of range. Please enter a valid slot number!");
            }

            var slot = _context.ParkingSlots.FirstOrDefault(sn => sn.SlotNumber == slotNumber && sn.Street.Name.Trim() == streetName.Trim());       
            if (slot != null && slot.IsBusy == false)
            {
                slot.IsBusy = true;
                Console.WriteLine($"The slot with the number {slotNumber} was closed successfully!");
            }
            else
            {
                Console.WriteLine($"The slot with the number {slotNumber} is already closed!");
            }
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not validate parking slot. {e.Message}");
        }
    }

    public void ValidateAParkingSlot(string streetName, int slotNumber)
    {
        try
        {
            //it should not be null or whitespace
            if (string.IsNullOrWhiteSpace(streetName))
            {
                throw new ArgumentNullException("Street name should not be null or whitespace!");
            }

            //should not be a number
            foreach(char c in streetName)
            {
                if (Char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric. Please enter a valid street name");
                }
            }

            //it shouldn't be a negative nr
            if (slotNumber < 0)
            {
                throw new Exception("The input you entered was out of range. Please enter a valid slot number!");
            }

            var slot = _context.ParkingSlots.FirstOrDefault(sn => sn.SlotNumber == slotNumber && sn.Street.Name.Trim() == streetName.Trim());
            if (slot !=null && slot.IsBusy == true)
            {
                slot.IsBusy = false;
                Console.WriteLine($"The slot with the number {slotNumber} was validated successfully!");
            }
            else
            {
                Console.WriteLine($"The slot with the number {slotNumber} is already validated!");
            }
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not validate parking slot. {e.Message}.");
        }
    }

    public void GetAllParkingSlots(string streetName)
    {
        var parkingSlots = _context.ParkingSlots.Where(ps => ps.StreetName == streetName).ToList();
        
        if(parkingSlots.Count == 0)
        {
            Console.WriteLine($"There are no parking slots for the street {streetName}.");
        }
        foreach (var parkingSlot in parkingSlots)
        {
            Console.WriteLine($"Parking slot with number: {parkingSlot.SlotNumber}");
        }
    }
}
    
