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

		public void Add<T1, T2>(IOperatorExpression value)
		{
			dictionary.Add<T1, T2>(value);
		}

		public Expression<Func<T1, T2, bool>> GetEqualityExpression<T1, T2>()
		{
			IOperatorExpression config;
			if (dictionary.TryGetValue(Tuple.Create(typeof(T1), typeof(T2)), out config))
			{
				return equalityComposer.Build(((RelationalOperaton<T1, T2>)config).Parts, equalityConstructor);
			}

			return equalityConstructor.Build<T1, T2>();
		}

		//public Expression<Func<T1, T2, bool>> GetInequalityExpression<T1, T2>()
		//{
		//	IOperatorExpression config;
		//	if (dictionary.TryGetValue(Tuple.Create(typeof(T1), typeof(T2)), out config))
		//	{
		//		return equalityComposer.Build(((RelationalOperaton<T1, T2>)config).Parts, inequalityConstructor);
		//	}

		//	return inequalityConstructor.Build<T1, T2>();
		//}
	}
}