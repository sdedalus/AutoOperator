using System;
using System.Linq.Expressions;

namespace AutoOperator
{
	public interface IRelationalOperatorExpressionBuilder
	{
		Expression<Func<T1, T2, bool>> BuildExpression<T1, T2, TReturn1, TReturn2>(Expression<Func<T1, TReturn1>> a, Expression<Func<T2, TReturn2>> b);
	}
}