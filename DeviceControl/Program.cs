namespace DeviceControl
{
    class Program
    {
        static void Main(string[] args)
        {
           
            string filePath = "devices.txt";

           
            DeviceManager manager = new DeviceManager();


           
            while (true)
            {
                Console.WriteLine("\nDevice Manager Menu:");
                Console.WriteLine("1. Show All Devices");
                Console.WriteLine("2. Add Device");
                Console.WriteLine("3. Remove Device");
                Console.WriteLine("4. Edit Device Name");
                Console.WriteLine("5. Turn On Device");
                Console.WriteLine("6. Turn Off Device");
                Console.WriteLine("7. Save to File");
                Console.WriteLine("8. Exit");
                Console.Write("Choose an option: ");
                
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        manager.ShowAllDevices();
                        break;

                    case "2":
                        Console.Write("Enter Device Type (SW, P, ED): ");
                        string type = Console.ReadLine();
                        Console.Write("Enter Device ID: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Enter Device Name: ");
                        string name = Console.ReadLine();

                        if (type == "SW")
                        {
                            Console.Write("Enter Battery Percentage: ");
                            int battery = int.Parse(Console.ReadLine());
                            manager.AddDevice(new Smartwatch(id, name, battery));
                        }
                        else if (type == "P")
                        {
                            Console.Write("Enter Operating System (or leave blank): ");
                            string os = Console.ReadLine();
                            manager.AddDevice(new PersonalComputer(id, name, os));
                        }
                        else if (type == "ED")
                        {
                            Console.Write("Enter IP Address: ");
                            string ip = Console.ReadLine();
                            Console.Write("Enter Network Name: ");
                            string network = Console.ReadLine();
                            manager.AddDevice(new EmbeddedDevice(id, name, ip, network));
                        }
                        else
                        {
                            Console.WriteLine("Invalid device type.");
                        }
                        break;

                    case "3":
                        Console.Write("Enter Device ID to Remove: ");
                        int removeId = int.Parse(Console.ReadLine());
                        manager.RemoveDevice(removeId);
                        break;

                    case "4":
                        Console.Write("Enter Device ID to Edit: ");
                        int editId = int.Parse(Console.ReadLine());
                        Console.Write("Enter New Device Name: ");
                        string newName = Console.ReadLine();
                        manager.EditDevice(editId, newName);
                        break;

                    case "5":
                        Console.Write("Enter Device ID to Turn On: ");
                        int onId = int.Parse(Console.ReadLine());
                        manager.TurnOnDevice(onId);
                        break;

                    case "6":
                        Console.Write("Enter Device ID to Turn Off: ");
                        int offId = int.Parse(Console.ReadLine());
                        manager.TurnOffDevice(offId);
                        break;

                    case "7":
                        manager.SaveToFile();
                        break;

                    case "8":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }
    }
}
