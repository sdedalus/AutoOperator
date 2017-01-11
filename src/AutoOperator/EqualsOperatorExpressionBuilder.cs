using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoOperator
{
	public class EqualsOperatorExpressionBuilder
	{
		private ConcurrentDictionary<Tuple<Type, Type>, IOperatorExpression> dictionary = new ConcurrentDictionary<Tuple<Type, Type>, IOperatorExpression>();

		public void Add(Tuple<Type, Type> key, IOperatorExpression value)
		{
			dictionary.TryAdd(key, value);
		}

		public void Add<T1, T2>(IOperatorExpression value)
		{
			dictionary.TryAdd(Tuple.Create(typeof(T1), typeof(T2)), value);
		}

		public Expression<Func<T1, T2, bool>> GetExpression<T1, T2>()
		{
			IOperatorExpression config;
			if (dictionary.TryGetValue(Tuple.Create(typeof(T1), typeof(T2)), out config))
			{
				return ((EqualsOperatorExpression<T1, T2>)config).Build();
			}

			return (T1 a, T2 b) => a.Equals(b);
		}

		public Expression<Func<T1, T2, bool>> BuildExpression<T1, T2, TReturn1, TReturn2>(Expression<Func<T1, TReturn1>> a, Expression<Func<T2, TReturn2>> b)
		{
			IOperatorExpression config;
			if (dictionary.TryGetValue(Tuple.Create(typeof(TReturn1), typeof(TReturn2)), out config))
			{
				var expCfg = (EqualsOperatorExpression<TReturn1, TReturn2>)config;
				var expr = expCfg.Build();
				return a.NestExpression(b, expr);
			}

			return BuildSimpleExpression<T1, T2, TReturn1, TReturn2>(a, b);
		}

		private static Expression<Func<T1, T2, bool>> BuildSimpleExpression<T1, T2, TReturn1, TReturn2>(Expression<Func<T1, TReturn1>> a, Expression<Func<T2, TReturn2>> b)
		{
			return a.Eq(b);
		}

		private class other
		{
			public eng eng;
		}

		private class eng
		{
			public int hp;
		}
	}
}