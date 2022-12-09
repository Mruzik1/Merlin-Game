using Merlin2d.Game;
using MyGame.Commands;
using MyGame.Spells;


namespace MyGame.Actors
{
    public class Player : AbstractCharacter, IWizard
    {
        private int mana;

        public Player(int x, int y, double speed, int health, int mana) : base(speed, health)
        {    
            SetJump(new Jump(150, this));

            Animation animation = new Animation("resources/sprites/player.png", 28, 47);
            SetAnimation(animation);
            SetPosition(x, y);
            GetAnimation().Start();

            this.mana = mana;
        }
        
        public override void Update()
        {  
            base.Update();

            // cast some spell on 1
            if (Input.GetInstance().IsKeyPressed(Input.Key.KP_1))
            {
                SpellDirector director = new SpellDirector(this);
                Cast(director.Build("Painful Slowdown"));
            }

            // cast some spell on 2
            if (Input.GetInstance().IsKeyPressed(Input.Key.KP_2))
            {
                SpellDirector director = new SpellDirector(this);
                Cast(director.Build("Healing"));
            }
        }

        public override void Walking()
        {
            // move from side to side
            if (Input.GetInstance().IsKeyDown(Input.Key.A))
            {
                ChangeDirection(ActorOrientation.FacingLeft);
                animation.Start();
                moveLeft.Execute();
            }
            else if (Input.GetInstance().IsKeyDown(Input.Key.D))
            {
                ChangeDirection(ActorOrientation.FacingRight);
                animation.Start();
                moveRight.Execute();
            }
            else
                animation.Stop();
            
            // jump
            MakeJump(Input.GetInstance().IsKeyPressed(Input.Key.W));
        }

        public void ChangeMana(int delta)
        {
            mana += delta;
        }

        public int GetMana() => mana;

        public void Cast(ISpell spell)
        {   
            if (mana >= spell.GetCost())
                spell.ApplyEffects(this);
            
            displayHealth.SetText($"{health} / {maxHealth} HP\n{(this as IWizard).GetMana()} MP");
        }
    }
}