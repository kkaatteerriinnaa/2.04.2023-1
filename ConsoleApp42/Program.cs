using System;

// Интерфейсы для устройств компьютера
interface IPowerSupply
{
    void PowerOn();
    void PowerOff();
}

interface ISensors
{
    bool CheckVoltage();
    int CheckTemperature();
}

interface IVideoCard
{
    void PowerOn();
    bool CheckMonitorConnection();
    void DisplayMemoryInfo();
}

interface IRAM
{
    void PowerOn();
    void RunSelfTest();
    void AnalyzeMemory();
    void ClearMemory();
}

interface IOpticalDrive
{
    void PowerOn();
    bool CheckDiskPresence();
    void ReturnToStartPosition();
}

interface IHardDrive
{
    void PowerOn();
    void RunSelfTest();
    bool CheckBootSector();
    void DisplayDriveInfo();
}

// Класс, объединяющий все устройства и выполняющий процесс загрузки
class ComputerFacade
{
    private IPowerSupply powerSupply;
    private ISensors sensors;
    private IVideoCard videoCard;
    private IRAM ram;
    private IOpticalDrive opticalDrive;
    private IHardDrive hardDrive;

    public ComputerFacade(IPowerSupply powerSupply, ISensors sensors, IVideoCard videoCard,
                          IRAM ram, IOpticalDrive opticalDrive, IHardDrive hardDrive)
    {
        this.powerSupply = powerSupply;
        this.sensors = sensors;
        this.videoCard = videoCard;
        this.ram = ram;
        this.opticalDrive = opticalDrive;
        this.hardDrive = hardDrive;
    }

    public void BeginWork()
    {
        Console.WriteLine("Computer boot process started");

        // Подать питание на блок питания
        powerSupply.PowerOn();

        // Проверить напряжение
        if (!sensors.CheckVoltage())
        {
            Console.WriteLine("Voltage check failed, shutting down computer");
            powerSupply.PowerOff();
            return;
        }

        // Проверить температуру в блоке питания
        int psuTemp = sensors.CheckTemperature();
        Console.WriteLine("Power supply temperature: {0} C", psuTemp);
        if (psuTemp > 50)
        {
            Console.WriteLine("Power supply overheating, shutting down computer");
            powerSupply.PowerOff();
            return;
        }

        // Проверить температуру в видеокарте
        int gpuTemp = sensors.CheckTemperature();
        Console.WriteLine("Video card temperature: {0} C", gpuTemp);
        if (gpuTemp > 90)
        {
            Console.WriteLine("Video card overheating, shutting down computer");
            powerSupply.PowerOff();
            return;
        }

        // Подать питание на видеокарту
        videoCard.PowerOn();

        // Запустить видеокарту
        Console.WriteLine("Video card started");

        // Проверить связь с монитором
        if (!videoCard.CheckMonitorConnection())
        {
            Console.WriteLine("Video card - monitor connection failed, shutting down computer");
            powerSupply.PowerOff();
            return;
        }

        // Проверить температуру в оперативной памяти
        int ramTemp = sensors.CheckTemperature();
        Console.WriteLine("RAM temperature: {0} C", ramTemp);
    }
}



