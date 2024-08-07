using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBlink : MonoBehaviour
{
    [SerializeField] private GameObject[] blinkGameObjectList;
    [SerializeField] private float blinkTime;
        
    private float timeElapsed;

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.fixedUnscaledDeltaTime;

        if (timeElapsed > blinkTime)
        {
            timeElapsed = 0.0f;
            foreach (var gameObject in blinkGameObjectList)
            {
                gameObject.SetActive(!gameObject.activeInHierarchy);
            }
        }
    }
}
