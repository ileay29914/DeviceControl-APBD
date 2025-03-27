namespace DeviceControl
{
    public class FileDeviceStorage : IDeviceStorage
    {
        private readonly string _filePath = "devices.txt";

        public void Save(List<Device> devices)
        {
            using (var writer = new StreamWriter(_filePath))
            {
                foreach (var device in devices)
                {
                    writer.WriteLine(device.ToString());
                }
            }
        }

        public List<Device> Load()
        {
            var devices = new List<Device>();
            foreach (var line in File.ReadAllLines(_filePath))
            {
                devices.Add(ParseDevice(line));
            }
            return devices;
        }

        private Device ParseDevice(string line)
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
                default:
                    return null;
            }
        }
    }
}