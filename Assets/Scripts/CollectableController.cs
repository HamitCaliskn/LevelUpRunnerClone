using TMPro;
using UnityEngine;

public class CollectableController : MonoBehaviour
{

    public enum Symbol
    {
        positive,
        negative
    }
    public Symbol _symbol;

    [SerializeField] bool isGate = false;
    public int LevelCount;

    public void Collect()
    {

        switch (_symbol)
        {
            case Symbol.positive:
                LevelManager.Instance.LevelUp(LevelCount);
                break;
            case Symbol.negative:
                LevelManager.Instance.LevelDown(LevelCount);
                break;

        }
    }

#if UNITY_EDITOR
    private void OnValidate() // editor icinde yapýlan degisikleri anlýk olarak calisir
    {
        if (_symbol == Symbol.positive && isGate)
        {
            GetComponentInChildren<TextMeshPro>().text = "+" + LevelCount;
        }
        else if (_symbol == Symbol.negative && isGate)
        {
            GetComponentInChildren<TextMeshPro>().text = "-" + LevelCount;
        }
    }
#endif
}
