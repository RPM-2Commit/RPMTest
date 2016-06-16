using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RPMTest.Models
{
    public interface IRPMTestDB : IDisposable
    {
        IQueryable<T> Query<T>() where T : class;
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        void SaveChanges();
    }
    
    public class RPMTestDB : DbContext, IRPMTestDB
    {
        public RPMTestDB() : base("name=DefaultConnection")
        {

        }

        public DbSet<Thing> Thing { get; set; }


        IQueryable<T> IRPMTestDB.Query<T>()
        {
            return Set<T>();
        }

        void IRPMTestDB.Add<T>(T entity)
        {
            Set<T>().Add(entity);
        }

        void IRPMTestDB.Update<T>(T entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        void IRPMTestDB.Remove<T>(T entity)
        {
            Set<T>().Remove(entity);
        }

        void IRPMTestDB.SaveChanges()
        {
            SaveChanges();
        }
    }
}