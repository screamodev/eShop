public interface IRepository<TEntity, TCreateDto, TUpdateDto>
    where TEntity : class
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity?> AddAsync(TCreateDto dto);
    Task<TEntity?> UpdateAsync(int id, TUpdateDto dto);
    Task<bool?> DeleteAsync(int id);
}