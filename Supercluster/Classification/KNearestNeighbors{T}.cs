namespace Supercluster.Classification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Supercluster.Structures;
    using Supercluster.Structures.Interfaces;

    /// <summary>
    /// kNN Learning Algorithm.
    /// </summary>
    /// <remarks>
    /// 
    /// <h2>Introduction</h2>
    /// <para> The <i>k</i> nearest neighbors learning algorithm (kNN) is a classification algorithm. To determine class membership of a unseen input the algorithm computes the <i>k</i> nearest neighbors in the learned feature space 
    /// and then chooses the class which is most common among the <i>k</i> neighbors.</para>
    /// 
    /// <para> The picture below is of the output of the kNN algorithm k=3 in a 2-dimensional feature space with 2 classes. The blue and red dots are point belonging to class 1 and class 2 which were learned during model training.
    ///