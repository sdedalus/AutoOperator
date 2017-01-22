using AutoOperator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
					.Relation<Car, Boat>(
						eq => eq
							.Operation(c1 => c1.Color, c2 => c2.Color)
							.Operation(c1 => c1.Price, c2 => c2.Price)
							.Operation(c1 => c1.CarEngine, c2 => c2.BoatEngine))
					.Relation<CarEngine, BoatEngine>(
						eq => eq
							.Operation(e1 => e1.Hp, e2 => e2.Hp)
						));

			Assert.IsFalse(Operators.Equals(new Car() { Color = "", Price = 10, CarEngine = new CarEngine() { Hp = 300 } }, new Boat() { Color = "", Price = 10, BoatEngine = new BoatEngine() { Hp = 200 } }));
		}

		[TestMethod]
		public void TestMethod2()
		{
			Operators.Initialize(
				conf => conf
					.Relation<Car, Boat>(
						eq => eq
							.Operation(c1 => c1.Color, c2 => c2.Color)
							.Operation(c1 => c1.Price, c2 => c2.Price)
							.Operation(c1 => c1.CarEngine, c2 => c2.BoatEngine))
					.Relation<CarEngine, BoatEngine>(
						eq => eq
							.Operation(e1 => e1.Hp, e2 => e2.Hp)
						));

			Assert.IsTrue(Operators.Equals(new Car() { Color = "", Price = 10, CarEngine = new CarEngine() { Hp = 200 } }, new Boat() { Color = "", Price = 10, BoatEngine = new BoatEngine() { Hp = 200 } }));
		}
	}

	internal class Car
	{
		public string Color { get; internal set; }
		public double Price { get; internal set; }

		public CarEngine CarEngine { get; internal set; }
	}

	internal class CarEngine
	{
		public int Hp { get; internal set; }
	}

	internal class Boat
	{
		public string Color { get; internal set; }
		public double Price { get; internal set; }

		public BoatEngine BoatEngine { get; internal set; }
	}

	internal class BoatEngine
	{
		public int Hp { get; internal set; }
	}
}