using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoLotDALModels.Models;
namespace AutoLotDAL.EF
{
    /*
     * Мощным средством инфраструктуры EF является возможность обеспечить соответствие базы данных и модели, а также 
     * инициализировать базу данных начальными данными. Это особенно удобно во время разработки и тестирования,
     * т.к. позволяет восстанавливать базу данных в известном состоянии перед каждым запуском кода. Для включения 
     * средства инициализации необходимо создать класс, производный от DrорСreateDatabaselfMode1Changes<TContext>
     * или DropCreateDatabaseAlways <TContext>. Начнем с создания в папке EF нового класса по имени MyData
     * Initializer, сделав его открытым и унаследованным от DropCreateDatabaseAlways<AutoLotEntities>.
     */
    public class MyDataInitializer:DropCreateDatabaseAlways<AutoLotEntities>
    {

        /*
         * Для заполнения базы данных применяется метод Seed (), доступный в обоих классах инициализации. 
         * За счет использования метода AddOrUpdate () можно обеспечить восстановление базы данных в то же 
         * самое состояние без дублирования данных. Ниже показан пример начального заполнения базы данных 
         * теми же записями, которые использовались в предыдущей главе
         */




        protected override void Seed(AutoLotEntities context)
        {
            var customers = new List<Customer>()
            {
                new Customer 
                {
                    FirstName = "Dave",
                    LastName = "Brenner"
                },
                new Customer
                {
                    FirstName = "Matt",
                    LastName = "Walton"
                },
                new Customer
                {
                    FirstName = "Steve",
                    LastName = "Hagen"
                },
                new Customer
                {
                    FirstName = "Pat", LastName = "Walton"
                },
                new Customer
                {
                    FirstName = "Bad",
                    LastName = "Customer"
                }
            };

            customers.ForEach(x => context.Customers.AddOrUpdate(c => new { c.FirstName,c.LastName },x));

            var cars = new List<Inventory> 
            {
                new Inventory {Make = "VW", Color = "'Black", PetName = "Zippy"},
                new Inventory {Make="Ford", Color = "Rust", PetName = "Rusty"},
                new Inventory {Make = "Saab", Color = "Black" , PetName = "Mel"},
                new Inventory {Make = "Yugo", Color = "Yellow ", PetName = "Clunker"},
                new Inventory {Make ="BMW", Color = "Black", PetName = "Bimmer"},
                new Inventory {Make = "BMW", Color = "Green", PetName = "Hank"},
                new Inventory {Make = "BMW", Color = "Pink", PetName = "Pinky"},
                new Inventory {Make = "Pinto", Color = "Black", PetName = "Pete"},
                new Inventory {Make = "Yugo", Color = "Brown" , PetName = "Brownie" }
            };
            context.Inventory.AddOrUpdate(x => new {x.Make,x.Color,x.PetName },cars.ToArray());

            var orders = new List<Order>
            {
                new Order{Car = cars[0],Customer = customers[0]},
                new Order{Car = cars[1],Customer = customers[1]},
                new Order{Car = cars[2],Customer = customers[2]},
                new Order{Car = cars[3],Customer = customers[3]}


            };
            orders.ForEach(x => context.Orders.AddOrUpdate(c => new { c.CarId, c.Id }, x));

            context.CreditRisks.AddOrUpdate(x => new { x.FirstName, x.LastName },
                new CreditRisk
                {
                    Id = customers[4].Id,
                    FirstName = customers[4].FirstName,
                    LastName = customers[4].LastName,
                }
                );




        }
        /*
         * Класс DropCreateDatabaseAlways является обобщенным классом, который типизирован для класса, производного 
         * от DbContext, в данном случае AutoLotEntities. Он будет удалять и воссоздавать базу данных каждый раз,
         * когда выполняется инициализатор. Есть также класс DropCreateDatabaseIfModelChanges<TContext>, который
         * удаляет и воссоздает базу данных, когда в модели появляются изменения.
         */



        /*Последний шаг заключается в установке инициализатора с помощью такого кода (который будет добавлен в следующем разделе): 
         * 
         * Database.Setlnitializer(new MyDatalnitializer ());*/
    }
}
