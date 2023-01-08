using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using AutoLotDALModels.Models;
using System.Data.Entity.Infrastructure.Interception;
using AutoLotDAL.Interception;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;



namespace AutoLotDAL.EF
{
    public partial class AutoLotEntities : DbContext
    {
        public AutoLotEntities()
            : base("name=AutoLotConnection")
        {
            //DbInterception.Add(new ConsoleWriterInterceptor()) ;
            //databaseLogger.StartLogging();
            //DbInterception.Add(databaseLogger);

            var context = (this as IObjectContextAdapter).ObjectContext;

            context.ObjectMaterialized += OnObjectMaterialized;
            context.SavingChanges += OnSavingChanges;



        }

        private void OnObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            
        }

        private void OnSavingChanges(object sender, EventArgs e)
        {
            // �������� sender ����� ��� ObjectContext.
            // ����� �������� ������� � �������� ��������,
            //� ����� ��������/�������������� ��������
            // ���������� ����� �������� �������,
            var context = sender as ObjectContext;
            if (context == null) return;
            foreach(ObjectStateEntry item in context.ObjectStateManager.GetObjectStateEntries(EntityState.Modified|EntityState.Added))
            {
                // ������ ����� ���-�� ������.
                if( (item.Entity as Inventory)!= null)
                {

                    var entity = (Inventory)item.Entity;
                    if (entity.Color == "Red") 
                    {
                        item.RejectPropertyChanges(nameof(entity.Color));
                    }

                }

            }




        }



        static readonly DatabaseLogger databaseLogger = new DatabaseLogger("D:\\The study C#\\SQLLog\\sqllog.txt", true);


        public virtual DbSet<CreditRisk> CreditRisks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }

        /*
         * ���������� ��� ������� �������������� � EF ������� AddOrUpdate() ���� DbSet<T>. ����� ��������� ������-���������
         * � ������������ ������������ ������ ������ � ������ ��������, ��������������� ��� ���������� ��� �������.
         * ���� ������ ���������� (����������� �� ����� ������������) � ���� ������, ����� ��� ����� ���������.
         * ���� ������ �� ����������, �� ��� ����� ���������. ���� �������� ������ ���������� ��� ������� ��� 
         * ������ Inventory, � ������� ����� �������� ������� ��������������� �������� ������� ���� � Color �������
         * ������� ����������
         * 
         * context.Cars.AddOrUpdate(x=>new {x.Make,x.Color},car);
         * 
         */

        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.CustomerId);
            
            modelBuilder.Entity<Inventory>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Car)
                .HasForeignKey(e => e.CarId);
        }
        protected override void Dispose(bool disposing)
        {
            DbInterception.Remove(databaseLogger);
            databaseLogger.StopLogging();


            base.Dispose(disposing);
        }
    }
}
