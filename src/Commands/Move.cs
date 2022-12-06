using Merlin2d.Game;
using Merlin2d.Game.Actors;
using MyGame.Actors;


namespace MyGame.Commands
{
    public class Move : ICommand
    {
        protected IActor actor;
        protected int dx;
        protected int dy;
        protected double carry;

        public Move(IMovable movable, int dx, int dy)
        {
            if (!(movable is IActor))
            {
                throw new ArgumentException("Can only move actors");
            }

            this.actor = (IActor)movable;
            this.dx = dx;
            this.dy = dy;
            this.carry = 0;
        }

        public virtual void Execute()
        {   
            int oldX = actor.GetX();
            int oldY = actor.GetY();

            int step = (int)Math.Floor(((IMovable)actor).GetSpeed());
            if (carry >= 1)
            {
                step += (int)Math.Floor(carry);
                carry -= Math.Floor(carry);
            }
            else
                carry += ((IMovable)actor).GetSpeed() - step;

            int newX = actor.GetX() + step * dx;
            int newY = actor.GetY() + step * dy;

            actor.SetPosition(newX, newY);

            if (actor.GetWorld().IntersectWithWall(actor))
                actor.SetPosition(oldX, oldY);
        }
    }
}