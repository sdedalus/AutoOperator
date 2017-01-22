using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AutoOperator
{
	public class ExpresionList<T1, T2> : List<Func<IRelationalOperatorExpressionBuilder, Expression<Func<T1, T2, bool>>>>, IExpresionList
	{
		public ExpresionList() : base()
		{
		}

		public ExpresionList(int capacity) : base(capacity)
		{
		}

		public ExpresionList(IEnumerable<Func<IRelationalOperatorExpressionBuilder, Expression<Func<T1, T2, bool>>>> collection) : base(collection)
		{
		}
	}

	public interface IExpresionList
	{
	}
}