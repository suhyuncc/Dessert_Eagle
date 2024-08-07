using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowOscillation : MonoBehaviour
{
    private Transform arrowTransform;
    private float factorScale = 0.01f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Oscillation");;
    }
    
    private IEnumerator Oscillation()
    {
        float factorScale = 0.05f;
        float moveSum = 0;
        bool isExpanding = false;

        while (true)
        {
            if (moveSum < 0) factorScale *= -1;
            if (moveSum > 2) factorScale *= -1;
            
            this.transform.position = new Vector3(this.transform.position.x - factorScale
                , this.transform.position.y
                , this.transform.position.z);
            // this.transform.localScale = new Vector3(this.transform.localScale.x - factorScale
            //     , this.transform.localScale.y
            //     , this.transform.localScale.z);
            moveSum -= this.factorScale;
                
            yield return new WaitForSeconds(0.5f);
        }
    }
}
