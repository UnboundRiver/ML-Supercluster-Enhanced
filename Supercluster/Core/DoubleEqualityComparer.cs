namespace Supercluster
{
    using System;
    using System.Collections.Generic;

    using Supercluster.Core;

    public class DoubleEqualityComparer : IEqualityComparer<double>
    {
        public bool Equals(double x, double y)
        {
            return Math.Abs(x - y) <= 1E-13;
        }

        public int GetHashCode(double obj)
        {
            return base.GetHashCode();
        }
    }

    public class DoubleArrayEqualityComparer : IEqualityComparer<double[]>