using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleModel.SpDbContext
{
    public partial class SpContext : DbContext
    {
        public SpContext()
        {
        }
        public static SpContext Create(string connid)
        {
            if (!string.IsNullOrEmpty(connid))
            {
                //  var connStr = ConnectionStrings[connid];
                var optionsBuilder = new DbContextOptionsBuilder<SpContext>();
                optionsBuilder.UseMySQL(connid);
                return new SpContext(optionsBuilder.Options);
            }
            else
            {
                throw new ArgumentNullException("ConnectionId");
            }
        }
        public SpContext(DbContextOptions<SpContext> options)
            : base(options)
        {
        }
        public virtual DbSet<ExecutreStoreProcedureResult> ExecutreStoreProcedureResult { get; set; }
        public virtual DbSet<ExecutreStoreProcedureResultWithSID> ExecutreStoreProcedureResultWithSID { get; set; }
        public virtual DbSet<ExecuteStoreProcedureResultWithId> ExecuteStoreProcedureResultWithId { get; set; }
        public virtual DbSet<ExecutreStoreProcedureResultList> ExecutreStoreProcedureResultList { get; set; }
        public virtual DbSet<ExecutreStoreProcedureResultWithEntitySID> ExecutreStoreProcedureResultWithEntitySID { get; set; }


        public virtual DbSet<ExecutreStoreProcedureResultNew> ExecutreStoreProcedureResultNews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL();
            }
        }
        private static DbContextOptions GetOptions(string connectionString)
        {
            return MySQLDbContextOptionsExtensions.UseMySQL(new DbContextOptionsBuilder(), connectionString).Options;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region SP           
            modelBuilder.Entity<ExecutreStoreProcedureResult>().HasNoKey().Property(e => e.ErrorMessage).HasConversion<System.String>();

            modelBuilder.Entity<ExecutreStoreProcedureResultWithSID>().HasNoKey().Property(e => e.ErrorMessage).HasConversion<System.String>();
            modelBuilder.Entity<ExecutreStoreProcedureResultWithSID>().HasNoKey().Property(e => e.SID).HasConversion<System.String>();
            modelBuilder.Entity<ExecuteStoreProcedureResultWithId>().HasNoKey().Property(e => e.ErrorMessage).HasConversion<string>();
            modelBuilder.Entity<ExecuteStoreProcedureResultWithId>().HasNoKey().Property(e => e.Id).HasConversion<int>();
            modelBuilder.Entity<ExecutreStoreProcedureResultList>().HasNoKey().Property(e => e.Result).HasConversion<System.String>();
            modelBuilder.Entity<ExecutreStoreProcedureResultWithEntitySID>().HasNoKey().Property(e => e.ErrorMessage).HasConversion<System.String>();

            modelBuilder.Entity<ExecutreStoreProcedureResultNew>(entity => {
                entity.HasNoKey();
                entity.Property(t => t.RESULT);
                entity.Property(t => t.PageNumber);
                entity.Property(t => t.PageSize);
                entity.Property(t => t.TotalCount);
                entity.Property(t => t.TotalPages);
            });

            

            #endregion

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
