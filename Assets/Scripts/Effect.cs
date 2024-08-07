using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private SpriteRenderer sprite;
    private AudioSource audio;

    private void Awake()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        audio = this.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        audio.Play();
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
