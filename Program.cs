using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using System.Threading;
using SFML_Tutorial2;



namespace SFML_Tutorial
{
    class Program 
    {
        static ContextSettings context = new ContextSettings();
        static RenderWindow window;
        public Guid guid = new Guid();
        

        #region Drawing Stuff
        static Texture mouse_tex = new Texture("../Content/Mouse.png");
        static Texture anim_tex = new Texture("../Content/Ktest.png");
       // static RenderTexture tile1 = new RenderTexture(32, 32);
        

       
        static Sprite anim;
        static Sprite mouse_sprite;
        static CircleShape Sp;
        static Vector2f cxcy;
        static Music mymusic = new Music("../Content/11.-select-screen.ogg");
        static FloatRect myrect = new FloatRect(0, 0, 200, 200);
        static View myiew = new View(myrect);
        static int ydir;
        static int xdir;
        static int Posx = 22;
        static int Posy = 23;
        static RenderStates states;
        static TMap map = new TMap();


        /// Tile map stuff
    static  int[] numbers = new int[128] { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
        0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 2, 0, 0, 0, 0,
        1, 1, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2,
        0, 1, 0, 0, 2, 0, 2, 2, 2, 0, 1, 1, 1, 0, 0, 0,
        0, 1, 1, 0, 2, 2, 2, 0, 0, 0, 1, 1, 1, 2, 0, 0,
        0, 0, 1, 0, 2, 0, 2, 2, 0, 0, 1, 1, 1, 1, 2, 0,
        2, 0, 1, 0, 2, 0, 2, 2, 2, 0, 1, 1, 1, 1, 1, 1,
        0, 0, 1, 0, 2, 2, 2, 2, 0, 0, 0, 0, 1, 1, 1, 1};
    
       
    
        /// Tile Map Stuff

        static List<VertexArray> mine = new List<VertexArray>();
        
        #endregion


        static int Main(string[] args){

            SFML.Window.Vector2f tilesize = new SFML.Window.Vector2f(32, 32);

            map.load("../Content/Tiles_test.png", tilesize, numbers, 16, 8);



           // if (!)
          //  {
              //  return -1;

          //  }           
                Vector2f my2f = new Vector2f();
                for (int i = 0; i < 200; i++)
                {
                    VertexArray temp = new VertexArray(SFML.Graphics.PrimitiveType.Quads, 4);
                    for (uint index = 0; index < 4; index++)
                    {
                        my2f.X = Posx;
                        my2f.Y = Posy;
                        temp[index] = new Vertex(my2f);
                       

                    }
                    mine.Add(temp);
                }

                Console.WriteLine("The value of mine[0][0] is " + mine[0][0].Position.X +", "+  mine[0][0].Position.Y);
                Thread.Sleep(2000);    




            window = new RenderWindow(new VideoMode(1024, 768), "SFML IS FUN!", Styles.Default, context);
            window.Closed += window_Closed;
            Sp = new CircleShape(25);
            mymusic.Play();
            window.GainedFocus += window_GainedFocus;
            window.Resized += window_Resized;
            window.KeyPressed += window_KeyPressed;
            window.KeyReleased += window_KeyReleased;
            window.MouseMoved += window_MouseMoved;
            window.MouseEntered += window_MouseEntered;
            window.MouseLeft += window_MouseLeft;
            window.SetActive(true);
            #region Drawing Stuff
            mouse_sprite = new Sprite(mouse_tex);
            anim = new Sprite(anim_tex);
            anim.TextureRect = new IntRect(0, 0, 32, 32);
            int a = 0;
            xdir = 1;
            ydir = 1;
            window.SetFramerateLimit(60);
            cxcy = new Vector2f(25, 25);

            #endregion
            while (window.IsOpen())
            {

                window.DispatchEvents();
                window.Clear();
                #region Drawing Stuff
                anim.TextureRect = new IntRect(a * 24, 0, 24, 24);
                a++;

                if (xdir == 1) { cxcy.X += 21; }
                if (xdir == 2) { cxcy.X -= 21; }
                if (ydir == 1) { cxcy.Y += 21; }
                if (ydir == 2) { cxcy.Y -= 21; }


                if (a == 11) { a = 0; };

                mouse_sprite.Draw(window, RenderStates.Default);
                anim.Draw(window, RenderStates.Default);
                Sp.Position = cxcy;
               // anim.Color = new Color(5, 5, 5, 0);

                //Seems to use byte, byte, byte, byte format. Final byte is transparency. 128 is half , 256 is full.
                    //Doesn't seem to do much alpha blending and hard to see in different colors.


                //Sp.Draw(window, RenderStates.Default);
                #endregion
                window.Draw(map, RenderStates.Default);
                window.Display();
                

            }
            return 0;

        }

        static void window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {

        }

        static void window_MouseLeft(object sender, EventArgs e)
        {
            window.SetMouseCursorVisible(true);
        }

        static void window_MouseEntered(object sender, EventArgs e)
        {
            window.SetMouseCursorVisible(false);
        }

        static void window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            mouse_sprite.Position = new Vector2f(e.X, e.Y);
            if (cxcy.X < e.X) { xdir = 1; };
            if (cxcy.X > e.X) { xdir = 2; };
            if (cxcy.Y < e.Y) { ydir = 1; };
            if (cxcy.Y > e.Y) { ydir = 2; };



        }

        static void window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {

        }

        static void window_KeyReleased(object sender, KeyEventArgs e)
        {

            if (e.Code == Keyboard.Key.Escape)
            {

                window.Close();
            }

        }

        static void window_KeyPressed(object sender, KeyEventArgs e)
        {

        }

        static void window_Closed(object sender, EventArgs e)
        {
            window.Close();

        }

        static void window_GainedFocus(object sender, EventArgs e)
        {

        }

        static void window_Resized(object sender, EventArgs e)
        {

        }



    }
}
