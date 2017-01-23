using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AutoOperator
{
	/// <summary>
	/// Used to construct a Equality Expression
	/// </summary>
	/// <typeparam name="T1">The type of the 1.</typeparam>
	/// <typeparam name="T2">The type of the 2.</typeparam>
	/// <seealso cref="AutoOperator.IRelationalOperaton{T1, T2}"/>
	public class RelationalOperaton<T1, T2> : IRelationalOperaton<T1, T2>
	{
		private ExpresionList<T1, T2> parts = new ExpresionList<T1, T2>();

		internal ExpresionList<T1, T2> Parts
		{
			get
			{
				return parts;
			}
		}

		/// <summary>
		/// Adds another block to the boolean operation.
		/// </summary>
		/// <typeparam name="TReturn1">The type of the return1.</typeparam>
		/// <typeparam name="TReturn2">The type of the return2.</typeparam>
		/// <param name="a">a.</param>
		/// <param name="b">The b.</param>
		/// <returns></returns>
		public IRelationalOperaton<T1, T2> Operation<TReturn1, TReturn2>(Expression<Func<T1, TReturn1>> a, Expression<Func<T2, TReturn2>> b)
		{
			parts.Add((IRelationalOperatorExpressionBuilder builder) => builder.BuildExpression<T1, T2, TReturn1, TReturn2>(a, b));

			return this;
		}
	}
}