﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OneListApplication
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class OneListEntitiesCore : DbContext
    {
        public OneListEntitiesCore()
            : base("name=OneListEntitiesCore")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemCategory> ItemCategories { get; set; }
        public virtual DbSet<List> Lists { get; set; }
        public virtual DbSet<ListItem> ListItems { get; set; }
        public virtual DbSet<ListStatu> ListStatus { get; set; }
        public virtual DbSet<ListType> ListTypes { get; set; }
        public virtual DbSet<ListUser> ListUsers { get; set; }
        public virtual DbSet<Retail> Retails { get; set; }
        public virtual DbSet<SuscriberGroup> SuscriberGroups { get; set; }
        public virtual DbSet<SuscriberGroupUser> SuscriberGroupUsers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
    
        public virtual int UpdateInfousers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateInfousers");
        }
    }
}
