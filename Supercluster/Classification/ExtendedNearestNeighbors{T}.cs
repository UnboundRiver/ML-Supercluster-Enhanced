namespace Supercluster.Classification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// The Extended Nearest Neighbors learning algorithm is a variation of the kNN learning algorithm which classification decisions are made in such a way that they maximize intra-class coherence.
    /// Bo Tang; Haibo He, "ENN: Extended Nearest Neighbor Method for Pattern Recognition [Research Frontier]," in Computational Intelligence Magazine, IEEE , vol.10, no.3, pp.52-60, Aug. 2015 doi: 10.1109/MCI.2015.2437512
    /// </summary>
    /// <typeparam name="T">The data-type of the data points to be classified.</typeparam>
    public class ExtendedNearestNeighbors<T> : ISupervisedClassifier<T>
    {
        /// <summary>
        /// An array containing the original training data.
   