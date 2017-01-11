using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoOperator
{
	internal static class ExpressionExtensions
	{
		/// <summary>
		/// Composes two expressions together with an operation and merges the parameters.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <param name="merge">The merge.</param>
		/// <returns></returns>
		public static Expression<T> ComposeAndMerge<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
		{
			// build parameter map (from parameters of second to parameters of first)
			var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

			// replace parameters in the second lambda expression with parameters from the first
			var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

			// apply composition of lambda expression bodies to parameters from the first expression
			return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
		}

		/// <summary>
		/// Composes two expressions together with an operation and concatenates the parameters.
		/// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
		/// <typeparam name="T2">The type of the 2.</typeparam>
		/// <typeparam name="TReturn1">The type of the return1.</typeparam>
		/// <typeparam name="TReturn2">The type of the return2.</typeparam>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <param name="merge">The merge.</param>
		/// <returns></returns>
		public static Expression<Func<T1, T2, bool>> ComposeAndConcatenate<T1, T2, TReturn1, TReturn2>(this Expression<Func<T1, TReturn1>> first, Expression<Func<T2, TReturn2>> second, Func<Expression, Expression, Expression> merge)
		{
			var Parameters = first.Parameters.Concat(second.Parameters);
			return Expression.Lambda<Func<T1, T2, bool>>(merge(first.Body, second.Body), Parameters);
		}

		/// <summary>
		/// Composes two expressions together with an Third expression that takes the return type of
		/// the other expressions and returns a bool.
		/// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
		/// <typeparam name="T2">The type of the 2.</typeparam>
		/// <typeparam name="TReturn1">The type of the return1.</typeparam>
		/// <typeparam name="TReturn2">The type of the return2.</typeparam>
		/// <param name="a">a.</param>
		/// <param name="b">The b.</param>
		/// <param name="expr">The expr.</param>
		/// <returns></returns>
		public static Expression<Func<T1, T2, bool>> NestExpression<T1, T2, TReturn1, TReturn2>(this Expression<Func<T1, TReturn1>> a, Expression<Func<T2, TReturn2>> b, Expression<Func<TReturn1, TReturn2, bool>> expr)
		{
			var left = ((System.Linq.Expressions.BinaryExpression)expr.Body).Left;
			var leftInnerProp = GetOuterProperty(left);
			var leftFull = Expression.PropertyOrField(a.Body, leftInnerProp.Name);

			var right = ((System.Linq.Expressions.BinaryExpression)expr.Body).Right;
			var rightInnerProp = GetOuterProperty(right);
			var rightFull = Expression.PropertyOrField(b.Body, rightInnerProp.Name);
			IEnumerable<ParameterExpression> expParams = a.Parameters.Concat(b.Parameters);
			return Expression.Lambda<Func<T1, T2, bool>>(Expression.Equal(leftFull, rightFull), expParams);
		}

		/// <summary>
		/// Gets the outer property.
		/// </summary>
		/// <param name="exp">The exp.</param>
		/// <returns></returns>
		private static PropertyInfo GetOuterProperty(Expression exp)
		{
			MemberExpression outerMember = (MemberExpression)exp;
			return (PropertyInfo)outerMember.Member;
		}

		/// <summary>
		/// compose two expressions with an and operator.
		/// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
		/// <typeparam name="T2">The type of the 2.</typeparam>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns></returns>
		public static Expression<Func<T1, T2, bool>> And<T1, T2>(this Expression<Func<T1, T2, bool>> first, Expression<Func<T1, T2, bool>> second)
		{
			return first.ComposeAndMerge(second, Expression.And);
		}

		/// <summary>
		/// compose two expressions with an and also operator.
		/// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
		/// <typeparam name="T2">The type of the 2.</typeparam>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns></returns>
		public static Expression<Func<T1, T2, bool>> AndAlso<T1, T2>(this Expression<Func<T1, T2, bool>> first, Expression<Func<T1, T2, bool>> second)
		{
			return first.ComposeAndMerge(second, Expression.AndAlso);
		}

		/// <summary>
		/// Compose two expressions together with equals.
		/// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
		/// <typeparam name="T2">The type of the 2.</typeparam>
		/// <typeparam name="TReturn1">The type of the return1.</typeparam>
		/// <typeparam name="TReturn2">The type of the return2.</typeparam>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns></returns>
		public static Expression<Func<T1, T2, bool>> Eq<T1, T2, TReturn1, TReturn2>(this Expression<Func<T1, TReturn1>> first, Expression<Func<T2, TReturn2>> second)
		{
			return first.ComposeAndConcatenate(second, Expression.Equal);
		}

		/// <summary>
		/// Compose two bool returning expressions together with and.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns></returns>
		public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
		{
			return first.ComposeAndMerge(second, Expression.And);
		}

		/// <summary>
		/// Compose two bool returning expressions together with or.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns></returns>
		public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
		{
			return first.ComposeAndMerge(second, Expression.Or);
		}

		/// <summary>
		/// Compose two bool returning expressions together with or else.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns></returns>
		public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
		{
			return first.ComposeAndMerge(second, Expression.OrElse);
		}
	}
}