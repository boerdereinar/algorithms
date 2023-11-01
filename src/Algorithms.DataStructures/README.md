# Data Structures
This library contains implementations of various different data structures.
The full list of implemented data structures can be found below.

## Data Structures
### Heaps
| Name                                                    |     Min     |         Delete         |        Insert         |
|---------------------------------------------------------|:-----------:|:----------------------:|:---------------------:|
| [Binary](https://en.wikipedia.org/wiki/Binary_heap)     | $\Theta(1)$ |    $\Theta(\log n)$    | $\mathcal O(\log n)$  |
| [Binomial](https://en.wikipedia.org/wiki/Binomial_heap) | $\Theta(1)$ |    $\Theta(\log n)$    |      $\Theta(1)$      |
| [D-heap](https://en.wikipedia.org/wiki/D-ary_heap)      | $\Theta(1)$ | $d\mathcal O(\log_dn)$ | $\mathcal O(\log_dn)$ |
| [Leftist](https://en.wikipedia.org/wiki/Leftist_tree)   | $\Theta(1)$ |    $\Theta(\log n)$    |   $\Theta(\log n)$    |
| [Weak](https://en.wikipedia.org/wiki/Weak_heap)         | $\Theta(1)$ |  $\mathcal O(\log n)$  | $\mathcal O(\log n)$  |

### Trees
| Name                                                              | Traversable |
|-------------------------------------------------------------------|:-----------:|
| [Binary](https://en.wikipedia.org/wiki/Binary_search_tree)        |   `true`    |
| [Binomial](https://en.wikipedia.org/wiki/Binomial_heap)           |   `false`   |
| [Cartesian](https://en.wikipedia.org/wiki/Cartesian_tree)         |   `true`    |
| [Leftist](https://en.wikipedia.org/wiki/Leftist_tree)             |   `false`   |
| [Red-Black](https://en.wikipedia.org/wiki/Red%E2%80%93black_tree) |   `true`    |
| [Splay](https://en.wikipedia.org/wiki/Splay_tree)                 |   `true`    |

A tree is traversable if it can be traversed such that the resulting sequence is sorted.
