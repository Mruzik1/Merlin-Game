using Merlin2d.Game;
using MyGame.Commands;
using MyGame.Spells;


namespace MyGame.Actors
{
    public class Player : AbstractCharacter, IWizard
    {
        private int mana;
        private SpellDirector spellCaster;

        public Player(int x, int y, double speed, int health, int mana) : base(speed, health)
        {    
            Animation animation = new Animation("resources/sprites/player.png", 28, 47);
            spellCaster = new SpellDirector(this);

            SetJump(new Jump(150, this));
            SetAnimation(animation);
            SetPosition(x, y);
            GetAnimation().Start();

            this.mana = mana;
        }
        
        public override void Update()
        {  
            base.Update();

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
            if (spell == null)
                return;

            if (spell is SelfCastSpell)
                spell.ApplyEffects(this);
            
            displayHealth.SetText($"{health} / {maxHealth} HP\n{(this as IWizard).GetMana()} MP");
        }
    }
}