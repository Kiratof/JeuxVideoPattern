using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCodeur
{
    class AssetManager
    {

        public static SpriteFont MainFont { get; private set; }
        public static Song MusicMenu { get; private set; }
        public static Song MusicGameplay { get; private set; }

        public static void Load(ContentManager pContent)
        {
            MainFont = pContent.Load<SpriteFont>("mainfont");
            MusicMenu = pContent.Load<Song>("cool");
            MusicGameplay = pContent.Load<Song>("techno");
        }
    }
}
