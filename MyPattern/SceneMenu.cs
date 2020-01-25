using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameCodeur
{
    class SceneMenu : Scene
    {
        //ATTRIBUTS

        private KeyboardState oldKbState;
        private MouseState oldMOState;
        private GamePadState oldGPState;
        private Button MyButton;
        private Song music;
        

        //METHODES
        //Constructeur
        public SceneMenu(MainGame pGame) : base(pGame)
        {
        }

        public void onClickPlay(Button pSender)
        {
            mainGame.gameState.ChangeScene(GameState.SceneType.Gameplay);
        }


        public override void Load()
        {

            Debug.WriteLine("Menu scene loaded");
            
            //MUSIQUE
            music = mainGame.Content.Load<Song>("cool");
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;


            //BUTTON PLAY
            Rectangle Screen = mainGame.Window.ClientBounds;
            MyButton = new Button(mainGame.Content.Load<Texture2D>("button"));
            MyButton.Position = new Vector2(
                (Screen.Width/2) - MyButton.Texture.Width/2,
                (Screen.Height/2) - MyButton.Texture.Height/2
                );

            MyButton.OnClick = onClickPlay; //Déléguation del a fonction onClickPlay

            listActors.Add(MyButton);

            base.Load();
        }

        public override void Unload()
        {
            MediaPlayer.Stop();
            base.Unload();
        }
        
        public override void Update(GameTime gameTime)
        {


            
            //GESTION DU CLAVIER
            KeyboardState newKbState = Keyboard.GetState(); //ETAT DU CLAVIER
            bool keySpace = false;

            //GESTION DU CLAVIER
            if ((newKbState.IsKeyDown(Keys.Space) && //Si la touche ESPACE est enfoncée
                !oldKbState.IsKeyDown(Keys.Space))) //Et ne l'étais pas à l'Update précédente
            {
                keySpace = true; //La touche est bien enfoncée
            }
            oldKbState = newKbState; //Sauvegarde de l'état actuel pour l'update suivante


            //GESTION DE LA SOURIS
            MouseState newMOState = Mouse.GetState(); //ETAT DE LA SOURIS
            bool lftClick = false;

            //GESTION DE LA SOURIS
            if ((newMOState.LeftButton == ButtonState.Pressed && //Si le CLIQUE GAUCHE est enfoncée
                oldMOState.LeftButton == ButtonState.Pressed)) //Et ne l'étais pas à l'Update précédente
            {
                lftClick = true; //La touche est bien enfoncée
            }
            oldMOState = newMOState; //Sauvegarde de l'état actuel pour l'update suivante


            //GESTION DU GAMEPAD
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One); //POUR TESTER DES TRUCS AVEC LA MANETTE
            GamePadState newGPState; //ETAT DU PAD
            bool butA = false;
            
            //GESTION DU PAD
            if (capabilities.IsConnected) //SI une manette est branchée
            {
                newGPState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.IndependentAxes); //on récupère l'état du pad

                if (
                (newGPState.IsButtonDown(Buttons.A) && //SI A enfoncé
                !oldGPState.IsButtonDown(Buttons.A)))  //Et ne l'étais pas à l'Update précédente
                {
                    butA = true;  //La touche est bien enfoncée
                }
                oldGPState = newGPState;  //Sauvegarde de l'état actuel pour l'update suivante


            }
            
            

            //ACTION
            if (keySpace || butA /*|| lftClick*/) //si ESPACE ou A ou CLIQUE GAUCHE
            {
                mainGame.gameState.ChangeScene(GameState.SceneType.Gameplay);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            mainGame.spriteBatch.DrawString(AssetManager.MainFont,
                "This is the menu !", new Vector2(1, 1), Color.White);
            base.Draw(gameTime);
        }
    }
}
