using System;
using System.Linq.Expressions;

namespace AutoOperator
{
	/// <summary>
	/// Primary Entry point for interacting with AutoOperator
	/// </summary>
	public class Operators
	{
		private static OperatorConfiguration conf;

		public static void Initialize(Action<OperatorConfiguration> config)
		{
			conf = new OperatorConfiguration();
			config(conf);
		}

		// this is just a quick implementation for testing
		public static bool Equals<T1, T2>(T1 a, T2 b) => ((Expression<Func<T1, T2, bool>>)conf.GetOperatorExpression<T1, T2>(Operator.Equals)).Compile()(a, b);
	}
}