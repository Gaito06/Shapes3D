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
            string filePath = GetFilePath(args);
            if (filePath == null) return;

            List<Shape3D> shapes = Solver.ParseShapesFromFile(filePath);
            if (shapes == null || shapes.Count == 0)
            {
                Console.WriteLine("No shapes were parsed. Please check your CSV file.");
                return;
            }

            decimal total = Solver.CalculateTotal(shapes);
            Console.WriteLine($"The sum of measurements is {total:N2}.");
        }

        // Extracted method to handle file validation and getting the file path.
        private static string GetFilePath(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a CSV file as an argument.");
                return null;
            }

            string filePath = args[0];
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}. Please check the file path.");
                return null;
            }

            return filePath;
        }
    }
}
