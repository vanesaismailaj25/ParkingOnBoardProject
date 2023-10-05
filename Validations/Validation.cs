using ParkingOnBoard.Context;

namespace ParkingOnBoard.Validations
{
    public class Validation
    {
        private readonly ParkingOnBoardContext _context;

        public Validation()
        {
            _context = new ParkingOnBoardContext();
        }

        public void CityValidatorDBShouldntExist(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                throw new ArgumentNullException("City name should not be null or whitespace!");
            }

            foreach (char c in cityName)
            {
                if (char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric or contains digits. Please enter a valid street name!");
                }
            }

            var cityExists = _context.Cities.Any(n => n.Name == cityName);
            if (cityExists)
            {
                throw new Exception($"The city with the name {cityName} already exists in the database!");
            }
        }

        public void CitiesValidatorShouldExist(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                throw new ArgumentNullException("City name should not be null or whitespace!");
            }

            foreach (char c in cityName)
            {
                if (char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric or contains digits. Please enter a valid street name!");
                }
            }

            var cityExists = _context.Cities.Any(n => n.Name == cityName);
            if (!cityExists)
            {
                throw new Exception($"The city with the name {cityName} doesn't exist in the database!");
            }
        }

        public void StreetValidatorForParking(string cityName, string streetName)
        {
            if (string.IsNullOrWhiteSpace(streetName))
            {
                throw new ArgumentNullException("Street name should not be null or whitespace!");
            }

            foreach (char c in streetName)
            {
                if (char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric or contains digits. Please enter a valid street name!");
                }
            }

            var streetExists = _context.Streets.Where(n => n.Name == streetName && n.City.Name == cityName);
            if (streetExists == null)
            {
                throw new Exception($"The street with the name {streetName} doesn't exist in the database!");
            }

        }
        public void StreetValidatorShouldntExist(string streetName)
        {
            if (string.IsNullOrWhiteSpace(streetName))
            {
                throw new ArgumentNullException("Street name should not be null or whitespace!");
            }

            foreach (char c in streetName)
            {
                if (char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric or contains digits. Please enter a valid street name!");
                }
            }
            
            var streetExists = _context.Streets.Any(n => n.Name == streetName);
            if (streetExists)
            {
                throw new Exception($"The street with the name {streetName} already exists in the database!");
            } 
        }
      
        public void StreetsValidatorShouldExist(string streetName)
        {
            if (string.IsNullOrWhiteSpace(streetName))
            {
                throw new ArgumentNullException("Street name should not be null or whitespace!");
            }

            foreach (char c in streetName)
            {
                if (char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric or contains digits. Please enter a valid street name!");
                }
            }

            var streetExists = _context.Streets.Any(n => n.Name == streetName);
            if (!streetExists)
            {
                throw new Exception($"The street with the name {streetName} doesn't exist in the database!");
            }
        }

        public void SlotValidatorShouldntExist(string name, int slotNumber)
        {
            if (slotNumber < 0)
            {
                throw new Exception("The input you typed was out of range. Please enter a valid slot number!");
            }

            var slotExists = _context.ParkingSlots.Any(s => s.SlotNumber == slotNumber && s.Street.Name == name);
            if (slotExists)
            {
                throw new Exception($"The slot with the number {slotNumber} already exists in the database!");
            }
        }

        public void SlotsValidatorShouldExist(int slotNumber)
        {
            if (slotNumber < 0)
            {
                throw new Exception("The input you typed was out of range. Please enter a valid slot number!");
            }

            var slotExists = _context.ParkingSlots.Any(s => s.SlotNumber == slotNumber);
            if (!slotExists)
            {
                throw new Exception($"The slot with the number {slotNumber} doesn't exist in the database!");
            }
        }      
    }
}
