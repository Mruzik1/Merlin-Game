using Merlin2d.Game.Items;
using Merlin2d.Game.Actors;


namespace MyGame.Actors.Items
{
    public abstract class AbstractItem : AbstractActor, IUsable, IItem
    {
        public virtual void Use(IActor actor) {}
    }
}