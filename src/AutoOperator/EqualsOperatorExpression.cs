using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AutoOperator
{
	public class EqualsOperatorExpression<T1, T2> : IBooleanOperatorExpression<T1, T2>
	{
		private List<Expression<Func<T1, T2, bool>>> parts = new List<Expression<Func<T1, T2, bool>>>();

		public IBooleanOperatorExpression<T1, T2> Operation<TReturn>(Expression<Func<T1, TReturn>> a, Expression<Func<T2, TReturn>> b)
		{
			parts.Add(BuildExpression(a, b));
			return this;
		}

		private static Expression<Func<T1, T2, bool>> BuildExpression<T1, T2, TReturn>(Expression<Func<T1, TReturn>> a, Expression<Func<T2, TReturn>> b)
		{
			return a.Eq(b);
		}

		public Expression<Func<T1, T2, bool>> Build()
		{
			return parts.Aggregate((a, b) => a.AndAlso(b));
		}
	}
}