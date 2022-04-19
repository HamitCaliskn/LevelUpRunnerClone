using UnityEngine;
using DG.Tweening;
using TMPro;

public class CharacterCollision : MonoBehaviour
{
    Sequence comeback;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Collectable"))
        {
            ParticleManager.Instance.PlayParticleSystem(ParticleManager.Instance.CollectableParticlePrefab, transform, 1);
            other.GetComponent<CollectableController>().Collect();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Door"))
        {
            other.GetComponent<CollectableController>().Collect();
            ParticleManager.Instance.PlayParticleSystem(ParticleManager.Instance.CollectableParticlePrefab, transform, 1);
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            ParticleManager.Instance.PlayParticleSystem(ParticleManager.Instance.HitParticlePrefab, transform, 1);

            LevelManager.Instance.PushBack(5);
            LevelManager.Instance.LevelDown(3);
        }
        else if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.GetDamage(LevelManager.Instance.Level);

            if (!enemy.isBoss)
            {
                enemy.toggRagdoll(true);

            }
            else
            {

                UIManager.Instance.HideGameObject(GetComponentInChildren<TextMeshPro>().gameObject);
            }


        }
    }
}
