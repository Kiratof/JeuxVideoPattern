using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameCodeur
{
    class SceneGameover : Scene
    {

        //ATTRIBUTS



        //METHODES
        public SceneGameover(MainGame pGame) : base(pGame)
        {
            Debug.WriteLine("New scene gameover");
        }

        public override void Load()
        {

            Debug.WriteLine("SceneGameover.Load()");
            base.Load();
        }

        public override void Unload()
        {

            Debug.WriteLine("SceneGameover.Unload()");
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            mainGame.spriteBatch.DrawString(AssetManager.MainFont,
                "This is the gameover !", new Vector2(1, 1), Color.White);
            base.Draw(gameTime);
        }
    }
}
