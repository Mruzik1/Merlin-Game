using Merlin2d.Game;
using Merlin2d.Game.Actors;
using System.Linq;


namespace MyGame.Commands
{
    public class Gravity : IPhysics
    {
        private IWorld world;
        private Fall<IActor> fall = new Fall<IActor>();

        public void Execute()
        {
            List<IActor> actorsG = this.world.GetActors().Where(a => a.IsAffectedByPhysics()).ToList();

            actorsG.ForEach(aG => fall.Execute(aG));
        }
        public void SetWorld(IWorld world)
        {
            this.world = world;
        }
    }
}