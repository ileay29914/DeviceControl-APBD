namespace DeviceControl
{
    public class DeviceManager
    {
        private readonly IDeviceFactory _deviceFactory;
        private readonly IDeviceStorage _deviceStorage;
        private readonly List<Device> _devices;

        
        public DeviceManager(IDeviceFactory deviceFactory, IDeviceStorage deviceStorage)
        {
            _deviceFactory = deviceFactory;
            _deviceStorage = deviceStorage;
            _devices = new List<Device>();
            LoadDevicesFromFile(); 
        }

       
        public void AddDevice(string deviceType, int id, string name)
        {
            var device = _deviceFactory.CreateDevice(deviceType, id, name);
            _devices.Add(device);
            _deviceStorage.Save(_devices);
        }

        
        public void ShowAllDevices()
        {
            if (_devices.Count == 0)
            {
                Console.WriteLine("No devices available.");
                return;
            }

            foreach (var device in _devices)
            {
                Console.WriteLine(device);
            }
        }

        
        public void RemoveDevice(int deviceId)
        {
            var device = _devices.Find(d => d.Id == deviceId);
            if (device == null)
            {
                Console.WriteLine($"Device with ID {deviceId} not found.");
                return;
            }

            _devices.Remove(device);
            Console.WriteLine($"Device removed: {device.Name}");
            _deviceStorage.Save(_devices); 
        }

        
        public void EditDevice(int deviceId, string newName)
        {
            var device = _devices.Find(d => d.Id == deviceId);
            if (device == null)
            {
                Console.WriteLine($"Device with ID {deviceId} not found.");
                return;
            }

            device.Name = newName;
            Console.WriteLine($"Device {deviceId} renamed to {newName}");
            _deviceStorage.Save(_devices); 
        }

        
        public void TurnOnDevice(int deviceId)
        {
            var device = _devices.Find(d => d.Id == deviceId);
            if (device == null)
            {
                Console.WriteLine($"Device with ID {deviceId} not found.");
                return;
            }

            device.TurnOn();
            Console.WriteLine($"{device.Name} is now ON.");
            _deviceStorage.Save(_devices); 
        }

       
        public void TurnOffDevice(int deviceId)
        {
            var device = _devices.Find(d => d.Id == deviceId);
            if (device == null)
            {
                Console.WriteLine($"Device with ID {deviceId} not found.");
                return;
            }

            device.TurnOff();
            Console.WriteLine($"{device.Name} is now OFF.");
            _deviceStorage.Save(_devices); 
        }

       
        private void LoadDevicesFromFile()
        {
            _devices.AddRange(_deviceStorage.Load()); 
        }
    }
}
