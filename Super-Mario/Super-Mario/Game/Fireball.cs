﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Super_Mario
{
    class Fireball : DynamicObject
    {
        private Rectangle myDrawBox;
        private bool 
            myIsAlive,
            myDirection;
        private float 
            myRotation,
            myRotationSpeed;

        public bool IsAlive
        {
            get => myIsAlive;
            set => myIsAlive = value;
        }

        public Fireball(Vector2 aPosition, Point aSize, Vector2 aVelocity, Vector2 aVelocityThreshold, float aGravity, bool aDirection) : base(aPosition, aSize, aVelocity, aVelocityThreshold, aGravity)
        {
            this.myDirection = aDirection;

            this.myIsAlive = true;
            switch (myDirection)
            {
                case true:
                    this.myRotationSpeed = -15.0f; //Set value
                    break;
                case false:
                    this.myRotationSpeed = 15.0f; //Set value
                    break;
            }
        }

        public void Update(GameTime aGameTime)
        {
            base.Update();
            myDrawBox = new Rectangle(myBoundingBox.X + (int)myOrigin.X, myBoundingBox.Y + (int)myOrigin.Y, myBoundingBox.Width, myBoundingBox.Height);

            myRotation += myRotationSpeed * (float)aGameTime.ElapsedGameTime.TotalSeconds;

            switch (myDirection)
            {
                case true:
                    myCurrentVelocity.X = -(myVelocity.X * 60 * (float)aGameTime.ElapsedGameTime.TotalSeconds);
                    myPosition.X += myCurrentVelocity.X;
                    break;
                case false:
                    myCurrentVelocity.X = myVelocity.X * 60 * (float)aGameTime.ElapsedGameTime.TotalSeconds;
                    myPosition.X += myCurrentVelocity.X;
                    break;
            }

            Gravity(aGameTime);
            Collisions();
        }

        private void Collisions()
        {
            CollisionBlock();
            CollisionEnemy();
        }
        private void CollisionEnemy()
        {
            foreach (Enemy enemy in EnemyManager.Enemies)
            {
                if (!enemy.HasCollided)
                {
                    if (CollisionManager.Collision(myBoundingBox, enemy.BoundingBox))
                    {
                        enemy.IsAlive = false;
                        enemy.HasCollided = true;
                        myIsAlive = false;

                        GameInfo.AddScore(enemy.BoundingBox.Center.ToVector2(), 100);
                    }
                }
            }
        }
        private void CollisionBlock()
        {
            foreach (Tile tile in Level.TilesAround(this))
            {
                if (tile.IsBlock)
                {
                    if (CollisionManager.CheckBelow(myBoundingBox, tile.BoundingBox, myCurrentVelocity))
                    {
                        myPosition.Y = tile.BoundingBox.Y - mySize.Y;
                        myCurrentVelocity.Y = -myVelocityThreshold.Y;
                    }

                    if (CollisionManager.CheckLeft(myBoundingBox, tile.BoundingBox, myCurrentVelocity) && myDirection)
                    {
                        myIsAlive = false;
                    }
                    if (CollisionManager.CheckRight(myBoundingBox, tile.BoundingBox, myCurrentVelocity) && !myDirection)
                    {
                        myIsAlive = false;
                    }
                }
            }

            foreach (Tile tile in Level.TilesOnAndAround(this))
            {
                if (tile.IsBlock)
                {
                    if (CollisionManager.Collision(myBoundingBox, tile.BoundingBox))
                    {
                        myIsAlive = false;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(myTexture, myDrawBox, null, Color.White, myRotation, myOrigin, SpriteEffects.None, 0.0f);
        }

        public override void SetTexture(string aName)
        {
            myTexture = ResourceManager.RequestTexture(aName);
            SetOrigin(new Point(1, 1));
        }
    }
}