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
            // Execute effects
            for (int i = 0; i < effects.Count; ++i)
                effects[i].Execute();

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

            // cast some spell on C
            if (Input.GetInstance().IsKeyPressed(Input.Key.C))
            {
                Console.WriteLine($"Casting Something... Mana: {mana}");

                SpellDirector director = new SpellDirector(this);
                Cast(director.Build("Painful Slowdown"));

                Console.WriteLine($"Mana: {mana}");
            }

            // cast some spell on Z
            if (Input.GetInstance().IsKeyPressed(Input.Key.Z))
            {
                Console.WriteLine($"Casting Something... Mana: {mana}");

                SpellDirector director = new SpellDirector(this);
                Cast(director.Build("Healing"));

                Console.WriteLine($"Mana: {mana}");
            }

            // jump
            MakeJump(Input.GetInstance().IsKeyPressed(Input.Key.W));
        }

        public override void Walking()
        {

        }

        public void ChangeMana(int delta)
        {
            mana += delta;
        }

        public int GetMana()
        {
            return mana;
        }

        public void Cast(ISpell spell)
        {   
            if (mana >= spell.GetCost())
                spell.ApplyEffects(this);
        }
    }
}