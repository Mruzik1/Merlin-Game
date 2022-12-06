namespace MyGame.Actors
{
    public class LivingState : ICharacterState
    {
        public void Update(ICharacter character)
        {
            // Walking
            character.Walking();
        }
    }
}