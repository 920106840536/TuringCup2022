using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class bullet_crash : MonoBehaviour
{
    private string owner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.tag != "Player" && other.gameObject.tag != "coins")
        {
            Destroy(gameObject);
        }*/
        if (other.gameObject.tag.Equals("obstacles"))
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag.Equals("Player") && !other.gameObject.name.Equals(owner))
        {
            if (other.gameObject.name.Equals("Player1"))
            {
                GameObject.Find("Player1").GetComponent<ConstScript>().hpDown(20);
            }
            if (other.gameObject.name.Equals("Player2"))
            {
                GameObject.Find("Player2").GetComponent<ConstScript>().hpDown(20);
            }
            if (other.gameObject.name.Equals("Player3"))
            {
                GameObject.Find("Player3").GetComponent<ConstScript>().hpDown(20);
            }
            if (other.gameObject.name.Equals("Player4"))
            {
                GameObject.Find("Player4").GetComponent<ConstScript>().hpDown(20);
            }

        }
    }

    public void setOwner(string o)
    {
        owner = o;
    }
}
