namespace Books {
	public interface IBook {
		string Name { get; }
		string Element { get; }
		string Rarity { get; }
		string Description { get; }
		int Charges { get; }

		void RemoveCharge();
		void AddCharge();
		int UseBook();

		string ToString();
	}
}