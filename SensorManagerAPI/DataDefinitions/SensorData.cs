using System.Data.SqlTypes;
using MySql.Data.Types;

namespace SensorManagerAPI.DataDefinitions;

public class SensorData
{
    public string data { get; set; }
    public DateTime time { get; set; }
    public int data_type { get; set; }
}

public class SensorDataJson
{
    public List<SensorData> SensorDataArr { get; set; }
}