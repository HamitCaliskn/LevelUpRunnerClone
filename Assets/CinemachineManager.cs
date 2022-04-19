using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineManager : MonoBehaviour
{
    

    private void Awake()
    {
        Instance = this;
    }

    public void SetCineMachineFollowTarget(Transform transform)
    {
        CinemachineVirtualCamera CineMachineVC = GetComponent<CinemachineVirtualCamera>();
        CineMachineVC.Follow = transform;
        CineMachineVC.LookAt= transform;
       
    }

    public static CinemachineManager Instance;
}
