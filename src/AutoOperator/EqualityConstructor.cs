﻿using System;
using System.Linq.Expressions;

namespace AutoOperator
{
	public class EqualityConstructor : IRelationalExpressionBuilder
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
			IExpresionList<TReturn1, TReturn2> config;
			if (dictionary.TryGetValue<TReturn1, TReturn2>(out config))
			{
				var expr = equalityComposer.Build(config, this);
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

	public class InequalityConstructor : IRelationalExpressionBuilder
	{
		private OperatorExpressionDictionary dictionary;
		private InequalityComposer inequalityComposer;

		public InequalityConstructor(OperatorExpressionDictionary dictionary, InequalityComposer inequalityComposer)
		{
			this.dictionary = dictionary;
			this.inequalityComposer = inequalityComposer;
		}

		public Expression<Func<T1, T2, bool>> BuildExpression<T1, T2, TReturn1, TReturn2>(Expression<Func<T1, TReturn1>> a, Expression<Func<T2, TReturn2>> b)
		{
			IExpresionList<TReturn1, TReturn2> config;
			if (dictionary.TryGetValue<TReturn1, TReturn2>(out config))
			{
				var expr = inequalityComposer.Build(config, this);

				return a.NestExpression(b, expr);
			}

			return (this.Build<T1, T2, TReturn1, TReturn2>(a, b));
		}

		public Expression<Func<T1, T2, bool>> Build<T1, T2>()
		{
			return (T1 a, T2 b) => !a.Equals(b);
		}

		private Expression<Func<T1, T2, bool>> Build<T1, T2, TReturn1, TReturn2>(Expression<Func<T1, TReturn1>> a, Expression<Func<T2, TReturn2>> b)
		{
			return a.NotEq(b);
		}
	}
}