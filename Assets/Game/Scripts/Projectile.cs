using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andremani.TwoDMultiplayerAndroidTest
{
    public class Projectile : MonoBehaviour
    {
        [Header("Options")]
        [SerializeField] private float speed = 5f;
        
        public GameObject Owner { get; set; }

        void Update()
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //can not collide with another projectiles due to CollisionMatrix
            if (Owner != collision.gameObject)
            {
                Destroy(gameObject);
                //Debug.Log("Hit");
            }
        }
    }
}