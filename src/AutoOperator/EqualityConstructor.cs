using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace AutoOperator
{
	public class EqualityConstructor : IRelationalOperatorExpressionBuilder
	{
		private OperatorExpressionDictionary dictionary;
		private EqualityComposer equalityComposer;

		public EqualityConstructor(OperatorExpressionDictionary dictionary, EqualityComposer equalityComposer)
		{
			this.dictionary = dictionary;
			this.equalityComposer = equalityComposer;
		}

		public Expression<Func<T1, T2, bool>> BuildExpression<T1, T2, TReturn1, TReturn2>(Expression<Func<T1, TReturn1>> a, Expression<Func<T2, TReturn2>> b)
		{
			IOperatorExpression config;
			if (dictionary.TryGetValue<TReturn1, TReturn2>(out config))
			{
				var expCfg = (RelationalOperaton<TReturn1, TReturn2>)config;

				var expr = equalityComposer.Build(expCfg.Parts, this);
				return a.NestExpression(b, expr);
			}

			return this.Build<T1, T2, TReturn1, TReturn2>(a, b);
		}

		public Expression<Func<T1, T2, bool>> Build<T1, T2>()
		{
			return (T1 a, T2 b) => a.Equals(b);
		}

		public Expression<Func<T1, T2, bool>> Build<T1, T2, TReturn1, TReturn2>(Expression<Func<T1, TReturn1>> a, Expression<Func<T2, TReturn2>> b)
		{
			return a.Eq(b);
		}
	}

	public class InequalityConstructor : IRelationalOperatorExpressionBuilder
	{
		private OperatorExpressionDictionary dictionary;
		private EqualityComposer equalityComposer;

		public InequalityConstructor(OperatorExpressionDictionary dictionary, EqualityComposer equalityComposer)
		{
			this.dictionary = dictionary;
			this.equalityComposer = equalityComposer;
		}

		public Expression<Func<T1, T2, bool>> BuildExpression<T1, T2, TReturn1, TReturn2>(Expression<Func<T1, TReturn1>> a, Expression<Func<T2, TReturn2>> b)
		{
			IOperatorExpression config;
			if (dictionary.TryGetValue<TReturn1, TReturn2>(out config))
			{
				var expCfg = (RelationalOperaton<TReturn1, TReturn2>)config;

				var expr = equalityComposer.Build(expCfg.Parts, this);

				return a.NestExpression(b, expr).Not();
			}

			return (this.Build<T1, T2, TReturn1, TReturn2>(a, b)).Not();
		}

		private Expression<Func<T1, T2, bool>> Build<T1, T2>()
		{
			return (T1 a, T2 b) => a.Equals(b);
		}

		private Expression<Func<T1, T2, bool>> Build<T1, T2, TReturn1, TReturn2>(Expression<Func<T1, TReturn1>> a, Expression<Func<T2, TReturn2>> b)
		{
			return a.Eq(b);
		}
	}
}