using System;

namespace AutoOperator
{
	/// <summary>
	///// Provides a starting point for operator configuration.
	/// </summary>
	public class OperatorConfiguration
	{
		private RelationalExpressionBuilder relationalOperatorExpressionBuilder;

		public OperatorConfiguration(RelationalExpressionBuilder relationalOperatorExpressionBuilder)
		{
			this.relationalOperatorExpressionBuilder = relationalOperatorExpressionBuilder;
		}

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

			relationalOperatorExpressionBuilder.Add<T1, T2>(tmp.Parts);
			relationalOperatorExpressionBuilder.Add<T2, T1>(tmp.PartsReversed);

			return this;
		}
	}
}