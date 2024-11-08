using System;

namespace Shapes3D
{
    // Abstract base class for all 3D shapes
    public abstract class Shape3D
    {
        // No need to implement area or volume here, only common properties or methods can go
    }

    // Interface for shapes that have an area
    public interface IShapeWithArea
    {
        double GetArea();
    }

    // Interface for shapes that have a volume
    public interface IShapeWithVolume
    {
        double GetVolume();
    }

    // Cuboid (rectangular prism)
    public class Cuboid : Shape3D, IShapeWithArea, IShapeWithVolume
    {
        public double Width { get; }
        public double Height { get; }
        public double Depth { get; }
        
        // Constructor initializes the dimensions of the cuboid
        public Cuboid(double width, double height, double depth)
        {
            Width = width;
            Height = height;
            Depth = depth;
        }

        // Calculate and return the surface area
        public double GetArea()
        {
            return 2 * (Width * Height + Height * Depth + Depth * Width); // Surface area formula
        }

        // Calculate and return the volume
        public double GetVolume()
        {
            return Width * Height * Depth; // Volume formula
        }
    }

    // Cube is a special case of Cuboid (sideLength = width = height = depth)
    public class Cube : Cuboid
    {
        public Cube(double sideLength) : base(sideLength, sideLength, sideLength) { }
    }

    // Cylinder
    public class Cylinder : Shape3D, IShapeWithArea, IShapeWithVolume
    {
        public double Radius { get; }
        public double Height { get; }

        // Constructor initializes the radius and height
        public Cylinder(double radius, double height)
        {
            Radius = radius;
            Height = height;
        }

        // Calculate and return the surface area
        public double GetArea()
        {
            return 2 * Math.PI * Radius * (Radius + Height); // Surface area of a cylinder
        }

        // Calculate and return the volume
        public double GetVolume()
        {
            return Math.PI * Math.Pow(Radius, 2) * Height; // Volume of a cylinder
        }
    }

    // Sphere
    public class Sphere : Shape3D, IShapeWithArea, IShapeWithVolume
    {
        public double Radius { get; }

        // Constructor initializes the radius
        public Sphere(double radius)
        {
            Radius = radius;
        }

        // Calculate and return the surface area
        public double GetArea()
        {
            return 4 * Math.PI * Math.Pow(Radius, 2); // Surface area of a sphere
        }

        // Calculate and return the volume
        public double GetVolume()
        {
            return (4.0 / 3.0) * Math.PI * Math.Pow(Radius, 3); // Volume of a sphere
        }
    }

    // Prism (n-gonal prism)
    public class Prism : Shape3D, IShapeWithArea, IShapeWithVolume
    {
        public double SideLength { get; }
        public int Faces { get; }
        public double Height { get; }
        public double SurfaceArea { get; }
        public double Volume { get; }

        // Constructor initializes side length, number of faces, and height of the prism
        public Prism(double sideLength, int faces, double height)
        {
            SideLength = sideLength;
            Faces = faces;
            Height = height;

            // Eager-load surface area and volume
            SurfaceArea = Faces * SideLength * Height; // Surface area of the prism
            Volume = 0.5 * Faces * Math.Pow(SideLength, 2) * Height; // Volume of the prism
        }

        // Return the already calculated surface area
        public double GetArea()
        {
            return SurfaceArea;
        }

        // Return the already calculated volume
        public double GetVolume()
        {
            return Volume;
        }
    }
}
