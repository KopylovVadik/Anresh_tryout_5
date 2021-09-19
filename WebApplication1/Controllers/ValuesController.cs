using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.X509Certificates;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        
        private const string CONNECTION_STRING = @"Server=DESKTOP-30N0GBN\SQLEXPRESS;Database=AnReshProbation;Trusted_Connection=True;";

        [HttpGet("{param}")]
        public ActionResult Get(string param)
        {


            if (param == "department")
            {


                using (var connection = new SqlConnection(CONNECTION_STRING))
                {
                    var names = connection.Query<Department>(
                        "SELECT * FROM [AnReshProbation].[dbo].[Department]").ToList();
                    return Ok(names);
                }

            }
          

            if (param == "employees")
            {
                using (var connection = new SqlConnection(CONNECTION_STRING))
                {
                    dynamic test = connection.Query<dynamic>("SELECT em.[id] as id, em.[id_department] as id_department, em.[fio] as fio, de.[id] as Dp_id, de.[name] as Dp_name, em.[salary] FROM Employees em INNER JOIN Department de ON em.id_department=de.id");

                  

                    Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(Employees), new List<string> { "id" });
                    Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(Department), new List<string> { "id" });
                    

                    var testData = (Slapper.AutoMapper.MapDynamic<Employees>(test) as IEnumerable<Employees>).ToList();
                    return Ok(testData);
                }
            }
            else
            {
                return BadRequest();
            }

        }

    }
}





public class Department
{
    public int id { get; set; }
    public string name { get; set; }
    
}


public class Employees
{
    public int id {get; set; }
    public int id_department { get; set; }

    public string fio { get; set; }

    public float salary { get; set; }
    public List<Department> Dp { get; set; }

}




