﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeltaMovies.DeltaAPI.DbEntities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DeltaMoviesEntities : DbContext
    {
        public DeltaMoviesEntities()
            : base("name=DeltaMoviesEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ActorInfo> ActorInfoes { get; set; }
        public virtual DbSet<ProducerInfo> ProducerInfoes { get; set; }
        public virtual DbSet<MovieInfo> MovieInfoes { get; set; }
        public virtual DbSet<ActorInMovie> ActorInMovies { get; set; }
    }
}
