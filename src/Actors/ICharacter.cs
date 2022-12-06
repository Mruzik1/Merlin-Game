using MyGame.Commands;


namespace MyGame.Actors
{
    public interface ICharacter : IMovable
    {
        public void ChangeHealth(int delta);
        public int GetHealth();
        public void Die();
        public void AddEffect(ICommand effect);
        public void RemoveEffect(ICommand effect);
    }
}