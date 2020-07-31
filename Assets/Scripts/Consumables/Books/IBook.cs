namespace Assets.Scripts.Consumables.Books
{
	public interface IBook : IConsumable
	{
		string Element { get; }
		string Rarity { get; }
		IPage.PageType PageType { get; }
		int Charges { get; }
		int CurrentCharges { get; }

		void RemoveCharge();
		bool AddCharge(IPage page);
	}
}