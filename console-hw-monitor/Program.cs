using ConsoleHwMonitor;

MyMonitor sensorMonitor = new MyMonitor();

//TODO Loop here is pure trash
for (int i = 0; i < 20; i++)
{
    sensorMonitor.Monitor();
}

