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
    ///  The green dots are new point which were classified as class 1 (meaning at least 2 of their nearest neighbors are in class 1). The yellow dots are new point which were classified as class 2 (meaning atleast 2 of their nearest neighbors are in class 2).</para>
    ///  <img src="..\..\media\knn_example.png" />
    /// 
    /// <h3>Advantages</h3>
    /// 
    /// <ul style="list-style-type:square">
    ///  <li>The kNN learning algorithm is perhaps the simplest learning algorithm. It is extremely easy to implement, and very easy to understand even for one who is a complete novice to machine learning.</li>
    ///  <li>When the kNN is used with a metric that is fast to compute (such as the <a href="https://en.wikipedia.org/wiki/Euclidean_distance">Euclidean Metric</a>) the model may usually be trained very quickly compared to more complex learning algorithms.</li>
    ///</ul>
    ///     
    /// <h3>Considerations</h3>
    /// <ul style="list-style-type:square">
    /// <li>To prevent ties during the classification step one should use an odd value of <i>k</i> if there is an even number of classes.</li>
    /// <li>The number of possible classification outputs must be known when using a kNN as it is a <a href="https://en.wikipedia.org/wiki/Supervised_learning">supervised learning algorithm</a>.</li>
    /// <li>The kNN learning algorithm essentially <i>memorizes</i> all supervised examples, thus very large example datasets may require much of memory.</li>
    /// <li>kNN performs very poorly when the training data has noisy or irre