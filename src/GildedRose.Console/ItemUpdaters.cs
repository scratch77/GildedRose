namespace GildedRose.Console
{
	public class LegendaryItemUpdater : IItemUpdater
	{
		public void Update(Item item)
		{
			// legendary item - do nothing
			return;
		}
	}

	public class AgedBrieItemUpdater : IItemUpdater
	{
		private const int DegradeBy = 1;
		public void Update(Item item)
		{
			Updater.ChangeQuality(item, DegradeBy);
			Updater.UpdateSellIn(item);
			if (item.SellIn < 0)
				Updater.ChangeQuality(item, DegradeBy);
		}
	}

	public class BackstagePassUpdater : IItemUpdater
	{
		private const int DegradeBy = 1;
		private const int MinQuality = 0;
		public void Update(Item item)
		{
			UpdateQuality(item);
			Updater.UpdateSellIn(item);
			if (item.SellIn < 0)
				UpdateQuality(item);
		}

		private void UpdateQuality(Item item)
		{
			Updater.ChangeQuality(item, DegradeBy);
			if (item.SellIn < 0)
			{
				item.Quality = MinQuality;
				return;
			}
			if (item.SellIn < 11)
			{
				Updater.ChangeQuality(item, DegradeBy);
				if (item.SellIn < 6)
				{
					Updater.ChangeQuality(item, DegradeBy);
				}
			}
		}
	}

	public class InventoryItemUpdater : IItemUpdater
	{
		private readonly int DegradeBy;

		public InventoryItemUpdater(int degradeBy = -1)
		{
			DegradeBy = degradeBy;
		}

		public void Update(Item item)
		{
			Updater.ChangeQuality(item, DegradeBy);
			Updater.UpdateSellIn(item);
			if (item.SellIn < 0)
				Updater.ChangeQuality(item, DegradeBy);
		}
	}


	public static class Updater
	{
		private const int MinQuality = 0;
		private const int MaxQuality = 50;

		public static void ChangeQuality(Item item, int degradeBy)
		{
			item.Quality += degradeBy;

			if (item.Quality < MinQuality)
				item.Quality = MinQuality;

			if (item.Quality > MaxQuality)
				item.Quality = MaxQuality;
		}

		public static void UpdateSellIn(Item item)
		{
			item.SellIn -= 1;
		}
	}
}
