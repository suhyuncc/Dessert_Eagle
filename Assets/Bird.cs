using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        transform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _change_gravity();
            _change_head();
        }

        
    }

    private void _change_gravity()
    {
        rigidbody2D.gravityScale *= -1;
    }

    private void _change_head()
    {
        Debug.Log(transform.eulerAngles.z);
        switch (transform.eulerAngles.z)
        {
            case 300:
                transform.Rotate(0, 0, -60);
                break;
            case 240:
                transform.Rotate(0, 0, 60);
                break;
        }
    }
}
