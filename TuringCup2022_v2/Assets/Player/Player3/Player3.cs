using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using turing;

public class Player3 : MonoBehaviour
{
    Player player3 = new Player();
    // Start is called before the first frame update
    void Start()
    {
        player3 = this.gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player3.moveTo(10.9f, 0);
    }
}
