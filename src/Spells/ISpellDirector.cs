namespace MyGame.Spells
{
    public interface ISpellDirector
    {
        ISpell Build(string spellName);
    }
}