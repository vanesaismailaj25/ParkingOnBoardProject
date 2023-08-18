using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ParkingOnBoard.Context;
using ParkingOnBoard.Validations;

namespace ParkingOnBoard.Services;

public class ParkingService
{
    private readonly ParkingOnBoardContext _context;
    private readonly ParkingSlotService _parkingSlotService;
    private readonly CityService _cityService;
    private readonly StreetService _streetService;
    private readonly Validation _validations;
    public ParkingService()
    {
        _context = new ParkingOnBoardContext();
        _parkingSlotService = new ParkingSlotService();
        _cityService = new CityService();
        _streetService = new StreetService();
        _validations = new Validation();
    }

    public void ManageParkingProcess()
    {
        try
        {
            Console.WriteLine("Choose what operation would you like to do: ");
            Console.WriteLine("1. Parking. \n2.Unparking. \n3.Main menu.");
            int input = int.Parse(Console.ReadLine()!);

            switch (input)
            {
                case 1:
                    try
                    {
                        Console.WriteLine("The cities list: ");
                        var result = _cityService.GetAllCities();
                        if (result.Count > 0)
                        {
                            Console.WriteLine("Enter the name of the city: ");
                            string city = Console.ReadLine()!;

                            _validations.CitiesValidatorShouldExist(city);

                            Console.WriteLine("At what street do you want to park? Please enter a street name or \"*\" if you want to display all streets: ");
                            string streetName = Console.ReadLine()!;

                            _validations.StreetValidatorForParking(city, streetName);
                            GetFreeParkingSlots(city, streetName);
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
                            Console.WriteLine("Enter the name of the city you want to select: ");
                            string cityName = Console.ReadLine()!;

                            _validations.CitiesValidatorShouldExist(cityName);

                            Console.WriteLine($"The list of all the streets of {cityName}: ");
                            var result = _streetService.GetAllStreets(cityName);

                            if (result.Count > 0)
                            {
                                Console.WriteLine("Enter the name of the street you want to free the parking slot from: ");
                                string streetNameValidate = Console.ReadLine()!;

                                _validations.StreetsValidatorShouldExist(streetNameValidate);

                                var parkingSlotsResult = _parkingSlotService.GetAllParkingSlots(streetNameValidate);
                                if (parkingSlotsResult.Count > 0)
                                {
                                    Console.WriteLine("Enter the number of the parking slot you wish to free: ");
                                    int parkingSlotNr;

                                    if (int.TryParse(Console.ReadLine(), out parkingSlotNr) && parkingSlotNr > 0)
                                    {  
                                        FreeAParkingSlot(streetNameValidate, parkingSlotNr);
                                    }
                                }
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                    break;

                case 3:
                    Console.WriteLine("Main menu!");
                    RunService.User();
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

    public void GetFreeParkingSlots(string cityName, string streetName)
    {
        try
        {

            if (streetName == "*")
            {
                var streets = _context.Streets.Where(s => s.City.CityName == cityName).ToList();

                foreach (var str in streets)
                {
                    Console.WriteLine($"{str.Name}");
                    var parkingSlots = _context.ParkingSlots.Where(ps => ps.StreetId == str.Id && ps.IsBusy == false && ps.IsClosed == false).ToList();
                    if (parkingSlots.Count > 0)
                    {
                        foreach (var ps in parkingSlots)
                        {
                            Console.WriteLine($"Parking slot with number {ps.SlotNumber}");
                        }
                    }
                    else
                    {
                        throw new Exception($"There are no free parking slots for the street {streetName}. You cannot park here!");
                    }
                }

                Console.WriteLine("Enter the name of the street you want to occupy a slot from: ");
                var stre = Console.ReadLine()!;

                _validations.StreetsValidatorShouldExist(stre);


                Console.WriteLine("Enter the slot you want to occupy: ");
                int slotNumber;

                if (int.TryParse(Console.ReadLine(), out slotNumber))
                {
                    _validations.SlotsValidatorShouldExist(slotNumber);
                    
                    if (_parkingSlotService != null)
                    {
                        _parkingSlotService.OccupyAParkingSlot(stre, slotNumber);
                        Console.WriteLine("The parking slot is now busy!");
                    }
                    else
                    {
                        Console.WriteLine("The parking slot with the given number is already busy");
                    }
                }
            }

            //Get free parking slots for only a specific street
            var street = _context.Streets.FirstOrDefault(s => s.Name == streetName);

            if (street != null)
            {

                List<ParkingSlot> parkingSlots = new List<ParkingSlot>();

                parkingSlots = _context.ParkingSlots.Where(ps => ps.StreetId == street.Id && ps.IsBusy == false && ps.IsClosed == false).ToList();

                Console.WriteLine($"Here will be displayed the free parking slots of the street {streetName}: ");

                foreach (var item in parkingSlots)
                {
                    Console.WriteLine($"Slot number {item.SlotNumber}");
                }
                if (parkingSlots.Count > 0)
                {

                    Console.WriteLine("Enter the slot you want to occupy: ");
                    int slotNumber;

                    if (int.TryParse(Console.ReadLine(), out slotNumber))
                    {
                        if (_parkingSlotService != null)
                        {
                            _parkingSlotService.OccupyAParkingSlot(street.Name, slotNumber);
                        }
                        else
                        {
                            Console.WriteLine("The parking slot with the given number is already busy");
                        }
                    }
                }
                else
                {
                    throw new Exception($"There are no free parking slots for the street {streetName}.");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    //Unpark a parking slot
    public void FreeAParkingSlot(string streetName, int slotNumber)
    {
        try
        {
            if (_parkingSlotService != null)
            {
                _parkingSlotService.ValidateAParkingSlot(streetName, slotNumber);
            }
            else
            {
                Console.WriteLine("The parking slot is already free to be used!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
}
