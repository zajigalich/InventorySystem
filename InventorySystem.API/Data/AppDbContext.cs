﻿using InventorySystem.API.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.API.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		public DbSet<Product> Products { get; set; }
	}
}
