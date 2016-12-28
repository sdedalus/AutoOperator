using System;
using System.Linq;
using System.Linq.Expressions;

namespace AutoOperator
{
	public static class Operations
	{
		public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
		{
			// build parameter map (from parameters of second to parameters of first)
			var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

			// replace parameters in the second lambda expression with parameters from the first
			var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

			// apply composition of lambda expression bodies to parameters from the first expression
			return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
		}

		public static Expression<Func<T1, T2, bool>> Operation<T1, T2, TReturn>(this Expression<Func<T1, TReturn>> first, Expression<Func<T2, TReturn>> second, Func<Expression, Expression, Expression> merge)
		{
			var Parameters = first.Parameters.Concat(second.Parameters);
			return Expression.Lambda<Func<T1, T2, bool>>(merge(first.Body, second.Body), Parameters);
		}

		public static Expression<Func<T1, T2, bool>> And<T1, T2>(this Expression<Func<T1, T2, bool>> first, Expression<Func<T1, T2, bool>> second)
		{
			return first.Compose(second, Expression.And);
		}

		public static Expression<Func<T1, T2, bool>> AndAlso<T1, T2>(this Expression<Func<T1, T2, bool>> first, Expression<Func<T1, T2, bool>> second)
		{
			return first.Compose(second, Expression.AndAlso);
		}

		public static Expression<Func<T1, T2, bool>> Eq<T1, T2, TReturn>(this Expression<Func<T1, TReturn>> first, Expression<Func<T2, TReturn>> second)
		{
			return first.Operation(second, Expression.Equal);
		}

		public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
		{
			return first.Compose(second, Expression.And);
		}

		public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
		{
			return first.Compose(second, Expression.Or);
		}

		public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
		{
			return first.Compose(second, Expression.OrElse);
		}

		public static Expression<Func<T1, T2, bool>> OrElse<T1, T2>(this Expression<Func<T1, T2, bool>> first, Expression<Func<T1, T2, bool>> second)
		{
			return first.Compose(second, Expression.OrElse);
		}
	}
}