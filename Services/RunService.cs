namespace ParkingOnBoard.Services
{
    public class RunService
    {
        public const string admin = "admin";
        public const string user = "user";
        public const string apass = "admin";
        public const string upass = "user";

        public static void RunProgram()
        {
            bool isLoggedIn = false;
            string password;

            while (!isLoggedIn)
            {
                Console.WriteLine("Welcome to \"Parking on board!\" console application! \n");
                try
                {
                    Console.WriteLine("Are you an admin or a user?");
                    string answer = Console.ReadLine()!;

                    if (string.IsNullOrWhiteSpace(answer))
                    {
                        throw new Exception("Input should not be null or whitespace!");
                    }

                    foreach (char a in answer)
                    {
                        if (Char.IsDigit(a))
                        {
                            throw new Exception("The input you entered was numeric or contains digits. Please enter a valid input!");
                        }
                    }

                    if (answer != admin && answer != user)
                    {
                        throw new Exception("Please enter a valid input!");
                    }

                    Console.WriteLine("Enter your password: ");
                    password = Console.ReadLine()!;

                    if (string.IsNullOrWhiteSpace(password))
                    {
                        throw new Exception("Input should not be null or whitespace!");
                    }

                    foreach (char p in password)
                    {
                        if (Char.IsDigit(p))
                        {
                            throw new Exception("The input you entered was numeric or contains digits. Please enter a valid input!");
                        }
                    }

                    Console.Clear();

                    if (password != admin && password != user)
                    {
                        throw new Exception("Please enter a valid input!");
                    }

                    if (answer == admin && password == apass)
                    {
                        isLoggedIn = true;
                        Admin();
                    }
                    else if (answer == user && password == upass)
                    {
                        isLoggedIn = true;
                        User();
                    }
                    Console.Clear();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
        }

        public static void Admin()
        {
            StreetService streetData = new StreetService();
            ParkingSlotService slotData = new ParkingSlotService();
            CityService cityData = new CityService();

            bool finished = false;

            while (!finished)
            {
                try
                {
                    Console.WriteLine("Welcome admin! \nPlease choose what action you want to do!");
                    Console.WriteLine("0.Manage cities. \n1.Managing information on the streets. \n2.Managing parking slots. \n3.Exit the program. \n4.Log out. \nChoose: ");

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
                        case 4:
                            RunProgram();
                            break;
                        default:
                            Console.WriteLine("Invalid option!");
                            break;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
                Console.WriteLine("Press \"Enter\" to continue: ");
                while(Console.ReadKey().Key != ConsoleKey.Enter) { }
                Console.Clear();
            }
        }

        public static void User()
        {
            ParkingService parkingData = new ParkingService();
            StatisticsService statisticsData = new StatisticsService();

            bool finished = false;

            while (!finished)
            {
                try
                {
                    Console.WriteLine("Welcome user! \nPlease choose what action you want to do!");
                    Console.WriteLine("\n1.Parking. \n2.Statistics. \n3.Exit the program. \n4.Log out. \nChoose: ");

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
                        case 4:
                            RunProgram();
                            break;
                        default:
                            Console.WriteLine("Invalid operation!");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
                Console.WriteLine("Press \"Enter\" to continue: ");
                while(Console.ReadKey().Key != ConsoleKey.Enter) { }
                Console.Clear();
            }
        }
    }
}
