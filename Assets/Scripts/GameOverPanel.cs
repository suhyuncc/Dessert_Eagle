using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    public void btnTest()
    {
        Debug.Log("Active");
    }
}
