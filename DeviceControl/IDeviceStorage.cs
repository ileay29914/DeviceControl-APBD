namespace DeviceControl
{
    public interface IDeviceStorage
    {
        void Save(List<Device> devices);
        List<Device> Load();
    }
}