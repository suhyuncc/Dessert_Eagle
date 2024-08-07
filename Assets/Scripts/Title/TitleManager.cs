using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = System.Numerics.Vector3;

public class TitleManager : MonoBehaviour
{
    public bool isPlayerControllable = false;
    
    [SerializeField] private GameObject eagle;
    [SerializeField] private AudioSource soundBGM;
    [SerializeField] private GameObject boxExit;
    [SerializeField] private float speedExit;
    [SerializeField] private float speedStartPhaseA;
    [SerializeField] private float speedStartPhaseB;
    
    private static TitleManager _titleManagerInstance;
    private bool isGameReady = false;
    private bool isGameExit = false;
    private int isGameStart = 0;
    
    void Awake()
    {
        if (_titleManagerInstance == null)
            _titleManagerInstance = this;
        else
            Destroy(this.gameObject);
    }
    
    public static TitleManager Instance
    {
        get
        {
            if (null == _titleManagerInstance)
            {
                return null;
            }
            return _titleManagerInstance;
        }
    }

    void Update()
    {
        // if (isGameStart == 0) soundBGM.volume += 0.5f * Time.deltaTime;
        
        if (!isGameReady)
        {
            eagle.transform.Translate(UnityEngine.Vector3.right * speedStartPhaseA * Time.deltaTime);

            if (eagle.transform.position.x > 0.0f)
            {
                isGameReady = true;
                isPlayerControllable = true;
                boxExit.gameObject.SetActive(true);
            }
        }
        
        if (isGameExit)
        {
            eagle.transform.Translate(UnityEngine.Vector3.left * speedExit * Time.deltaTime);
        }

        if ((isGameStart > 0 || isGameExit) && soundBGM.volume > 0.2f)
        {
            soundBGM.volume -= 0.5f * Time.deltaTime;
        }
        
        if (isGameStart == 1)
        {
            eagle.transform.Translate(UnityEngine.Vector3.left * speedStartPhaseA * Time.deltaTime);
            if (eagle.transform.position.x < -1.0f)
            {
                eagle.GetComponent<TitleBird>().PlayEagleCrying();
                isGameStart = 2;
            }
        }
        else if (isGameStart == 2)
        {
            eagle.transform.Translate(UnityEngine.Vector3.right * speedStartPhaseB * Time.deltaTime);
        }
        
        if (isGameReady && eagle.transform.position.x < -10.25)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        else if (isGameReady && eagle.transform.position.x > 10.25)
        {
            SceneManager.LoadScene("Bird");
        }
    }

    public void OnTriggerGameStart()
    {
        if (isGameReady && isGameStart == 0)
        {
            isPlayerControllable = false;
            isGameStart = 1;
        
            // 접촉 판정 효과음
            
            // 독수리 발진 시 효과음 재생 필요
        }
    }

    public void OnTriggerGameExit()
    {
        if (isGameReady)
        {
            isPlayerControllable = false;
            isGameExit = true;
        }
        
        // 접촉 판정 효과음
        
        // 이후 즉시 독수리 효과음 재생 필요
    }
}
