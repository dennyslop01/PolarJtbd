using System.ComponentModel.DataAnnotations;

namespace Jtbd.Domain.Entities
{
    public class MatrizCSV()
    {
        [Range(0, 1000, ErrorMessage = "El tipo separador es obligatorio.")]
        public int TipoSeparador { get; set; }
    }

    public class GenerarCluster()
    {
        [Range(2, 10, ErrorMessage = "El número de clústeres es obligatorio.")]
        public int NroCluster { get; set; }
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

    public class TablaCluster()
    {
        public int IdCluster { get; set; }
        public int IdGroup { get; set; }
        public string? GroupName { get; set; }
        public int IdStorie { get; set; }
        public string? StorieName { get; set; }
        public int? ValorGroup { get; set; }
    }
}
