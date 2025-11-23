using System;

namespace Shapes
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
        public double Bx { get; protected set; }
        public double By { get; protected set; }
        public double Bz { get; protected set; }

        protected Solid() { }

        protected Solid(double bx, double by, double bz)
        {
            Bx = bx;
            By = by;
            Bz = bz;
        }

        public abstract void SetParameters(double bx, double by, double bz, params double[] values);
        public abstract double Volume();

        public virtual void PrintParameters()
        {
            Console.WriteLine($"Center: ({Bx}, {By}, {Bz})");
        }

        // деструктор (для виконання вимоги)
        ~Solid() { }
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
                throw new ArgumentException("Sphere requires one parameter: radius.");

            if (values[0] <= 0)
                throw new ArgumentOutOfRangeException(nameof(values), "Radius must be positive.");

            Bx = bx;
            By = by;
            Bz = bz;

            Radius = values[0];
        }

        public override double Volume()
        {
            return 4.0 / 3.0 * Math.PI * Math.Pow(Radius, 3);
        }

        public override void PrintParameters()
        {
            Console.WriteLine("Sphere:");
            base.PrintParameters();
            Console.WriteLine($"Radius: {Radius}");
        }

        ~Sphere() { }
    }

    // ============================
    //         ЕЛІПСОЇД
    // ============================
    public class Ellipsoid : Solid
    {
        public double A { get; private set; }
        public double B { get; private set; }
        public double C { get; private set; }

        public Ellipsoid() { }

        public Ellipsoid(double bx, double by, double bz, double a, double b, double c)
            : base(bx, by, bz)
        {
            if (a <= 0  b <= 0  c <= 0)
                throw new ArgumentOutOfRangeException(nameof(a), "Axes must be positive.");

            A = a;  
            B = b;
            C = c;
        }

        public override void SetParameters(double bx, double by, double bz, params double[] values)
        {
            if (values == null || values.Length != 3)
                throw new ArgumentException("Ellipsoid requires 3 parameters: a, b, c.");

            if (values[0] <= 0  values[1] <= 0  values[2] <= 0)
                throw new ArgumentOutOfRangeException(nameof(values), "Axes must be positive.");

            Bx = bx;
            By = by;
            Bz = bz;

            A = values[0];
            B = values[1];
            C = values[2];
        }

        public override double Volume()
        {
            return 4.0 / 3.0 * Math.PI * A * B * C;
        }

        public override void PrintParameters()
        {
            Console.WriteLine("Ellipsoid:");
            base.PrintParameters();
            Console.WriteLine($"Axes: a={A}, b={B}, c={C}");
        }

        ~Ellipsoid() { }
    }

    // ============================
    //            MAIN// ============================
    public static class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Sphere ===");
            IShape3D sphere = new Sphere();
            sphere.SetParameters(1, 2, 3, 5);
            sphere.PrintParameters();
            Console.WriteLine($"Volume = {sphere.Volume():F4}\n");

            Console.WriteLine("=== Ellipsoid ===");
            IShape3D ellipse = new Ellipsoid();
            ellipse.SetParameters(0, 0, 0, 2, 3, 4);
            ellipse.PrintParameters();
            Console.WriteLine($"Volume = {ellipse.Volume():F4}\n");
        }
    }
}
