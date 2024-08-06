using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private float _direction;
    private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        transform = this.GetComponent<Transform>();

        _direction = _speed * - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _change_head();
        }

        transform.localPosition += Vector3.up * _direction;
    }

    private void _change_head()
    {

        if(transform.rotation.z > 0)
        {
            transform.Rotate(0, 0, -50);
        }
        else
        {
            transform.Rotate(0, 0, 50);
        }


        _direction = _direction * -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Bug"))
        {
            Debug.Log("³È³È!!");
        }
        
    }
}
