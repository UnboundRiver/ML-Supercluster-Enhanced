namespace Supercluster.Classification
{
    /// <summary>
    /// Interface for supervised classifiers.
    /// </summary>
    /// <typeparam name="T">The type of data the algorithm performs classification on.</typeparam>
    public interface ISupervisedClassifier<in T>
    {
        /// <summary>
        /// Performs classification of a single Point.
        /// </summary>
        /// <param name="dataPoint">Datum to be classified.</param>
        /// <retur