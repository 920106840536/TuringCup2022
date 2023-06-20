using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int hp = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject.Find("Terrain").GetComponent<basic>().setHp(this.gameObject.name, this.hp);
    }
    
    public void hpDown(int h)
    {
        hp -= h;
    }
    public void hpUp(int h)
    {
        hp += h;
        if (hp >= 100)
        {
            hp = 100;
        }
    }
    public void hpSet(int h)
    {
        hp = h;
    }
    public int getHp()
    {
        return hp;
    }
}
