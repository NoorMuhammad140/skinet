using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Entities;
using core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity :BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
         ISpecification<TEntity> Spec)
         {
   
             var query = inputQuery;

            if (Spec.Criteria != null)
            {
                query = query.Where(Spec.Criteria);
            }

            query = Spec.Include.Aggregate(query,(current, include) => current.Include(include));
           

           return query;
         }
    }
}