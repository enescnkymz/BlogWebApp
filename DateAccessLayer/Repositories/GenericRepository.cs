using DateAccessLayer.Abstract;
using DateAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DateAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        public void Delete(T t)
        {
            using var c = new Context();
            c.Remove(t);
            c.SaveChanges();
        }

        public List<T> GetListAll()
        {
            using var c = new Context();
            return c.Set<T>().ToList();

        }

        public T GetById(int id)
        {
            using var c = new Context();
            return c.Set<T>().Find(id);

        }

		public List<T> GetListAll(Expression<Func<T, bool>> Filtre)
		{
			using var c = new Context();
            return c.Set<T>().Where(Filtre).ToList();
		}

		public void Update(T t)
        {
            using var c = new Context();
            c.Update(t);
            c.SaveChanges();

        }

        public void İnsert(T t)
        {
            using var c = new Context();
            c.Add(t);
            c.SaveChanges();

        }

       
   
    
    }
   
}
