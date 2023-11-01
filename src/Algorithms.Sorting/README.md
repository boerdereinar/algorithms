# Sorting Algorithms
This library contains implementations of various different sorting algorithms.
The full list of implemented sorting algorithms can be found below.

## Usage
This library provides extension methods for sorting elements in a sequence. The syntax uses similar syntax to LINQ.

In order to sort the the following collection you can use two different methods.
```csharp
var x = new int[] { 5, 3, 1, 2, 4 };
```

Method 1: Using a type parameter.
```csharp
x.Order<int, QuickSort<int>>();
```

Method 2: Using an instance of the sorting algorithm.
```csharp
x.Order(new QuickSort<int>());
```

Both of these methods are implemented for `Order`, `OrderBy`, `OrderDescending`, and `OrderDescending` and their
overloads and return an `IOrderedEnumerable<T>` such that sorting algorithms, that support sorting by multiple keys,
can use `ThenBy` to perform subsequent ordering of elements.
```csharp
// First orders by parity then by ascending order
x.OrderBy(x => x % 2, new QuickSort<int>()).ThenBy(x => x);
```

## Algorithms
### Concurrent Sorts
| Name                                                                                             |          Best          |        Average         |         Worst          |
|--------------------------------------------------------------------------------------------------|:----------------------:|:----------------------:|:----------------------:|
| [Batcher Odd-Even Merge Sort](https://en.wikipedia.org/wiki/Batcher_odd%E2%80%93even_mergesort)  | $\mathcal O(\log^2 n)$ | $\mathcal O(\log^2 n)$ | $\mathcal O(\log^2 n)$ |
| [Bitonic Sort](https://en.wikipedia.org/wiki/Bitonic_sorter)                                     | $\mathcal O(\log^2 n)$ | $\mathcal O(\log^2 n)$ | $\mathcal O(\log^2 n)$ |
| [Parallel Merge Sort](https://en.wikipedia.org/wiki/Merge_sort#Merge_sort_with_parallel_merging) | $\mathcal O(\log^3 n)$ | $\mathcal O(\log^3 n)$ | $\mathcal O(\log^3 n)$ |
| Parallel [Quicksort](https://en.wikipedia.org/wiki/Quicksort)                                    | $\mathcal O(n\log n)$  | $\mathcal O(n\log n)$  |   $\mathcal O(n^2)$    |

### Distribution Sorts
| Name                                                                                           |      Best       |             Average             |              Worst              |
|------------------------------------------------------------------------------------------------|:---------------:|:-------------------------------:|:-------------------------------:|
| [American Flag Sort](https://en.wikipedia.org/wiki/American_flag_sort)                         |       $-$       | $\mathcal O(n\cdot\frac{k}{d})$ | $\mathcal O(n\cdot\frac{k}{d})$ |
| [Bucket Sort](https://en.wikipedia.org/wiki/Bucket_sort)                                       |       $-$       |       $\mathcal O(n + r)$       |       $\mathcal O(n + r)$       |
| [Burstsort](https://en.wikipedia.org/wiki/Burstsort)                                           |       $-$       | $\mathcal O(n\cdot\frac{k}{d})$ | $\mathcal O(n\cdot\frac{k}{d})$ |
| [Counting Sort](https://en.wikipedia.org/wiki/Counting_sort)                                   |       $-$       |       $\mathcal O(n + r)$       |       $\mathcal O(n + r)$       |
| [Flashsort](https://en.wikipedia.org/wiki/Flashsort)                                           | $\mathcal O(n)$ |       $\mathcal O(n + r)$       |       $\mathcal O(n + r)$       |
| [LSD Radix Sort](https://en.wikipedia.org/wiki/Radix_sort#Least_significant_digit_radix_sorts) | $\mathcal O(n)$ | $\mathcal O(n\cdot\frac{k}{d})$ | $\mathcal O(n\cdot\frac{k}{d})$ |
| [MSD Radix Sort](https://en.wikipedia.org/wiki/Radix_sort#Most_significant_digit_radix_sorts)  |       $-$       | $\mathcal O(n\cdot\frac{k}{d})$ | $\mathcal O(n\cdot\frac{k}{d})$ |
| [Pigeonhole Sort](https://en.wikipedia.org/wiki/Pigeonhole_sort)                               |       $-$       |      $\mathcal O(n + 2^k)$      |      $\mathcal O(n + 2^k)$      |
| [Proxmap Sort](https://en.wikipedia.org/wiki/Proxmap_sort)                                     | $\mathcal O(n)$ |         $\mathcal O(n)$         |        $\mathcal O(n^2)$        |

### Exchange Sorts
| Name                                                                           |         Best          |        Average        |       Worst       |
|--------------------------------------------------------------------------------|:---------------------:|:---------------------:|:-----------------:|
| [Bubble Sort](https://en.wikipedia.org/wiki/Bubble_sort)                       |    $\mathcal O(n)$    |   $\mathcal O(n^2)$   | $\mathcal O(n^2)$ |
| [Cocktail Shaker Sort](https://en.wikipedia.org/wiki/Cocktail_shaker_sort)     |    $\mathcal O(n)$    |   $\mathcal O(n^2)$   | $\mathcal O(n^2)$ |
| [Comb Sort](https://en.wikipedia.org/wiki/Comb_sort)                           | $\mathcal O(n\log n)$ |   $\mathcal O(n^2)$   | $\mathcal O(n^2)$ |
| [Dual-Pivot Quicksort](https://arxiv.org/abs/1503.08498)                       | $\mathcal O(\log n)$  | $\mathcal O(\log n)$  | $\mathcal O(n^2)$ |
| [Exchange Sort](https://en.wikipedia.org/wiki/Sorting_algorithm#Exchange_sort) |   $\mathcal O(n^2)$   |   $\mathcal O(n^2)$   | $\mathcal O(n^2)$ |
| [Gnome Sort](https://en.wikipedia.org/wiki/Gnome_sort)                         |    $\mathcal O(n)$    |   $\mathcal O(n^2)$   | $\mathcal O(n^2)$ |
| [Odd-even Sort](https://en.wikipedia.org/wiki/Odd%E2%80%93even_sort)           |    $\mathcal O(n)$    |   $\mathcal O(n^2)$   | $\mathcal O(n^2)$ |
| [Quicksort](https://en.wikipedia.org/wiki/Quicksort)                           | $\mathcal O(n\log n)$ | $\mathcal O(n\log n)$ | $\mathcal O(n^2)$ |

### Hybrid Sorts
| Name                                                                     |         Best          |        Average        |         Worst         |
|--------------------------------------------------------------------------|:---------------------:|:---------------------:|:---------------------:|
| [Introsort](https://en.wikipedia.org/wiki/Introsort)                     | $\mathcal O(n\log n)$ | $\mathcal O(n\log n)$ | $\mathcal O(n\log n)$ |
| [Multi-key Quicksort](https://en.wikipedia.org/wiki/Multi-key_quicksort) |          $-$          |          $-$          |          $-$          |
| [Timsort](https://en.wikipedia.org/wiki/Timsort)                         |    $\mathcal O(n)$    | $\mathcal O(n\log n)$ | $\mathcal O(n\log n)$ |

### Insertion Sorts
| Name                                                                           |         Best          |           Average           |            Worst            |
|--------------------------------------------------------------------------------|:---------------------:|:---------------------------:|:---------------------------:|
| [Insertion Sort](https://en.wikipedia.org/wiki/Insertion_sort)                 |    $\mathcal O(n)$    |      $\mathcal O(n^2)$      |      $\mathcal O(n^2)$      |
| [Cartesian Tree Sort](https://en.wikipedia.org/wiki/Cartesian_tree#In_sorting) | $\mathcal O(n\log n)$ |    $\mathcal O(n\log n)$    |      $\mathcal O(n^2)$      |
| [Patience Sort](https://en.wikipedia.org/wiki/Patience_sorting)                |    $\mathcal O(n)$    |    $\mathcal O(n\log n)$    |    $\mathcal O(n\log n)$    |
| [Red-Black Tree Sort](https://en.wikipedia.org/wiki/Red%E2%80%93black_tree)    |          $-$          |             $-$             |    $\mathcal O(n\log n)$    |
| [Shellsort](https://en.wikipedia.org/wiki/Shellsort)                           | $\mathcal O(n\log n)$ | $\mathcal O(n^\frac{4}{3})$ | $\mathcal O(n^\frac{3}{2})$ |
| [Splaysort](https://en.wikipedia.org/wiki/Splaysort)                           |    $\mathcal O(n)$    |    $\mathcal O(n\log n)$    |    $\mathcal O(n\log n)$    |
| [Tree Sort](https://en.wikipedia.org/wiki/Tree_sort)                           | $\mathcal O(n\log n)$ |    $\mathcal O(n\log n)$    |    $\mathcal O(n\log n)$    |

### Merge Sorts
| Name                                                     |         Best          |        Average        |         Worst         |
|----------------------------------------------------------|:---------------------:|:---------------------:|:---------------------:|
| [Merge Sort](https://en.wikipedia.org/wiki/Merge_sort)   | $\mathcal O(n\log n)$ | $\mathcal O(n\log n)$ | $\mathcal O(n\log n)$ |
| [Strand Sort](https://en.wikipedia.org/wiki/Strand_sort) |    $\mathcal O(n)$    |   $\mathcal O(n^2)$   |   $\mathcal O(n^2)$   |

### Selection Sorts
| Name                                                                     |          Best          |        Average         |          Worst          |
|--------------------------------------------------------------------------|:----------------------:|:----------------------:|:-----------------------:|
| [Binomial Heapsort](https://en.wikipedia.org/wiki/Binomial_heap)         |          $-$           |          $-$           |  $\mathcal O(n\log n)$  |
| [Circle Sort](https://sourceforge.net/p/forth-4th/wiki/Circle%20sort/)   | $\mathcal O(n\log n)$  | $\mathcal O(n\log n)$  | $\mathcal O(n\log^2 n)$ |
| [Cycle Sort](https://en.wikipedia.org/wiki/Cycle_sort)                   |   $\mathcal O(n^2)$    |   $\mathcal O(n^2)$    |    $\mathcal O(n^2)$    |
| [D-Heap Sort](https://en.wikipedia.org/wiki/D-ary_heap)                  | $\mathcal O(n\log_kn)$ | $\mathcal O(n\log_kn)$ | $\mathcal O(n\log_kn)$  |
| [Heapsort](https://en.wikipedia.org/wiki/Heapsort)                       | $\mathcal O(n\log n)$  | $\mathcal O(n\log n)$  |  $\mathcal O(n\log n)$  |
| [Leftist Heapsort](https://en.wikipedia.org/wiki/Leftist_tree)           |          $-$           |          $-$           |  $\mathcal O(n\log n)$  |
| [Selection Sort](https://en.wikipedia.org/wiki/Selection_sort)           |   $\mathcal O(n^2)$    |   $\mathcal O(n^2)$    |    $\mathcal O(n^2)$    |
| [Smoothsort](https://en.wikipedia.org/wiki/Smoothsort)                   |    $\mathcal O(n)$     | $\mathcal O(n\log n)$  |  $\mathcal O(n\log n)$  |
| [Tournament Sort](https://en.wikipedia.org/wiki/Tournament_sort)         | $\mathcal O(n\log n)$  | $\mathcal O(n\log n)$  |  $\mathcal O(n\log n)$  |
| [Weak-Heap Sort](https://en.wikipedia.org/wiki/Weak_heap#Weak-heap_sort) | $\mathcal O(n\log n)$  | $\mathcal O(n\log n)$  |  $\mathcal O(n\log n)$  |

### Other Sorts
| Name                                                          |      Best       |     Average     |      Worst      |
|---------------------------------------------------------------|:---------------:|:---------------:|:---------------:|
| [Pancake Sort](https://en.wikipedia.org/wiki/Pancake_sorting) | $\mathcal O(1)$ | $\mathcal O(n)$ | $\mathcal O(n)$ |

### Impractical Sorts
| Name                                                                          |                  Best                   |                 Average                 |                  Worst                  |
|-------------------------------------------------------------------------------|:---------------------------------------:|:---------------------------------------:|:---------------------------------------:|
| Assumption Sort                                                               |             $\mathcal O(1)$             |             $\mathcal O(1)$             |             $\mathcal O(1)$             |
| [Bogobogosort](https://www.dangermouse.net/esoteric/bogobogosort.html)        |                   $-$                   |                   $-$                   |         $\mathcal O(n!^{n-k})$          |
| [Bogosort](https://en.wikipedia.org/wiki/Bogosort)                            |               $\Omega(n)$               |         $\mathcal O(n\cdot n!)$         |          $\mathcal O(\infty)$           |
| [Bozosort](https://en.wikipedia.org/wiki/Bogosort#Related_algorithms)         |             $\mathcal O(n)$             |            $\mathcal O(n!)$             |          $\mathcal O(\infty)$           |
| [I Can't Believe It Can Sort](https://arxiv.org/abs/2110.01111)               |            $\mathcal O(n^2)$            |            $\mathcal O(n^2)$            |            $\mathcal O(n^2)$            |
| Permutation Sort                                                              |             $\mathcal O(n)$             |            $\mathcal O(n!)$             |            $\mathcal O(n!)$             |
| [Quantum Bogosort](https://en.wikipedia.org/wiki/Bogosort#Related_algorithms) |             $\mathcal O(n)$             |        The universe is destroyed        |        The universe is destroyed        |
| Sleep Sort                                                                    |             $\mathcal O(n)$             |             $\mathcal O(n)$             |             $\mathcal O(n)$             |
| [Slowsort](https://en.wikipedia.org/wiki/Slowsort)                            |          $\Omega(n^{\log n})$           |          $\Omega(n^{\log n})$           |          $\Omega(n^{\log n})$           |
| [Stalin Sort](https://github.com/gustavo-depaula/stalin-sort)                 |             $\mathcal O(n)$             |             $\mathcal O(n)$             |             $\mathcal O(n)$             |
| [Stooge Sort](https://en.wikipedia.org/wiki/Stooge_sort)                      | $\mathcal O(n^\frac{\log 3}{\log 1.5})$ | $\mathcal O(n^\frac{\log 3}{\log 1.5})$ | $\mathcal O(n^\frac{\log 3}{\log 1.5})$ |
