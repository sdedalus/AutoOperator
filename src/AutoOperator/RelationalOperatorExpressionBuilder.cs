using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace AutoOperator
{
	public class RelationalExpressionBuilder
	{
		private OperatorExpressionDictionary dictionary;

		private EqualityComposer equalityComposer;
		private EqualityConstructor equalityConstructor;
		private InequalityConstructor inequalityConstructor;

		public RelationalExpressionBuilder()
		{
			dictionary = new OperatorExpressionDictionary();
			this.equalityComposer = new EqualityComposer();
			equalityConstructor = new EqualityConstructor(dictionary, equalityComposer);
			inequalityConstructor = new InequalityConstructor(dictionary, equalityComposer);
		}

		public void Add<T1, T2>(IExpresionList value)
		{
			dictionary.Add<T1, T2>(value);
		}

		public Expression<Func<T1, T2, bool>> GetEqualityExpression<T1, T2>()
		{
			IExpresionList<T1, T2> config;
			if (dictionary.TryGetValue<T1, T2>(out config))
			{
				return equalityComposer.Build(config, equalityConstructor);
			}

			return equalityConstructor.Build<T1, T2>();
		}
	}
}