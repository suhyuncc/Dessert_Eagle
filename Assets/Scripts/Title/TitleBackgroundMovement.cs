using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBackgroundMovement : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if(this.transform.position.x < -25)
        {
            this.transform.position = new Vector3(25f, 0f, 0f);
        }

        this.transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
