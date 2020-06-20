using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace NEW_2D_TERRAIN
{
    public class Game
    {
        float currentHeight = .5f;
        float[] yVertices;
        float[] xVertices;

        int xOffset = 4;

        int terrainSize = 200;

        GameWindow window;
        public Game(GameWindow window)
        {
            yVertices = new float[terrainSize];
            xVertices = new float[terrainSize];
            this.window = window;
            start();
        }

        void start() {

            yVertices[0] = currentHeight;
            xVertices[0] = 0;

            for (int i = 0; i < yVertices.Length-1; i++) {
                Random randomH = new Random();
                float randomHeight = randomH.Next(1, 5);

                Random randomOp = new Random();
                int randomOperator = randomOp.Next(1, 3);
                if (randomOperator == 1) currentHeight += randomHeight/2;
                else currentHeight -= randomHeight/2;

                yVertices[i + 1] = currentHeight;
                xVertices[i + 1] = i * xOffset;
            }

            GL.Translate(0, 0, -55f);

            window.RenderFrame += render;
            window.Resize += resize;
            window.Load += load;

            window.Run(1 / 60);
        }

        void render(object sender, EventArgs args) {

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Translate(-.1, 0, 0);

            GL.Begin(PrimitiveType.QuadStrip);
            GL.Color3(1.0, 1.0, 1.0);
            for (int i = 0; i < yVertices.Length; i++) {
                GL.Vertex2(xVertices[i], yVertices[i]);
                GL.Vertex2(xVertices[i], -50);

                try {
                    GL.Vertex2(xVertices[i+1], yVertices[i+1]);
                    GL.Vertex2(xVertices[i + 1], -50);
                }
                catch(Exception e) {}
            }
            GL.End();

            window.SwapBuffers();
        }
        void resize(object sender, EventArgs args)  {

            GL.Viewport(0, 0, window.Width, window.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Matrix4 perspectiveMatrix = Matrix4.CreatePerspectiveFieldOfView(1, window.Width / window.Height, 1.0f, 1000.0f);
            GL.LoadMatrix(ref perspectiveMatrix);
            GL.MatrixMode(MatrixMode.Modelview);

            GL.End();

        }
        void load(object sender, EventArgs args) {
            GL.ClearColor(0, 0, 0, 0);
        }
    }
}
