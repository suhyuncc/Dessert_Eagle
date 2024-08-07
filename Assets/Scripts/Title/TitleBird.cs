using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleBird : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private AudioClip eagleCrying;
    [SerializeField] private AudioClip eagleReload;
    
    private float _direction;
    private Transform transformBird;
    private bool isBeingHeld = false;
    private float startPosX;
    private float startPosY;
    private AudioSource eagleAudioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        transformBird = this.GetComponent<Transform>();

        _direction = _speed;

        eagleAudioSource = this.gameObject.GetComponent<AudioSource>();
        this.eagleAudioSource.Play();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().PlayDelayed(1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isBeingHeld)
        {
            if (TitleManager.Instance.isPlayerControllable)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transformBird.position = new Vector2(mousePos.x - startPosX, startPosY);
            }
        }
    }

    public void PlayEagleCrying(AudioClip clip)
    {
        eagleAudioSource.PlayOneShot(clip);
    }
    
    public void PlayEagleCrying()
    {
        eagleAudioSource.PlayOneShot(eagleCrying);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit.");
        
        if (other.gameObject.name == "BoxStart")
        {
            Debug.Log("Move to next scene.");
            TitleManager.Instance.OnTriggerGameStart();
            other.gameObject.SetActive(false);
            PlayEagleCrying(eagleReload);
        }
        else if (other.gameObject.name == "BoxExit")
        {
            Debug.Log("Game exit.");
            TitleManager.Instance.OnTriggerGameExit();
            PlayEagleCrying();
        }
    }
    
    private void OnMouseDown() 
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            startPosX = mousePos.x - this.transform.position.x;
            startPosY = this.transform.position.y;

            isBeingHeld = true;
        }
    }

    private void OnMouseUp() 
    {
        isBeingHeld = false;
    }
}