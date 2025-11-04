namespace Jtbd.Domain.Entities
{
    public class ForceGroupViewModel
    {
        public int Id { get; set; } // El ID del grupo de la BBDD
        public string Name { get; set; } // El nombre del grupo

        // La lista de fuerzas que actualmente pertenecen a este grupo
        public List<ForceItemViewModel> ContainedForces { get; set; } = new();
    }

}
