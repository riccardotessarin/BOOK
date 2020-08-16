using System.Collections.Generic;
using Consumables.Books;

namespace Characters.Interfaces
{
	public interface IPlayableCharacter : ICharacter {
        void UseSelectedBook(IBook book);

        void UseSpecialAttack();
    }
}