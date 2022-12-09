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
                PressurePlate pressurePlate = (PressurePlate)w.GetActors().Find(x => x.GetName() == "PressurePlate");
                Door door = (Door)w.GetActors().Find(x => x.GetName() == "Door");

                player.InitHealthMsg();

                // init enemies
                w.GetActors().ForEach(actor => {
                    if (actor.GetName().Contains("John"))
                    {
                        (actor as Skeleton).InitHealthMsg();
                        (actor as Skeleton).SetPlayer(player);
                    }
                });

                // init other things
                pressurePlate.SetMechanism(door);
                door.MakeSolid();

                // set the camera up
                w.CenterOn(player);
            });

            container.Run();
        }
    }
}