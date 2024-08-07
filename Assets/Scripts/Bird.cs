using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _angle;
    [SerializeField]
    private Collider2D _upCollider;
    [SerializeField]
    private Collider2D _downCollider;
    private float _direction;
    private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        transform = this.GetComponent<Transform>();

        _direction = - 1;
        _downCollider.enabled = true;
        _upCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        _speed = GameManager.instance.Eagle_speed;
        _angle = GameManager.instance.Eagle_angle;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _change_head();
        }

        transform.localPosition += Vector3.up * _direction * _speed * Time.deltaTime;
    }

    private void _change_head()
    {
        if(transform.rotation.z > 0)
        {
            transform.Rotate(0, 0, -2 * _angle);
            _downCollider.enabled = true;
            _upCollider.enabled = false;
        }
        else
        {
            transform.Rotate(0, 0, 2 * _angle);
            _downCollider.enabled = false;
            _upCollider.enabled = true;
        }

        _direction = _direction * -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Sky":
                _change_head();
                break;

            case "Ground":
                _change_head();
                GameManager.instance.GetDamage();
                break;
        }


    }

    public void Reposition(float angle)
    {

        if (transform.rotation.z > 0)
        {
            transform.rotation = Quaternion.identity;
            transform.Rotate(0, 0, angle);
        }
        else
        {
            transform.rotation = Quaternion.identity;
            transform.Rotate(0, 0, -1 * angle);
        }
        

    }
}
