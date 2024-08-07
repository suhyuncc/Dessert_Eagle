using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float Rate;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        speed = GameManager.instance.Earth_speed * Rate;

        if (this.transform.position.x < -153)
        {
            this.transform.position = new Vector3(295f, 0f, 0.0f);
        }

        this.transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
