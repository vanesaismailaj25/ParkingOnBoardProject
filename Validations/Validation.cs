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

        public void CityValidator(string cityName)
        {
            //Should not be null or whitespace
            if (string.IsNullOrWhiteSpace(cityName))
            {
                throw new ArgumentNullException("City name should not be null or whitespace!");
            }

            //Should not be a number
            foreach (char c in cityName)
            {
                if (char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric or contains digits. Please enter a valid street name!");
                }
            }

            //Should not exist in the db
            var cityExists = _context.Cities.Any(n => n.CityName == cityName);
            if (cityExists)
            {
                throw new Exception($"The city with the name {cityName} already exists in the database!");
            }
        }

        public void CitiesValidator(string cityName)
        {
            //Should not be null or whitespace
            if (string.IsNullOrWhiteSpace(cityName))
            {
                throw new ArgumentNullException("City name should not be null or whitespace!");
            }

            //Should not be a number
            foreach (char c in cityName)
            {
                if (char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric or contains digits. Please enter a valid street name!");
                }
            }

            //Should  exist in the db
            var cityExists = _context.Cities.Any(n => n.CityName == cityName);
            if (!cityExists)
            {
                throw new Exception($"The city with the name {cityName} doesn't exist in the database!");
            }
        }

        public void StreetValidatorForParking(string cityName, string streetName)
        {
            //Should not be null or whitespace
            if (string.IsNullOrWhiteSpace(streetName))
            {
                throw new ArgumentNullException("Street name should not be null or whitespace!");
            }

            //Should not be a number
            foreach (char c in streetName)
            {
                if (char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric or contains digits. Please enter a valid street name!");
                }
            }

            var streetExists = _context.Streets.Where(n => n.Name == streetName && n.CityName == cityName);
            if (streetExists is null)
            {
                throw new Exception($"The street with the name {streetName} doesn't exist in the database!");
            }

        }
        public void StreetValidator(string streetName)
        {
            //Should not be null or whitespace
            if (string.IsNullOrWhiteSpace(streetName))
            {
                throw new ArgumentNullException("Street name should not be null or whitespace!");
            }

            //Should not be a number
            foreach (char c in streetName)
            {
                if (char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric or contains digits. Please enter a valid street name!");
                }
            }

            //Should not exist in the db--ky validation hyn ne pune vetem per AddACity method
            var streetExists = _context.Streets.Any(n => n.Name == streetName);
            if (streetExists)
            {
                throw new Exception($"The street with the name {streetName} already exists in the database!");
            }
     
        }
      
        public void StreetsValidator(string streetName)
        {
            //Should not be null or whitespace
            if (string.IsNullOrWhiteSpace(streetName))
            {
                throw new ArgumentNullException("Street name should not be null or whitespace!");
            }

            //Should not be a number
            foreach (char c in streetName)
            {
                if (char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric or contains digits. Please enter a valid street name!");
                }
            }

            //Should exist in the db
            var streetExists = _context.Streets.Any(n => n.Name == streetName);
            if (!streetExists)
            {
                throw new Exception($"The street with the name {streetName} doesn't exist in the database!");
            }
        }

        public void SlotValidator(string name, int slotNumber)
        {
            if (slotNumber < 0)
            {
                throw new Exception("The input you typed was out of range. Please enter a valid slot number!");
            }

            var slotExists = _context.ParkingSlots.Any(s => s.SlotNumber == slotNumber && s.StreetName == name);
            if (slotExists)
            {
                throw new Exception($"The slot with the number {slotNumber} already exists in the database!");
            }
        }

        public void SlotsValidator(int slotNumber)
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
