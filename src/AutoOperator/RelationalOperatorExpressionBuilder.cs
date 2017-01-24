using System;
using System.Linq.Expressions;

namespace AutoOperator
{
	public class RelationalExpressionBuilder
	{
		private OperatorExpressionDictionary dictionary;

		private EqualityComposer equalityComposer;
		private InequalityComposer inequalityComposer;
		private EqualityConstructor equalityConstructor;
		private InequalityConstructor inequalityConstructor;

		public RelationalExpressionBuilder()
		{
			this.dictionary = new OperatorExpressionDictionary();
			this.equalityComposer = new EqualityComposer();
			this.inequalityComposer = new InequalityComposer();
			this.equalityConstructor = new EqualityConstructor(dictionary, equalityComposer);
			this.inequalityConstructor = new InequalityConstructor(dictionary, inequalityComposer);
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

		public Expression<Func<T1, T2, bool>> GetInequalityExpression<T1, T2>()
		{
			IExpresionList<T1, T2> config;
			if (dictionary.TryGetValue<T1, T2>(out config))
			{
				return inequalityComposer.Build(config, inequalityConstructor);
			}

			return inequalityConstructor.Build<T1, T2>();
		}
	}
}