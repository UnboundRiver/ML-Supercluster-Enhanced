# Supercluster.KDTree
This is a [KD-Tree](https://en.wikipedia.org/wiki/K-d_tree) written completely in C#.  This project originally 
started as a fork of the KD-Tree Written by [CodeandCats](https://github.com/codeandcats), however, the 
structure and intention of this project has changed drastically from its origin. 

## About the Project

This is a KD-Tree that is optimized for machine learning applications, **however, it can used for less intensive purposes as well**. (In fact, I am writing this for my machine learning library [Supercluster](https://github.com/MathFerret1013/Supercluster)) In machine learning data-sets
 are often built, re-built, and built again. Also, in machine learning, algorithms need to be **fast** 
for look ups but it is more acceptable to be slow for construction.
Thus the tree bas been designed with this philosophy in mind. General characteristics are:
* **The a nuget package is available.** just run `Install-Package Supercluster.KDTree` to install it.
* **The code is unit tested and well documented.** Style-cop, unit-test, wiki tutorials and MSDN style docs. It's all here.
* **The tree is extremely fast for search.** 
    * For a 10,000 3-nearest-neighbor searches on a 1,000,000 node 2-Dimensional tree using floats is about 7.5 times than the KD-Tree 
by [CodeandCats](https://github.com/codeandcats) (number based off of a 1,000,000 sample
independent T-test for mean comparisons, equal variance assumed). 
    * The near