using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class basic : MonoBehaviour
{
    //分数和兵种
    public int score1, type1, hp1;
    public int score2, type2, hp2;
    public int score3, type3, hp3;
    public int score4, type4, hp4;
    //UI
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public Text text5;
    public Text text6;
    public Text text7;
    //BUFF
    public GameObject HpBuff1;
    private float HpBuff1_timeVal;
    private bool HpBuff1_restart;
    public GameObject HpBuff2;
    private float HpBuff2_timeVal;
    private bool HpBuff2_restart;
    public GameObject SpeedBuff1;
    private float SpeedBuff1_timeVal;
    private bool SpeedBuff1_restart;
    public GameObject SpeedBuff2;
    private float SpeedBuff2_timeVal;
    private bool SpeedBuff2_restart;
    //玩家
    public GameObject prefeb_player1;
    private bool player1_die;
    private float player1Die_timeVal;
    private bool player1_restart;
    private float player1Restart_timVal;
    public GameObject prefeb_player2;
    private bool player2_die;
    private float player2Die_timeVal;
    private bool player2_restart;
    private float player2Restart_timVal;
    public GameObject prefeb_player3;
    private bool player3_die;
    private float player3Die_timeVal;
    private bool player3_restart;
    private float player3Restart_timVal;
    public GameObject prefeb_player4;
    private bool player4_die;
    private float player4Die_timeVal;
    private bool player4_restart;
    private float player4Restart_timVal;

    private string[] textTxt;
    private int time = 90; //设定倒计时时间
    /*public GameObject player2;
    public GameObject player3;
    public GameObject player4;*/
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeTime()); //StartCoroutine()函数来调用协程函数

        //textTxt = File.ReadAllLines(Application.streamingAssetsPath + "/teams_name.txt");


        GameObject player1 = GameObject.Instantiate(prefeb_player1);
        player1.transform.position = new Vector3(1, 0, 1);
        player1.name = "Player1";

        GameObject player2 = GameObject.Instantiate(prefeb_player2);
        player2.transform.position = new Vector3(1, 0, 49);
        player2.name = "Player2";

        GameObject player3 = GameObject.Instantiate(prefeb_player3);
        player3.transform.position = new Vector3(49f, 0, 1);
        player3.name = "Player3";

        GameObject player4 = GameObject.Instantiate(prefeb_player4);
        player4.transform.position = new Vector3(49, 0, 49);
        player4.name = "Player4";

        GameObject h1 = GameObject.Instantiate(HpBuff1);
        h1.transform.position = new Vector3(3, 0, 14);
        h1.name = "HpBuff1";

        GameObject h2 = GameObject.Instantiate(HpBuff2);
        h2.transform.position = new Vector3(3, 0, 36);
        h2.name = "HpBuff2";

        GameObject h3 = GameObject.Instantiate(SpeedBuff1);
        h3.transform.position = new Vector3(30, 0, 15);
        h3.name = "SpeedBuff1";

        GameObject h4 = GameObject.Instantiate(SpeedBuff2);
        h4.transform.position = new Vector3(30, 0, 35);
        h4.name = "SpeedBuff2";

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //选手版本
        text1.text = "\n兵种：" + typeUI(type1) + "\nScore: " + score1 + "\nHp: " + hp1;
        text2.text = "\n兵种：" + typeUI(type2) + "\nScore: " + score2 + "\nHp: " + hp2;
        text3.text = "\n兵种：" + typeUI(type3) + "\nScore: " + score3 + "\nHp: " + hp3;
        text4.text = "\n兵种：" + typeUI(type4) + "\nScore: " + score4 + "\nHp: " + hp4;
        //比赛版本
        /*text1.text = "\n兵种：" + typeUI(type1) + "\nScore: " + score1 + "\nHp: " + hp1 + "\n" + textTxt[0];
        text2.text = "\n兵种：" + typeUI(type2) + "\nScore: " + score2 + "\nHp: " + hp2 + "\n" + textTxt[1];
        text3.text = "\n兵种：" + typeUI(type3) + "\nScore: " + score3 + "\nHp: " + hp3 + "\n" + textTxt[2];
        text4.text = "\n兵种：" + typeUI(type4) + "\nScore: " + score4 + "\nHp: " + hp4 + "\n" + textTxt[3];
        */
        //buff重生
        if (HpBuff1_restart)
        {
            HpBuff1_timeVal += Time.deltaTime;
            if (HpBuff1_timeVal >= 20.0f)
            {
                float x = Random.Range(2f, 8f);
                float y = Random.Range(14f, 20f);
                GameObject h1 = GameObject.Instantiate(HpBuff1);
                h1.transform.position = new Vector3(x, 0, y);
                h1.name = "HpBuff1";
                HpBuff1_timeVal = 0.0f;
                HpBuff1_restart = false;
            }
        }
        if (HpBuff2_restart)
        {
            HpBuff2_timeVal += Time.deltaTime;
            if (HpBuff2_timeVal >= 20.0f)
            {
                float x = Random.Range(2f, 8f);
                float y = Random.Range(31f, 37f);
                GameObject h2 = GameObject.Instantiate(HpBuff2);
                h2.transform.position = new Vector3(x, 0, y);
                h2.name = "HpBuff2";
                HpBuff2_timeVal = 0.0f;
                HpBuff2_restart = false;
            }
        }
        if (SpeedBuff1_restart)
        {
            SpeedBuff1_timeVal += Time.deltaTime;
            if (SpeedBuff1_timeVal >= 20.0f)
            {
                float x = Random.Range(26f,30f);
                float y = Random.Range(13f, 17f);
                GameObject h1 = GameObject.Instantiate(SpeedBuff1);
                h1.transform.position = new Vector3(x, 0, y);
                h1.name = "SpeedBuff1";
                SpeedBuff1_timeVal = 0.0f;
                SpeedBuff1_restart = false;
            }
        }
        if (SpeedBuff2_restart)
        {
            SpeedBuff2_timeVal += Time.deltaTime;
            if (SpeedBuff2_timeVal >= 20.0f)
            {
                float x = Random.Range(26f, 30f);
                float y = Random.Range(31f, 37f);
                GameObject h1 = GameObject.Instantiate(SpeedBuff2);
                h1.transform.position = new Vector3(x, 0, y);
                h1.name = "SpeedBuff2";
                SpeedBuff2_timeVal = 0.0f;
                SpeedBuff2_restart = false;
            }
        }
        //玩家死亡
        if (player1_die)
        {
            player1Die_timeVal += Time.deltaTime;
            if (player1Die_timeVal >= 0.1f)
            {
                player1Die_timeVal = 0f;
                Destroy(GameObject.Find("Player1"));
                player1_die = false;
                player1_restart = true;
            }
        }
        if (player2_die)
        {
            player2Die_timeVal += Time.deltaTime;
            if (player2Die_timeVal >= 0.1f)
            {
                player2Die_timeVal = 0f;
                Destroy(GameObject.Find("Player2"));
                player2_die = false;
                player2_restart = true;
            }
        }
        if (player3_die)
        {
            player3Die_timeVal += Time.deltaTime;
            if (player3Die_timeVal >= 0.1f)
            {
                player3Die_timeVal = 0f;
                Destroy(GameObject.Find("Player3"));
                player3_die = false;
                player3_restart = true;
            }
        }
        if (player4_die)
        {
            player4Die_timeVal += Time.deltaTime;
            if (player4Die_timeVal >= 0.1f)
            {
                player4Die_timeVal = 0f;
                Destroy(GameObject.Find("Player4"));
                player4_die = false;
                player4_restart = true;
            }
        }
        //玩家重生
        if (player1_restart)
        {
            player1Restart_timVal += Time.deltaTime;
            if (player1Restart_timVal >= 10f)
            {
                GameObject player1 = GameObject.Instantiate(prefeb_player1);
                player1.transform.position = new Vector3(1, 0, 1);
                player1.name = "Player1";
                player1Restart_timVal = 0f;
                player1_restart = false;
            }
        }
        if (player2_restart)
        {
            player2Restart_timVal += Time.deltaTime;
            if (player2Restart_timVal >= 10f)
            {
                GameObject player2 = GameObject.Instantiate(prefeb_player2);
                player2.transform.position = new Vector3(1, 0, 49);
                player2.name = "Player2";
                player2Restart_timVal = 0f;
                player2_restart = false;
            }
        }
        if (player3_restart)
        {
            player3Restart_timVal += Time.deltaTime;
            if (player3Restart_timVal >= 10f)
            {
                GameObject player3 = GameObject.Instantiate(prefeb_player3);
                player3.transform.position = new Vector3(49, 0, 1);
                player3.name = "Player3";
                player3Restart_timVal = 0f;
                player3_restart = false;
            }
        }
        if (player4_restart)
        {
            player4Restart_timVal += Time.deltaTime;
            if (player4Restart_timVal >= 10f)
            {
                GameObject player4 = GameObject.Instantiate(prefeb_player4);
                player4.transform.position = new Vector3(49, 0, 49);
                player4.name = "Player4";
                player4Restart_timVal = 0f;
                player4_restart = false;
            }
        }
    }
 
    //倒计时计数
    private IEnumerator ChangeTime()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);// 每次 自减1，等待 1 秒
            time--;
            text5.text = time + "s";
        }

        GameOver();
    }

    //游戏结束
    private void GameOver()
    {
        //判定倒计时结束时该做什么的方法
        Destroy(GameObject.Find("Player1"));
        Destroy(GameObject.Find("Player2"));
        Destroy(GameObject.Find("Player3"));
        Destroy(GameObject.Find("Player4"));
        text7.text = "Player1: " + score1 + "\n" + "Player2: " + score2 + "\n"
            + "Player3: " + score3 + "\n" + "Player4: " + score4 + "\n";
    }

    //buff消失计时开始
    public void buff_restart(string name)
    {
        if (name.Equals("HpBuff1"))
        {
            HpBuff1_restart = true;
        }
        if (name.Equals("HpBuff2"))
        {
            HpBuff2_restart = true;
        }
        if (name.Equals("SpeedBuff1"))
        {
            SpeedBuff1_restart = true;
        }
        if (name.Equals("SpeedBuff2"))
        {
            SpeedBuff2_restart = true;
        }
    }
    //角色死亡
    public void playerDie(string name)
    {
        if (name.Equals("Player1"))
        {
            player1_die = true;
        }
        if (name.Equals("Player2"))
        {
            player2_die = true;
        }
        if (name.Equals("Player3"))
        {
            player3_die = true;
        }
        if (name.Equals("Player4"))
        {
            player4_die = true;
        }
    }

    //debug
    public void setT6(string str)
    {
        text6.text = str;
    }
    public int getScore(string s)
    {
        if (s.Equals("Player1")) return score1;
        if (s.Equals("Player2")) return score2;
        if (s.Equals("Player3")) return score3;
        if (s.Equals("Player4")) return score4;
        else return 0;
    }
    public void setScore(string s, int sco)
    {
        if (s.Equals("Player1")) score1 = sco;
        if (s.Equals("Player2")) score2 = sco;
        if (s.Equals("Player3")) score3 = sco;
        if (s.Equals("Player4")) score4 = sco;
    }

    public void setHp(string s, int hp)
    {
        if (s.Equals("Player1")) hp1 = hp;
        if (s.Equals("Player2")) hp2 = hp;
        if (s.Equals("Player3")) hp3 = hp;
        if (s.Equals("Player4")) hp4 = hp;
    }

    public int getType(string s)
    {
        if (s.Equals("Player1")) return type1;
        if (s.Equals("Player2")) return type2;
        if (s.Equals("Player3")) return type3;
        if (s.Equals("Player4")) return type4;
        else return 0;
    }
    public void setType(string s, int sco)
    {
        if (s.Equals("Player1")) type1 = sco;
        if (s.Equals("Player2")) type2 = sco;
        if (s.Equals("Player3")) type3 = sco;
        if (s.Equals("Player4")) type4 = sco;
    }

    public string typeUI(int type)
    {
        string typeUI = "山贼";
        if (type == 0)
        {
            typeUI = "山贼";
        }
        if (type == 1)
        {
            typeUI = "弓箭手";
        }
        if (type == 2)
        {
            typeUI = "骑兵";
        }
        if (type == 3)
        {
            typeUI = "枪兵";
        }

        return typeUI;
    }
}
