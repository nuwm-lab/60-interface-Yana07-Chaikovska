using System;

namespace Shapes3D
{
    // ============================
    //        ІНТЕРФЕЙС
    // ============================
    public interface IShape3D
    {
        void SetParameters(double bx, double by, double bz, params double[] values);
        double Volume();
        void PrintParameters();
    }

    // ============================
    //     АБСТРАКТНИЙ КЛАС
    // ============================
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

        public abstract void SetParameters(double bx, double by, double bz, params double[] values);
        public abstract double Volume();

        // Метод для встановлення центру (щоб уникати дублювання)
        protected void SetCenter(double bx, double by, double bz)
        {
            CenterX = bx;
            CenterY = by;
            CenterZ = bz;
        }

        public virtual void PrintParameters()
        {
            Console.WriteLine($"Center: ({CenterX}, {CenterY}, {CenterZ})");
        }
    }

    // ============================
    //           КУЛЯ
    // ============================
    public class Sphere : Solid
    {
        public double Radius { get; private set; }

        public Sphere() { }

        public Sphere(double bx, double by, double bz, double radius)
            : base(bx, by, bz)
        {
            if (radius <= 0)
                throw new ArgumentOutOfRangeException(nameof(radius), "Radius must be positive.");
            Radius = radius;
        }

        public override void SetParameters(double bx, double by, double bz, params double[] values)
        {
            if (values == null || values.Length != 1)
                throw new ArgumentException("Sphere requires 1 parameter: radius.");

            if (values[0] <= 0)
                throw new ArgumentOutOfRangeException(nameof(values), "Radius must be positive.");

            SetCenter(bx, by, bz);
            Radius = values[0];
        }

        public override double Volume()
        {
            return 4.0 / 3.0 * Math.PI * Radius * Radius * Radius; // оптимізовано без Math.Pow
        }

        public override void PrintParameters()
        {
            Console.WriteLine("Sphere:");
            base.PrintParameters();
            Console.WriteLine($"Radius: {Radius}");
        }
    }

    // ============================
    //         ЕЛІПСОЇД
    // ============================
    public class Ellipsoid : Solid
    {
        public double AxisA { get; private set; }
        public double AxisB { get; private set; }
        public double AxisC { get; private set; }

        public Ellipsoid() { }

        public Ellipsoid(double bx, double by, double bz, double a, double b, double c)
            : base(bx, by, bz)
        {
            if (a <= 0 || b <= 0 || c <= 0)
                throw new ArgumentOutOfRangeException(nameof(a), "All axes must be positive.");

            AxisA = a;
            AxisB = b;
            AxisC = c;
        }

        public override void SetParameters(double bx, double by, double bz, params double[] values)
        {
            if (values == null || values.Length != 3)
                throw new ArgumentException("Ellipsoid requires 3 parameters: a, b, c.");

            if (values[0] <= 0 || values[1] <= 0 || values[2] <= 0)
                throw new ArgumentOutOfRangeException(nameof(values), "All axes must be positive.");

            SetCenter(bx, by, bz);
            AxisA = values[0];
            AxisB = values[1];
            AxisC = values[2];
        }

        public override double Volume()
        {
            return 4.0 / 3.0 * Math.PI * AxisA * AxisB * AxisC;
        }

        public override void PrintParameters()
        {
            Console.WriteLine("Ellipsoid:");
            base.PrintParameters();
            Console.WriteLine($"Axes: A = {AxisA}, B = {AxisB}, C = {AxisC}");
        }
    }

    // ============================
    //            MAIN
    // ============================
    public static class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("=== Sphere ===");
                IShape3D sphere = new Sphere();
                sphere.SetParameters(1, 2, 3, 5);
                sphere.PrintParameters();
                Console.WriteLine($"Volume = {sphere.Volume():F4}\n");

                Console.WriteLine("=== Ellipsoid ===");
                IShape3D ellipsoid = new Ellipsoid();
                ellipsoid.SetParameters(0, 0, 0, 2, 3, 4);
                ellipsoid.PrintParameters();
                Console.WriteLine($"Volume = {ellipsoid.Volume():F4}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

