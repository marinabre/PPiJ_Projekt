﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GuessWhere.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Guess_WhereEntities1 : DbContext
    {
        public Guess_WhereEntities1()
            : base("name=Guess_WhereEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<LeaderBoard> LeaderBoard { get; set; }
        public virtual DbSet<RegisteredUser> RegisteredUser { get; set; }
        public virtual DbSet<SavedGames> SavedGames { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}