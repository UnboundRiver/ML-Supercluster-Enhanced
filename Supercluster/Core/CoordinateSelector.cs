﻿namespace Supercluster.Core
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// A static utility class which provides methods for creating a Coordinate Selector.
    /// </summary>
    /// <remarks>
    /// A "Coordinate Selector" is a <see cref="Func{T,TResult}"/> which accepts any type of object and returns and array of doubles.
    /// This double array is the "coordinate" for the given instance of T. Coordinate selectors are used which an algorithm or data-structure
    /// requires a specific coordinate representation for an instance of a generic type.
    /// </remarks>
    public static class Coordinat