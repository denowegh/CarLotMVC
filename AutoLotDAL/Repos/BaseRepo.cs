using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoLotDAL.EF;
using AutoLotDALModels.Models;

namespace AutoLotDAL.Repos
{
    public class BaseRepo<T> : IRepo<T>, IDisposable where T : EntityBase, new()
    {
        private readonly DbSet<T> _table;
        private readonly AutoLotEntities _db;
        public BaseRepo()
        {
            
            _db = new AutoLotEntities();
            
            _table = _db.Set<T>();
        }
        protected AutoLotEntities Context => _db;
        public int Add(T entity)
        {
            _table.Add(entity);
            return SeveChenges();
        }

        public int AddRange(IList<T> entities)
        {
            _table.AddRange(entities);
            return SeveChenges();
        }

        public int Delete(int id, byte[] timestamp)
        {
            _db.Entry(new T { Id = id, Timestamp = timestamp }).State = EntityState.Deleted;
            return SeveChenges();
        }

        public int Delete(T entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
            return SeveChenges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public List<T> ExecuteQuery(string sql) =>_table.SqlQuery(sql).ToList();

        public List<T> ExecuteQuery(string sql, object[] sqlParametersObjects)
        => _table.SqlQuery(sql,sqlParametersObjects).ToList();

        public virtual List<T> GetAll() =>_table.ToList();

        public T GetOne(int? id)=>_table.Find(id);
        

        public int Save(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            return SeveChenges();
        }
        internal int SeveChenges()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Генерируется, когда возникает ошибка, связанная с параллелизмом.
                // Пока что просто повторно сгенерировать исключение,
                throw;
            }
            catch (DbUpdateException ex)
            {
                // Генерируется, когда обновление базы данных терпит неудачу.
                // Проверить внутреннее исключение (исключения), чтобы получить
                // дополнительные сведения и выяснить, на какие объекты это повлияло.
                // Пока что просто повторно сгенерировать исключение.
                throw;
            }
            catch (CommitFailedException ex)
            {
                // Обработать здесь отказы транзакции.
                // Пока что просто повторно сгенерировать исключение,
                throw;
            }
            catch (Exception ex)
            {
                // Произошло какое-то другое исключение, которое должно быть обработано,
                throw;
            }
            /*
             * На заметку! Создание нового экземпляра DbContext может оказаться затратным процессом с точки зрения 
             * производительности. В коде примера новый экземпляр AutoLotEntities создается с каждым экземпляром 
             * хранилища. Если такое действие не выполняется настолько хорошо, как хотелось бы (или необходимо), 
             * тогда следует обдумать возможность использования только одного контекстного класса и его
             * разделение между хранилищами. Не существует единственного правильного способа кодирования 
             * этого, потому что каждая ситуация отличается и должна быть подстроена под конкретное приложение.
             */
        }
    }
    
}
