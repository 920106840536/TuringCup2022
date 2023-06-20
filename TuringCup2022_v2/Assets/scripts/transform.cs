using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("obstacles"))
        {
            Destroy(this.gameObject);
        }
    }
}
