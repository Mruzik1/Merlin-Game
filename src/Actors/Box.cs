using Merlin2d.Game;


namespace MyGame.Actors
{
    public class Box : AbstractCharacter
    {
        private Player player;

        public Box(int x, int y) : base(1, -1)
        {    
            Animation animation = new Animation("resources/sprites/box.png", 32, 32);

            SetAnimation(animation);
            SetPosition(x, y);
            GetAnimation().Start();
        }

        public void SetPlayer(Player player)
        {
            this.player = player;
        }
        
        public override void Update()
        {  
            base.Update();
        }

        private bool IsPushableToLeft()
        {
            bool isEdge = GetX()+GetWidth()-player.GetSpeed() <= player.GetX();
            bool hasRightDirection = player.GetDirection() == ActorOrientation.FacingLeft;
            bool pullingButtonPressed = Input.GetInstance().IsKeyDown((Input.Key)ActorControls.Interact);

            return hasRightDirection && (isEdge || pullingButtonPressed);
        }

        private bool IsPushableToRight()
        {
            bool isEdge = player.GetX()+player.GetWidth() <= GetX()+player.GetSpeed();
            bool hasRightDirection = player.GetDirection() == ActorOrientation.FacingRight;
            bool pullingButtonPressed = Input.GetInstance().IsKeyDown((Input.Key)ActorControls.Interact);

            return hasRightDirection && (isEdge || pullingButtonPressed);
        }

        public override void Walking()
        {
            if (!IntersectsWithActor(player))
                return;

            speed = player.GetSpeed();

            // move by the player
            if (Input.GetInstance().IsKeyDown((Input.Key)ActorControls.MoveLeft) && IsPushableToLeft())
            {
                moveLeft.Execute();
            }
            else if (Input.GetInstance().IsKeyDown((Input.Key)ActorControls.MoveRight) && IsPushableToRight())
            {
                moveRight.Execute();
            }
        }
    }
}