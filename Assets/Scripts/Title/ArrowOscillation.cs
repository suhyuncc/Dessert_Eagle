using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowOscillation : MonoBehaviour
{
    [SerializeField]
    private float factorScale;
    [SerializeField]
    private bool _isVertical;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Oscillation");
    }
    
    private IEnumerator Oscillation()
    {

        while (true)
        {
            factorScale *= -1;
            
            if(_isVertical)
            {
                this.transform.position = new Vector3(this.transform.position.x 
                , this.transform.position.y - factorScale
                , this.transform.position.z);
            }
            else
            {
                this.transform.position = new Vector3(this.transform.position.x - factorScale
                , this.transform.position.y
                , this.transform.position.z);
            }
            
            // this.transform.localScale = new Vector3(this.transform.localScale.x - factorScale
            //     , this.transform.localScale.y
            //     , this.transform.localScale.z);
                
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
}
