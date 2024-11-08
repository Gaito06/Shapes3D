using System;

namespace Shapes3D
{
    public abstract class Shape3D
    {
        // Abstract base class for all 3D shapes
    }

    public interface IShapeWithArea
    {
        double GetArea();
    }

    public interface IShapeWithVolume
    {
        double GetVolume();
    }

    public class Cube : Cuboid
    {
        public Cube(double sideLength) : base(sideLength, sideLength, sideLength) { }
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

        public double GetArea() => 2 * (Width * Height + Height * Depth + Depth * Width);
        public double GetVolume() => Width * Height * Depth;
    }

    public class Sphere : Shape3D, IShapeWithArea, IShapeWithVolume
    {
        public double Radius { get; }

        public Sphere(double radius)
        {
            Radius = radius;
        }

        public double GetArea() => 4 * Math.PI * Math.Pow(Radius, 2);
        public double GetVolume() => (4.0 / 3.0) * Math.PI * Math.Pow(Radius, 3);
    }

    public class Cylinder : Shape3D, IShapeWithArea, IShapeWithVolume
    {
        public double Radius { get; }
        public double Height { get; }

        public Cylinder(double radius, double height)
        {
            Radius = radius;
            Height = height;
        }

        public double GetArea() => 2 * Math.PI * Radius * (Radius + Height);
        public double GetVolume() => Math.PI * Math.Pow(Radius, 2) * Height;
    }

    public class Prism : Shape3D, IShapeWithArea, IShapeWithVolume
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

        public double GetArea() => Faces * SideLength * Height;
        public double GetVolume() => 0.5 * Faces * Math.Pow(SideLength, 2) * Height;
    }
}
