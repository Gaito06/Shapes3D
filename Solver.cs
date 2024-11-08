using System;
using System.Collections.Generic;
using System.Globalization;
using Shapes3D;

namespace FinalAssignment
{
    public static class Solver
    {
        // This method processes each line, creating shapes and calculating totals when requested
        public static decimal Solve(string[] data)
        {
            var shapes = new List<Shape3D>();
            decimal totalMeasurement = 0.0m;  // Use decimal for accurate money calculations and to match your output formatting

            foreach (var line in data)
            {
                var parts = line.Split(',');

                // If the first part is the shape, create it
                if (parts[0] == "cube")
                {
                    shapes.Add(new Cube(double.Parse(parts[1], CultureInfo.InvariantCulture)));
                }
                else if (parts[0] == "cuboid")
                {
                    shapes.Add(new Cuboid(
                        double.Parse(parts[1], CultureInfo.InvariantCulture),
                        double.Parse(parts[2], CultureInfo.InvariantCulture),
                        double.Parse(parts[3], CultureInfo.InvariantCulture)
                    ));
                }
                else if (parts[0] == "prism")
                {
                    shapes.Add(new Prism(
                        double.Parse(parts[1], CultureInfo.InvariantCulture),
                        int.Parse(parts[2]),
                        double.Parse(parts[3], CultureInfo.InvariantCulture)
                    ));
                }
                else if (parts[0] == "cylinder")
                {
                    shapes.Add(new Cylinder(
                        double.Parse(parts[1], CultureInfo.InvariantCulture),
                        double.Parse(parts[2], CultureInfo.InvariantCulture)
                    ));
                }
                else if (parts[0] == "sphere")
                {
                    shapes.Add(new Sphere(double.Parse(parts[1], CultureInfo.InvariantCulture)));
                }
                else if (parts[0] == "area" || parts[0] == "volume")
                {
                    decimal scale = decimal.Parse(parts[1], CultureInfo.InvariantCulture);
                    foreach (var shape in shapes)
                    {
                        decimal measurement = 0;

                        // Get area or volume based on the instruction
                        if (parts[0] == "area" && shape is IShapeWithArea shapeWithArea)
                        {
                            measurement = (decimal)shapeWithArea.GetArea();
                        }
                        else if (parts[0] == "volume" && shape is IShapeWithVolume shapeWithVolume)
                        {
                            measurement = (decimal)shapeWithVolume.GetVolume();
                        }

                        // Apply the scaling factor and add to total
                        totalMeasurement += measurement * scale;
                    }
                }
            }

            return totalMeasurement;
        }
    }
}
