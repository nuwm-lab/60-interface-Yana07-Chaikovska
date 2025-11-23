using System;

namespace Shapes3D
{
    // ============================================
    //                ІНТЕРФЕЙС
    // ============================================
    public interface IShape3D
    {
        void SetParameters(double[] parameters);
        void PrintParameters();
        double Volume();
    }

    // ============================================
    //         АБСТРАКТНИЙ БАЗОВИЙ КЛАС
    // ============================================
    public abstract class Solid : IShape3D
    {
        public abstract void SetParameters(double[] parameters);

        public virtual void PrintParameters()
        {
            Console.WriteLine("3D shape parameters:");
        }

        public abstract double Volume();
    }

    // ============================================
    //                   КУЛЯ
    // ============================================
    public class Sphere : Solid
    {
        private double _radius;

        public Sphere() { }

        public Sphere(double radius)
        {
            if (radius <= 0)
                throw new ArgumentException("Radius must be positive.");

            _radius = radius;
        }

        public override void SetParameters(double[] parameters)
        {
            if (parameters.Length != 1)
                throw new ArgumentException("Sphere requires 1 parameter: radius.");

            if (parameters[0] <= 0)
                throw new ArgumentException("Radius must be positive.");

            _radius = parameters[0];
        }

        public override void PrintParameters()
        {
            Console.WriteLine($"Sphere: radius = {_radius}");
        }

        public override double Volume()
        {
            return (4.0 / 3.0) * Math.PI * Math.Pow(_radius, 3);
        }
    }

    // ============================================
    //                  ЕЛІПСОЇД
    // ============================================
    public class Ellipsoid : Solid
    {
        private double _a, _b, _c;

        public Ellipsoid() { }

        public Ellipsoid(double a, double b, double c)
        {
            if (a <= 0  b <= 0  c <= 0)
                throw new ArgumentException("All axes must be positive.");

            _a = a;
            _b = b;
            _c = c;
        }

        public override void SetParameters(double[] parameters)
        {
            if (parameters.Length != 3)
                throw new ArgumentException("Ellipsoid requires 3 parameters: a, b, c.");

            if (parameters[0] <= 0  parameters[1] <= 0  parameters[2] <= 0)
                throw new ArgumentException("All axes must be positive.");

            _a = parameters[0];
            _b = parameters[1];
            _c = parameters[2];
        }

        public override void PrintParameters()
        {
            Console.WriteLine($"Ellipsoid: a = {_a}, b = {_b}, c = {_c}");
        }

        public override double Volume()
        {
            return (4.0 / 3.0) * Math.PI * _a * _b * _c;
        }
    }

    // ============================================
    //                      MAIN
    // ============================================
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Sphere ===");
            IShape3D sphere = new Sphere();
            sphere.SetParameters(new double[] { 5 });
            sphere.PrintParameters();
            Console.WriteLine($"Volume = {sphere.Volume():F4}");

            Console.WriteLine("\n=== Ellipsoid ===");
            IShape3D ellipsoid = new Ellipsoid();
            ellipsoid.SetParameters(new double[] { 2, 3, 4 });
            ellipsoid.PrintParameters();
            Console.WriteLine($"Volume = {ellipsoid.Volume():F4}");

            Console.WriteLine("\nDone.");
        }
    }
}