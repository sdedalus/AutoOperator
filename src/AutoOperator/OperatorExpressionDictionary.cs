using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoOperator
{
	public class OperatorExpressionDictionary : IDictionary<Tuple<Type, Type>, IOperatorExpression>
	{
		private ConcurrentDictionary<Tuple<Type, Type>, IOperatorExpression> innerDictionary;

		public OperatorExpressionDictionary()
		{
			innerDictionary = new ConcurrentDictionary<Tuple<Type, Type>, IOperatorExpression>();
		}

		public IOperatorExpression this[Tuple<Type, Type> key]
		{
			get
			{
				return innerDictionary[key];
			}

			set
			{
				innerDictionary[key] = value;
			}
		}

		public int Count => innerDictionary.Count;

		public bool IsReadOnly => false;

		public ICollection<Tuple<Type, Type>> Keys => innerDictionary.Keys;

		public ICollection<IOperatorExpression> Values => innerDictionary.Values;

		public void Add(KeyValuePair<Tuple<Type, Type>, IOperatorExpression> item)
		{
			innerDictionary.TryAdd(item.Key, item.Value);
		}

		public void Add<T1, T2>(IOperatorExpression value)
		{
			innerDictionary.TryAdd(Tuple.Create(typeof(T1), typeof(T2)), value);
		}

		public void Add(Tuple<Type, Type> key, IOperatorExpression value)
		{
			innerDictionary.TryAdd(key, value);
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool Contains(KeyValuePair<Tuple<Type, Type>, IOperatorExpression> item)
		{
			return innerDictionary.Contains(item);
		}

		public bool ContainsKey(Tuple<Type, Type> key)
		{
			return innerDictionary.ContainsKey(key);
		}

		public void CopyTo(KeyValuePair<Tuple<Type, Type>, IOperatorExpression>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<KeyValuePair<Tuple<Type, Type>, IOperatorExpression>> GetEnumerator()
		{
			return innerDictionary.GetEnumerator();
		}

		public bool Remove(KeyValuePair<Tuple<Type, Type>, IOperatorExpression> item)
		{
			IOperatorExpression ret;
			return innerDictionary.TryRemove(item.Key, out ret);
		}

		public bool Remove(Tuple<Type, Type> key)
		{
			IOperatorExpression ret;
			return innerDictionary.TryRemove(key, out ret);
		}

		public bool Remove<T1, T2>()
		{
			IOperatorExpression ret;
			return innerDictionary.TryRemove(Tuple.Create(typeof(T1), typeof(T2)), out ret);
		}

		public bool TryGetValue(Tuple<Type, Type> key, out IOperatorExpression value)
		{
			return innerDictionary.TryRemove(key, out value);
		}

		public bool TryGetValue<T1, T2>(out IOperatorExpression value)
		{
			return innerDictionary.TryGetValue(Tuple.Create(typeof(T1), typeof(T2)), out value);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return innerDictionary.GetEnumerator();
		}
	}
}