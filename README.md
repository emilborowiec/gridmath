# GridMath

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

We model our Square Grid as a coordinate space over set of integer numbers Z, 
with the following correspondence to real numbers R:

- For each integer number n, half-open real interval R[n,n+1) maps to n in our discrete integer space.
- Conversely, the n maps to a midpoint of corresponding real interval, so Z(n) => R(n + 0.5)

**Grid Coordinates**

Grid Coordinates are more precisely 2D Square Grid Coordinates.
While one-dimensional integer value maps to real interval,
grid coordinates map to a square in real coordinate space.
Origin of Grid Coordinate space maps to point (0.5,0.5) of real coordinate space.

Supported features include:  

- Read-only value type for storing Grid Coordinates.
- Mapping to and from cartesian plane coordinates.
- Measuring distance (Manhattan, Chebyshev, Euclidean).
- Translation (with grid coordinates; *todo: with grid polar and grid direction coordinates*).

**Grid Polar Coordinates**

Like normal polar coordinates, those are expressed with Radius and Theta angle.
Reference point of those coordinates corresponds to point (0,0) in Grid Coordinate space,
which in turn is (0.5,0.5) in real cartesian coordinates.

- Read-only value type for storing Grid Polar Coordinates.
- Conversion to and from Grid Coordinates.
- Rotation around angle.

**Grid Directions and Rotations**

- Support for cardinal directions (up, down, left, right) and intermediate directions (with diagonals).
- Conversion to and from angles (angle intervals are approximated to nearest direction).
- Grid rotation of directions and angles. Grid rotation is expressed in subdivision and number of ticks to go either clockwise or counterclockwise.

**Grid Direction Coordinates**

Grid Direction Coordinates are like Grid Polar Coordinates, but simplified to use Direction instead of angle.
Useful to express simple translation vectors on grid.

- Mapping to Grid Coordinates.

**Grid Intervals**

Grid Interval are one dimensional grid-geometry objects.

- Read-only value type for storing intervals.
- Finding center.
- Spatial tests (contains, overlaps, touches).
- Measuring distance and overlap depth to point or other interval.
- Translations.
- Alignment relative to other interval with an anchor.
- Separation of overlapping intervals.
- Algorithm for finding subsets of overlapping intervals in larger collection.
- Algorithm for interval packing with given spacing.

**Grid Bounding Boxes**

Grid Bounding Boxes are axis-aligned bounding boxes composed of two intervals - along X and Y axes.

- Read-only value type for storing bounding boxes.
- Finding center.
- Spatial tests (contains, overlaps, touches).
- Finding bounding boxes nearest point to other point. 
- Alignment relative to other bounding box with an anchor for each axis.
- Algorithm for finding subsets of overlapping bounding boxes in larger collection.
- Algorithm for box packing with given spacing (needs improvement).

**Rasterization**

Rasterization is about approximating real geometry on discrete medium.

- Bresenham's algorithm for plotting lines on grid
- Bresenham's algorithm for plotting circles on grid
- FloodFill algorithm to find grid points within arbitrary bounds
- Finding octant of segment slope

**Grid Shapes**

Grid shapes are geometrical shapes approximated to discrete grid.

- Supported shapes:
    - Point,
    - Axis aligned rectangle,
    - Axis aligned line segment,
    - Line segment,
    - Circle,
    - Fan (circle quadrant)
- Bounding boxes
- Enumerating edge of the shape
- Enumerating interior of the shape
- Spatial testing (contains and overlaps)
- Translation
- Rotation


