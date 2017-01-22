using System;
using System.Linq.Expressions;

namespace AutoOperator
{
	/// <summary>
	/// </summary>
	/// <typeparam name="T1">The type of the 1.</typeparam>
	/// <typeparam name="T2">The type of the 2.</typeparam>
	/// <seealso cref="AutoOperator.IOperatorExpression"/>
	public interface IRelationalOperaton<T1, T2> : IOperatorExpression
	{
		/// <summary>
		/// Operations the specified a.
		/// </summary>
		/// <typeparam name="TReturn1">The type of the return1.</typeparam>
		/// <typeparam name="TReturn2">The type of the return2.</typeparam>
		/// <param name="a">a.</param>
		/// <param name="b">The b.</param>
		/// <returns></returns>
		IRelationalOperaton<T1, T2> Operation<TReturn1, TReturn2>(Expression<Func<T1, TReturn1>> a, Expression<Func<T2, TReturn2>> b);
	}
}