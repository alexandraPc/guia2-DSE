using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Personas.Models
{
    public class Persona
    {

        public int ID { get; set; }
        public string DUI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }

    }

    public class PersonaDBContext : DbContext
    {
        public DbSet<Persona> Personas { get; set; }
    }
}