
﻿// <copyright file="HyperRect.cs" company="Eric Regina">
// Copyright (c) Eric Regina. All rights reserved.
// </copyright>

namespace Supercluster.DataStructures.KDTree
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents a hyper-rectangle. An N-Dimensional rectangle.
    /// </summary>
    /// <typeparam name="T">The type of "dimension" in the metric space in which the hyper-rectangle lives.</typeparam>
    public struct HyperRect<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Backing field for the <see cref="MinPoint"/> property.
        /// </summary>
        private T[] minPoint;

        /// <summary>
        /// Backing field for the <see cref="MaxPoint"/> property.
        /// </summary>
        private T[] maxPoint;

        /// <summary>
        /// The minimum Point of the hyper-rectangle. One can think of this Point as the
        /// bottom-left Point of a 2-Dimensional rectangle.
        /// </summary>
        public T[] MinPoint
        {
            get
            {
                return this.minPoint;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                this.minPoint = new T[value.Length];
                value.CopyTo(this.minPoint, 0);