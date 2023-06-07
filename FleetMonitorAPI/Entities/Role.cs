using System.ComponentModel.DataAnnotations.Schema;

namespace FleetMonitorAPI.Entities
{
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
