using Merlin2d.Game;


namespace MyGame.Spells
{
    public interface ISpellBuilder
    {
        ISpellBuilder AddEffect(string effectName);
        ISpellBuilder SetAnimation(Animation animation); // unused for self-cast spells
        ISpellBuilder SetSpellCost(int cost);
        ISpell CreateSpell(IWizard wizard); // reference to the wizard doing the spell
    }
}