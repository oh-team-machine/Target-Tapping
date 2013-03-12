using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary.UI;

namespace TargetTapping.Screens
{
    class MenuScreen : AbstractRichScreen
    {

        private Button btnNew, btnLoad, btnExit;

        public override void LoadContent()
        {
            base.LoadContent();

            btnNew = MakeButton(340, 200, "GUI/newButton");
            btnLoad = MakeButton(340, 350, "GUI/loadButton");
            btnExit = MakeButton(340, 500, "GUI/exitButton");

        }

        public override void PreparedDraw(SpriteBatch SpriteBatch)
        {
            btnNew.Draw(SpriteBatch);
            btnLoad.Draw(SpriteBatch);
            btnExit.Draw(SpriteBatch);
        }

    }
}
