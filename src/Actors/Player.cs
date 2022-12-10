using Merlin2d.Game;
using Merlin2d.Game.Items;
using MyGame.Commands;
using MyGame.Actors.Items;
using MyGame.Spells;


namespace MyGame.Actors
{
    public class Player : AbstractCharacter, IWizard
    {
        private int mana;
        private IInventory inventory;
        private SpellDirector spellCaster;

        public Player(int x, int y, double speed, int health, int mana) : base(speed, health)
        {    
            Animation animation = new Animation("resources/sprites/player.png", 28, 47);
            spellCaster = new SpellDirector(this);
            inventory = new Backpack(10);

            inventory.AddItem(new HealingPotion(50));
            inventory.AddItem(new ManaPotion(50));
            inventory.AddItem(new HealingPotion(50));
            inventory.AddItem(new HealingPotion(50));
            inventory.AddItem(new ManaPotion(50));
            inventory.AddItem(new HealingPotion(50));
            inventory.AddItem(new HealingPotion(50));
            inventory.AddItem(new ManaPotion(50));
            inventory.AddItem(new ManaPotion(50));

            SetJump(new Jump(150, this));
            SetPosition(x, y);

            SetAnimation(animation);
            GetAnimation().Start();

            this.mana = mana;
        }

        public IInventory GetInventory() => inventory;
        
        public override void Update()
        {  
            base.Update();

            // do magic
            // cast fireball
            if (Input.GetInstance().IsKeyPressed(Input.Key.ONE))
                Cast(spellCaster.Build("fireball"));

            // cast iceball
            else if (Input.GetInstance().IsKeyPressed(Input.Key.TWO))
                Cast(spellCaster.Build("iceball"));

            // cast healing over time
            else if (Input.GetInstance().IsKeyPressed(Input.Key.THREE))
                Cast(spellCaster.Build("healing"));

            // cast instant heal
            else if (Input.GetInstance().IsKeyPressed(Input.Key.FOUR))
                Cast(spellCaster.Build("instant heal"));
                        
            // cast speed boost
            else if (Input.GetInstance().IsKeyPressed(Input.Key.FIVE))
                Cast(spellCaster.Build("speed boost"));

            // shift inventory
            // to the left
            if (Input.GetInstance().IsKeyPressed((Input.Key)ActorControls.ShiftInventoryLeft))
                inventory.ShiftLeft();

            // to the right
            else if (Input.GetInstance().IsKeyPressed((Input.Key)ActorControls.ShiftInventoryRight))
                inventory.ShiftRight();
            
            // use an item from the inventory
            else if (Input.GetInstance().IsKeyPressed((Input.Key)ActorControls.UseFromInventory))
            {
                AbstractItem item = (inventory.GetItem() as AbstractItem);

                if (item != null)
                    item.Use(this);
            }
        }

        public override void Walking()
        {
            // move from side to side
            if (Input.GetInstance().IsKeyDown((Input.Key)ActorControls.MoveLeft))
            {
                ChangeDirection(ActorOrientation.FacingLeft);
                animation.Start();
                moveLeft.Execute();
            }
            else if (Input.GetInstance().IsKeyDown((Input.Key)ActorControls.MoveRight))
            {
                ChangeDirection(ActorOrientation.FacingRight);
                animation.Start();
                moveRight.Execute();
            }
            else
                animation.Stop();
            
            // jump
            MakeJump(Input.GetInstance().IsKeyPressed((Input.Key)ActorControls.Jump));
        }

        public void ChangeMana(int delta)
        {
            mana += delta;
            UpdateStats();
        }

        public int GetMana() => mana;

        public void Cast(ISpell spell)
        {   
            if (spell == null)
                return;

            if (spell is SelfCastSpell)
                spell.ApplyEffects(this);
            
            displayHealth.SetText($"{health} / {maxHealth} HP\n{(this as IWizard).GetMana()} MP");
        }
    }
}