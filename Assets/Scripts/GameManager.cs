using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;

public class GameManager : MonoBehaviour
{
    //dokunca ekrana oyun baþlama komutu
    public  bool isStart=false ;
    public bool isFailed = false;

    public float speed = 5f;
    private void Awake()
    {
        Instance = this;
    }
     void Update()
    {
        if (Input.GetMouseButton(0)&& !isStart && !isFailed)
        {
            isStart = true;
            FindObjectOfType<PathFollower>().speed = speed;
            LevelManager.Instance.setAnimatorTrigger("run");
            UIManager.Instance.HideGameObject(UIManager.Instance.TapToPlay);
        }
    }









    public static GameManager Instance;

    

}
