using System;

namespace AutoOperator
{
	/// <summary>
	/// Primary Entry point for interacting with AutoOperator
	/// </summary>
	public static class Operators
	{
		private static OperatorConfiguration operatorConfiguration;
		private static RelationalExpressionBuilder relationalExpressionBuilder = new RelationalExpressionBuilder();

		/// <summary>
		/// Initializes the operator configuration.
		/// </summary>
		/// <param name="config">The configuration.</param>
		public static void Initialize(Action<OperatorConfiguration> config)
		{
			operatorConfiguration = new OperatorConfiguration(relationalExpressionBuilder);
			config(operatorConfiguration);
		}

		/// <summary>
		/// performs a equality check for two types. this is just a quick implementation for testing
		/// the final version will need to be memoized.
		/// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
		/// <typeparam name="T2">The type of the 2.</typeparam>
		/// <param name="a">a.</param>
		/// <param name="b">The b.</param>
		/// <returns></returns>
		public static bool Equals<T1, T2>(T1 a, T2 b)
		{
			var exp = relationalExpressionBuilder.GetEqualityExpression<T1, T2>();
			return exp.Compile()(a, b);
		}

		public static bool NotEquals<T1, T2>(T1 a, T2 b)
		{
			var exp = relationalExpressionBuilder.GetInequalityExpression<T1, T2>();
			return exp.Compile()(a, b);
		}
	}
}