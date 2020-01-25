using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameCodeur
{
    class Hero : Sprite
    {
        public float Energy;
        public Hero(Texture2D pTexture) : base(pTexture)
        {
            Energy = 100.0f;
        }


        public override void TouchedBy(IActor pBy)
        {
            if (pBy is Meteor)
            {
                Energy -= 10f;
            }
        }

    }

    class Meteor : Sprite
    {
        public Meteor(Texture2D pTexture) : base(pTexture)
        {
            do
            {
                vX = (float)GameCodeur.Util.GetInt(-10, 10) / 3;
            } while (vX == 0 );

            do
            {
                vY = (float)GameCodeur.Util.GetInt(-3, 3) / 5;
            } while (vY == 0);
        }
    }


    class SceneGameplay : Scene
    {

        //ATTRIBUTS

        private KeyboardState oldKbState;
        private GamePadState oldGPState;
        private Hero MyShip;
        private Song music;
        private SoundEffect sndExplode;

        //METHODES
        public SceneGameplay(MainGame pGame) : base(pGame)
        {

        }

        public override void Load()
        {
            Debug.WriteLine("Gameplay scene loaded");
            oldKbState = Keyboard.GetState(); //Etat du clavier à l'entrée de  la fenêtre 
            Rectangle Screen = mainGame.Window.ClientBounds;

            //SON
            sndExplode = mainGame.Content.Load<SoundEffect>("explode");
            //MUSIQUE
            music = AssetManager.MusicGameplay;
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;


            for (int i = 0; i < 20; i++)
            {
                Meteor m = new Meteor(mainGame.Content.Load<Texture2D>("meteor"));
                m.Position = new Vector2(
                    GameCodeur.Util.GetInt(1, Screen.Width - m.Texture.Width),
                    GameCodeur.Util.GetInt(1, Screen.Height - m.Texture.Height)
                    );

                listActors.Add(m);
            }

            //Ajout de notre vaisseau
            MyShip = new Hero(mainGame.Content.Load<Texture2D>("ship"));
            MyShip.Position = new Vector2(
                (Screen.Width/2) - MyShip.Texture.Width/2,
                (Screen.Height/2) - MyShip.Texture.Height/2
                );
            listActors.Add(MyShip);

            base.Load();
        }

        public override void Unload()
        {
            MediaPlayer.Stop();
            Debug.WriteLine("Gameplay scene unloaded");
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle Screen = mainGame.Window.ClientBounds;

            foreach (IActor Actor in listActors) //Pour tous les acteurs de listActorrs
            {

                //GESTION DES COLISIONS DES METEORES
                if (Actor is Meteor m) //Pour les météores
                {
                    //AVEC LES MURS
                    if (m.Position.X < 0)
                    {
                        m.vX = 0 - m.vX;
                        m.Position = new Vector2(0, m.Position.Y);
                    }
                    if (m.Position.X + m.BoundingBox.Width > Screen.Width)
                    {
                        m.vX = 0 - m.vX;
                        m.Position = new Vector2(Screen.Width - m.BoundingBox.Width, m.Position.Y);
                    }
                    if (m.Position.Y < 0)
                    {
                        m.vY = 0 - m.vY;
                        m.Position = new Vector2(m.Position.X, 0);
                    }
                    if (m.Position.Y + m.BoundingBox.Height > Screen.Height)
                    {
                        m.vY = 0 - m.vY;
                        m.Position = new Vector2(m.Position.X, Screen.Height - m.BoundingBox.Height);
                    }

                    //AVEC LE VAISEAU
                    if (Util.CollideByBox(m, MyShip))
                    {
                        MyShip.TouchedBy(m);
                        m.TouchedBy(MyShip);
                        m.ToRemove = true;

                        //Joue le son
                        sndExplode.Play();
                        
                    }
                }
            }




            //GESTION DU CLAVIER
            KeyboardState newKbState = Keyboard.GetState(); //ETAT DU CLAVIER
            bool keyZ= false;
            bool keyQ= false;
            bool keyS= false;
            bool keyD= false;

            //QUELLE TOUCHE EST TOUCHEE ???
            if ((newKbState.IsKeyDown(Keys.Z) && //Si la touche ESPACE est enfoncée
                !oldKbState.IsKeyDown(Keys.Z))) //Et ne l'étais pas à l'Update précédente
            {
                keyZ = true; //La touche est bien enfoncée
            }

            if ((newKbState.IsKeyDown(Keys.Q) && //Si la touche ESPACE est enfoncée
                !oldKbState.IsKeyDown(Keys.Q))) //Et ne l'étais pas à l'Update précédente
            {
                keyQ = true; //La touche est bien enfoncée
            }
            
            if ((newKbState.IsKeyDown(Keys.S) && //Si la touche ESPACE est enfoncée
                !oldKbState.IsKeyDown(Keys.S))) //Et ne l'étais pas à l'Update précédente
            {
                keyS = true; //La touche est bien enfoncée
            }
            
            if ((newKbState.IsKeyDown(Keys.D) && //Si la touche ESPACE est enfoncée
                !oldKbState.IsKeyDown(Keys.D))) //Et ne l'étais pas à l'Update précédente
            {
                keyD = true; //La touche est bien enfoncée
            }

            oldKbState = newKbState; //Sauvegarde de l'état actuel pour l'update suivante



            //GESTION DU GAMEPAD
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One); //POUR TESTER DES TRUCS AVEC LA MANETTE
            GamePadState newGPState; //ETAT DU PAD
            //bool butA = false;
            bool lftThuUp = false;
            bool lftThuLft = false;
            bool lftThuRgt = false;
            bool lftThuDwn = false;

            if (capabilities.IsConnected) //SI une manette est branchée
            {
                newGPState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.IndependentAxes); //on récupère l'état du pad
                
                //QUELLE BOUTON EST TOUCHE ???
                if ((newGPState.IsButtonDown(Buttons.LeftThumbstickUp)))
                {
                    lftThuUp = true;
                }
                if ((newGPState.IsButtonDown(Buttons.LeftThumbstickLeft)))
                {
                    lftThuLft = true;
                }
                if ((newGPState.IsButtonDown(Buttons.LeftThumbstickRight)))
                {
                    lftThuRgt = true;
                }
                if ((newGPState.IsButtonDown(Buttons.LeftThumbstickDown)))
                {
                    lftThuDwn = true;
                }
                
                oldGPState = newGPState;  //Sauvegarde de l'état actuel pour l'update suivante
                
            }
            
            //ACTIONS
            if (keyZ || lftThuUp)
            {
                MyShip.Move(0, 0 - 10);
            }
            if (keyQ || lftThuLft)
            {
                MyShip.Move(0 - 10, 0);
            }
            if (keyS || lftThuDwn)
            {
                MyShip.Move(0, 10);
            }
            if (keyD || lftThuRgt)
            {
                MyShip.Move(10, 0);
            }


            //GAMEOVER
            if (MyShip.Energy <= 0)
            {
                MyShip.Energy = 0;
                mainGame.gameState.ChangeScene(GameState.SceneType.Gameover);
            }

            Clean(); //Retire tous les éléments qui doivent être retirés de l'image

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            mainGame.spriteBatch.DrawString(AssetManager.MainFont,
                "GAMEPLAY ! - ENERGY " + MyShip.Energy, new Vector2(1, 1), Color.White);
            base.Draw(gameTime);
        }
    }
}
