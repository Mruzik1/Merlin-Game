using Merlin2d.Game.Items;
using Merlin2d.Game.Actors;


namespace MyGame.Actors.Items
{
    public abstract class AbstractItem : AbstractActor, IUsable, IItem
    {
        public AbstractItem()
        {
            SetPhysics(true);
        }

        public void Drop(IActor player)
        {
            Renew();
            SetPosition(player.GetX(), player.GetY());
            player.GetWorld().AddActor(this);
        }

        public virtual void Use(IActor actor) {}
    }
}