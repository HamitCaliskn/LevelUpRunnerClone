
using UnityEngine;
using DG.Tweening;
using TMPro;
using PathCreation.Examples;

public class LevelManager : MonoBehaviour
{
    public int Level => _level;//getirin kýsa hali
    private int _level = 1;
    [Header("Player Size Settings")]
    [SerializeField] float maxPlayerSize;
    [SerializeField] float minPlayerSize;
    [SerializeField] float sizeDelay;
    [SerializeField] float scaleAmount;

    Animator animator;

    Vector3 targetSize = Vector3.one;



    private void Awake()
    {
        GetComponentInChildren<TextMeshPro>().text = _level.ToString();
        Instance = this;
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void PushBack(int distance)
    {
        transform.DOMoveZ(transform.position.z - distance, 1)
        .SetEase(Ease.OutSine);
    }

    public void LevelUp(int levelCount)
    {
        _level += levelCount;
        SetPlayerSize();
        SetLevelText();
        ParticleManager.Instance.PlayParticleSystem(ParticleManager.Instance.LevelUpParticlePrefab, transform, 1);

    }
    public void LevelDown(int levelCount)
    {
        if (Level - levelCount >= 1)
        {
            _level -= levelCount;
        }
        else
        {
            _level = 0;
            NotEnoughtLevel();
        }
        SetPlayerSize();
        SetLevelText();
        ParticleManager.Instance.PlayParticleSystem(ParticleManager.Instance.LevelDownParticlePrefab, transform, 1);
    }

    public void setAnimatorTrigger(string parametreReferance)
    {
        animator.SetTrigger(parametreReferance);
    }


    void SetPlayerSize()
    {

        targetSize = Vector3.one + (new Vector3(_level-1, _level-1, _level-1) * scaleAmount);

        if (targetSize.x <= maxPlayerSize && targetSize.x >= minPlayerSize)
        {
            transform.DOScale(targetSize*1.2f, sizeDelay/5).OnComplete(()=>transform.DOScale(targetSize,sizeDelay/5));
        }

    }

    public void NotEnoughtLevel()
    {
        if (_level <=0)
        {
            GetComponentInChildren<Animator>().SetTrigger("die");
            FindObjectOfType<PathFollower>().speed = 0f;
            GameManager.Instance.isStart = false;
            GameManager.Instance.isFailed = true;   
            UIManager.Instance.ShowGameObject(UIManager.Instance.levelFailedPanel);

        }
    }
    private void SetLevelText()
    {
        GetComponentInChildren<TextMeshPro>().text = Level.ToString();
    }



    public static LevelManager Instance;
}
