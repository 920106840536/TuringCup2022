using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using turing;

public class Player4 : MonoBehaviour
{
    Player player4 = new Player();
    // Start is called before the first frame update
    void Start()
    {
        player4 = this.gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player4.CloseAttack(49, 24);
    }
}
