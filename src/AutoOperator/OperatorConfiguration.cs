using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace AutoOperator
{
	public class OperatorConfiguration
	{
		private ConcurrentDictionary<Tuple<Type, Type, Operator>, Expression> dictionary = new ConcurrentDictionary<Tuple<Type, Type, Operator>, Expression>();

		public void ConfigureEquality<T1, T2>(Action<EqualsOperatorExpression<T1, T2>> configure)
		{
			var tmp = new EqualsOperatorExpression<T1, T2>();
			configure(tmp);

			Expression<Func<T1, T2, bool>> exp = tmp.Build();
			dictionary.TryAdd(Tuple.Create(typeof(T1), typeof(T2), Operator.Equals), exp);
		}

		public Expression GetOperatorExpression<T1, T2>(Operator op)
		{
			return dictionary[Tuple.Create(typeof(T1), typeof(T2), op)];
		}

		public Expression GetOperatorExpression(Type a, Type b, Operator op)
		{
			return dictionary[Tuple.Create(a, b, op)];
		}
	}
}