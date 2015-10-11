using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;

        //  VertexPositionColor[] verts;
        VertexPositionNormalTexture[] verts;
        short[] indices; // declaring short to save memory . it is integer
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;// changing from colour to texture. Contain all the index for gpu to draw many many traiangles
        BasicEffect effect; // controls lighting or visual effects. This is basic effects
        

        MouseState currentMouse;
        MouseState prevMouseState;
        Matrix translation = Matrix.Identity; // every identity matrix = 1
        Matrix rotation = Matrix.Identity;
        Matrix scale = Matrix.Identity;
        
        

        Texture2D texture;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new Camera(this, new Vector3(0, 0, 5), Vector3.Zero, new Vector3(0, 1, 0));
            Components.Add(camera);


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // texture coordinates for front and top (using that jpeg picture)
            Vector2 front_topleft = new Vector2(0, 0); // top left hand corner of crate diagram
            Vector2 front_topright = new Vector2(0.5f, 0); //f = float
            Vector2 front_bottomright = new Vector2(0.5f, 0.5f);
            Vector2 front_bottomleft = new Vector2(0, 0.5f);


            //texture coordinates for back and bottom
            Vector2 back_topleft = new Vector2(0.5f, 0);
            Vector2 back_topright = new Vector2(1f, 0);
            Vector2 back_bottomleft = new Vector2(0.5f, 0.5f);
            Vector2 back_bottomright = new Vector2(1f, 0.5f);

            //texture coordinates for left side
            Vector2 left_topleft = new Vector2(0, 0.5f);
            Vector2 left_topright = new Vector2(0.5f, 0.5f);
            Vector2 left_bottomleft = new Vector2(0, 1);
            Vector2 left_bottomright = new Vector2(0.5f, 1);

            //texture coordinates for right side of cube
            Vector2 right_topleft = new Vector2(0.5f, 0.5f);
            Vector2 right_topright = new Vector2(1, 0.5f);
            Vector2 right_bottomleft = new Vector2(0.5f, 1);
            Vector2 right_bottomright = new Vector2(1f, 1f);


                                          

            // define normal - used for lighting effect. Normal are vectors which are perpendicular to the vertex. Think of the centre of cube as (0,0,0)
            Vector3 frontNormal = new Vector3(0, 0, 1); 
            Vector3 backNormal = new Vector3(0, 0, -1);
            Vector3 topNormal = new Vector3(0, 1, 0);
            Vector3 bottomNormal = new Vector3(0, -1, 0);
            Vector3 leftNormal = new Vector3(-1, 0, 0);
            Vector3 rightNormal = new Vector3(1, 0, 0);

                
            verts = new VertexPositionNormalTexture[24];

            // Adding vertices to the FRONT face
            verts[0] = new VertexPositionNormalTexture(new Vector3(-1, 1, 1), frontNormal, front_topleft);
            verts[1] = new VertexPositionNormalTexture(new Vector3(1, 1, 1), frontNormal, front_topright);
            verts[2] = new VertexPositionNormalTexture(new Vector3(1, -1, 1), frontNormal, front_bottomright);
            verts[3] = new VertexPositionNormalTexture(new Vector3(-1, -1, 1), frontNormal, front_bottomleft);

   
            //Adding vertices to the BACK face
            verts[4] = new VertexPositionNormalTexture(new Vector3(1, 1, -1), backNormal, back_topleft);
            verts[5] = new VertexPositionNormalTexture(new Vector3(-1, 1, -1), backNormal, back_topright);
            verts[6] = new VertexPositionNormalTexture(new Vector3(1, -1, -1), backNormal, back_bottomleft);
            verts[7] = new VertexPositionNormalTexture(new Vector3(-1, -1, -1), backNormal, back_bottomright);

            // Adding vertices to the BOTTOM face
            verts[8] = new VertexPositionNormalTexture(new Vector3(-1, -1, 1), bottomNormal, back_topleft);
            verts[9] = new VertexPositionNormalTexture(new Vector3(1, -1, 1), bottomNormal, back_topright);
            verts[10] = new VertexPositionNormalTexture(new Vector3(-1, -1, -1), bottomNormal, back_bottomleft);
            verts[11] = new VertexPositionNormalTexture(new Vector3(1, -1, -1), bottomNormal, back_bottomright);


            //Adding vertices to the TOP face
            verts[12] = new VertexPositionNormalTexture(new Vector3(-1, 1,-1), topNormal, front_topleft);
            verts[13] = new VertexPositionNormalTexture(new Vector3(1, 1, -1), topNormal, front_topright);
            verts[14] = new VertexPositionNormalTexture(new Vector3(-1, 1, 1), topNormal, front_bottomleft);
            verts[15] = new VertexPositionNormalTexture(new Vector3(1, 1, 1), topNormal, front_bottomright);

            //Adding vertices to the LEFT face
            verts[16] = new VertexPositionNormalTexture(new Vector3(-1, 1, -1), leftNormal, left_topleft);
            verts[17] = new VertexPositionNormalTexture(new Vector3(-1, 1, 1), leftNormal, left_topright);
            verts[18] = new VertexPositionNormalTexture(new Vector3(-1, -1, -1), leftNormal, left_bottomleft);
            verts[19] = new VertexPositionNormalTexture(new Vector3(-1, -1, 1), leftNormal, left_bottomright);


            //Adding vertices to the RIGHT face
            verts[20] = new VertexPositionNormalTexture(new Vector3(1, 1, 1), rightNormal, right_topleft);
            verts[21] = new VertexPositionNormalTexture(new Vector3(1, 1, -1), rightNormal, right_topright);
            verts[22] = new VertexPositionNormalTexture(new Vector3(1, -1, 1), rightNormal, right_bottomleft);
            verts[23] = new VertexPositionNormalTexture(new Vector3(1, -1, -1), rightNormal, right_bottomright);



            // Drawing two triangles requires four vertices, but six index entries if you are using a PrimitiveType.TriangleList. The indices are specified in clockwise order. 
            //XNA is a right-handed coordinate system so triangles drawn in counter-clockwise order are assumed to be facing away from the camera which causes them to be culled. 
            // Below is using a triangle list
            /* A triangle list will take the first three vertices specified and build a triangle out of them.
                It will then build an additional triangle out of each additional set of three vertices, as
                shown in Figure 9-12. The triangle list is perhaps the least complicated way to draw
                triangles, but it’s also the least efficient, as you have to specify three new vertices for
                each triangle drawn */


            indices = new short[36];

            // FRONT of cube. NOTE: First number indices is an identifier. After = sign is referring to verts above.
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 0;
            indices[4] = 2; 
            indices[5] = 3;

            // LEFT of cube
            indices[6] = 16;
            indices[7] = 17;
            indices[8] = 19;
            indices[9] = 16;
            indices[10] = 19;
            indices[11] = 18;


            //BACK of cube
            indices[12] = 4;
            indices[13] = 5;
            indices[14] = 7;
            indices[15] = 4;
            indices[16] = 7;
            indices[17] = 6;


            // RIGHT of cube
            indices[18] = 20;
            indices[19] = 21;
            indices[20] = 23;
            indices[21] = 20;
            indices[22] = 23;
            indices[23] = 22;


            // BOTTOM of cube
            indices[24] = 8;
            indices[25] = 9;
            indices[26] = 11;
            indices[27] = 8;
            indices[28] = 11;
            indices[29] = 10;


            // TOP of cube
            indices[30] = 12;
            indices[31] = 13;
            indices[32] = 15;
            indices[33] = 12;
            indices[34] = 15;
            indices[35] = 14;


            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionNormalTexture), verts.Length, BufferUsage.None);

           

            //   vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor),verts.Length, BufferUsage.None);//bufferusage is how the pc will buffer 'none' = write and read
            //so gpu knows what color you are joining // if you join stuff , you normally need to pass your graphics.
            vertexBuffer.SetData(verts);

            indexBuffer = new IndexBuffer(GraphicsDevice, IndexElementSize.SixteenBits, 
                sizeof(short) * indices.Length, BufferUsage.None); // entire size

            indexBuffer.SetData(indices);
            GraphicsDevice.Indices = indexBuffer; // setting buffer to graphics device

            // initialising effect - handling visual effect and translation / rotation
            effect = new BasicEffect(GraphicsDevice);

            texture = Content.Load<Texture2D>(@"Textures/Crates"); // loading texture


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // Mouse click and rotation on X and Y axis.
            currentMouse = Mouse.GetState();
            if (currentMouse.LeftButton == ButtonState.Pressed && currentMouse.X != prevMouseState.X && currentMouse.Y != prevMouseState.Y ) //mouse input and second condition is the movement of your mouse
                

                    {
                        int x = currentMouse.X;
                        float xDifference = currentMouse.X - prevMouseState.X; // to provide left or right rotation on x
                        rotation *= Matrix.CreateRotationY(MathHelper.PiOver4/150 * xDifference) ; // because fps very high divide by a large number
                                                                                                


                        int y = currentMouse.Y;
                        float yDifference = currentMouse.Y - prevMouseState.Y; // to provide left or right rotation on x
                        rotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / 150 * yDifference);


                
                    }

            

              if (currentMouse.ScrollWheelValue != prevMouseState.ScrollWheelValue && ((currentMouse.ScrollWheelValue - prevMouseState.ScrollWheelValue) > 0))

                    {

                                // Positive scaling of shape. Making it bigger
                                scale *= Matrix.CreateScale(1.1f);
                                prevMouseState = currentMouse;
                    }

            if (currentMouse.ScrollWheelValue != prevMouseState.ScrollWheelValue && ((currentMouse.ScrollWheelValue - prevMouseState.ScrollWheelValue) < 0))

                    {

                                // Negative scaling of shape. Making it smaller
                                scale *= Matrix.CreateScale(0.9f); // scaling you do not use negatives, you use < 1 values.
                                prevMouseState = currentMouse;
                    }

            prevMouseState = currentMouse;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.SetVertexBuffer(vertexBuffer); // tells which buffer you want to use. THis tells the gpu

            //controls behavioural of project on screen
            // effect.World = Matrix.Identity;
            effect.World = scale * rotation;
            effect.View = camera.view;
            effect.Projection = camera.projection;
            // effect.VertexColorEnabled = true; // enabling colour
            effect.VertexColorEnabled = false; // enabling colour
            effect.Texture = texture;
            effect.TextureEnabled = true;

            effect.EnableDefaultLighting();
            effect.LightingEnabled = true; // enabling lighting effect - Lab09 demostration
            
            // operation on pixel and rendering images
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                //  GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList,0,1); //primitive count = how many triangle you want to draw
                // GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4,0, 2); // list

                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,0,0,24,0,12);


            }
            // TODO: Add your drawing code here
            // Lab09 Demonstration with branching and merging
            // Lab09 Demonstration with branching and merging Part II

            // Lab09 Demonstration with branching and merging Part III

            base.Draw(gameTime);
        }
    }
}

