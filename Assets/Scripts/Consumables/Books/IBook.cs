namespace Assets.Scripts.Consumables.Books
{
	public interface IBook
	{
		string Name { get; }
		string Element { get; }
		string Rarity { get; }
		string Description { get; }
		int Charges { get; }
		int CurrentCharges { get; }

		void RemoveCharge();
		void AddCharge();
		/// <summary>
		/// Define the behavior of the ability
		/// </summary>
		/// <returns>
		/// Returns the damage dealt
		/// </returns>
		int UseBook();

		string ToString();
	}
}