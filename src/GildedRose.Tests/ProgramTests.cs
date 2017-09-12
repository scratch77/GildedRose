using System.Collections.Generic;
using Xunit;
using GildedRose.Console;

namespace GildedRose.Tests
{
	public class ProgramTests
	{
		private Program _program;
		private const string StandardProduct = "Standard Item";
		private const string AgedBrie = "Aged Brie";
		private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
		private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
		private const string ConjuredItems = "Conjured item";
		
		private void SetupTest(Item item)
		{
			_program = new Program();
			_program.Items = new List<Item> { item };
			_program.UpdateQuality();
		}

		[Fact]
		public void StandardItemDegradeTest()
		{
			Item item = new Item { Name = StandardProduct, Quality = 10, SellIn = 10 };
			SetupTest(item);
			// At the end of each day our system lowers both values for every item
			Assert.True(item.Quality == 9);
			Assert.True(item.SellIn == 9);
		}

		[Fact]
		public void StandardItemDegradeAfterSellByTest()
		{
			Item item = new Item { Name = StandardProduct, Quality = 10, SellIn = 0 };
			SetupTest(item);
			// Once the sell by date has passed, Quality degrades twice as fast
			Assert.True(item.Quality == 8);
			Assert.True(item.SellIn == -1);
		}

		[Fact]
		public void MinQualityTest()
		{
			Item item = new Item { Name = StandardProduct, Quality = 0, SellIn = 10 };
			SetupTest(item);
			// The Quality of an item is never negative
			Assert.True(item.Quality == 0);
			Assert.True(item.SellIn == 9);
		}

		[Fact]
		public void AgedBrieIncreasesTest()
		{
			Item item = new Item { Name = AgedBrie, Quality = 10, SellIn = 10 };
			SetupTest(item);
			// "Aged Brie" actually increases in Quality the older it gets
			Assert.True(item.Quality == 11);
			Assert.True(item.SellIn == 9);
		}

		[Fact]
		public void AgedBrieIncreasesFromZeroTest()
		{
			Item item = new Item { Name = AgedBrie, Quality = 0, SellIn = 10 };
			SetupTest(item);
			// "Aged Brie" actually increases in Quality the older it gets
			Assert.True(item.Quality == 1);
			Assert.True(item.SellIn == 9);
		}

		[Fact]
		public void AgedBrieMaxTest()
		{
			Item item = new Item { Name = AgedBrie, Quality = 50, SellIn = 10 };
			SetupTest(item);
			// The Quality of an item is never more than 50
			Assert.True(item.Quality == 50);
			Assert.True(item.SellIn == 9);
		}

		public void AgedBrieAfterSellByTest()
		{
			Item item = new Item { Name = AgedBrie, Quality = 10, SellIn = 0 };
			SetupTest(item);
			// "Aged Brie" actually increases in Quality the older it gets
			Assert.True(item.Quality == 12);
			Assert.True(item.SellIn == 9);
		}

		[Fact]
		public void LegendaryItemDoesNotDegradeOrLoseTimeTest()
		{
			Item item = new Item { Name = Sulfuras, Quality = 80, SellIn = 10 };
			SetupTest(item);
			// "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
			Assert.True(item.Quality == 80);
			Assert.True(item.SellIn == 10);
		}

		[Fact]
		public void BackstagePassesIncreasesTest()
		{
			Item item = new Item { Name = BackstagePasses, Quality = 10, SellIn = 11 };
			SetupTest(item);
			// "Backstage passes",  increases in Quality as it's SellIn value approaches
			Assert.True(item.Quality == 11);
			Assert.True(item.SellIn == 10);
		}

		[Fact]
		public void BackstagePassesIncreasesByTwoTest()
		{
			Item item = new Item { Name = BackstagePasses, Quality = 10, SellIn = 10 };
			SetupTest(item);
			// "Backstage passes", Quality increases by 2 when there are 10 days or less
			Assert.True(item.Quality == 12);
			Assert.True(item.SellIn == 9);
		}

		[Fact]
		public void BackstagePassesIncreasesByThreeTest()
		{
			Item item = new Item { Name = BackstagePasses, Quality = 10, SellIn = 5 };
			SetupTest(item);
			// "Backstage passes", Quality increases by 3 when there are 5 days or less 
			Assert.True(item.Quality == 13);
			Assert.True(item.SellIn == 4);
		}
		
		[Fact]
		public void BackstagePassesMaxTest()
		{
			Item item = new Item { Name = BackstagePasses, Quality = 48, SellIn = 5 };
			SetupTest(item);
			// would expect 51, but max = 50
			Assert.True(item.Quality == 50);
			Assert.True(item.SellIn == 4);
		}

		[Fact]
		public void BackstagePassesAfterSellByTest()
		{
			Item item = new Item { Name = BackstagePasses, Quality = 10, SellIn = 0 };
			SetupTest(item);
			// Quality drops to 0 after the concert
			Assert.True(item.Quality == 0);
			Assert.True(item.SellIn == -1);
		}

		[Fact]
		public void ConjuredItemDegradeTest()
		{
			Item item = new Item { Name = ConjuredItems, Quality = 10, SellIn = 10 };
			SetupTest(item);
			// "Conjured" items degrade in Quality twice as fast as normal items
			Assert.True(item.Quality == 8);
			Assert.True(item.SellIn == 9);
		}

		[Fact]
		public void ConjuredItemDegradeAfterSellByTest()
		{
			Item item = new Item { Name = ConjuredItems, Quality = 10, SellIn = 0 };
			SetupTest(item);
			// "Conjured" items degrade in Quality twice as fast as normal items
			Assert.True(item.Quality == 6);
			Assert.True(item.SellIn == -1);
		}
	}
}
