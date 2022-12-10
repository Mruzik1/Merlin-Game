using Merlin2d.Game;

namespace MyGame.Actors
{
    public enum ActorControls
    {
        MoveLeft = Input.Key.LEFT,
        MoveRight = Input.Key.RIGHT,
        Jump = Input.Key.UP,
        Interact = Input.Key.Z,
        UseFromInventory = Input.Key.C,
        ShiftInventoryLeft = Input.Key.A,
        ShiftInventoryRight = Input.Key.D,
        InventoryThrowPickup = Input.Key.X
    }
}