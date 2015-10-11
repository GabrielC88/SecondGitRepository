using System;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

public class Class1
{
    public Class1()
    {
    }
}


namespace Sample2

    class Camera: Microsoft.xna.framework.Gamecomponent
{
        public matrix view { get; protected set; }
        public matrix projection { get; protected set; }

    public Camera (Game game,Vector3 pos, Vector3 target, Vector3 up)
        : base (game)

    {
        view = matrix.createlookat(pos, target, up);
        projection = matrix.createperspectivefieldofview(
            mathhelper.PiOver4,
            (float)Game.Window.ClientBounds.Width / (float)Game.Window.ClientBounds.Height,
            1, 100);
    }

        public override void Initialize()
    {
        base.Initialize();
    }

        public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }





}
