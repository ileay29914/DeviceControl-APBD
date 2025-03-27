namespace DeviceControl
{
    class Program
    {
        static void Main(string[] args)
        {
            IDeviceFactory deviceFactory = new DeviceFactory();
            IDeviceStorage deviceStorage = new FileDeviceStorage();
            DeviceManager deviceManager = new DeviceManager(deviceFactory, deviceStorage);

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
                        deviceManager.ShowAllDevices();
                        break;
                    case "2":
                        Console.Write("Enter Device Type (smartwatch, personalcomputer): ");
                        string type = Console.ReadLine();
                        Console.Write("Enter Device ID: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Enter Device Name: ");
                        string name = Console.ReadLine();
                        deviceManager.AddDevice(type, id, name);
                        break;
                    case "3":
                        Console.Write("Enter Device ID to Remove: ");
                        int removeId = int.Parse(Console.ReadLine());
                        deviceManager.RemoveDevice(removeId);
                        break;
                    case "4":
                        Console.Write("Enter Device ID to Edit: ");
                        int editId = int.Parse(Console.ReadLine());
                        Console.Write("Enter New Device Name: ");
                        string newName = Console.ReadLine();
                        deviceManager.EditDevice(editId, newName);
                        break;
                    case "5":
                        Console.Write("Enter Device ID to Turn On: ");
                        int onId = int.Parse(Console.ReadLine());
                        deviceManager.TurnOnDevice(onId);
                        break;
                    case "6":
                        Console.Write("Enter Device ID to Turn Off: ");
                        int offId = int.Parse(Console.ReadLine());
                        deviceManager.TurnOffDevice(offId);
                        break;
                    case "7":
                        deviceManager.ShowAllDevices();
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
