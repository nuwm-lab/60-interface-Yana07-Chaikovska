using System;

namespace Shapes3D
{
    // ============================================
    //                ІНТЕРФЕЙС
    // ============================================
    public interface IShape3D
    {
        void SetParameters(double bx, double by, double bz, params double[] parameters);
        void PrintParameters();
        double Volume();
    }

    // ============================================
    //          АБСТРАКТНИЙ БАЗОВИЙ КЛАС
    // ============================================
    public abstract class Solid : IShape3D
    {
        protected double _bx, _by, _bz; // центр фігури

        public Solid() { }

        public Solid(double bx, double by, double bz)
        {
            _bx = bx;
            _by = by;
            _bz = bz;
        }

        public abstract void SetParameters(double bx, double by, double bz, params double[] parameters);

        public virtual void PrintParameters()
        {
            Console.WriteLine($"Center: ({_bx}, {_by}, {_bz})");
        }

        public abstract double Volume();

        // За вимогою можна додати деструктор
        ~Solid() { }
    }

    // ============================================
    //                   КУЛЯ
    // ============================================
    public class Sphere : Solid
    {
        private double _radius;

        public Sphere() { }

        public Sphere(double bx, double by, double bz, double radius)
            : base(bx, by, bz)
        {
            if (radius <= 0)
                throw new ArgumentException("Radius must be positive.");
            _radius = radius;
        }

        public override void SetParameters(double bx, double by, double bz, params double[] parameters)
        {
            if (parameters == null || parameters.Length != 1)
                throw new ArgumentException("Sphere requires 1 parameter: radius.");

            if (parameters[0] <= 0)
                throw new ArgumentException("Radius must be positive.");

            _bx = bx;
            _by = by;
            _bz = bz;

            _radius = parameters[0];
        }

        public override void PrintParameters()
        {
            Console.WriteLine("Sphere:");
            base.PrintParameters();
            Console.WriteLine($"Radius: {_radius}");
        }

        public override double Volume()
        {
            return (4.0 / 3.0) * Math.PI * Math.Pow(_radius, 3);
        }

        ~Sphere() { }
    }

    // ============================================
    //                  ЕЛІПСОЇД
    // ============================================
    public class Ellipsoid : Solid
    {
        private double _a, _b, _c;

        public Ellipsoid() { }

        public Ellipsoid(double bx, double by, double bz,
                         double a, double b, double c)
            : base(bx, by, bz)
        {
            if (a <= 0  b <= 0  c <= 0)
                throw new ArgumentException("Axes must be positive.");

            _a = a;
            _b = b;
            _c = c;
        }

        public override void SetParameters(double bx, double by, double bz, params double[] parameters)
        {
            if (parameters == null || parameters.Length != 3)
                throw new ArgumentException("Ellipsoid requires parameters: a, b, c.");

            if (parameters[0] <= 0  parameters[1] <= 0  parameters[2] <= 0)
                throw new ArgumentException("Axes must be positive.");

            _bx = bx;
            _by = by;
            _bz = bz;

            _a = parameters[0];
            _b = parameters[1];
            _c = parameters[2];
        }

        public override void PrintParameters()
        {
            Console.WriteLine("Ellipsoid:");
            base.PrintParameters();
            Console.WriteLine($"Axes: a = {_a}, b = {_b}, c = {_c}");
        }

        public override double Volume()
        {
            return (4.0 / 3.0) * Math.PI * _a * _b * _c;
        }

        ~Ellipsoid() { }
    }

    // ============================================
    //                    MAIN// ============================================
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Sphere ===");
            IShape3D sphere = new Sphere();
            sphere.SetParameters(1, 2, 3, 5); // центр (1,2,3), радіус 5
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
