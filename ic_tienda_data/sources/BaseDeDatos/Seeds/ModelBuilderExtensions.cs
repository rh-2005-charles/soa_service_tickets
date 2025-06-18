using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ic_tienda_data.sources.BaseDeDatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ic_tienda_data.sources.BaseDeDatos.Seeds
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Semilla para la tabla Company
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    FirstName = "Pedrito",
                    LastName = "Pica Piedra",
                    Email = "pedro@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Phone = "123456789",
                }
            );
        }


    }
}