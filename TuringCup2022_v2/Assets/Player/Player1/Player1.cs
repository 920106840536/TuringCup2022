using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using turing;
using System;
using UnityEngine.Assertions.Comparers;

public class Player1 : MonoBehaviour
{
    Player player1 = new Player();
    int num = 0;
    List<coin> cos = new List<coin>();
    string str;
    // Start is called before the first frame update
    void Start()
    {
        player1 = this.gameObject.GetComponent<Player>();
                
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player1.moveTo(2f, 8f);
        player my = player1.getMyself();
        float dis = Mathf.Sqrt((my.x - 2) * (my.x - 2) + (my.y - 8) * (my.y - 8));
        player1.log(Convert.ToString(dis));
    }


}
