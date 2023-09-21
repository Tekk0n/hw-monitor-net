using LibreHardwareMonitor.Hardware;

namespace ConsoleHwMonitor
{
    public class MyMonitor
    {
        public void Monitor()
        {
            Computer computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                //IsMemoryEnabled = true,
                //IsMotherboardEnabled = true,
                //IsControllerEnabled = true,
                //IsNetworkEnabled = true,
                //IsStorageEnabled = true
            };

            computer.Open();
            computer.Accept(new UpdateVisitor());

            //Iterates over desired hardware instead of all computer.Hardware
            var filteredHardware = computer.Hardware.Where(hardware => hardware.HardwareType == HardwareType.Cpu || hardware.HardwareType == HardwareType.GpuNvidia);

            foreach (IHardware hardware in filteredHardware)
            {
                Console.WriteLine("Hardware: {0}", hardware.Name);

                foreach (ISensor sensor in hardware.Sensors.Where(sensor => sensor.SensorType == SensorType.Temperature))
                {
                    Console.WriteLine("\tSensor: {0}, value: {1}", sensor.Name, sensor.Value);
                }
            }

            computer.Close();
        }
    }
}
