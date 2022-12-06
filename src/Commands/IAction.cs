namespace MyGame.Commands {
    public interface IAction<T>
    {
        public void Execute(T value);
    }
}