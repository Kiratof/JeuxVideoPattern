using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCodeur
{
    abstract public class Scene
    {
        //ATTRIBUTS
        protected MainGame mainGame; //Référence à l'instance du jeu
        protected List<IActor> listActors;
        

        //METHODES
        public Scene(MainGame pGame)//Constructeur
        {
            mainGame = pGame;
            listActors = new List<IActor>();
        }


        //Autres

        public void Clean()
        {
            listActors.RemoveAll(item => item.ToRemove == true); //Expression lambda
        }

        public virtual void Load()
        {


        }

        public virtual void Unload()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (IActor actor in listActors)
            {
                actor.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            foreach (IActor actor in listActors)
            {
                actor.Draw(mainGame.spriteBatch);
            }
        }

    }
}
