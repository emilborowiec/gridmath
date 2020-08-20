#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace PonderingProgrammer.GridMath
{
    /// <summary>
    ///     Utility class for operations on collections of GriBoundingBoxes
    /// </summary>
    public static class GridBoundingBoxes
    {
        public static List<List<int>> FindOverlappingBoxes(GridBoundingBox[] boxes)
        {
            var listOfOverlapLists = new List<List<int>>();

            var xOverlapGroups =
                GridIntervals.FindOverlappingIntervals(boxes.Select(box => box.XInterval).ToArray());
            var yOverlapGroups =
                GridIntervals.FindOverlappingIntervals(boxes.Select(box => box.YInterval).ToArray());

            // is there a list on x lists which has the same two or more indices that some y list
            foreach (var xOverlapList in xOverlapGroups)
            foreach (var yOverlapList in yOverlapGroups)
            {
                var commons = xOverlapList.Where(yOverlapList.Contains).ToArray();
                if (commons.Length > 1)
                {
                    var overlappingBoxes = new List<int>();
                    for (var i = 0; i < commons.Length; i++) overlappingBoxes.Add(commons[i]);

                    listOfOverlapLists.Add(overlappingBoxes);
                }
            }

            return listOfOverlapLists;
        }

        public static GridCoordinatePair FindCenterOfMass(GridBoundingBox[] boxes)
        {
            var xCenter = GridIntervals.FindCenterOfMass(boxes.Select(b => b.XInterval).ToArray());
            var yCenter = GridIntervals.FindCenterOfMass(boxes.Select(b => b.YInterval).ToArray());
            return new GridCoordinatePair(xCenter, yCenter);
        }

        /// <summary>
        ///     Packs boudning boxes somewhat organically. Does not try too hard to save space.
        /// </summary>
        /// <param name="boxes"></param>
        /// <param name="alignment"></param>
        /// <param name="spacing"></param>
        public static void Pack(GridBoundingBox[] boxes, BoxAlignment alignment = BoxAlignment.Center, int spacing = 0)
        {
            if (boxes == null) throw new ArgumentNullException(nameof(boxes));

            var originalTotalMinX = int.MaxValue;
            var originalTotalMaxX = int.MinValue;
            var newTotalMaxX = int.MinValue;
            var originalTotalMinY = int.MaxValue;
            var originalTotalMaxY = int.MinValue;
            var newTotalMaxY = int.MinValue;
            var centerOfMass = FindCenterOfMass(boxes);
            var possibilities = new List<GridCoordinatePair> {new GridCoordinatePair(0, 0)};
            for (var i = 0; i < boxes.Length; i++)
            {
                if (originalTotalMinX > boxes[i].MinX) originalTotalMinX = boxes[i].MinX;
                if (originalTotalMinY > boxes[i].MinY) originalTotalMinY = boxes[i].MinY;
                if (originalTotalMaxX < boxes[i].MaxX) originalTotalMaxX = boxes[i].MaxX;
                if (originalTotalMaxY < boxes[i].MaxY) originalTotalMaxY = boxes[i].MaxY;

                foreach (var coords in possibilities)
                {
                    var box = boxes[i].SetPosition(coords.X, coords.Y, IntervalAnchor.Start, IntervalAnchor.Start);
                    var fits = true;
                    for (var j = 0; j < i; j++)
                    {
                        if (box.Overlaps(boxes[j]))
                        {
                            fits = false;
                        }
                    }

                    if (!fits) continue;

                    boxes[i] = box;
                    possibilities.Remove(coords);
                    // alternate horizontal and vertical possibilities
                    if (i % 2 == 0)
                    {
                        possibilities.Add(new GridCoordinatePair(box.MaxXExcl + spacing, box.MinY));
                        possibilities.Add(new GridCoordinatePair(box.MinX, box.MaxYExcl + spacing));
                    }
                    else
                    {
                        possibilities.Add(new GridCoordinatePair(box.MinX, box.MaxYExcl + spacing));
                        possibilities.Add(new GridCoordinatePair(box.MaxXExcl + spacing, box.MinY));
                    }

                    if (newTotalMaxX < boxes[i].MaxX) newTotalMaxX = boxes[i].MaxX;
                    if (newTotalMaxY < boxes[i].MaxY) newTotalMaxY = boxes[i].MaxY;
                    break;
                }
            }

            var newCenterOfMass = FindCenterOfMass(boxes);
            var translation = alignment switch
            {
                BoxAlignment.TopLeft => new GridCoordinatePair(originalTotalMinX, originalTotalMinY),
                BoxAlignment.Top => new GridCoordinatePair(centerOfMass.X - newCenterOfMass.X, originalTotalMinY),
                BoxAlignment.TopRight => new GridCoordinatePair(originalTotalMaxX - newTotalMaxX, originalTotalMinY),
                BoxAlignment.Right => new GridCoordinatePair(
                    originalTotalMaxX - newTotalMaxX,
                    centerOfMass.Y - newCenterOfMass.Y),
                BoxAlignment.BottomRight => new GridCoordinatePair(
                    originalTotalMaxX - newTotalMaxX,
                    originalTotalMaxY - newTotalMaxY),
                BoxAlignment.Bottom => new GridCoordinatePair(
                    centerOfMass.X - newCenterOfMass.X,
                    originalTotalMaxY - newTotalMaxY),
                BoxAlignment.BottomLeft => new GridCoordinatePair(originalTotalMinX, originalTotalMaxY - newTotalMaxY),
                BoxAlignment.Left => new GridCoordinatePair(originalTotalMinX, centerOfMass.Y - newCenterOfMass.Y),
                BoxAlignment.Center => new GridCoordinatePair(
                    centerOfMass.X - newCenterOfMass.X,
                    centerOfMass.Y - newCenterOfMass.Y),
                _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null),
            };

            foreach (var box in boxes) box.Translation(translation.X, translation.Y);
        }
    }
}