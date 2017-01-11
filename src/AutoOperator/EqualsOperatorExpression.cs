using AutoOperator;
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
	/// <seealso cref="AutoOperator.IBooleanOperatorExpression{T1, T2}"/>
	public class EqualsOperatorExpression<T1, T2> : IBooleanOperatorExpression<T1, T2>
	{
		private List<Func<Expression<Func<T1, T2, bool>>>> parts = new List<Func<Expression<Func<T1, T2, bool>>>>();
		private readonly EqualsOperatorExpressionBuilder builder;

		/// <summary>
		/// Initializes a new instance of the <see cref="EqualsOperatorExpression{T1, T2}"/> class.
		/// </summary>
		/// <param name="builder">The builder.</param>
		internal EqualsOperatorExpression(EqualsOperatorExpressionBuilder builder)
		{
			this.builder = builder;
		}

		/// <summary>
		/// Adds another block to the boolean operation.
		/// </summary>
		/// <typeparam name="TReturn1">The type of the return1.</typeparam>
		/// <typeparam name="TReturn2">The type of the return2.</typeparam>
		/// <param name="a">a.</param>
		/// <param name="b">The b.</param>
		/// <returns></returns>
		public IBooleanOperatorExpression<T1, T2> Operation<TReturn1, TReturn2>(Expression<Func<T1, TReturn1>> a, Expression<Func<T2, TReturn2>> b)
		{
			parts.Add(() => this.builder.BuildExpression<T1, T2, TReturn1, TReturn2>(a, b));
			return this;
		}

		/// <summary>
		/// Builds this instance.
		/// </summary>
		/// <returns></returns>
		public Expression<Func<T1, T2, bool>> Build()
		{
			return parts.Select(v => v()).Aggregate((a, b) => a.AndAlso(b));
		}
	}
}