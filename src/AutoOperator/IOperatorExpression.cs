using System;
using System.Linq.Expressions;

namespace AutoOperator
{
	public interface IBooleanOperatorExpression<T1, T2>
	{
		IBooleanOperatorExpression<T1, T2> Operation<TReturn>(Expression<Func<T1, TReturn>> a, Expression<Func<T2, TReturn>> b);

		Expression<Func<T1, T2, bool>> Build();
	}
}