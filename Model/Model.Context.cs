﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ahorcadoDBEntities : DbContext
    {
        public ahorcadoDBEntities()
            : base("name=ahorcadoDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<Matches> Matches { get; set; }
        public virtual DbSet<MatchResults> MatchResults { get; set; }
        public virtual DbSet<MatchScores> MatchScores { get; set; }
        public virtual DbSet<MatchStatuses> MatchStatuses { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<Words> Words { get; set; }
    }
}
