namespace SensorManagerAPI.DataDefinitions;

public class SensorList
{
    public List<Sensor> sensors { set; get; }
}

public class Sensor
{
    public string uuid { set; get; }
    public string name { set; get; }
}