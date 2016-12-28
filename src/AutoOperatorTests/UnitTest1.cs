using AutoOperator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;

namespace AutoOperatorTests
{
	[TestClass]
	public class AutoOperatorConfigurationShould
	{
		[TestMethod]
		public void TestMethod1()
		{
			Operators.Initialize(
				conf => conf
					.ConfigureEquality<Car, Boat>(
						eq => eq
							.Operation(c1 => c1.Color, c2 => c2.Color)
							.Operation(c1 => c1.Price, c2 => c2.Price)));

			Assert.IsFalse(Operators.Equals(new Car() { Color = "", Price = 10 }, new Boat() { Color = "", Price = 11 }));
		}

		[TestMethod]
		public void TestMethod2()
		{
			var builder = new EqualsOperatorExpression<Car, Boat>();

			var theCarMatchesTheBoat = builder
				.Operation(c1 => c1.Color, c2 => c2.Color)
				.Operation(c1 => c1.Price, c2 => c2.Price)
				.Build().Compile();

			Assert.IsTrue(theCarMatchesTheBoat(new Car() { Color = "", Price = 10 }, new Boat() { Color = "", Price = 10 }));
		}

		private Expression<Func<T1, T2, bool>> BuildExpression<T1, T2, TReturn>(Expression<Func<T1, TReturn>> a, Expression<Func<T2, TReturn>> b)
		{
			return a.Eq(b);
		}
	}

	internal class Car
	{
		public string Color { get; internal set; }
		public double Price { get; internal set; }
	}

	internal class Boat
	{
		public string Color { get; internal set; }
		public double Price { get; internal set; }
	}
}