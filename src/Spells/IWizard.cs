using Merlin2d.Game.Actors;


namespace MyGame.Spells
{
    public interface IWizard : IActor
    {
        void ChangeMana(int delta);
        int GetMana();
        void Cast(ISpell spell);
    }
}