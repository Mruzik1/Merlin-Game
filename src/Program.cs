using MyGame.Actors;
using MyGame.Commands;
using MyGame.Factories;
using Merlin2d.Game;
using Merlin2d.Game.Enums;


namespace MyGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameContainer container = new GameContainer("Game window", 800, 500);

            container.SetMap("resources/maps/map01.tmx");
            container.GetWorld().SetPhysics(new Gravity());
            container.GetWorld().SetFactory(new ActorFactory());

            container.SetCameraFollowStyle(CameraFollowStyle.CenteredInsideMapPreferTop);

            container.GetWorld().AddInitAction(w => {
                Player player = (Player)w.GetActors().Find(x => x.GetName() == "Merlin");
                TestActor testActor = (TestActor)w.GetActors().Find(x => x.GetName() == "Crystal");
                Skeleton enemy = (Skeleton)w.GetActors().Find(x => x.GetName() == "John");

                player.InitHealthMsg();
                enemy.InitHealthMsg();

                // to perform actors collision
                testActor.SetPlayer(player);
                enemy.SetPlayer(player);
                w.CenterOn(player);
            });

            container.Run();
        }
    }
}