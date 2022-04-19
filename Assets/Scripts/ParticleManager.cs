using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject CollectableParticlePrefab;
    public GameObject DamageParticlePrefab;
    public GameObject HitParticlePrefab;
    public GameObject LevelUpParticlePrefab;
    public GameObject LevelDownParticlePrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayParticleSystem(GameObject particle, Transform targetObject, float ofSetY)
    {
        Vector3 targetPos = targetObject.position;
        targetPos.y += ofSetY;

        Instantiate(particle, targetPos, Quaternion.identity);
    }

    public static ParticleManager Instance;
}
