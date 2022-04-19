using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public int CurrentLevel = 1;
    public GameObject TapToPlay;
    public GameObject levelPassPanel;
    public GameObject levelFailedPanel;

    public TextMeshProUGUI SceneLevelText;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
        if (PlayerPrefs.HasKey("level")) // "level" isimli key var m� yok mu diye kontrol ediyor daha �nce kay�t etmediyse false d�ner
        {
            CurrentLevel = PlayerPrefs.GetInt("level"); // level isimli kay�tl� keyi getirme
            Debug.Log(PlayerPrefs.GetInt("level"));
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        SetSceneLevelText();
    }

    public void HideGameObject(GameObject HideObject)
    {
        HideObject.SetActive(false);
    }

    public void ShowGameObject(GameObject HideObject)
    {
        HideObject.SetActive(true);
    }
    public void NextLevel()
    {
        Debug.Log("test");

        int activeSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(activeSceneBuildIndex);
        Debug.Log(SceneManager.sceneCount);
        if (activeSceneBuildIndex + 1 >= SceneManager.sceneCount)
        {
            SceneManager.LoadScene(0);
            CurrentLevel++;
            PlayerPrefs.SetInt("level", CurrentLevel); // integer de�er kaydetme 

        }
        else
        {
            CurrentLevel++;
            PlayerPrefs.SetInt("level", CurrentLevel); // integer de�er kaydetme 
            SceneManager.LoadScene(activeSceneBuildIndex + 1);


        }
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetSceneLevelText()
    {
        SceneLevelText.text = "Level " + CurrentLevel;
    }

}
