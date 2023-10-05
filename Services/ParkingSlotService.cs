using ParkingOnBoard.Context;
using ParkingOnBoard.Services;
using ParkingOnBoard.Validations;

namespace ParkingOnBoard;

public class ParkingSlotService
{
    private readonly ParkingOnBoardContext _context;
    private readonly StreetService _streetService;
    private readonly CityService _cityService;
    private readonly Validation _validations;

    public ParkingSlotService()
    {
        _context = new ParkingOnBoardContext();
        _streetService = new StreetService();
        _cityService = new CityService();
        _validations = new Validation();
    }

    public void ManageParkingSlots()
    {
        try
        {
            Console.WriteLine("Choose what operation would you like to do: ");
            Console.WriteLine("1. Add a new parking slot to a street. \n2. Remove a parking slot from a street. \n3. Occupy a parking slot. \n4. Close a parking slot. \n5. Validate a parking slot. \n6.Main menu. \nAnswer: ");
            int input = int.Parse(Console.ReadLine()!);
            Console.Clear();

            switch (input)
            {
                case 1:
                    try
                    {
                        Console.WriteLine("The list of all the cities: ");
                        var citiesResult = _cityService.GetAllCities();
                        if (citiesResult.Count > 0)
                        {
                            Console.WriteLine("Enter the name of the city you want to add the parking slot: ");
                            string cityName = Console.ReadLine()!;

                            _validations.CitiesValidatorShouldExist(cityName);

                            Console.WriteLine($"The list of all the streets of {cityName}");
                            var result = _streetService.GetAllStreets(cityName);
                            if (result.Count > 0)
                            {
                                Console.WriteLine("Enter the name of the street where you want to add the parking slot: ");
                                string streetName = Console.ReadLine()!;

                                _validations.StreetsValidatorShouldExist(streetName);

                                Console.WriteLine("All the parking slots: ");
                                GetAllParkingSlots(streetName);


                                Console.WriteLine("Enter the number of the slot you want to add: ");
                                int slotNumber;

                                if (int.TryParse(Console.ReadLine(), out slotNumber))
                                {
                                    _validations.SlotValidatorShouldntExist(streetName, slotNumber);
                                    AddSlotToTheStreet(streetName, slotNumber);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input for slot number!");
                                }
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
                        var citiesResults = _cityService.GetAllCities();
                        if (citiesResults.Count > 0)
                        {

                            Console.WriteLine("Enter the name of the city you want to remove the parking slot from: ");
                            string cityNameRemove = Console.ReadLine()!;

                            _validations.CitiesValidatorShouldExist(cityNameRemove);

                            Console.WriteLine($"The list of all the streets of {cityNameRemove}");
                            var result = _streetService.GetAllStreets(cityNameRemove);

                            if (result.Count > 0)
                            {
                                Console.WriteLine("Enter the name of the street you want to remove the parking slot from: ");
                                string streetNameRemove = Console.ReadLine()!;

                                _validations.StreetsValidatorShouldExist(streetNameRemove);

                                Console.WriteLine("All the parking slots: ");

                                var slotsResult = GetAllParkingSlots(streetNameRemove);
                                if (slotsResult.Count > 0) 
                                {
                                    Console.WriteLine("Enter the number of the slot you want to remove: ");
                                    int slotNumberRemove;

                                    if (int.TryParse(Console.ReadLine(), out slotNumberRemove))
                                    {
                                        _validations.SlotsValidatorShouldExist(slotNumberRemove);
                                        RemoveASlotFromStreet(streetNameRemove, slotNumberRemove);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input for the slot number!");
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
                    Console.WriteLine("The list of all the cities: ");
                    var cityResult = _cityService.GetAllCities();
                    if (cityResult.Count > 0)
                    {
                        Console.WriteLine("Enter the name of the city you want to remove the parking slot from: ");
                        string cityNameClose = Console.ReadLine()!;

                        _validations.CitiesValidatorShouldExist(cityNameClose);

                        Console.WriteLine($"The list of all the streets of {cityNameClose}");
                        var streetResults = _streetService.GetAllStreets(cityNameClose);

                        if (streetResults.Count > 0)
                        {
                            Console.WriteLine("Enter the name of the street you want to close/make it busy a parking slot: ");
                            string streetNameClose = Console.ReadLine()!;

                            _validations.StreetsValidatorShouldExist(streetNameClose);

                            Console.WriteLine("All the parking slots: ");
                            var slotResult = GetAllFreeParkingSlots(streetNameClose);
                            if (slotResult.Count > 0)
                            {
                                Console.WriteLine("Enter the number of the slot you want to close: ");
                                int slotNumberClose;

                                if (int.TryParse(Console.ReadLine(), out slotNumberClose))
                                {
                                    _validations.SlotsValidatorShouldExist(slotNumberClose);
                                    OccupyAParkingSlot(streetNameClose, slotNumberClose);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input of the slot number!");
                                }
                            }
                        }
                    }
                    break;

                case 4:
                    Console.WriteLine("The list of all the cities: ");
                    var cityResultList = _cityService.GetAllCities();
                    if (cityResultList.Count > 0)
                    {
                        Console.WriteLine("Enter the name of the city you want to remove the parking slot from: ");
                        string cityNameClose = Console.ReadLine()!;

                        _validations.CitiesValidatorShouldExist(cityNameClose);

                        Console.WriteLine($"The list of all the streets of {cityNameClose}");
                        var streetResults = _streetService.GetAllStreets(cityNameClose);

                        if (streetResults.Count > 0)
                        {
                            Console.WriteLine("Enter the name of the street you want to close/make it busy a parking slot: ");
                            string streetNameClose = Console.ReadLine()!;

                            _validations.StreetsValidatorShouldExist(streetNameClose);

                            Console.WriteLine("All the parking slots: ");
                            var slotResult = GetAllFreeParkingSlots(streetNameClose);
                            if (slotResult.Count > 0) 
                            {
                                Console.WriteLine("Enter the number of the slot you want to close: ");
                                int slotNumberClose;

                                if (int.TryParse(Console.ReadLine(), out slotNumberClose))
                                {
                                    _validations.SlotsValidatorShouldExist(slotNumberClose);
                                    CloseAParkingSlot(streetNameClose, slotNumberClose);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input of the slot number!");
                                }
                            }
                        }
                    }
                    break;
                case 5:
                    Console.WriteLine("The list of all the cities: ");
                    var results = _cityService.GetAllCities();

                    if (results.Count > 0)
                    {
                        Console.WriteLine("Enter the name of the city you want to remove the parking slot from: ");
                        string cityNameValidate = Console.ReadLine()!;

                        _validations.CitiesValidatorShouldExist(cityNameValidate);

                        Console.WriteLine($"The list of all the streets of {cityNameValidate}: ");
                        var streetsResultList = _streetService.GetAllStreets(cityNameValidate);
                        if (streetsResultList.Count > 0)
                        {
                            Console.WriteLine("Enter the name of the street you want to validate a parking slot on: ");
                            string streetNameValidate = Console.ReadLine()!;

                            _validations.StreetsValidatorShouldExist(streetNameValidate);

                            Console.WriteLine("The parking slots that will be validated: ");
                            var slotResults = GetAllBusyParkingSlots(streetNameValidate);

                            if (slotResults.Count > 0) 
                            {
                                Console.WriteLine("Enter the slot number to validate: ");
                                int slotNumberValidate;

                                if (int.TryParse(Console.ReadLine(), out slotNumberValidate))
                                {
                                    _validations.SlotsValidatorShouldExist(slotNumberValidate);
                                    ValidateAParkingSlot(streetNameValidate, slotNumberValidate);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input of the slot number!");
                                }
                            }
                        }
                    }
                    break;

                case 6:
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

    public void AddSlotToTheStreet(string streetName, int slotNumber)
    {
        try
        {
            var street = _context.Streets.First(s => s.Name == streetName);

            ParkingSlot slot = new ParkingSlot
            {
                SlotNumber = slotNumber,
                StreetId = street.Id,
            };
            _context.ParkingSlots.Add(slot);
            _context.SaveChanges();

            Console.WriteLine($"The parking lot was added successfully to the street:{streetName}.");
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
            var slot = _context.ParkingSlots.FirstOrDefault(sn => sn.SlotNumber == slotNumber && sn.Street.Name == streetName);

            if (slot == null)
            {
                throw new Exception($"The slot with the number {slotNumber} in the street {streetName} doesn't exist!");
            }
            else
            {
                _context.ParkingSlots.Remove(slot);
                _context.SaveChanges();
                Console.WriteLine($"The slot with the number {slotNumber} in the street {streetName} was removed successfully from the database!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    public void OccupyAParkingSlot(string streetName, int slotNumber)
    {
        try
        {
            var slot = _context.ParkingSlots.FirstOrDefault(sn => sn.SlotNumber == slotNumber && sn.Street.Name == streetName);

            if (slot == null)
            {
                throw new Exception($"The slot with the number {slotNumber} in the street {streetName} doesn't exist!");
            }
            else if (slot.IsBusy == true)
            {
                throw new Exception($"The slot wth the number {slotNumber} in the street {streetName} is already occupied!");
            }
            else
            {
                slot.IsBusy = true;
                _context.ParkingSlots.Update(slot);
                _context.SaveChanges();
                Console.WriteLine($"The slot with the number {slotNumber} is now occupied!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    public void CloseAParkingSlot(string streetName, int slotNumber)
    {
        try
        {
            var slot = _context.ParkingSlots.FirstOrDefault(sn => sn.SlotNumber == slotNumber && sn.Street.Name == streetName);

            if (slot == null)
            {
                throw new Exception($"The slot with the number {slotNumber} in the street {streetName} doesn't exist!");
            }
            else if (slot.IsClosed == true)
            {
                throw new Exception($"The slot with the number {slotNumber} in the street {streetName} is already closed!");
            }
            else
            {
                slot.IsClosed = true;
                _context.ParkingSlots.Update(slot);
                _context.SaveChanges();
                Console.WriteLine($"The slot with the number {slotNumber} is now closed!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    public void ValidateAParkingSlot(string streetName, int slotNumber)
    {
        try
        {
            var slot = _context.ParkingSlots.FirstOrDefault(sn => sn.SlotNumber == slotNumber && sn.Street.Name == streetName);

            if (slot == null)
            {
                throw new Exception($"The slot with the number {slotNumber} in the street {streetName} doesn't exist!");
            }
            else if (slot.IsBusy == false)
            {
                throw new Exception($"The slot wth the number {slotNumber} in the street {streetName} is already validated and opened!");
            }
            else
            {
                slot.IsBusy = false;
                _context.ParkingSlots.Update(slot);
                _context.SaveChanges();
                Console.WriteLine($"The slot with the number {slotNumber} is now validated!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}.");
        }
    }

    public List<ParkingSlot> GetAllParkingSlots(string streetName)
    {
        var parkingSlots = _context.ParkingSlots.Where(ps => ps.Street.Name == streetName && ps.IsClosed == false).ToList();

        if (parkingSlots.Count == 0)
            Console.WriteLine($"There are no parking slots for the street {streetName}.");

        foreach (var parkingSlot in parkingSlots)
        {
            Console.WriteLine($"Parking slot with number: {parkingSlot.SlotNumber}");
        }
        return parkingSlots;
    }

    public List<ParkingSlot> GetAllFreeParkingSlots(string streetName)
    {
        var parkingSlots = _context.ParkingSlots.Where(ps => ps.Street.Name == streetName && ps.IsBusy == false && ps.IsClosed == false).ToList();

        if (parkingSlots.Count == 0)
            Console.WriteLine($"There are no parking slots for the street {streetName}.");

        foreach (var parkingSlot in parkingSlots)
        {
            Console.WriteLine($"Parking slot with number: {parkingSlot.SlotNumber}");
        }
        return parkingSlots;
    }

    public List<ParkingSlot> GetAllBusyParkingSlots(string streetName)
    {
        var parkingSlots = _context.ParkingSlots.Where(ps => ps.Street.Name == streetName && ps.IsBusy == true && ps.IsClosed == false).ToList();

        if (parkingSlots.Count == 0)
            Console.WriteLine($"There are no parking slots for the street {streetName}.");

        foreach (var parkingSlot in parkingSlots)
        {
            Console.WriteLine($"Parking slot with number: {parkingSlot.SlotNumber}");
        }
        return parkingSlots;
    }
}

