using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TitleGround : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        transform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Vector3.left * speed;

        if(transform.position.x < -25)
        {
            transform.position = new Vector3(32,0,0);
        }
    }
}