using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySQLDapper.Models;

namespace MySQLDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var query = "Select Id, Nombre , Email  From Personas p;";
            var cs = "Server = localhost; Port = 3306; Database = Dapper; Uid = root; Pwd = _RE41Nz000";

            using var db = new MySqlConnection(cs);
            return Ok(db.Query<Persona>(query).ToList());


        }

        [HttpGet("personasdirecciones")]
        public IActionResult GetPersonasDirecciones()
        {
            var query = "Select p.Nombre , p.Email , d.Calle  From Direcciones d\r\nright join Personas p \r\non p.Id = d.PersonaId \r\nORDER BY 1";
            var cs = "Server = localhost; Port = 3306; Database = Dapper; Uid = root; Pwd = _RE41Nz000";

            using var db = new MySqlConnection(cs);
            return Ok(db.Query(query).ToList());


        }

        [HttpPost]
        public IActionResult Post([FromBody] Persona persona)
        {
            var cs = "Server = localhost; Port = 3306; Database = Dapper; Uid = root; Pwd = _RE41Nz000";
            var query = "Insert INTO Personas (Nombre,Email) values (@nombre,@email);";
            using var db = new MySqlConnection(cs);
            var response = db.Execute(query, new { nombre = persona.Nombre, email = persona.Email });
            return Ok(response);
        }
    }
}
