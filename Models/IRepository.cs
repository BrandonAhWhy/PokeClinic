using System.Collections.Generic;

public interface IRepository<T>
{
     void Add(T entity);
     void Delete(T entity);
     T Get(int id);
     List<T> GetAll();
     
}