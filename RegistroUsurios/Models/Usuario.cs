using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroUsurios.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public int Edad { get; set; }
        public bool Activo { get; set; }

        public override string ToString()
        {
            return $"ID: {Id} | Nombre: {Nombres} {Apellidos} | Correo: {Correo} | Edad: {Edad} | Activo: {Activo}";
        }
    }
}
