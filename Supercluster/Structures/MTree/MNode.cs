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
        public MNodeEntry<TValue> ParentEntry { get; set; } = null;

        /// <summary>
        /// The entries of the node.
        /// </summary>
        public List<MNodeEntry<TValue>> Entries
        {
            get
            {
                return this.entries;
            }

            set
            {
                this.entries = value;
                foreach (var entry in this.entries)
                {
                    entry.EnclosingNode = this;
                }
            }
        } // TODO: Should probably not have setter

        /// <summary>
        /// 
        /// 
        /// Note: We assume that the <see cref="MNodeEntry{TValue}"/> DistanceFromParent
        /// and Value properties have been set.
        /// </summary>
        /// <param name="newEntry"></param>
        public void Add(MNodeEntry<TValue> newEntry)
        {
            newEntry.EnclosingNode = this;
            this.Entries.Add(newEntry);
        }

        public void AddRange(IEnumerable<MNodeEntry<TValue>> newEntries)
        {
            foreach (var mNodeEntry in newEntries)
            {
                this.A