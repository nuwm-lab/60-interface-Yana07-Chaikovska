using System;

namespace AbstractsANDInterfaces
{
    // --------------------------
    //     ІНТЕРФЕЙС
    // --------------------------
    public interface IFractionFunction
    {
        void SetCoefficients(double[] numerator, double[] denominator);
        void PrintCoefficients();
        double ValueAt(double x);
    }

    // ---------------------------------------
    //   АБСТРАКТНИЙ КЛАС ДРОБОВО-ЛІНІЙНОЇ ФУНКЦІЇ
    // ---------------------------------------
    public abstract class FractionLinear : IFractionFunction
    {
        protected double a1, a0;
        protected double b1, b0;

        public FractionLinear() { }

        public abstract void SetCoefficients(double[] numerator, double[] denominator);

        public virtual void PrintCoefficients()
        {
            Console.WriteLine($"Numerator:   {a1} * x + {a0}");
            Console.WriteLine($"Denominator: {b1} * x + {b0}");
        }

        public virtual double ValueAt(double x)
        {
            double numerator = a1 * x + a0;
            double denominator = b1 * x + b0;

            if (denominator == 0)
                throw new DivideByZeroException("Denominator equals zero!");

            return numerator / denominator;
        }
    }

    // ---------------------------------------------------
    //      ПОХІДНИЙ КЛАС — ДРОБОВА ФУНКЦІЯ 2-ГО ПОРЯДКУ
    // ---------------------------------------------------
    public class FractionQuadratic : FractionLinear
    {
        protected double a2;
        protected double b2;

        public FractionQuadratic() { }

        public override void SetCoefficients(double[] numerator, double[] denominator)
        {
            a2 = numerator[0];
            a1 = numerator[1];
            a0 = numerator[2];

            b2 = denominator[0];
            b1 = denominator[1];
            b0 = denominator[2];
        }

        public override void PrintCoefficients()
        {
            Console.WriteLine($"Numerator:   {a2} * x^2 + {a1} * x + {a0}");
            Console.WriteLine($"Denominator: {b2} * x^2 + {b1} * x + {b0}");
        }

        public override double ValueAt(double x)
        {
            double numerator = a2 * x * x + a1 * x + a0;
            double denominator = b2 * x * x + b1 * x + b0;

            if (denominator == 0)
                throw new DivideByZeroException("Denominator equals zero!");

            return numerator / denominator;
        }
    }

    // --------------------------
    //            MAIN
    // --------------------------
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Fraction Linear Function ===");

            FractionLinear f = new FractionLinearImpl();
            f.SetCoefficients(
                new double[] { 2, -5 },   // 2x - 5
                new double[] { 1, 3 }     // 1x + 3
            );
            f.PrintCoefficients();
            Console.WriteLine("Value at x0 = 2: " + f.ValueAt(2));

            Console.WriteLine("\n=== Fraction Quadratic Function ===");

            FractionQuadratic fq = new FractionQuadratic();
            fq.SetCoefficients(
                new double[] { 1, -3, 2 },     // x² - 3x + 2
                new double[] { 2, 0, -1 }      // 2x² - 1
            );
            fq.PrintCoefficients();
            Console.WriteLine("Value at x0 = 2: " + fq.ValueAt(2));
        }
    }

    // Реалізація абстрактного класу (для демонстрації)
    public class FractionLinearImpl : FractionLinear
    {
        public override void SetCoefficients(double[] numerator, double[] denominator)
        {
            a1 = numerator[0];
            a0 = numerator[1];

            b1 = denominator[0];
            b0 = denominator[1];
        }
    }
}
