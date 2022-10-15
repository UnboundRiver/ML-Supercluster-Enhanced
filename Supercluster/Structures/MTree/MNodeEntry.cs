
namespace Supercluster.MTree.NewDesign
{
    using System.Diagnostics;

    /// <summary>
    /// Represents a node entry. All node entries should belong to a node.
    /// </summary>
    /// <typeparam name="TValue">The </typeparam>
    public class MNodeEntry<TValue>
    {
        private MNode<TValue> childNode = null;

        /// <summary>
        /// The covering radius of the routing object. If the node entry is a leaf-node entry, then 