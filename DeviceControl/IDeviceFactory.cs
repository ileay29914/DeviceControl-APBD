namespace DeviceControl
{
    public interface IDeviceFactory
    {
        Device CreateDevice(string deviceType, int id, string name);
    }
}