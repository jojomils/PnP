using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace PnP.Models
{
  public static class SeedData
  {
    public static void EnsurePopulated(IApplicationBuilder app)
    {
      ApplicationDbContext context = app.ApplicationServices
        .GetRequiredService<ApplicationDbContext>();
      //Following code is to Migrate the data into the database
      context.Database.Migrate();

      if (!context.Products.Any())
      {
        context.Products.AddRange(

          new Product
          {
            Name = "Iphone Xs Max",
            Description = "Latest Gen With A12 chip",
            Category = "Phone",
            Price = 22500
          },
          new Product
          {
            Name = "Samsung S10",
            Description = "With Infinity-o display",
            Category = "Phone",
            Price = 19900
          },
          new Product
          {
            Name = "Samsung Note 9",
            Description = "Fastest of 2018",
            Category = "Phone",
            Price = 12000
          },
          new Product
          {
            Name = "Samsung S10 Plus",
            Description = "Up to 1Tb of Storage",
            Category = "Phone",
            Price = 23500
          },
          new Product
          {
            Name = "Huawei P30",
            Description = "wITH 5x Optical Zoom",
            Category = "Phone",
            Price = 16000
          },
          new Product
          {
            Name = "MacBook Pro",
            Description = "Up To 4Tb of SSD",
            Category = "Laptop",
            Price = 43900
          },
          new Product
          {
            Name = "HP Elite",
            Description = "For work",
            Category = "Laptop",
            Price = 22000
          },
          new Product
          {
            Name = "HP Probook 430 G2",
            Description = "Windows pro installed",
            Category = "Laptop",
            Price = 8900
          },
          new Product
          {
            Name = "HP Probook 450 G3",
            Description = "For business",
            Category = "Laptop",
            Price = 11900
          },
          new Product
          {
            Name = "Garden Chair",
            Description = "For outdoor",
            Category = "Lifestyle",
            Price = 399
          },
          new Product
          {
            Name = "Flower",
            Description = "Decoration",
            Category = "Lifestyle",
            Price = 299
          },
          new Product
          {
            Name = "Entrance Carpet",
            Description = "A welcome carpet",
            Category = "Lifestyle",
            Price = 149
          },
          new Product
          {
            Name = "Vase",
            Description = "Use for decoration",
            Category = "Lifestyle",
            Price = 99
          },
          new Product
          {
            Name = "Braai Stand",
            Description = "Used for outdoor Braai",
            Category = "Lifestyle",
            Price = 400
          },
          new Product
          {
            Name = "Nike Predator",
            Description = "Running Shoes",
            Category = "Sports",
            Price = 1799
          },
          new Product
          {
            Name = "Europa League Football",
            Description = "Bali Final Football",
            Category = "Sports",
            Price = 299.9m
          },
          new Product
          {
            Name = "Boxing Gloves",
            Description = "Used to protect the hand",
            Category = "Sports",
            Price = 499
          },
          new Product
          {
            Name = "Barcelona Jersey",
            Description = "T-shirt For football",
            Category = "Sports",
            Price = 579
          }
          );
        context.SaveChanges();
      }
    }
  }
}
