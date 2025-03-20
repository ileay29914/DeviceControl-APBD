using System;
using System.Collections.Generic;
using System.IO;

namespace DeviceControl
{
    public class DeviceManager
    {
        private readonly string _filePath;
        private readonly List<Device> _devices;

        public DeviceManager()
        {
            _filePath = "devices.txt";
            _devices = new List<Device>();
            LoadDevicesFromFile();
        }

     
        private void LoadDevicesFromFile()
        {
            if (!File.Exists(_filePath))
            {
                Console.WriteLine($"️ Device file '{_filePath}' not found. Creating a new one...");
                File.Create(_filePath).Close();
                return;
            }

            Console.WriteLine($" Loading devices from {_filePath}...");

            foreach (var line in File.ReadAllLines(_filePath))
            {
                try
                {
                    var device = ParseDevice(line);
                    if (device != null)
                    {
                        _devices.Add(device);
                        Console.WriteLine($" Loaded: {device}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Skipping invalid line: {line}. Error: {ex.Message}");
                }
            }
        }

      
        public void SaveToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_filePath, false))
                {
                    foreach (var device in _devices)
                    {
                        if (device is Smartwatch sw)
                            writer.WriteLine($"SW-{sw.Id},{sw.Name},{sw.IsOn},{sw.BatteryPercentage}%");
                        else if (device is PersonalComputer pc)
                            writer.WriteLine($"P-{pc.Id},{pc.Name},{pc.IsOn},{pc.OperatingSystem ?? "null"}");
                        else if (device is EmbeddedDevice ed)
                            writer.WriteLine($"ED-{ed.Id},{ed.Name},{ed.IpAddress},{ed.NetworkName}");
                    }
                }
                Console.WriteLine($" Devices saved successfully to {_filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error saving devices: {ex.Message}");
            }
        }

        
        public void AddDevice(Device device)
        {
            if (_devices.Exists(d => d.Id == device.Id))
            {
                Console.WriteLine($" Device with ID {device.Id} already exists.");
                return;
            }

            _devices.Add(device);
            Console.WriteLine($" Device added: {device.Name}");

            SaveToFile();
        }

       
        public void RemoveDevice(int deviceId)
        {
            var device = _devices.Find(d => d.Id == deviceId);
            if (device == null)
            {
                Console.WriteLine($"️ Device {deviceId} not found.");
                return;
            }

            _devices.Remove(device);
            Console.WriteLine($" Device removed: {device.Name}");
            SaveToFile();
        }


        public void EditDevice(int deviceId, string newName)
        {
            var device = _devices.Find(d => d.Id == deviceId);
            if (device == null)
            {
                Console.WriteLine($" Device {deviceId} not found.");
                return;
            }

            device.Name = newName;
            Console.WriteLine($" Device {deviceId} renamed to {newName}");
            SaveToFile();
        }

        
        public void TurnOnDevice(int deviceId)
        {
            var device = _devices.Find(d => d.Id == deviceId);
            if (device == null)
            {
                Console.WriteLine($"️ Device {deviceId} not found.");
                return;
            }

            try
            {
                device.TurnOn();
                Console.WriteLine($" {device.Name} is now ON.");
                SaveToFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error turning on device: {ex.Message}");
            }
        }

        
        public void TurnOffDevice(int deviceId)
        {
            var device = _devices.Find(d => d.Id == deviceId);
            if (device == null)
            {
                Console.WriteLine($"️ Device {deviceId} not found.");
                return;
            }

            device.IsOn = false;
            Console.WriteLine($" {device.Name} is now OFF.");
            SaveToFile();
        }

     
        public void ShowAllDevices()
        {
            if (_devices.Count == 0)
            {
                Console.WriteLine(" No devices available.");
                return;
            }

            foreach (var device in _devices)
            {
                Console.WriteLine(device);
            }
        }

     
        private Device? ParseDevice(string line)
        {
            var parts = line.Split(',');
            if (parts.Length < 3)
                return null;

            string typeAndId = parts[0];
            string[] typeParts = typeAndId.Split('-');

            if (typeParts.Length != 2 || !int.TryParse(typeParts[1], out int id))
                return null;

            string type = typeParts[0];
            string name = parts[1];
            bool isOn = bool.Parse(parts[2]);

            switch (type)
            {
                case "SW":
                    int battery = int.Parse(parts[3].Replace("%", ""));
                    return new Smartwatch(id, name, battery) { IsOn = isOn };

                case "P":
                    string os = parts.Length > 3 && parts[3] != "null" ? parts[3] : null;
                    return new PersonalComputer(id, name, os) { IsOn = isOn };

                case "ED":
                    if (parts.Length < 4) return null;
                    string ip = parts[3];
                    string network = parts[4];
                    return new EmbeddedDevice(id, name, ip, network);

                default:
                    return null;
            }
        }
    }
}
