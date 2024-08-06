using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if(this.transform.position.x < -153)
        {
            this.transform.position = new Vector3(306f, 0f, 0f);
        }

        this.transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
