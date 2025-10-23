using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    public class CollisionManager
    {
        public bool enemyAttackedPlayer = false;
        Player player;
        Enemy enemy;
        Key key;
        EndGoal zelda;
        public static List<Enemy> PlayerAttackingEnemyCollision(Player player)
        {
            List<Enemy> removedEnemyList = new List<Enemy>();
            foreach (Enemy ene in Game1.enemyList)
            {
                if (player.attackHitbox.Intersects(ene.enemyHitbox))
                {
                    if (player.attacking)
                    {
                        Game1.score += 100;
                        removedEnemyList.Add(ene);
                    }
                }
            }
            foreach (Enemy ene in removedEnemyList)
            {
                Game1.enemyList.Remove(ene);
            }
            return removedEnemyList;
        }

        public static void PlayerTakingDamageFromEnemy(Player player, GameTime gameTime, List<Enemy> removedEnemyList)
        {
            float enemyInternalCoolDown = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (Enemy ene in Game1.enemyList)
            {
                // Skips this method for the enemy killed
                if(removedEnemyList.Contains(ene))
                {
                    continue;
                }

                ene.attackCoolDown -= enemyInternalCoolDown;
                if(ene.attackCoolDown < 0f)
                {
                    ene.attackCoolDown = 0f; //reset cooldown
                }

                if(player.playerHitbox.Intersects(ene.enemyHitbox))
                {
                    if(!player.attacking && ene.attackCoolDown <= 0f)
                    {
                        player.lives -= 1;
                        Game1.score -= 10;
                        ene.attackCoolDown = 1.0f;
                    }

                }
            }
        }

        public static void PlayerKey(Player player, Key key)
        {
            if (player.playerHitbox.Intersects(key.keyHitbox))
            {
                player.keyRetrieved = true;
            }
            if(player.keyRetrieved)
            {
                key.keyPos.X = player.position.X;
                key.keyPos.Y = player.position.Y -1;

                key.keyHitbox = new Rectangle( (int)key.keyPos.X, (int)key.keyPos.Y, Game1.tileSize, Game1.tileSize);
            }
        }

        public static void PlayerEndGoal(Player player, EndGoal zelda)
        {
            if (player.playerHitbox.Intersects(zelda.goalHitBox))
            {
                zelda.acheivedEndGoal = true;
                Game1.score += 1000;
            }
        }
        
    }
}
