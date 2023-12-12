using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PopastNaStajirovku2.Models;
using System.Data.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace PopastNaStajirovku2.Entyties
{
    public class Context : Microsoft.EntityFrameworkCore.DbContext
    {
        public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
            Database.OpenConnection();
        }
    }
}
