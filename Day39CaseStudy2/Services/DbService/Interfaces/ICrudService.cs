namespace Day39CaseStudy.Services.DbService.Interfaces;

public interface ICrudService<T>
{
    Task AddAsync(T entity);
   Task <IEnumerable<T>> GetAllAsync();
    Task UpdateAsync(T entity);
   Task <T> GetByNameAsync(string entityName);
    Task DeleteAsync(int entityId);
}
