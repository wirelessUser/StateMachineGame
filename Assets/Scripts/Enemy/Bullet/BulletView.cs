using StatePattern.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern.Enemy.Bullet
{
    public class BulletView : MonoBehaviour
    {
        private BulletController controller;

        public void SetController(BulletController controlleroSet) => controller = controlleroSet;

        private void Update() => controller.UpdateBullet();

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<PlayerView>() != null && !other.isTrigger)
                controller.HitPlayer(other.GetComponent<PlayerView>());
            Destroy(this.gameObject);
        }
    }
}