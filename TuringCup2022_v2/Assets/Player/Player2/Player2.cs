using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using turing;

public class Player2 : MonoBehaviour
{
    Player player2 = new Player();
    int num = 0;
    // Start is called before the first frame update
    void Start()
    {
        player2 = this.gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (num == 0)
        {
            player2.moveTo(5f, 48f);
            if (player2.getMyself().coins > 0)
            {
                num++;
            }
            else
            {
                player2.moveTo(48f, 48f);
            }
        }
        if (player2.getMyself().coins == 3)
        {
            player2.moveTo(33f, 46f);
        }
        if (num == 1 && player2.getMyself().coins!=3)
        {          
            player2.moveTo(48f, 48f);
            if (player2.getMyself().x >= 47)
            {
                player2.moveTo(49f, 49f);
            }
        }
        player2.ChangeType(2);
    }
}
