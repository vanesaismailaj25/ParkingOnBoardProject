namespace ParkingOnBoard.Services
{
    public class RunService
    {
        public const string admin = "admin";
        public const string user = "user";
        public const string apass = "admin";
        public const string upass = "user";
        //private readonly string name;
        public static void RunProgram()
        {
            bool isLoggedIn = false;
            bool isAdmin = false;
            string password;

            while (!isLoggedIn)
            {
                Console.WriteLine("Welcome to \"Parking on board!\" console application! \n");
                try
                {
                    Console.WriteLine("Are you admin or user?");
                    string answer = Console.ReadLine()!;

                    //should not be null or whitespace
                    if (string.IsNullOrWhiteSpace(answer))
                    {
                        throw new Exception("Input should not be null or whitespace!");
                    }

                    //should not be a number 
                    foreach (char a in answer)
                    {
                        if (Char.IsDigit(a))
                        {
                            throw new Exception("The input you entered was numeric or contains digits. Please enter a valid input!");
                        }
                    }

                    if(answer != admin && answer != user)
                    {
                        throw new Exception("Please enter a valid input!");
                    }

                    Console.WriteLine("Enter your password: ");
                    password = Console.ReadLine()!;

                    if (string.IsNullOrWhiteSpace(password))
                    {
                        throw new Exception("Input should not be null or whitespace!");
                    }

                    //should not be a number 
                    foreach (char p in password)
                    {
                        if (Char.IsDigit(p))
                        {
                            throw new Exception("The input you entered was numeric or contains digits. Please enter a valid input!");
                        }
                    }

                    if (password != admin && password != user)
                    {
                        throw new Exception("Please enter a valid input!");
                    }

                    if (answer == admin &&  password == apass)
                    {
                        isAdmin = true;
                        isLoggedIn = true;
                    }
                    else if(answer == user && password == upass)
                    {
                        isLoggedIn = true;
                    }
                    Console.Clear();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }

            StreetService streetData = new StreetService();
            ParkingService parkingData = new ParkingService();
            StatisticsService statisticsData = new StatisticsService();
            ParkingSlotService slotData = new ParkingSlotService();
            CityService cityData = new CityService();

            bool finished = false;
          
            while (!finished)
            {
                try
                {
                    if (isAdmin)
                    {
                        Console.WriteLine("Welcome admin!");
                        /*cityData.GetAllCities();
                        Console.WriteLine("Enter the name of the city you want to manage information for: ");
                        string name = Console.ReadLine()!;
                        name = name;*/
                        
                        Console.WriteLine("Please choose what action you want to do!");
                        Console.WriteLine("0.Manage cities. \n1.Managing information on the streets. \n2.Managing parking slots. \n3.Exit the program.");
                        Console.WriteLine("Choose: ");

                        var option = Convert.ToInt32(Console.ReadLine());

                        Console.Clear();

                        switch (option)
                        {
                            case 0:
                                Console.WriteLine("You selected the option: \"Managing information on cities\".");
                                cityData.ManageCities();
                                break;

                            case 1:
                                Console.WriteLine("You selected the option: \"Managing information on streets\".");
                                streetData.ManageStreets();
                                break;
                            case 2:
                                Console.WriteLine("You selected the option: \"Managing parking slots.\"");
                                slotData.ManageParkingSlots();
                                break;
                            case 3:
                                finished = true;
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Invalid option!");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Welcome user!");
                        Console.WriteLine("Please choose what action you want to do!");
                        Console.WriteLine("\n1.Parking. \n2.Statistics. \n3.Exit the program.");
                        Console.WriteLine("Choose: ");

                        var option = Convert.ToInt32(Console.ReadLine());

                        Console.Clear();
                        switch (option)
                        {
                            case 1:
                                Console.WriteLine("You selected the option: \"Parking.\" ");
                                parkingData.ManageParkingProcess();
                                break;
                            case 2:
                                Console.WriteLine("You selected the option: \" Statistics.\"");
                                statisticsData.DisplayStatistics();
                                break;
                            case 3:
                                finished = true;
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Invalid operation!");
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }

                Console.WriteLine("Press \"Enter\" to continue: ");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
