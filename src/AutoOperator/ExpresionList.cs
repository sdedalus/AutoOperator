using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AutoOperator
{
	public class ExpresionList<T1, T2> : List<Func<IRelationalExpressionBuilder, Expression<Func<T1, T2, bool>>>>, IExpresionList<T1, T2>
	{
		public ExpresionList() : base()
		{
		}

		public ExpresionList(int capacity) : base(capacity)
		{
		}

		public ExpresionList(IEnumerable<Func<IRelationalExpressionBuilder, Expression<Func<T1, T2, bool>>>> collection) : base(collection)
		{
		}
	}

	public interface IExpresionList
	{
	}

	public interface IExpresionList<T1, T2> : IEnumerable<Func<IRelationalExpressionBuilder, Expression<Func<T1, T2, bool>>>>, IList<Func<IRelationalExpressionBuilder, Expression<Func<T1, T2, bool>>>>, IExpresionList
	{
	}
}