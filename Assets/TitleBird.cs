using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TitleBird : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private float _direction;
    private Transform transformBird;

    // Start is called before the first frame update
    void Start()
    {
        transformBird = this.GetComponent<Transform>();

        _direction = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transformBird.Translate(Vector3.right * _direction);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transformBird.Translate(Vector3.left * _direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit.");
        
        if (other.gameObject.name == "BoxStart")
        {
            Debug.Log("Move to next scene.");
            TitleManager.Instance.OnTriggerGameStart();
        }
        else if (other.gameObject.name == "BoxExit")
        {
            Debug.Log("Game exit.");
            TitleManager.Instance.OnTriggerGameExit();
        }
    }
}