namespace GildedRose.Console
{
	public class ItemFactory
	{
		public IItemUpdater Create(Item item)
		{
			if (item.Name == "Sulfuras, Hand of Ragnaros")
				return new LegendaryItemUpdater();
			if (item.Name == "Aged Brie")
				return new AgedBrieItemUpdater();
			if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
				return new BackstagePassUpdater();
			if (item.Name.Contains("Conjured"))
				return new InventoryItemUpdater(-2);
			return new InventoryItemUpdater(-1);
		}
	}
}
