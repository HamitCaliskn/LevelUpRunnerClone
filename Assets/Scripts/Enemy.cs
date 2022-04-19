using TMPro;
using UnityEngine;
using DG.Tweening;
using PathCreation.Examples;
using System.Collections;

public abstract class Enemy : MonoBehaviour // base - ana class 
{
    public bool isEnabled = true;
    public int Level;
    public bool isBoss = false;
    Rigidbody rigidbody;
    Rigidbody[] ragdollboides;
    Collider[] ragdollcolliders;
    Collider mainCollider;
    Animator animator;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        ragdollboides = GetComponentsInChildren<Rigidbody>();
        ragdollcolliders = GetComponentsInChildren<Collider>();
        animator = GetComponentInChildren<Animator>();
        mainCollider = GetComponent<Collider>();
        toggRagdoll(false);
    }

    public void GetDamage(int damegeCount)
    {
        if (!isEnabled) return;

        if (Level - damegeCount <= 0)
        {
            LevelManager.Instance.setAnimatorTrigger("attack");
            LevelManager.Instance.LevelUp(Level);
            ParticleManager.Instance.PlayParticleSystem(ParticleManager.Instance.DamageParticlePrefab, transform, 1);


            UIManager.Instance.HideGameObject(GetComponentInChildren<TextMeshPro>().gameObject);
            if (isBoss)
            {
                LevelManager.Instance.setAnimatorTrigger("idle");
                ThrowEnemy(2.5f,10,50);
                FindObjectOfType<PathFollower>().speed = 0;
                //CinemachineManager.Instance.SetCineMachineFollowTarget(transform);
               

            }
            else
            {
                GetComponentInChildren<Animator>().SetTrigger("die");
            }
            //Destroy(gameObject, 5f);

        }
        else
        {
            if (isBoss)
            {
                UIManager.Instance.ShowGameObject(UIManager.Instance.levelFailedPanel);
            }
            ParticleManager.Instance.PlayParticleSystem(ParticleManager.Instance.HitParticlePrefab, transform, 1);
            LevelManager.Instance.PushBack(5);
            LevelManager.Instance.LevelDown(Level);
        }

        isEnabled = false;

    }


    

    public void toggRagdoll(bool state)
    {
        if (state == true)
        {
            animator.enabled = !state;

        }
        for (int i = 0; i < ragdollboides.Length; i++)
        {
            ragdollboides[i].isKinematic = !state;
        }
        for (int i = 0; i < ragdollcolliders.Length; i++)
        {
            ragdollcolliders[i].enabled = state;
        }
        mainCollider.enabled = true;

    }
    
    public void ThrowEnemy(float time, float forceY, float forceZ)
    { 
        transform.DOMove(new Vector3(transform.position.x,transform.position.y+forceY,transform.position.z+forceZ),time);
        toggRagdoll(true);
    }

    IEnumerator waitAndShowLevelPassPanel()
    {
        yield return new WaitForSeconds(1f);
        UIManager.Instance.ShowGameObject(UIManager.Instance.levelPassPanel);
    }
#if UNITY_EDITOR
    private void OnValidate() // editor icinde yapýlan degisikleri anlýk olarak calisir
    {
        GetComponentInChildren<TextMeshPro>().text = Level.ToString();
    }
#endif

}
