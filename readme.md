# AutoOperator

I started thinking the other day about AutoMapper and how what it does can be seen as facilitating the creation of cast functions for arbitrary types.  
Since implicit and explicit casts are defined on a class as operators that map from an instance of one type to an instance of another. I started wondering 
if all of the other operators could be defined that way as well.  This library is my first step in that direction.  

 
## Usage

An operation to compare a car and boat type can be written 
```csharp
Operators.Initialize(
	conf => conf
		.ConfigureEquality<Car, Boat>(
			eq => eq
				.Operation(c1 => c1.Color, c2 => c2.Color)
				.Operation(c1 => c1.Price, c2 => c2.Price)
				.Operation(c1 => c1.CarEngine, c2 => c2.BoatEngine))
		.ConfigureEquality<CarEngine, BoatEngine>(
			eq => eq
				.Operation(e1 => e1.Hp, e2 => e2.Hp)
			));
```

any comparison between two types that is defined in the configuration will be used when it is defined as part of the comparison of a containing type.

the above configuration produces the flowing expression for checking Car/Boat equality.

```csharp
{(c1, c2) => (((c1.Color == c2.Color) AndAlso (c1.Price == c2.Price)) AndAlso (c1.CarEngine.Hp == c2.BoatEngine.Hp))}
```




## Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request :D

## History

initial work is focused around equality.


## License

The MIT License

Copyright (c) 2010-2017 Google, Inc. http://angularjs.org

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.


