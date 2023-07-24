using StatePattern.Main;
using StatePattern.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        public EnemyController Controller { get; private set; }
        [SerializeField] public NavMeshAgent Agent;
        private SphereCollider rangeTriggerCollider;
        [SerializeField] private SpriteRenderer detectableRange;
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private ParticleSystem bloodSplatter;

        private void Start()
        {
            rangeTriggerCollider = GetComponent<SphereCollider>();
            Controller?.InitializeAgent();
        }

        public void SetController(EnemyController controllerToSet) => Controller = controllerToSet;

        public void SetTriggerRadius(float radiusToSet)
        {
            SetRangeColliderRadius(radiusToSet);
            SetRangeImageRadius(radiusToSet);
        }

        private void SetRangeColliderRadius(float radiusToSet)
        {
            if (rangeTriggerCollider != null)
                rangeTriggerCollider.radius = radiusToSet;
        }

        private void SetRangeImageRadius(float radiusToSet) => detectableRange.transform.localScale = new Vector3(radiusToSet, radiusToSet, 1);

        public void PlayShootingEffect() => muzzleFlash.Play();

        private void Update() => Controller?.UpdateEnemy();

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerView>() != null && !other.isTrigger)
                Controller.PlayerEnteredRange(other.GetComponent<PlayerView>().Controller);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerView>() != null && !other.isTrigger)
                Controller.PlayerExitedRange();
        }

        public void Destroy() => StartCoroutine(EnemyDeathSequence());

        private IEnumerator EnemyDeathSequence()
        {
            bloodSplatter.Play();
            yield return new WaitForSeconds(0.1f);
            Destroy(gameObject);
        }

    }
}