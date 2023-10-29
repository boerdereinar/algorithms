using SortingAlgorithms.DataStructures.Trees;
using SortingAlgorithms.Utilities;

namespace SortingAlgorithms.Tests.DataStructures.Trees;

/// <summary>
/// Tests for <see cref="BurstTrie{T}"/>.
/// </summary>
public sealed class BurstTrieTests
{
	/// <summary>
	/// Tests if <see cref="BurstTrie{T}.InsertRange"/> uses the duplicate bucket.
	/// </summary>
	/// <param name="length">Length of the strings to insert.</param>
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	public void InsertRange_Duplicates_UsesDuplicateBucket(int length)
	{
		var trie = new BurstTrie<string>();
		var expected = Enumerable.Range(0, 100).Select(_ => new string('a', length)).ToArray();

		trie.InsertRange(expected, x => x);
		trie.Sort(Comparer<char?>.Default);

		Assert.Equal(expected, trie);
	}

	/// <summary>
	/// Tests if <see cref="BurstTrie{T}.Sort"/> sorts correctly.
	/// </summary>
	[Fact]
	public void Sort_SortsCorrectly()
	{
		const string Alphabet = "abcdefghijklmnopqrstuvwxyz";

		var random = new Random(42);
		var trie = new BurstTrie<string>();
		var expected = Alphabet[..10].SelectMany(a => Alphabet.SelectMany(b => new[] { $"{a}{b}a", $"{a}{b}b", $"{a}{b}c" })).ToArray();

		trie.InsertRange(random.Shuffle(expected), x => x);
		trie.Sort(Comparer<char?>.Default);

		Assert.Equal(expected, trie);
	}
}
