
﻿namespace Supercluster.Structures.MTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Supercluster.MTree;
    using Supercluster.MTree.NewDesign;
    using Supercluster.Structures;
    using Supercluster.Structures.Interfaces;
    using Supercluster.Structures.KDTree;

    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    /// 
    /// MTrees have a number of properties that make them very attractive for use with large high dimensional data sets.
    /// 
    /// <list>
    /// They are self balancing.
    /// They do not require the items they index have a coordinate representation such as the <see cref="KDTree"/>.
    /// They handle high-dimensional data quite well as they index new items using the relative distance to other items already in the tree.
    /// This does not mean that MTrees are immune to the curse of dimensionality but they can efficiently handled data with 1000's of dimensions.
    /// 
    /// Draw Backs:
    /// The MTree makes explicit use of properties of Metric Spaces. If your data set cannot be appropriately embedded into a metric space you cannot use an MTree.
    /// For low dimension data sets with a coordinate representation, an MTree will be much slower than a KDTree (but still faster than a linear search).
    /// 
    /// MTrees are not easy to implement and their heavy reliance on Metric Space properties makes them quite difficult (if even possible) to modify for a non-metric space application.
    /// 