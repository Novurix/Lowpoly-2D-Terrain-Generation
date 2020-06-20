using System;
using OpenTK;

namespace NEW_2D_TERRAIN
{
    class MainClass
    {
        static Game game;
        static GameWindow window;

        public static void Main(string[] args)
        {
            window = new GameWindow(1000, 720);
            game = new Game(window);
        }
    }
}
