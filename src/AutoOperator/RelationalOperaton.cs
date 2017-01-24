using System;
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
		private ExpresionList<T2, T1> partsReversed = new ExpresionList<T2, T1>();

		internal ExpresionList<T1, T2> Parts
		{
			get
			{
				return parts;
			}
		}

		internal ExpresionList<T2, T1> PartsReversed
		{
			get
			{
				return partsReversed;
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
			parts.Add((IRelationalExpressionBuilder builder) => builder.BuildExpression<T1, T2, TReturn1, TReturn2>(a, b));
			partsReversed.Add((IRelationalExpressionBuilder builder) => builder.BuildExpression<T2, T1, TReturn2, TReturn1>(b, a));
			return this;
		}
	}
}