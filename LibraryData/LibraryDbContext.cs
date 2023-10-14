﻿using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }
    public DbSet<Book> Books { get; set; }
    public DbSet<BranchHours> BranchHours { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }
    public DbSet<CheckoutHistory> CheckoutHistories { get; set; }
    public DbSet<Hold> Holds { get; set; }
    public DbSet<LibraryBranch> LibraryBranches { get; set; }
    public DbSet<LibraryCard> LibraryCards { get; set; }
    public DbSet<LibraryAsset> LibraryAssets { get; set; }
    public DbSet<Patron> Patrons { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Video> Videos { get; set; }
}
