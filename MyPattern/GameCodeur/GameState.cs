using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCodeur
{
    public class GameState
    {
        //NOS SCENES
        public enum SceneType
        {
            Menu,
            Gameplay,
            Gameover

        }

        //ATTRIBUTS
        protected MainGame mainGame;
        public Scene CurrentScene { get; set; }



        //METHODES

        public GameState(MainGame pGame) //Constructeur
        {
            mainGame = pGame;
        }

        public void ChangeScene(SceneType pSceneType)
        {
            //Déchargement de la scene si une scene exite déjà
            if(CurrentScene != null)
            {
                CurrentScene.Unload();
                CurrentScene = null;
            }

            //Selon la scene demandée, on charge la nouvelle scene
            switch (pSceneType)
            {
                case SceneType.Menu:
                    CurrentScene = new SceneMenu(mainGame);
                    break;

                case SceneType.Gameplay:
                    CurrentScene = new SceneGameplay(mainGame);
                    break;

                case SceneType.Gameover:
                    CurrentScene = new SceneGameover(mainGame);
                    break;

                default:
                    break;
            }

            CurrentScene.Load();
            
        }
    }
}
