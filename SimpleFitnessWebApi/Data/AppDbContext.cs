using Microsoft.EntityFrameworkCore;
using SimpleFitnessWebApi.Models;

namespace SimpleFitnessWebApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Weight> Weights { get; set; }
}