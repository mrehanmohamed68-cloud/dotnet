using System;

namespace day9_csharp
{
    public class ComplexNumber
    {
        public int Real { get; set; }
        public int Imag { get; set; }

        public override string ToString() => $"{Real} + {Imag}i";

        public static ComplexNumber operator +(ComplexNumber left, ComplexNumber right)
        {
            return new ComplexNumber
            {
                Real = (left?.Real ?? 0) + (right?.Real ?? 0),
                Imag = (left?.Imag ?? 0) + (right?.Imag ?? 0)
            };
        }

        // Multiplication: (a + bi) * (c + di) = (ac - bd) + (ad + bc)i
        public static ComplexNumber operator *(ComplexNumber left, ComplexNumber right)
        {
            int a = left?.Real ?? 0, b = left?.Imag ?? 0;
            int c = right?.Real ?? 0, d = right?.Imag ?? 0;

            return new ComplexNumber
            {
                Real = (a * c) - (b * d),
                Imag = (a * d) + (b * c)
            };
        }
    }
}
