//using Microsoft.Management.Infrastructure;

//CimSession session = CimSession.Create("localhost");
//IEnumerable<CimInstance> instances = session.QueryInstances("root\\CIMv2", "WQL", "SELECT * FROM Win32_VideoController");

//foreach (CimInstance instance in instances)
//{
//    Console.WriteLine(instance.CimInstanceProperties["Name"].Value);
//    int counter = 0;
//    foreach (var property in instance.CimInstanceProperties)
//    {
//        Console.WriteLine(counter + "   " + property.Name + "\n     " + (property.Value != null ? property.Value : "---Empty---"));
//        counter++;
//    }
//}
//session.Dispose();

using LibreHardwareMonitor.Hardware;

public class UpdateVisitor : IVisitor
{
    public void VisitComputer(IComputer computer)
    {
        computer.Traverse(this);
    }
    public void VisitHardware(IHardware hardware)
    {
        hardware.Update();
        foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
    }
    public void VisitSensor(ISensor sensor) { }
    public void VisitParameter(IParameter parameter) { }
}

public class MyMonitor
{
    public static void Main()
    {
        MyMonitor app = new MyMonitor();
        app.Monitor();
    }

    public void Monitor()
    {
        Computer computer = new Computer
        {
            IsCpuEnabled = true,
            IsGpuEnabled = true,
            IsMemoryEnabled = true,
            IsMotherboardEnabled = true,
            IsControllerEnabled = true,
            IsNetworkEnabled = true,
            IsStorageEnabled = true
        };

        computer.Open();
        computer.Accept(new UpdateVisitor());

        foreach (IHardware hardware in computer.Hardware)
        {
            Console.WriteLine("Hardware: {0}", hardware.Name);

            foreach (IHardware subhardware in hardware.SubHardware)
            {
                Console.WriteLine("\tSubhardware: {0}", subhardware.Name);

                foreach (ISensor sensor in subhardware.Sensors)
                {
                    Console.WriteLine("\t\tSensor: {0}, value: {1}", sensor.Name, sensor.Value);
                }
            }

            foreach (ISensor sensor in hardware.Sensors)
            {
                Console.WriteLine("\tSensor: {0}, value: {1}", sensor.Name, sensor.Value);
            }
        }

        computer.Close();
    }
}
