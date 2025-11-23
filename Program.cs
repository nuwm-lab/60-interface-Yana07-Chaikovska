using System;

namespace Shapes3D
{
    // ============================================
    //                ІНТЕРФЕЙС
    // ============================================
    public interface IShape3D
    {
        void SetParameters(double centerX, double centerY, double centerZ, params double[] parameters);
        void PrintParameters();
        double Volume();
    }

    // ============================================
    //          АБСТРАКТНИЙ БАЗОВИЙ КЛАС
    // ============================================
    public abstract class Solid : IShape3D
    {
        public double CenterX { get; protected set; }
        public double CenterY { get; protected set; }
        public double CenterZ { get; protected set; }

        protected Solid() { }

        protected Solid(double centerX, double centerY, double centerZ)
        {
            CenterX = centerX;
            CenterY = centerY;
            CenterZ = centerZ;
        }

        public abstract void SetParameters(double centerX, double centerY, double centerZ, params double[] parameters);

        public virtual void PrintParameters()
        {
            Console.WriteLine($"Center: ({CenterX}, {CenterY}, {CenterZ})");
        }

        public abstract double Volume();
    }

    // ============================================
    //                   КУЛЯ
    // ============================================
    public class Sphere : Solid
    {
        public double Radius { get; private set; }

        public Sphere() { }

        public Sphere(double centerX, double centerY, double centerZ, double radius)
            : base(centerX, centerY, centerZ)
        {
            if (radius <= 0)
                throw new ArgumentOutOfRangeException(nameof(radius), "Radius must be positive.");

            Radius = radius;
        }

        public override void SetParameters(double centerX, double centerY, double centerZ, params double[] parameters)
        {
            if (parameters == null || parameters.Length != 1)
                throw new ArgumentException("Sphere requires 1 parameter: radius.");

            if (parameters[0] <= 0)
                throw new ArgumentOutOfRangeException(nameof(parameters), "Radius must be positive.");

            CenterX = centerX;
            CenterY = centerY;
            CenterZ = centerZ;

            Radius = parameters[0];
        }

        public override void PrintParameters()
        {
            Console.WriteLine("Sphere:");
            base.PrintParameters();
            Console.WriteLine($"Radius: {Radius}");
        }

        public override double Volume()
        {
            return 4.0 / 3.0 * Math.PI * Math.Pow(Radius, 3);
        }
    }

    // ============================================
    //                  ЕЛІПСОЇД
    // ============================================
    public class Ellipsoid : Solid
    {
        public double A { get; private set; }
        public double B { get; private set; }
        public double C { get; private set; }

        public Ellipsoid() { }

        public Ellipsoid(double centerX, double centerY, double centerZ,
                         double a, double b, double c)
            : base(centerX, centerY, centerZ)
        {
            if (a <= 0  b <= 0  c <= 0)
                throw new ArgumentOutOfRangeException(nameof(a), "Axes must be positive.");

            A = a;
            B = b;
            C = c;
        }

        public override void SetParameters(double centerX, double centerY, double centerZ, params double[] parameters)
        {
            if (parameters == null || parameters.Length != 3)
                throw new ArgumentException("Ellipsoid requires 3 parameters: a, b, c.");

            if (parameters[0] <= 0  parameters[1] <= 0  parameters[2] <= 0)
                throw new ArgumentOutOfRangeException(nameof(parameters), "Axes must be positive.");

            CenterX = centerX;
            CenterY = centerY;
            CenterZ = centerZ;

            A = parameters[0];
            B = parameters[1];
            C = parameters[2];}

        public override void PrintParameters()
        {
            Console.WriteLine("Ellipsoid:");
            base.PrintParameters();
            Console.WriteLine($"Axes: a = {A}, b = {B}, c = {C}");
        }

        public override double Volume()
        {
            return 4.0 / 3.0 * Math.PI * A * B * C;
        }
    }

    // ============================================
    //                    MAIN
    // ============================================
    public static class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Sphere ===");
            IShape3D sphere = new Sphere();
            sphere.SetParameters(1, 2, 3, 5); // центр (1,2,3), R = 5
            sphere.PrintParameters();
            Console.WriteLine($"Volume = {sphere.Volume():F4}");

            Console.WriteLine("\n=== Ellipsoid ===");
            IShape3D ellipsoid = new Ellipsoid();
            ellipsoid.SetParameters(0, 0, 0, 2, 3, 4); // центр (0,0,0), осі 2,3,4
            ellipsoid.PrintParameters();
            Console.WriteLine($"Volume = {ellipsoid.Volume():F4}");

            Console.WriteLine("\nDone.");
        }
    }
}
