using System;
using System.Collections.Generic;
using Shapes3D;
using System.Globalization;

namespace FinalAssignment
{
    public static class Solver
    {
        public static double Solve(string[] data)
        {
            var shapes = new List<Shape>();
            double totalMeasurement = 0.0;

            foreach (var line in data)
            {
                var parts = line.Split(',');

                // Shape creation based on type
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
                    double scale = double.Parse(parts[1], CultureInfo.InvariantCulture);
                    foreach (var shape in shapes)
                    {
                        double measurement = (parts[0] == "area") ? shape.SurfaceArea : shape.Volume;
                        totalMeasurement += measurement * scale;
                    }
                }
            }

            return totalMeasurement;
        }
    }
}
