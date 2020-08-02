namespace Assets.Scripts.Consumables.Books
{
	public interface IBook : IConsumable
	{
		string Element { get; }
		string Rarity { get; }
		IPage.PageType PageType { get; }
		int Charges { get; }
		int CurrentCharges { get; }

		/// <summary>
		/// Remove a charge from the BOOK and delete the BOOK from the inventory if
		/// the number of charges reaches 0.
		/// </summary>
		/// <returns>
		/// </returns>
		void RemoveCharge();

		/// <summary>
		/// Add a charge to the BOOK if it is not already at maximum charge. Returns true if the charge has been added correctly.
		/// </summary>
		/// <returns>
		/// Returns true if the charge has been added.
		/// </returns>
		bool AddCharge(IPage page);
	}
}