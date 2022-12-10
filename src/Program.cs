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
                // init player
                Player player = (Player)w.GetActors().Find(x => x.GetName() == "Merlin");
                player.InitHealthMsg();
                w.ShowInventory(player.GetInventory());

                // init actors with specific names
                w.GetActors().ForEach(actor => {
                    string name = actor.GetName();

                    if (name.Contains("John"))
                    {
                        (actor as AbstractCharacter).InitHealthMsg();
                        (actor as Skeleton).SetPlayer(player);
                    }
                    else if (name.Contains("Box"))
                    {
                        (actor as Box).SetPlayer(player);
                    }
                    else if (name.Contains("Door"))
                    {
                        (actor as AbstractActor).MakeSolid(true);
                    }
                    else if (name.Contains("PressurePlate") || name.Contains("Switch"))
                    {
                        int mechanismNumber = name.Length-2;
                        IMechanism mechanism = (IMechanism)w.GetActors().Find(x => x.GetName().Contains($"Mechanism{name[mechanismNumber]}"));
                        (actor as IController).SetMechanism(mechanism);
                    }
                });

                // set final messages
                container.SetEndGameMessage(new Message("You won!", 100, 100, 40, Color.Blue, MessageDuration.Short), MapStatus.Finished);
                container.SetEndGameMessage(new Message("Game over", 100, 100, 40, Color.Blue, MessageDuration.Short), MapStatus.Failed);
                
                // set the camera up
                w.CenterOn(player);
            });

            container.Run();
        }
    }
}