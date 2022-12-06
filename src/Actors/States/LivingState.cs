using MyGame.Commands;


namespace MyGame.Actors
{
    public class LivingState : ICharacterState
    {
        private ICharacter character;

        public LivingState(ICharacter character)
        {
            this.character = character;
        }

        public void Update()
        {
            // Walking
            character.Walking();

            // Execute effects
            for (int i = 0; i < character.GetEffects().Count; ++i)
                character.GetEffects()[i].Execute();
        }
    }
}