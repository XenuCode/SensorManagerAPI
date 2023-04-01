using System.Text.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SensorManagerAPI.DataDefinitions;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace SensorManagerAPI.Controllers;

[ApiController]
[Route("/sensors")]
[EnableCors]
public class SensorController : ControllerBase
{
    // GET
    [HttpGet("getsensordata")]
    public string GetSensorData(string sensor_UUID,int number_of_records)
    {
        MySqlConnection conn;
        conn = new MySqlConnection(ConfigurationManager.AppSettings["sensors_credentials"]);
        conn.Open();
        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = $"SELECT data_type, data, time FROM sensors.`{sensor_UUID}` ORDER BY ID DESC LIMIT {number_of_records}";
        cmd.Prepare();
        var a = cmd.ExecuteReader();
        SensorDataJson sensorDataJson = new SensorDataJson();
        sensorDataJson.SensorDataArr = new List<SensorData>();
        for(int x=0; x < number_of_records; x++)
        {
            if(a.Read())
            {
                SensorData data = new SensorData();
                data.data_type = a.GetInt32("data_type");
                data.data = a.GetString("data");
                data.time = a.GetDateTime("time");
                sensorDataJson.SensorDataArr.Add(data);
            }
            else
            {
                break;
            }
        }
        return JsonSerializer.Serialize(sensorDataJson, typeof(SensorDataJson));
    }
    [HttpGet("setsensordata")]
    public string SetSensorData(string sensor_UUID,string data)
    {
        MySqlConnection conn;
        conn = new MySqlConnection(ConfigurationManager.AppSettings["sensors_credentials"]);
        conn.Open();
        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = $"INSERT INTO sensors.`{sensor_UUID}` (data_type, data, time) VALUES (DEFAULT,@data, DEFAULT);";
        cmd.Parameters.AddWithValue("@data", data);
        cmd.Prepare();
        cmd.ExecuteNonQuery();
        return "OK";
    }
    
    [HttpGet("getsensors")]
    public string GetSensors()
    {
        MySqlConnection conn;
        conn = new MySqlConnection(ConfigurationManager.AppSettings["sensors_location_credentials"]);
        conn.Open();
        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = $"SELECT uuid, name FROM sensors";
        cmd.Prepare();
        var a = cmd.ExecuteReader();
        SensorList sensorsList = new SensorList();
        sensorsList.sensors = new List<Sensor>();
        
        while(a.Read())
        {
            Sensor sensor = new Sensor();
            sensor.uuid = a.GetString("uuid");
            sensor.name = a.GetString("name");
            sensorsList.sensors.Add(sensor);
        }
            
        return JsonSerializer.Serialize(sensorsList);
    }
}