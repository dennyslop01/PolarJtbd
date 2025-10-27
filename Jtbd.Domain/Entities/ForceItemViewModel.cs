using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    public class ForceItemViewModel
    {
        public int Id { get; set; } // El ID de la BBDD (ej. StoriesPush.Id)
        public string Text { get; set; } // El texto de la fuerza
        public string OriginStoryTitle { get; set; } // Contexto

        // Es crucial sobreescribir Equals y GetHashCode para que MudBlazor
        // pueda identificar y mover los objetos correctamente.
        public override bool Equals(object obj)
        {
            return obj is ForceItemViewModel model && Id == model.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
