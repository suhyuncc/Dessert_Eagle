using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowEffect : MonoBehaviour
{
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("Rainbow");
    }

    IEnumerator Rainbow()
    {
        float ran = 0.0f;

        while (true)
        {
            ran += Time.deltaTime;
            sprite.color = Color.HSVToRGB((ran - (int)ran), 1.0f, 1.0f);
            yield return null;
        }
        
    }
}
