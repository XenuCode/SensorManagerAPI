using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SensorManagerAPI.Controllers;

[ApiController]
[Route("/instance")]
public class SensorController : ControllerBase
{
    // GET
    [HttpGet("getsensordata")]
    public string GetSensorData()
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myConnectionString;

        myConnectionString = "server=127.0.0.1;uid=root;" +
                             "pwd=;database=test";
        conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
        conn.Open();
        
        
        
    }
}