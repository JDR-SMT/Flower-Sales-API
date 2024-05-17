using System.Linq.Expressions;

namespace FlowerSales.API.Models
{
	public static class IQuerableExtension
	{
		public static IQueryable<TEntity> OrderByCustom<TEntity>(this IQueryable<TEntity> items, string sortBy, string sortOrder)
		{
			var type = typeof(TEntity);
			var property = type.GetProperty(sortBy);
			var expressionB = Expression.Parameter(type, "t");
			var expressionA = Expression.MakeMemberAccess(expressionB, property);
			var lambda = Expression.Lambda(expressionA, expressionB);

			var result = Expression.Call(
				typeof(Queryable),
				sortOrder == "desc" ? "OrderByDescending" : "OrderBy",
				[type, property.PropertyType],
				items.Expression,
				Expression.Quote(lambda));

			return items.Provider.CreateQuery<TEntity>(result);
		}
	}
}
