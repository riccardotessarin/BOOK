namespace Characters.Interfaces {
    public interface ICharacter {
        string Name { get; }
        int Health { get; }
        int CurrentHealth { get; }
        int Stamina { get; }
        int Speed { get; }

        void TakeDamage(int damage);
        int Attack();

        string ToString();
    }
}