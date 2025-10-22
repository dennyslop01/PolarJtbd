using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    public class MatrizCSV()
    {
        [Range(0, 1000, ErrorMessage = "El tipo separador es obligatorio.")]
        public int TipoSeparador { get; set; }
    }

    public class Tabla()
    {
        public int IdStorie { get; set; }
        public int IdGroup { get; set; }
        public string? Nombre { get; set; }
        public string? Contexto { get; set; }
        public string? Pushes { get; set; }
        public string? Pulls { get; set; }
        public string? Habits { get; set; }
        public string? Anxieties { get; set; }
        public string? Grupos { get; set; }
        public string? Tipo { get; set; }

        //[Range(0, 2, ErrorMessage = "El valor es requerido")]
        public int ValorSeleccionado { get; set; }

        public string Valor { get; set; }
    }

}
