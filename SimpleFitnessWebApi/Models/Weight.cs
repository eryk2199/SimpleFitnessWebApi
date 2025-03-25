using System.ComponentModel.DataAnnotations;

namespace SimpleFitnessWebApi.Models;

public class Weight
{
    public int Id { get; set; }
    
    [Required]
    public DateOnly Date { get; set; }
    
    [Required]
    [Range(0, 1000.00)]
    public float Value { get; set; }  //weight in kg
}