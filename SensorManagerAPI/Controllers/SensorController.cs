using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace SensorManagerAPI.Controllers;

[ApiController]
[Route("/sensors")]
public class SensorController : ControllerBase
{
    // GET
    [HttpGet("getsensordata")]
    public string GetSensorData(string sensor_UUID,int number_of_records)
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        conn = new MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.AppSettings["sensors_credentials"]);
        conn.Open();
        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = $"SELECT * FROM sensors.`{sensor_UUID}` ORDER BY ID DESC LIMIT {number_of_records}";
        cmd.Prepare();
        var a = cmd.ExecuteReader();
        
        return "";
    }
}