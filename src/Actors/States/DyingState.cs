using Merlin2d.Game.Actors;
using Merlin2d.Game.Enums;


namespace MyGame.Actors
{
    public class DyingState : ICharacterState
    {
        private ICharacter character;

        public DyingState(ICharacter character)
        {
            this.character = character;
        }

        public void Update()
        {
            (character as IActor).RemoveFromWorld();

            if (character is Player)
                (character as Player).GetWorld().SetEndCondition(w => MapStatus.Failed);
        }
    }
}