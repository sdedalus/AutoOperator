using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoOperator
{
	public class OperatorExpressionDictionary : IDictionary<Tuple<Type, Type>, IExpresionList>
	{
		private ConcurrentDictionary<Tuple<Type, Type>, IExpresionList> innerDictionary;

		public OperatorExpressionDictionary()
		{
			innerDictionary = new ConcurrentDictionary<Tuple<Type, Type>, IExpresionList>();
		}

		public IExpresionList this[Tuple<Type, Type> key]
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

		public ICollection<IExpresionList> Values => innerDictionary.Values;

		public void Add(KeyValuePair<Tuple<Type, Type>, IExpresionList> item)
		{
			innerDictionary.TryAdd(item.Key, item.Value);
		}

		public void Add<T1, T2>(IExpresionList value)
		{
			innerDictionary.TryAdd(Tuple.Create(typeof(T1), typeof(T2)), value);
		}

		public void Add(Tuple<Type, Type> key, IExpresionList value)
		{
			innerDictionary.TryAdd(key, value);
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool Contains(KeyValuePair<Tuple<Type, Type>, IExpresionList> item)
		{
			return innerDictionary.Contains(item);
		}

		public bool ContainsKey(Tuple<Type, Type> key)
		{
			return innerDictionary.ContainsKey(key);
		}

		public void CopyTo(KeyValuePair<Tuple<Type, Type>, IExpresionList>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<KeyValuePair<Tuple<Type, Type>, IExpresionList>> GetEnumerator()
		{
			return innerDictionary.GetEnumerator();
		}

		public bool Remove(KeyValuePair<Tuple<Type, Type>, IExpresionList> item)
		{
			IExpresionList ret;
			return innerDictionary.TryRemove(item.Key, out ret);
		}

		public bool Remove(Tuple<Type, Type> key)
		{
			IExpresionList ret;
			return innerDictionary.TryRemove(key, out ret);
		}

		public bool Remove<T1, T2>()
		{
			IExpresionList ret;
			return innerDictionary.TryRemove(Tuple.Create(typeof(T1), typeof(T2)), out ret);
		}

		public bool TryGetValue(Tuple<Type, Type> key, out IExpresionList value)
		{
			return innerDictionary.TryRemove(key, out value);
		}

		public bool TryGetValue<T1, T2>(out IExpresionList<T1, T2> value)
		{
			IExpresionList outVaue;
			var returnValue = innerDictionary.TryGetValue(Tuple.Create(typeof(T1), typeof(T2)), out outVaue);
			if (returnValue)
			{
				value = (IExpresionList<T1, T2>)outVaue;
			}
			else
			{
				value = null;
			}

			return returnValue;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return innerDictionary.GetEnumerator();
		}
	}
}