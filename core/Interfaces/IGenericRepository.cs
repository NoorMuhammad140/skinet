using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Entities;
using core.Specifications;

namespace core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
       Task <T> GetByIdAsync(int id);   
       Task <IReadOnlyList<T>> ListAllAsync(); 

       Task <T> GetEntityWithSpec (ISpecification<T> Spec );
    
       Task <IReadOnlyList<T>> listAsync (ISpecification<T> Spec);

       Task<int> CountAsync(ISpecification<T> spec);
    }
}