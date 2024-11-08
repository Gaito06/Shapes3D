using System;

namespace Shapes3D
{
    // Abstract base class
    public abstract class Shape
    {
        public abstract double SurfaceArea { get; }
        public abstract double Volume { get; }
    }

    // Cuboid (rectangular prism)
    public class Cuboid : Shape
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

        public override double SurfaceArea => 2 * (Width * Height + Height * Depth + Depth * Width);
        public override double Volume => Width * Height * Depth;
    }

    // Cube (child of Cuboid)
    public class Cube : Cuboid
    {
        public Cube(double sideLength) : base(sideLength, sideLength, sideLength) { }
    }

    // Cylinder
    public class Cylinder : Shape
    {
        public double Radius { get; }
        public double Height { get; }

        public Cylinder(double radius, double height)
        {
            Radius = radius;
            Height = height;
        }

        public override double SurfaceArea => 2 * Math.PI * Radius * (Radius + Height);
        public override double Volume => Math.PI * Math.Pow(Radius, 2) * Height;
    }

    // Sphere
    public class Sphere : Shape
    {
        public double Radius { get; }

        public Sphere(double radius)
        {
            Radius = radius;
        }

        public override double SurfaceArea => 4 * Math.PI * Math.Pow(Radius, 2);
        public override double Volume => (4.0 / 3.0) * Math.PI * Math.Pow(Radius, 3);
    }

    // Prism (n-gonal prism)
    public class Prism : Shape
    {
        public double SideLength { get; }
        public int Faces { get; }
        public double Height { get; }

        public Prism(double sideLength, int faces, double height)
        {
            SideLength = sideLength;
            Faces = faces;
            Height = height;
        }

        public override double SurfaceArea => Faces * SideLength * Height;
        public override double Volume => 0.5 * Faces * Math.Pow(SideLength, 2) * Height;
    }
}
