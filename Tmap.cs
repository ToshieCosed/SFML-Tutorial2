using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;


namespace SFML_Tutorial2
{
    class TMap : SFML.Graphics.Transformable, SFML.Graphics.Drawable
    {
        static SFML.Graphics.VertexArray m_vertices;
        SFML.Graphics.Texture m_tileset;

        //Load the tileset manually because screw the boolean thingy



       public bool load(string tileset, SFML.Window.Vector2f tilesize, int[] tiles, uint width, uint height)
        {

            // tileimage.load
            SFML.Graphics.Texture m_tileset = new Texture(tileset);

            // SFML.Graphics tileimage = new Texture(



            //resize the vertex array to fit the level size
            m_vertices = new SFML.Graphics.VertexArray(SFML.Graphics.PrimitiveType.Quads, width * height * 4);

            //populate the vertex array, with one quad per tile

            for (uint i = 0; i < width; i++)
                for (uint j = 0; j < height; j++)
                {
                    //get the current tile number
                    int tileNumber = tiles[i + j * width];

                    if (tileNumber > 2) { tileNumber = 2; }

                    //find it's position in the tileset structure

                    //in C++ it uses ints but we can't use ints here because it won't convert implicitly from float to int
                    float tu = tileNumber % (m_tileset.Size.X / tilesize.X);
                    float tv = tileNumber / (m_tileset.Size.X / tilesize.X);

                    int gu = (int)tu;
                    int gv = (int)tv;


                    //get a ref to the current tile's quad


                    SFML.Graphics.Vertex quad0 = m_vertices[(i + j * width) * 4];
                    SFML.Graphics.Vertex quad1 = m_vertices[(i + j * width) * 4 + 1];
                    SFML.Graphics.Vertex quad2 = m_vertices[(i + j * width) * 4 + 2];
                    SFML.Graphics.Vertex quad3 = m_vertices[(i + j * width) * 4 + 3];


                    //Define it's 4 corners
                    quad0.Position = new SFML.Window.Vector2f(i * tilesize.X, j * tilesize.Y);
                    quad1.Position = new SFML.Window.Vector2f((i + 1) * tilesize.X, j * tilesize.Y);
                    quad2.Position = new SFML.Window.Vector2f((i + 1) * tilesize.X, (j + 1) * tilesize.Y);
                    quad3.Position = new SFML.Window.Vector2f(i * tilesize.X, (j + 1) * tilesize.Y);

                    //Define it's 4 texture coordinates
                    quad0.TexCoords = new SFML.Window.Vector2f(gu * tilesize.X, gv * tilesize.Y);
                    quad1.TexCoords = new SFML.Window.Vector2f((gu + 1) * tilesize.X, gv * tilesize.Y);
                    quad2.TexCoords = new SFML.Window.Vector2f((gu + 1) * tilesize.X, (gv + 1) * tilesize.Y);
                    quad3.TexCoords = new SFML.Window.Vector2f(gu * tilesize.X, (gv + 1) * tilesize.Y);

                    quad0.Color = new Color(0, 0, 0);
                    quad1.Color = new Color(0, 0, 0);
                    quad2.Color = new Color(0, 0, 0);
                    quad3.Color = new Color(0, 0, 0);

                    

                    

                }
            return true;

        }


        //private virtual void draw(SFML.Graphics.RenderTarget target, RenderStates states){

        //states.Transform = this.Transform;
        //states.Texture = m_tileset;
        //target.Draw(m_vertices, states);


        public void Draw(RenderTarget target, RenderStates states)
        {

            Console.WriteLine("Well it's trying to draw");
            // apply the transform
            states.Transform *= this.Transform;


           

            // apply the tileset texture
            states.Texture = m_tileset;

            // draw the vertex array
            target.Draw(m_vertices, states);

          

            // throw new NotImplementedException();
        }
    }


}










