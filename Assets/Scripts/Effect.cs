using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = this.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine("PadeOut");
    }

    IEnumerator PadeOut()
    {
        float alpha = 1.0f;

        while (alpha > 0.01f)
        {
            alpha -= Time.deltaTime * 2;
            sprite.color = new Color(1f,1f,1f,alpha);
            yield return null;
        }

        this.gameObject.SetActive(false);
    }
}
