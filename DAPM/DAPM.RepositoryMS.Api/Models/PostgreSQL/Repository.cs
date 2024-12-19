using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
 * All new changes are made by:
 * @Author: s204197
 */
 
namespace DAPM.RepositoryMS.Api.Models.PostgreSQL
{
    public class Repository
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
