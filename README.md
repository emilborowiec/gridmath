# GridMath

> This documentation is very much a work in progress. As is the whole library.

GridMath is a lightweight library for applications relying heavily on grid geometry. 
Example candidates include pixel art programs, certain kinds of games and procedural map generation libraries.

## The Grid

There are many kinds of grids. Two most commonly found in graphical applications are 
Square Grids and Hex Grids. In general, we can define any grid as a discrete coordinate space.
What makes grids different, however, are the operations on their coordinates. 
How do we determine adjacency? How do we measure distance? And most importantly,
How do we transform grid coordinates to cartesian coordinate plane and back. 

It is this mapping transformation that determines presentation of the grid on screen.
It also allows to take a point from real coordinate space and tell on which point of the grid it lands. 

GridMath library currently supports only Square Grids. Support for Hex Grids will come in later versions.

## Features

**Real To Grid Conversions**

- Real number to grid number.
- Grid number to real number.

**Grid Coordinates**

- Read-only value type for storing Grid Coordinates.
- Mapping to and from cartesian plane coordinates.
- Measuring distance (Manhattan, Chebyshev, Euclidean).
- Translation (with grid coordinates; *todo: with grid polar and grid direction coordinates*).

**Grid Polar Coordinates**

- Read-only value type for storing Grid Polar Coordinates.
- 

**Grid Directions and Rotations**

- 

**Grid Direction Coordinates**

- 

**Grid Intervals**

- 

**Grid Bounding Boxes**

- 

**Rasterization**

- 

**Grid Shapes**

- 


## Real to Grid Conversion





## Grid coordinate system

GridMath uses int to model a single grid coordinate.

## Mapping from real coordinates to grid coordinates

GridMath supports mapping real nubmers to grid coordinates.
Each discrete grid coordinate N corresponds to half-opened interval in real space R[N,N+1)

**todo:** drawing

This mapping is be implemented by getting Floor of the real value and converting to int.
Note that a simple cast of double to int only truncates the floating part.
This gives correct results only for positive numbers.

**todo:** drawing

You can use `RealToGrid` utility class to do this mapping for you.

## Grid Interval

`GridInterval` is our one-dimensional workhorse.

The class offers 3 convenient representations of an interval:

- Min to Max
- Min to Exclusive Max
- Min and Length

**todo:** drawing

Here are some of the method offered by `GridInterval`:

**Contains**

TODO: write about it

**Overlaps**

TODO: write about it

**Touches**

TODO: write about it

**Translate**

TODO: write about it

**Relate**

TODO: write about it

**Multiply**

TODO: write about it

**Distance**

TODO: write about it

**Depth**

There is also a utility class `GridIntervals` for more complex operations on intervals.
Some of the supported operations include:

**Finding subsets of overlapping intervals in collection**

TODO: write about it

**Packing intervals**

TODO: write about it

## 2D Grid Coordinates

`GridCoordinatePair` object stores x and y coordinates pointing to a place on 2d grid. 
Not very exciting but it also supports measuring Manhattan and Chebyshev distance to another location.

## Grid Bounding Box

`GridBoudningBox` is our two-dimensional workhorse and corresponding 
`GridBoundingBoxes` utility class supports complex operations on collections of boxes.

**Finding subset of overlapping boxes in collection**

TODO: write about it

**Packing boxes**

TODO: write about it

