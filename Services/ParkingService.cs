using ParkingOnBoard.Context;

namespace ParkingOnBoard.Services;

public class ParkingService
{
    private ParkingOnBoardContext _context { get; set; }
    private ParkingSlotService _parkingSlotService { get; set; }
    private CityService _cityService { get; set; }
    private StreetService _streetService { get; set; }
    public ParkingService()
    {
        _context = new ParkingOnBoardContext();
        _parkingSlotService = new ParkingSlotService();
        _cityService = new CityService();
        _streetService = new StreetService();
    }

    public void ManageParkingProcess()
    {
        try
        {
            Console.WriteLine("Choose what operation would you like to do: ");
            Console.WriteLine("1. Parking. \n2.Unparking.");
            int input = int.Parse(Console.ReadLine()!);

            switch (input)
            {
                case 1:
                    try
                    {
                        Console.WriteLine("At what street do you want to park? Please enter a street name or \"*\" if you want to display all streets: ");
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
                        GetFreeParkingSlots(streetName);
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
                        Console.WriteLine("Enter the name of the city you want to select: ");
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

                        Console.WriteLine($"the list of all the streets of {cityName}: ");
                        _streetService.GetAllStreets(cityName);

                        Console.WriteLine("Enter the name of the street you want to free the parking slot from: ");
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

                        Console.WriteLine("Enter the number of the parking slot you wish to free: ");
                        int parkingSlotNr;

                        if ((int.TryParse(Console.ReadLine(), out parkingSlotNr)))
                        {
                            FreeAParkingSlot(streetNameValidate, parkingSlotNr);
                            Console.WriteLine($"The parking slot with the number {parkingSlotNr} is now free!");
                        }
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
            Console.WriteLine($"Error : {e.Message}");
        }
    }

    public void GetFreeParkingSlots(string streetName)
    {
        try
        {
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

            if (streetName == "*")
            {
                var streets = _context.Streets.ToList();
                foreach (var str in streets)
                {
                    Console.WriteLine($"{str.Name}");
                    var parkingSlots = _context.ParkingSlots.Where(ps => ps.StreetId == str.Id && ps.IsBusy == false).ToList();

                    foreach (var ps in parkingSlots)
                    {
                        Console.WriteLine($"Parking slot with number {ps.SlotNumber}");
                    }

                }
                Console.WriteLine("Enter the name of the street you want to occupy a slot from: ");
                var stre = Console.ReadLine()!;

                Console.WriteLine("Enter the slot you want to occupy: ");
                int slotNumber;

                if (int.TryParse(Console.ReadLine(), out slotNumber))
                {
                    if (_parkingSlotService != null)
                    {
                        _parkingSlotService.CloseAParkingSlot(stre, slotNumber);
                        Console.WriteLine("The parking slot is now busy!");
                    }
                    else
                    {
                        Console.WriteLine("The parking slot with the given number is already busy");
                    }
                }
            }

            else
            {
                throw new Exception("The street name you entered does not exist in the database!");
            }


            //Get free parking slots for only a speciific street
            var street = _context.Streets.FirstOrDefault(s => s.Name == streetName);

            if (street != null)
            {
                Console.WriteLine($"Here will be displayed the free parking slots of the street {streetName}: ");

                List<ParkingSlot> parkingSlots = new List<ParkingSlot>();

                parkingSlots = _context.ParkingSlots.Where(ps => ps.StreetId == street.Id && ps.IsBusy == false).ToList();

                foreach (var item in parkingSlots)
                {
                    Console.WriteLine($"Slot number {item.SlotNumber}");
                }

                Console.WriteLine("Enter the slot you want to occupy: ");
                int slotNumber;

                if (int.TryParse(Console.ReadLine(), out slotNumber))
                {
                    if (_parkingSlotService != null)
                    {
                        _parkingSlotService.CloseAParkingSlot(street.Name, slotNumber);
                        Console.WriteLine("The parking slot is now busy!");
                    }
                    else
                    {
                        Console.WriteLine("The parking slot with the given number is already busy");
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not get free parking slots for the street {e.Message}");
        }
    }

    //Unpark a parking slot
    public void FreeAParkingSlot(string streetName, int slotNumber)
    {
        try
        {
            //it should not be null or whitespace
            if (string.IsNullOrWhiteSpace(streetName))
            {
                throw new ArgumentNullException("Street name should not be null or whitespace!");
            }

            //it should not be a numeric
            foreach (char c in streetName)
            {
                if (Char.IsDigit(c))
                {
                    throw new Exception("THe input you typed was numeric.Please enter a valid street name!");
                }
            }

            if (slotNumber < 0)
            {
                throw new Exception("The input you typed was out of range.Please enter a valid slot number!");
            }

            if (_parkingSlotService != null)
            {
                _parkingSlotService.ValidateAParkingSlot(streetName, slotNumber);
            }
            else
            {
                Console.WriteLine("The parking slot is already free to be used!");
            }
        }
        catch (Exception)
        {
            Console.WriteLine($"Could not free the parking slot with number {slotNumber}.");
        }
    }
}
