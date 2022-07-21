using System.Collections.Generic;

namespace Supercluster.MTree.NewDesign
{
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class MNode<TValue>
    {

        /// <summary>
        /// The node entry that is the parent of this node. This property is null if this is the root node;
        /// </summary>
        public MNodeEntry<TValue> ParentEntry { get; set