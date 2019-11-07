using ExamProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Data
{
    public class AppRepository : IAppRepository
    {
        private examDBContext _context;
        public AppRepository(examDBContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {

            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
   

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

      
    }
}
