using LojinhaDaPaulinhaAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LojinhaDaPaulinhaAPI.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Product : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public double Price { get; set; }

        public bool IsAvaliable { get; set; }

    }
}
