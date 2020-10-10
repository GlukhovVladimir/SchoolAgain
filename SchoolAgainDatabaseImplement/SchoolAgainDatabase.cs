using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SchoolAgainDatabaseImplement.Models;

namespace SchoolAgainDatabaseImplement
{
    public class SchoolAgainDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=RIPZ-PC\SQLEXPRESS;
                                            Initial Catalog=SchoolAgainDatabase;
                                            Integrated Security=True;
                                            MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Service> Services { set; get; }
        public virtual DbSet<Reception> Receptions { set; get; }
        public virtual DbSet<ReceptionService> ReceptionServices { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<Kid> Kids { set; get; }
        public virtual DbSet<ClientKid> ClientKids { set; get; }
        public virtual DbSet<Payment> Payments { set; get; }
    }
}
