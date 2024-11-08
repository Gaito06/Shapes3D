using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace FinalAssignment
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a CSV file as an argument.");
                return;
            }

            string filePath = args[0];

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found. Please check the file path.");
                return;
            }

            List<Shape3D> shapes = Solver.ParseShapesFromFile(filePath);

            if (shapes == null || shapes.Count == 0)
            {
                Console.WriteLine("No shapes were parsed. Please check your CSV file.");
                return;
            }

            decimal total = Solver.CalculateTotal(shapes);

            Console.WriteLine($"The sum of measurements is {total:N2}.");
        }
    }

    public static class Solver
    {
        public static List<Shape3D> ParseShapesFromFile(string filePath)
        {
            var shapes = new List<Shape3D>();
            try
            {
                foreach (var line in File.ReadLines(filePath))
                {
                    var parts = line.Split(',');

                    if (parts.Length == 0) continue;

                    string shapeType = parts[0].Trim().ToLower();
                    try
                    {
                        switch (shapeType)
                        {
                            case "cube":
                                if (parts.Length == 2)
                                {
                                    double sideLength = double.Parse(parts[1], CultureInfo.InvariantCulture);
                                    shapes.Add(new Cube(sideLength));
                                }
                                break;
                            case "cuboid":
                                if (parts.Length == 4)
                                {
                                    double width = double.Parse(parts[1], CultureInfo.InvariantCulture);
                                    double height = double.Parse(parts[2], CultureInfo.InvariantCulture);
                                    double depth = double.Parse(parts[3], CultureInfo.InvariantCulture);
                                    shapes.Add(new Cuboid(width, height, depth));
                                }
                                break;
                            case "sphere":
                                if (parts.Length == 2)
                                {
                                    double radius = double.Parse(parts[1], CultureInfo.InvariantCulture);
                                    shapes.Add(new Sphere(radius));
                                }
                                break;
                            case "cylinder":
                                if (parts.Length == 3)
                                {
                                    double radius = double.Parse(parts[1], CultureInfo.InvariantCulture);
                                    double height = double.Parse(parts[2], CultureInfo.InvariantCulture);
                                    shapes.Add(new Cylinder(radius, height));
                                }
                                break;
                            case "prism":
                                if (parts.Length == 4)
                                {
                                    double sideLength = double.Parse(parts[1], CultureInfo.InvariantCulture);
                                    int faces = int.Parse(parts[2]);
                                    double height = double.Parse(parts[3], CultureInfo.InvariantCulture);
                                    shapes.Add(new Prism(sideLength, faces, height));
                                }
                                break;
                            default:
                                Console.WriteLine($"Unrecognized shape: {shapeType}");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error parsing line '{line}': {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }

            return shapes;
        }

        public static decimal CalculateTotal(List<Shape3D> shapes)
        {
            decimal total = 0;

            foreach (var shape in shapes)
            {
                if (shape is IShapeWithArea)
                {
                    total += (decimal)((IShapeWithArea)shape).GetArea();
                }
                if (shape is IShapeWithVolume)
                {
                    total += (decimal)((IShapeWithVolume)shape).GetVolume();
                }
            }

            return total;
        }
    }
}
