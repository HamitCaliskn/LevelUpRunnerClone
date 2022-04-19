using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveInput : MonoBehaviour
{
    #region Singleton

    private static SwerveInput instance = null;

    public static SwerveInput Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("SwerveInput").AddComponent<SwerveInput>();
            }
            return instance;
        }
    }

    private void OnEnable()
    {
        instance = this;
    }

    #endregion

    public float MoveFactorX => _moveFactorX;
    public float HalfLaneSize = 3;

    private float referenceScreenWidth = 540;
    private float _moveFactorX;
    private float lastFingerPositionX;



    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            lastFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {

            _moveFactorX = (Input.mousePosition.x - lastFingerPositionX) / Screen.width * referenceScreenWidth;

            if (Mathf.Approximately(_moveFactorX, 0)) _moveFactorX = 0;

            lastFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0;
        }


#else

        Touch touch = Input.GetTouch(0);



        if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
        {
            _moveFactorX = 0;
        }
        else if (touch.phase == TouchPhase.Moved)
        {

            _moveFactorX = (touch.position.x - lastFingerPositionX) / Screen.width * referenceScreenWidth;
            if (Mathf.Approximately(_moveFactorX, 0))
                _moveFactorX = 0;
            lastFingerPositionX = touch.position.x;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            _moveFactorX = 0;
        }

#endif

    }

}



