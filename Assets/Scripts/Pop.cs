using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Pop : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    private AudioSource audio;

    private void Awake()
    {
        audio = this.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        audio.Play();
        StartCoroutine("Hit");
    }

    IEnumerator Hit()
    {
        anim.SetTrigger("Pop");

        yield return new WaitForSeconds(0.3f);


        StopCoroutine("Hit");
        this.gameObject.SetActive(false);
    }
}
