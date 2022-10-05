
namespace Supercluster.MTree.NewDesign
{
    using System.Diagnostics;

    /// <summary>
    /// Represents a node entry. All node entries should belong to a node.
    /// </summary>
    /// <typeparam name="TValue">The </typeparam>
    public class MNodeEntry<TValue>
    {
        private MNode<TValue> childNode = nu