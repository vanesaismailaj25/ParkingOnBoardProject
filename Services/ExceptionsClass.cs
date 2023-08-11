using ParkingOnBoard.Context;

namespace ParkingOnBoard.Services
{ 
    public class ExceptionsClass
    {
        private  readonly ParkingOnBoardContext _context;

        public ExceptionsClass()
        {
            _context = new ParkingOnBoardContext();
        }

        public void ExceptionHandlerCities(string cityName)
        {
            //Should not be null or whitespace
            if (string.IsNullOrWhiteSpace(cityName))
            {
                throw new ArgumentNullException("City name should not be null or whitespace!");
            }

            //Should not be a number
            foreach (char c in cityName)
            {
                if (Char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric. Please enter a valid street name!");
                }
            }

            //Should not exist in the db
            var cityExists = _context.Cities.Any(n => n.CityName == cityName);
            if (cityExists)
            {
                throw new Exception($"The city with the name {cityName} already exists in the database!");
            }
        }
        
        public  void ExceptionHandlerStreets(string streetName)
        {
            //Should not be null or whitespace
            if (string.IsNullOrWhiteSpace(streetName))
            {
                throw new ArgumentNullException("City name should not be null or whitespace!");
            }

            //Should not be a number
            foreach (char c in streetName)
            {
                if (Char.IsDigit(c))
                {
                    throw new Exception("The input you entered was numeric. Please enter a valid street name!");
                }
            }

            //Should not exist in the db
            var cityExists = _context.Cities.Any(n => n.CityName == streetName);
            if (cityExists)
            {
                throw new Exception($"The city with the name {streetName} already exists in the database!");
            }
        }
    }
}
