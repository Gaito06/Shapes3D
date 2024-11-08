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

            // Call the Solver to calculate the requested total
            decimal total = Solver.CalculateTotal(shapes);

            // Format the output
            Console.WriteLine($"The sum of measurements is {total.ToString("N2", CultureInfo.InvariantCulture)}.");
        }
    }

    public static class Solver
    {
        public static List<Shape3D> ParseShapesFromFile(string filePath)
        {
            var shapes = new List<Shape3D>();

            foreach (var line in File.ReadLines(filePath))
            {
                var parts = line.Split(',');

                if (parts.Length == 0) continue;

                string shapeType = parts[0].Trim().ToLower();

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
                    case "area":
                    case "volume":
                        // Handle calculation commands in the next loop
                        break;
                    default:
                        Console.WriteLine($"Unrecognized shape or command: {shapeType}");
                        break;
                }
            }

            return shapes;
        }

        public static decimal CalculateTotal(List<Shape3D> shapes)
        {
            decimal total = 0;

            foreach (var shape in shapes)
            {
                // We need to calculate the area or volume based on the shape type
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

    public abstract class Shape3D
    {
        // Common properties and methods for all shapes can go here
    }

    public interface IShapeWithArea
    {
        double GetArea();
    }

    public interface IShapeWithVolume
    {
        double GetVolume();
    }

    public class Cube : Shape3D, IShapeWithArea, IShapeWithVolume
    {
        public double SideLength { get; }

        public Cube(double sideLength)
        {
            SideLength = sideLength;
        }

        public double GetArea()
        {
            return 6 * Math.Pow(SideLength, 2); // Surface area of a cube
        }

        public double GetVolume()
        {
            return Math.Pow(SideLength, 3); // Volume of a cube
        }
    }

    public class Cuboid : Shape3D, IShapeWithArea, IShapeWithVolume
    {
        public double Width { get; }
        public double Height { get; }
        public double Depth { get; }

        public Cuboid(double width, double height, double depth)
        {
            Width = width;
            Height = height;
            Depth = depth;
        }

        public double GetArea()
        {
            return 2 * (Width * Height + Width * Depth + Height * Depth); // Surface area of a cuboid
        }

        public double GetVolume()
        {
            return Width * Height * Depth; // Volume of a cuboid
        }
    }

    public class Sphere : Shape3D, IShapeWithArea, IShapeWithVolume
    {
        public doubl
