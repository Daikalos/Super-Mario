﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Super_Mario
{
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        State myGameState;

        public void ChangeState(State aNewState)
        {
            myGameState = null;

            myGameState = aNewState;
            myGameState.LoadContent();
        }

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 704;
            graphics.ApplyChanges();

            ResourceManager.Initialize();

            GameInfo.Initialize(0.5f, 0.4f);
            GameInfo.FolderLevels = "../../../../Levels/";
            GameInfo.FolderHighScores = "../../../../HighScores/";

            myGameState = new MenuState(this, Window);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ResourceManager.AddFont("8-bit", this.Content.Load<SpriteFont>("Fonts/8bit"));

            ResourceManager.AddTexture("Null", this.Content.Load<Texture2D>("Sprites/null_texture"));

            ResourceManager.AddTexture("Grass-00", this.Content.Load<Texture2D>("Tileset/tile-grass-00"));
            ResourceManager.AddTexture("Grass-01", this.Content.Load<Texture2D>("Tileset/tile-grass-01"));
            ResourceManager.AddTexture("Flag", this.Content.Load<Texture2D>("Tileset/flag"));
            ResourceManager.AddTexture("Ladder", this.Content.Load<Texture2D>("Tileset/ladder"));
            ResourceManager.AddTexture("Coin_Block", this.Content.Load<Texture2D>("Tileset/coin_block"));
            ResourceManager.AddTexture("Item_Block", this.Content.Load<Texture2D>("Tileset/item_block"));
            ResourceManager.AddTexture("Gravity_Block", this.Content.Load<Texture2D>("Tileset/gravity_block"));
            ResourceManager.AddTexture("Empty_Block", this.Content.Load<Texture2D>("Tileset/empty_block"));
            ResourceManager.AddTexture("Mushroom-00", this.Content.Load<Texture2D>("Tileset/mushroom-00"));
            ResourceManager.AddTexture("Mushroom-01", this.Content.Load<Texture2D>("Tileset/mushroom-01"));
            ResourceManager.AddTexture("FireFlower", this.Content.Load<Texture2D>("Tileset/fireflower"));

            ResourceManager.AddTexture("Mario_Walking", this.Content.Load<Texture2D>("Sprites/mario_walking"));
            ResourceManager.AddTexture("Mario_Jumping", this.Content.Load<Texture2D>("Sprites/mario_jumping"));
            ResourceManager.AddTexture("Mario_Climbing", this.Content.Load<Texture2D>("Sprites/mario_climbing"));
            ResourceManager.AddTexture("Mario_Idle", this.Content.Load<Texture2D>("Sprites/mario_idle"));
            ResourceManager.AddTexture("Mario_Death", this.Content.Load<Texture2D>("Sprites/mario_death"));

            ResourceManager.AddTexture("Mario-Super_Walking", this.Content.Load<Texture2D>("Sprites/mario-super_walking"));
            ResourceManager.AddTexture("Mario-Super_Jumping", this.Content.Load<Texture2D>("Sprites/mario-super_jumping"));
            ResourceManager.AddTexture("Mario-Super_Climbing", this.Content.Load<Texture2D>("Sprites/mario-super_climbing"));
            ResourceManager.AddTexture("Mario-Super_Idle", this.Content.Load<Texture2D>("Sprites/mario-super_idle"));

            ResourceManager.AddTexture("Mario-Fire_Walking", this.Content.Load<Texture2D>("Sprites/mario-fire_walking"));
            ResourceManager.AddTexture("Mario-Fire_Jumping", this.Content.Load<Texture2D>("Sprites/mario-fire_jumping"));
            ResourceManager.AddTexture("Mario-Fire_Climbing", this.Content.Load<Texture2D>("Sprites/mario-fire_climbing"));
            ResourceManager.AddTexture("Mario-Fire_Idle", this.Content.Load<Texture2D>("Sprites/mario-fire_idle"));
            ResourceManager.AddTexture("Fireball", this.Content.Load<Texture2D>("Sprites/fireball"));

            ResourceManager.AddTexture("Goomba_Walking", this.Content.Load<Texture2D>("Sprites/goomba_walking"));
            ResourceManager.AddTexture("Goomba_Death", this.Content.Load<Texture2D>("Sprites/goomba_death"));
            ResourceManager.AddTexture("Goomba_Editor", this.Content.Load<Texture2D>("Tileset/goomba_editor"));

            ResourceManager.AddTexture("Koopa_Walking", this.Content.Load<Texture2D>("Sprites/koopa_walking"));
            ResourceManager.AddTexture("Koopa_Death", this.Content.Load<Texture2D>("Sprites/koopa_death"));
            ResourceManager.AddTexture("Koopa_Editor", this.Content.Load<Texture2D>("Tileset/koopa_editor"));

            ResourceManager.AddTexture("Border", this.Content.Load<Texture2D>("Sprites/border"));
            ResourceManager.AddTexture("Background", this.Content.Load<Texture2D>("Sprites/background"));

            Background.SetTexture("Background");

            myGameState.LoadContent();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyMouseReader.Update();

            Background.Update();

            myGameState.Update(Window, gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DodgerBlue);

            spriteBatch.Begin();

            Background.Draw(spriteBatch);

            myGameState.Draw(spriteBatch, Window, gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
