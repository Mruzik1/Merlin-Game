using MyGame.Actors;
using MyGame.Commands;


namespace MyGame.Spells
{
    public interface ISpell
    {
        ISpell AddEffect(ICommand effect);
        void AddEffects(IEnumerable<ICommand> effects);
        int GetCost();
        void ApplyEffects(ICharacter target);
    }
}