using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace AutoOperator
{
	/// <summary>
	///// Provides a starting point for operator configuration.
	/// </summary>
	public class OperatorConfiguration
	{
		private ConcurrentDictionary<Tuple<Type, Type, Operator>, Expression> dictionary = new ConcurrentDictionary<Tuple<Type, Type, Operator>, Expression>();
		private RelationalExpressionBuilder relationalOperatorExpressionBuilder = new RelationalExpressionBuilder();

		/// <summary>
		/// Configures the equality operator.
		/// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
		/// <typeparam name="T2">The type of the 2.</typeparam>
		/// <param name="configure">The configure.</param>
		/// <returns></returns>
		public OperatorConfiguration Relation<T1, T2>(Action<RelationalOperaton<T1, T2>> configure)
		{
			var tmp = new RelationalOperaton<T1, T2>();
			configure(tmp);

			relationalOperatorExpressionBuilder.Add<T1, T2>((IOperatorExpression)tmp);

			return this;
		}

		/// <summary>
		/// Gets the operator expression.
		/// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
		/// <typeparam name="T2">The type of the 2.</typeparam>
		/// <param name="op">The op.</param>
		/// <returns></returns>
		public Expression<Func<T1, T2, bool>> GetOperatorExpression<T1, T2>(Operator op)
		{
			return relationalOperatorExpressionBuilder.GetEqualityExpression<T1, T2>();
		}

		/// <summary>
		/// Gets the operator expression.
		/// </summary>
		/// <param name="a">a.</param>
		/// <param name="b">The b.</param>
		/// <param name="op">The op.</param>
		/// <returns></returns>
		public Expression GetOperatorExpression(Type a, Type b, Operator op)
		{
			return dictionary[Tuple.Create(a, b, op)];
		}
	}
}