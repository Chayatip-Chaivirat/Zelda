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
        public static void PlayerEnemyCollision(Player player)
        {
            List<Enemy> removedEnemyList = new List<Enemy>();
            foreach (Enemy ene in Game1.enemyList)
            {
                if (player.playerHitbox.Intersects(ene.enemyHitbox))
                {
                    if (player.attacking == false) //If player intersects with enemy without attacking
                    {
                        player.lives -= 1;
                    }
                    else //If player intersects with enemy and is attacking
                    {
                        Game1.score += 1;
                        removedEnemyList.Add(ene);
                    }
                }
            }
            foreach (Enemy ene in removedEnemyList)
            {
                Game1.enemyList.Remove(ene);
            }

            //Game1.enemyList.RemoveAll(ene =>
            //{
            //    if (player.playerHitbox.Intersects(ene.enemyHitbox))
            //    {
            //        if (player.attacking == false) //If player intersects with enemy without attacking
            //        {
            //            player.lives -= 1;
            //            return false;
            //        }
            //        else //If player intersects with enemy and is attacking
            //        {
            //            Game1.score += 1;
            //            removedEnemyList.Add(ene);
            //            return true;
            //        }
            //    }
            //    return false;
            //});
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
        
    }
}
