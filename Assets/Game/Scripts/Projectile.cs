using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class Projectile : NetworkBehaviour
    {
        [Header("Options")]
        [SerializeField] private float speed = 5f;
        [SerializeField] private float damage = 30f;
        
        public GameObject Owner { get; set; }

        [ServerCallback]
        void Update()
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
        }

        [ServerCallback]
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //can not collide with another projectiles due to CollisionMatrix
            if (Owner != collision.gameObject)
            {
                NetworkServer.UnSpawn(gameObject);
                Destroy(gameObject);
                //Debug.Log("Hit");

                if (collision.gameObject.TryGetComponent<Player>(out Player collidedPlayer))
                {
                    if(collidedPlayer.HealthSystem != null)
                    {
                        collidedPlayer.HealthSystem.RecieveDamage(damage);
                    }
                }
            }
        }
    }
}