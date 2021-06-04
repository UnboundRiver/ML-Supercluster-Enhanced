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
        /// </summary>
        private T[] trainingData;

        /// <summary>
        /// An array containg the original training outputs.
        /// </summary>
        private int[] trainingOutputs;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedNearestNeighbors{T}"/> class.
        /// </summary>
        /// <param name="k">The number of neighbors during classification</param>
        /// <param name="clusters">The number of clusters (classes) that the model should have</param>
        public ExtendedNearestNeighbors(int k, int clusters)
        {
            this.Clusters = clusters;
            this.K = k;
        }

        /// <summary>
        /// The metric used to calculate distance between two datapoints.
        /// </summary>
        public Func<T, T, double> Metric { get; set; }

        private double[][] WeightedKNNDistance;
        private int[][] WeightedKNNLabels;
        private double[] ClassStatistics;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testingData"></param>
        /// <returns></returns>
        public int[] Predict(T[] testingData)
        {
            var predictedLabels = Enumerable.Repeat(-1, testingData.Length).ToArray();
            var numClass = this.trainingOutputs.Distinct().Count();
            var numTrainingEachClass = new int[numClass];

            // calculate the num training each class
            for (int i = 0; i < numClass; i++)
            {
                numTrainingEachClass[i] = this.trainingOutputs.Count(l => l == i);
            }

            // classify each Point
            for (int i = 0; i < testingData.Length; i++)
            {

                // get all distances for the Point
                var distances = new List<double[]>();
                for (int j = 0; j < this.trainingData.Length; j++)
                {
                    // TODO: Here we have the index, distance, then class label, make this more elegant
                    distances.Add(new double[] { j, this.Metric(testingData[i], this.trainingData[j]), this.trainingOutputs[j] });
                }

                // index 0: index of trainingData
                // index 1: distance between testingData and trainingData
                distances = distances.OrderBy(d => d[1]).ToList();

                // count how many training labels per class
                var hitNumKNN = new int[numClass];
                for (int j = 0; j < numClass; j++)
                {
                    hitNumKNN[j] = distances.Take(this.K).Count(d => (int)d[2] == j);
                }

                // variables defined in matlab program
                var tsEnn = new double[numClass];
                var numTrainingNN = new double[numClass];
                var num