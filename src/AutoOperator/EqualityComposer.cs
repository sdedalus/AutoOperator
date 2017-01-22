using AutoOperator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AutoOperator
{

	public class EqualityComposer
	{
		/// <summary>
		/// Builds this instance.
		/// </summary>
		/// <returns></returns>
		public Expression<Func<T1, T2, bool>> Build<T1, T2>(IEnumerable<Func<IRelationalOperatorExpressionBuilder, Expression<Func<T1, T2, bool>>>> expParts, IRelationalOperatorExpressionBuilder builder)
		{
			return expParts.Select(v => v(builder)).Aggregate((a, b) => a.AndAlso(b));
		}
	}
}