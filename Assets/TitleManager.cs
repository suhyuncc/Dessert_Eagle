using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject eagle;
    [SerializeField] private GameObject boxStart;
    [SerializeField] private GameObject boxExit;
    
    private static TitleManager _titleManagerInstance;
    
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

    public void OnTriggerGameStart()
    {
        // 시작 애니메이션 추가 필요
        
        SceneManager.LoadScene("Bird");
    }

    public void OnTriggerGameExit()
    {
        // 종료 애니메이션 추가 필요
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
