namespace DeviceControl
{
    public class DeviceFactory : IDeviceFactory
    {
        public Device CreateDevice(string deviceType, int id, string name)
        {
            switch (deviceType.ToLower())
            {
                case "smartwatch":
                    return new Smartwatch(id, name, 100); 
                case "personalcomputer":
                    return new PersonalComputer(id, name);
                default:
                    throw new ArgumentException("Invalid device type.");
            }
        }
    }
}