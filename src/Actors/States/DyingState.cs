using Merlin2d.Game.Actors;


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
        }
    }
}