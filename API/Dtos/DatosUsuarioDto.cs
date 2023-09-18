using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class DatosUsuarioDto
    {
        public string Message {get;set;}
        public string IsRequired {get;set;}
        public string Name {get;set;}
        public List<string> Rols {get;set;}
        public string Token {get;set;}
    }
}