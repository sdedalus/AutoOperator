using System;
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
		public Expression<Func<T1, T2, bool>> Build<T1, T2>(IExpresionList<T1, T2> expParts, IRelationalExpressionBuilder builder)
		{
			return expParts.Select(v => v(builder)).Aggregate((a, b) => a.AndAlso(b));
		}
	}

	public class InequalityComposer
	{
		/// <summary>
		/// Builds this instance.
		/// </summary>
		/// <returns></returns>
		public Expression<Func<T1, T2, bool>> Build<T1, T2>(IExpresionList<T1, T2> expParts, IRelationalExpressionBuilder builder)
		{
			return expParts.Select(v => v(builder)).Aggregate((a, b) => a.OrElse(b));
		}
	}
}