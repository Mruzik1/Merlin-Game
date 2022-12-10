using Merlin2d.Game.Actors;
using Merlin2d.Game.Enums;
using Merlin2d.Game;


namespace MyGame.Actors.Items
{
    public class EndKey : AbstractItem
    {
        protected Animation animationDefault;

        public EndKey(int x, int y)
        {
            animationDefault = new Animation("resources/sprites/end_key.png", 16, 16);

            if (x >= 0 && y >= 0)
                SetPosition(x, y);
            SetAnimation(animationDefault);
            GetAnimation().Start();
        }

        public EndKey() : this(-1, -1) {}

        public override void Update() {}

        public override void Use(IActor actor) 
        {
            Message msg = new Message("Keep me in the first slot to beat the game!", 0, 0, 30, Color.Blue, MessageDuration.Long);
            actor.GetWorld().AddMessage(msg);
        }
    }
}