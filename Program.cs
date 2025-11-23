using System;

namespace Shapes3D
{
    // ============================
    //        ІНТЕРФЕЙС
    // ============================
    public interface IShape3D
    {
        void SetParameters(double bx, double by, double bz, params double[] parameters);
        void PrintParameters();
        double Volume();
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

        public abstract void SetParameters(double bx, double by, double bz, params double[] parameters);
        public abstract double Volume();

        public virtual void PrintParameters()
        {
            Console.WriteLine($"Center: ({Bx}, {By}, {Bz})");
        }

        ~Solid() { } // деструктор, якщо він потрібен за вимогою викладача
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

        public override void SetParameters(double bx, double by, double bz, params double[] parameters)
        {
            if (parameters == null || parameters.Length != 1)
                throw new ArgumentException("Sphere requires 1 parameter: radius.");

            if (parameters[0] <= 0)
                throw new ArgumentOutOfRangeException(nameof(parameters), "Radius must be positive.");

            Bx = bx;
            By = by;
            Bz = bz;
            Radius = parameters[0];
        }

        public override double Volume()
        {
            return 4.0 / 3.0 * Math.PI * Radius * Radius * Radius;
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
        public double BxAxis { get; private set; }
        public double C { get; private set; }

        public Ellipsoid() { }

        public Ellipsoid(double bx, double by, double bz, double a, double b, double c)
            : base(bx, by, bz)
        {
            if (a <= 0  b <= 0  c <= 0)
                throw new ArgumentOutOfRangeException(nameof(a), "All axes must be positive.");

            A = a;
            BxAxis = b;
            C = c;
        }

        public override void SetParameters(double bx, double by, double bz, params double[] parameters)
        {
            if (parameters == null || parameters.Length != 3)
                throw new ArgumentException("Ellipsoid requires 3 parameters: a, b, c.");

            if (parameters[0] <= 0  parameters[1] <= 0  parameters[2] <= 0)
                throw new ArgumentOutOfRangeException(nameof(parameters), "All axes must be positive.");

            Bx = bx;
            By = by;
            Bz = bz;

            A = parameters[0];
            BxAxis = parameters[1];
            C = parameters[2];
        }

        public override double Volume()
        {
            return 4.0 / 3.0 * Math.PI * A * BxAxis * C;
        }

        public override void PrintParameters()
        {
            Console.WriteLine("Ellipsoid:");
            base.PrintParameters();
            Console.WriteLine($"Axes: a = {A}, b = {BxAxis}, c = {C}");
        }

        ~Ellipsoid() { }
    }

    // ============================
    //            MAIN
    // ============================
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
            IShape3D ellipsoid = new Ellipsoid();
            ellipsoid.SetParameters(0, 0, 0, 2, 3, 4);
            ellipsoid.PrintParameters();
            Console.WriteLine($"Volume = {ellipsoid.Volume():F4}\n");
        }
    }
}
