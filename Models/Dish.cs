using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD_DELI.Models
{
    public class Dish
    {
        [Key]

        public int DishId {get;set;}

        [Required(ErrorMessage ="Please enter a name.")]
        public string Name {get;set;}
        [Required(ErrorMessage ="Please enter a chef.")]
        public string Chef {get;set;}
        [Required]
        [Range(1,5, ErrorMessage ="Please pick a level")]
        public int Tastiness {get;set;}
        [Required]
        [Range(1,int.MaxValue, ErrorMessage ="Calories must be more than 0.")]
        public int Calories {get;set;}
        public string Description {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}