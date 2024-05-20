
# ML-Supercluster-Enhanced
An advanced .NET machine learning library focusing on [clustering](https://en.wikipedia.org/wiki/Cluster_analysis) and [metric learning](https://en.wikipedia.org/wiki/Similarity_learning).

This project, now maintained by UnboundRiver, aims to concentrate on cutting-edge algorithms, including ENN, as well as age-old classics like DBSCAN. It's under **intensive** development with **many** innovations in pipeline.

### Philosophy
Being the current steward of this project, our guidelines involve:
* **Research first:** All algorithms are homegrown. A deep understanding is crucial before initiating coding. This involves dissecting numerous research papers.
* **Correctness:** Stemming from a mathematical background, Machine Learning is just mathematics to us. Mathematics must be correct, hence, **all** deployed algorithms are thoroughly unit tested.
* **Documentation:**  Striving for comprehensive documentation is a primary goal. Although it might seem challenging at this stage, with the release 1.1, there'll be a re-documentation of the codebase.
* **Quality:** Strengthened by thorough commenting, style-cop compliance, and adherence to robust software design principles. We firmly believe quality is as significant as correctness.
* **Appropriate Efficiency:** Code optimization is performed where it **needs** to be optimized. Though C# doesn't compete with C++, an increasing number of developers are embracing high-level languages for data-science. Where optimizations can notably enhance performance (e.g., using k-d-trees, multi-threading), we optimize. When optimization complicates the code for a 1% speed increase, we don't.


###Algorithms
List of currently supported algorithms:

* [k-NN](https://en.wikipedia.org/wiki/K-nearest_neighbors_algorithm)
* [ENN](http://www.ele.uri.edu/faculty/he/PDFfiles/ENN.pdf) (1st C# implementation)
* [k-Means](https://en.wikipedia.org/wiki/K-means_clustering)

## Next Release

### Algorithms 
* [DBSCAN](https://en.wikipedia.org/wiki/DBSCAN)
* [OPTICS](https://en.wikipedia.org/wiki/OPTICS_algorithm)

### Data Structures
* ~~A KD-Tree implementation.~~ Done: https://github.com/UnboundRiver/ML-Supercluster-Enhanced.KDTree
* ~~An M-Tree implementation.~~ (1st C# implementation (That we could find.))

### Code
* Refactor of the ENN code.
* Updating kNN, kMeans and ENN to accept a Tree during construction