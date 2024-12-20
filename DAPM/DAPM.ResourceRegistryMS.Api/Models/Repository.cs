﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
 * All new changes are made by:
 * @Author: s204197
 */
 
namespace DAPM.ResourceRegistryMS.Api.Models
{
    public class Repository
    {
        // Attributes (Columns)
        
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name {  get; set; } = string.Empty;
        public Guid PeerId { get; set; }

        // Navigation Attributes (Foreign Keys)
        [ForeignKey("PeerId")]
        public virtual Peer Peer { get; set; }
    }
}
