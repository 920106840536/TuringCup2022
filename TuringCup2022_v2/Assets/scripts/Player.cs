using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;


namespace turing
{
    //玩家
    /**
     * class: player
     * Member:
     *      float x
     *      float y
     *      int hp
     *      int type
     *      int coins
     *      
     */
    public class player
    {
        public float x;
        public float y;
        public int hp;
        public int type;
        public int coins;
        public void setPlayer(float x0,float y0,int hp0,int type0,int coins0)
        {
            x = x0;y = y0;hp = hp0;type = type0;coins = coins0;
        }
    }
    public class rate
    {
        public float x;
        public float y;

        public rate() { }
        public rate(float x0,float y0)
        {
            x = x0;y = y0;
        }
    }
    public class bullet
    {
        public float x;
        public float y;
        public rate bullet_rate;

        public bullet() { }
        public bullet(float x0,float y0,rate r0)
        {
            x = x0;y = y0;bullet_rate = r0;
        }
    }


    //敌人
    public class enemy
    {
        public string name;
        public float x;
        public float y;
        public int hp;
        public int type;
        public int coins;
        public rate rates;
        public void setEnemy(string name0, float x0, float y0, int hp0, int type0, int coins0, rate v0)
        {
            name = name0; x = x0; y = y0; hp = hp0; type = type0; coins = coins0;rates = v0;
        }
    }
    //金币
    public class coin
    {
        public float x;
        public float y;
        public void setCoin(float x0,float y0)
        {
            x = x0;
            y = y0;
        }
    }

    public class wall
    {
        public float x;
        public float y;
        public float xlength;
        public float ylength;
    }

    public class buff
    {
        public string buffName;
        public float x;
        public float y;
    }

    public class Player : MonoBehaviour
    {
        //系统变量
        public Rigidbody rd;
        public Animator anim;        
        public GameObject bulletPrefeb;
        public GameObject coins;
        //private CharacterController cc;

        //通用变量        
        private int type;//兵种
        private float attack_timeVal;//攻击间隔
        private float change_timeVal;
        private bool change_start = false;
        private int score;//分数
        private float speed = 2.0f;
        private int heritage;
        //近战变量
        private float CloseAttackRange = 60.0f;
        private float CloseAttackLength = 2.0f;
        private int Power = 5;
        //远程变量
        private int num = 1;
        private int ammo1 = 10;
        private int ammo2 = 10;

        private rate preRate;
        private rate nowRate;
        private bool pre_now;

        private float target_x;
        private float target_y;

        void Start()
        {
            rd = this.gameObject.GetComponent<Rigidbody>();
            anim = this.gameObject.GetComponent<Animator>();
            target_x = -1f;
            target_y = -1f;

            attack_timeVal = 0.5f;
            score = GameObject.Find("Terrain").GetComponent<basic>().getScore(this.gameObject.name);
            type = GameObject.Find("Terrain").GetComponent<basic>().getType(this.gameObject.name);
            RestartChangeType(type);
            //cc = this.gameObject.GetComponent<CharacterController>();
            heritage = 0;
            preRate = new rate();
            nowRate = new rate();
        }
        void Update()
        {
            //moveTo(25, 25);
            //float h = Input.GetAxis("Horizontal");
            //float v = Input.GetAxis("Vertical");
            // Debug.Log(h);
            //test_moveto(-v, h);
            

            if (attack_timeVal < 0.5f)
            {
                attack_timeVal += Time.deltaTime;
            }

            if (change_start)
            {
                change_timeVal += Time.deltaTime;
                if (change_timeVal >= 1.0f)
                {
                    if (num == 1)
                    {
                        num = 2;
                        change_timeVal = 0.0f;
                        change_start = false;
                    }
                    else
                    {
                        num = 1;
                        change_timeVal = 0.0f;
                        change_start = false;
                    }
                    
                }
            }

            if (!pre_now)
            {
                nowRate = getRate();
                pre_now = true;
            }
            else
            {
                preRate = nowRate;
                nowRate = getRate();
            }

            if (this.gameObject.GetComponent<ConstScript>().getHp() <= 0)
            {
                /*GameObject bullet = GameObject.Instantiate(bulletPrefeb, transform.position, transform.rotation);
                Rigidbody rd_bullet = bullet.GetComponent<Rigidbody>();
                rd_bullet.GetComponent<bullet_crash>().setOwner(this.gameObject.name);
                rd_bullet.velocity = new Vector3(v_x, 0, v_y);*/
                if (heritage == 0)
                {
                    score -= 5;
                    GameObject.Find("Terrain").GetComponent<basic>().setScore(this.gameObject.name, score);
                    GameObject.Find("Terrain").GetComponent<basic>().setType(this.gameObject.name, type);
                    Vector3 v1 = new Vector3(this.transform.position.x + 2, 0.5f, this.transform.position.z + 2);
                    GameObject coin1 = GameObject.Instantiate(coins);
                    coin1.transform.position = v1;

                    Vector3 v2 = new Vector3(this.transform.position.x + 2, 0.5f, this.transform.position.z - 2);
                    GameObject coin2 = GameObject.Instantiate(coins);
                    coin2.transform.position = v2;

                    Vector3 v3 = new Vector3(this.transform.position.x - 2, 0.5f, this.transform.position.z + 2);
                    GameObject coin3 = GameObject.Instantiate(coins);
                    coin3.transform.position = v3;

                    Vector3 v4 = new Vector3(this.transform.position.x - 2, 0.5f, this.transform.position.z - 2);
                    GameObject coin4 = GameObject.Instantiate(coins);
                    coin4.transform.position = v4;

                    heritage += 4;

                    GameObject.Find("Terrain").GetComponent<basic>().playerDie(this.gameObject.name);
                }
                //Destroy(this.gameObject);             
            }
            anim.SetFloat("timeAct", attack_timeVal); 
        }

        //通用
        //获取自己的信息
        /**
         * Function: getMyself
         *      get the infomation of player's solider
         * Return -> player
         */
        public player getMyself()
        {
            player p = new player();
            int h = GameObject.Find(this.gameObject.name).GetComponent<ConstScript>().getHp();
            p.setPlayer(this.transform.position.x, this.transform.position.z, h, type, score);
            return p;
        }
        //获取周围敌人的信息
        public List<enemy> getEnemy()
        {
            List<enemy> e = new List<enemy>();            
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");
            foreach (var enemy in enemies)
            {
                if (enemy.name.Equals(this.gameObject.name)) continue;
                enemy en = new enemy();
                float x = enemy.transform.position.x;
                float y = enemy.transform.position.z;
                float d_x = x - this.transform.position.x;
                float d_y = y - this.transform.position.z;
                int h = enemy.GetComponent<ConstScript>().getHp();
                int t = enemy.GetComponent<Player>().getType();
                int s = enemy.GetComponent<Player>().getScore();
                //rate r = enemy.GetComponent<Player>().getRate();
                rate r = enemy.GetComponent<Player>().preRate;
                en.setEnemy(enemy.name, x, y, h, t, s, r);
                if (Math.Sqrt(d_x * d_x + d_y * d_y) <= 10.0f)
                {
                    e.Add(en);
                }
            }
            return e;
        }
        //获取障碍物信息
        public List<wall> getWalls()
        {
            List<wall> walls = new List<wall>();
            GameObject[] coins = GameObject.FindGameObjectsWithTag("obstacles");
            foreach (var coin in coins)
            {
                wall now = new wall();
                Transform ts = coin.GetComponent<Transform>();
                MeshRenderer mr = coin.GetComponent<MeshRenderer>();
                /*Debug.Log(coin.name);
                Debug.Log(ts.position);
                Debug.Log(mr.bounds.size);*/
                Vector3 pos = ts.position;
                now.x = pos.x;
                now.y = pos.z;
                now.xlength = mr.bounds.size.x;
                now.ylength = mr.bounds.size.z;
                //Debug.Log(coin.name);
                //Debug.Log(now.x.ToString() + " " + now.y.ToString());
                walls.Add(now);
            }
            return walls;
        }

        //获取金币信息
        public List<coin> getCoin()
        {
            List<coin> c = new List<coin>();
            GameObject[] coins = GameObject.FindGameObjectsWithTag("coins");
            foreach(var coin in coins)
            {
                coin c0 = new coin();
                c0.setCoin(coin.transform.position.x, coin.transform.position.z);
                c.Add(c0);
            }
            return c;
        }
        //获取场上的子弹信息
        public List<bullet> getBullet()
        {
            List<bullet> lb = new List<bullet>();
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("bullet");
            foreach (var bullet in bullets)
            {
                bullet b = new bullet();
                b.x = bullet.transform.position.x;
                b.y = bullet.transform.position.z;
                b.bullet_rate = new rate(bullet.GetComponent<Rigidbody>().velocity.x, bullet.GetComponent<Rigidbody>().velocity.z);
                lb.Add(b);
            }
            return lb;
        }
        //获取场上的buff信息
        public List<buff> getBuff()
        {
            List<buff> lb = new List<buff>();
            GameObject[] buffs1 = GameObject.FindGameObjectsWithTag("HpBuff");
            foreach (var buff in buffs1)
            {
                buff b = new buff();
                b.buffName = "HpBuff";
                b.x = buff.transform.position.x;
                b.y = buff.transform.position.z;
                lb.Add(b);
            }
            GameObject[] buffs2 = GameObject.FindGameObjectsWithTag("SpeedBuff");
            foreach (var buff in buffs2)
            {
                buff b = new buff();
                b.buffName = "SpeedBuff";
                b.x = buff.transform.position.x;
                b.y = buff.transform.position.z;
                lb.Add(b);
            }
            return lb;
        }

        //向目标点方向移动
        public void moveTo(float x, float y)
        {
            target_x = x;
            target_y = y;
            rd.velocity = new Vector3(0, 0, 0);
            float target_dis = Mathf.Sqrt((this.transform.position.x - target_x) * (this.transform.position.x - target_x) + (this.transform.position.z - target_y) * (this.transform.position.z - target_y));
            if (target_dis<=0.001f)
            {
                anim.SetFloat("Speed", 0.0f);
                return;
            }
            transform.LookAt(transform.position + new Vector3(x, 0, y));
            float f_x = x - this.transform.localPosition.x;
            float f_y = y - this.transform.localPosition.z;         
            if (target_dis <= 0.05f)
            {
                float speed0 = target_dis*40;
                float ratio = speed0 / Mathf.Sqrt(f_x * f_x + f_y * f_y);
                f_x *= ratio;
                f_y *= ratio;
                Vector3 move = new Vector3(f_x, 0, f_y);
                anim.SetFloat("Speed", move.magnitude);
                rd.velocity = new Vector3(f_x, 0, f_y);
            }
            else
            {
                float ratio = this.speed / Mathf.Sqrt(f_x * f_x + f_y * f_y);
                f_x *= ratio;
                f_y *= ratio;
                Vector3 move = new Vector3(f_x, 0, f_y);
                anim.SetFloat("Speed", move.magnitude);
                rd.velocity = new Vector3(f_x, 0, f_y);
            }
            

            //var moveDirection = new Vector3(x, 0.0f, y);
            //moveDirection.Normalize();
            //moveDirection = moveDirection * speed;
            //cc.Move(moveDirection * Time.deltaTime);

        }
        //键盘控制移动
        public void test_moveto(float x,float y)
        {
            rd.velocity = new Vector3(0, 0, 0);
            Vector3 move = new Vector3(x, 0, y);
            transform.LookAt(transform.position + new Vector3(x, 0, y));
            anim.SetFloat("Speed", move.magnitude);
            rd.AddForce(new Vector3(x, 0, y), ForceMode.Impulse);
        }
        //近战
        public void CloseAttack(float x, float y)
        {
            if (attack_timeVal >= 0.5f)
            {
                rd.velocity = new Vector3(0, 0, 0);
                this.transform.forward = new Vector3(x - this.transform.position.x, 0, y - this.transform.position.z);
                
                Vector2 dir = new Vector2(x - this.transform.position.x, y - this.transform.position.z);
                
                GameObject[] AttackableGameObjects = GameObject.FindGameObjectsWithTag("Player");
                // AttackableGameObjects.AddRange(GameObject.FindGameObjectsWithTag("Attackable"));
                foreach (var AttackableGameObject in AttackableGameObjects)
                {
                    if (AttackableGameObject.name == this.gameObject.name) continue;
                    //Debug.Log(AttackableGameObject.transform.forward.ToString());
                    //Debug.Log(this.gameObject.transform.forward.ToString());
                    Vector2 dis = new Vector2(
                        AttackableGameObject.transform.position.x - this.transform.position.x,
                        AttackableGameObject.transform.position.z - this.transform.position.z
                    );
                    //Debug.Log(AttackableGameObject.name);
                    //Debug.Log(dis);
                    float angle = Vector2.Angle(dir, dis);
                    
                    //Debug.Log(angle);
                    //Debug.Log(Vector2.Distance(transform.position, AttackableGameObject.transform.position));
                    if (angle <= CloseAttackRange && Vector3.Distance(this.transform.position, AttackableGameObject.transform.position) <= CloseAttackLength)
                    {
                        ConstScript? constScript = AttackableGameObject.GetComponent<ConstScript>();
                        if (constScript == null) continue;
                        //Debug.Log(this.gameObject.name);
                        //Debug.Log(AttackableGameObject.name);
                        //Debug.Log(Vector3.Distance(this.transform.position, AttackableGameObject.transform.position));
                        //Debug.Log(this.transform.position.ToString());
                        //Debug.Log(AttackableGameObject.transform.position.ToString());
                        AttackableGameObject.GetComponent<ConstScript>().hpDown(Power);
                        
                    }
                }
                attack_timeVal = 0.0f;
            }
        }
        //远程
        /**
         * Fucntion: RemoteAttact
         *      进行远程攻击
         * Params:
         *      float x
         *          the target's x position
         *      float y
         */
        public void RemoteAttack(float x, float y)
        {
            if (type != 1) return;
            if (attack_timeVal >= 0.5f)
            {
                if (num == 1 && ammo1 > 0)
                {
                    rd.velocity = new Vector3(0, 0, 0);
                    float v_x = x - this.transform.localPosition.x;
                    float v_y = y - this.transform.localPosition.z;
                    float ratio = 30.0f / Mathf.Sqrt(v_x * v_x + v_y * v_y);
                    v_x *= ratio;
                    v_y *= ratio;

                    GameObject bullet = GameObject.Instantiate(bulletPrefeb, transform.position, transform.rotation);
                    Rigidbody rd_bullet = bullet.GetComponent<Rigidbody>();
                    rd_bullet.GetComponent<bullet_crash>().setOwner(this.gameObject.name);
                    rd_bullet.velocity = new Vector3(v_x, 0, v_y);
                    ammo1--;
                    attack_timeVal = 0.0f;
                }
                if (num == 2 && ammo2 > 0)
                {
                    rd.velocity = new Vector3(0, 0, 0);
                    float v_x = x - this.transform.localPosition.x;
                    float v_y = y - this.transform.localPosition.z;
                    float ratio = 20.0f / Mathf.Sqrt(v_x * v_x + v_y * v_y);
                    v_x *= ratio;
                    v_y *= ratio;

                    GameObject bullet = GameObject.Instantiate(bulletPrefeb, transform.position, transform.rotation);
                    Rigidbody rd_bullet = bullet.GetComponent<Rigidbody>();
                    rd_bullet.GetComponent<bullet_crash>().setOwner(this.gameObject.name);
                    rd_bullet.velocity = new Vector3(v_x, 0, v_y);
                    ammo2--;
                    attack_timeVal = 0.0f;
                }
                if (ammo1 <= 0 && ammo2 <= 0)
                {
                    CloseAttack(x, y);
                }
            }
        }
        //远程攻击：换弹匣
        public void reload()
        {
            if (type != 1) return;
            change_start = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("coins"))
            {
                Destroy(other.gameObject);
                score++;
                GameObject.Find("Terrain").GetComponent<basic>().setScore(this.gameObject.name, score);
            }
            if (other.gameObject.tag == "HpBuff")
            {
                GameObject.Find("Terrain").GetComponent<basic>().buff_restart(other.gameObject.name);
                Destroy(other.gameObject);
                this.GetComponent<ConstScript>().hpUp(30);
            }
            if (other.gameObject.tag == "SpeedBuff" && type!=2)
            {
                GameObject.Find("Terrain").GetComponent<basic>().buff_restart(other.gameObject.name);
                Destroy(other.gameObject);
                speed += 2.0f;
            }
        }


        public int getType()
        {
            return type;
        }
        public int getScore()
        {
            return score;
        }
        public rate getRate()
        {
            rate r = new rate(rd.velocity.x, rd.velocity.z);
            return r;
        }

        // Todo
        // 通过父节点的Transform计算子节点的世界坐标


        // 变化兵种
        /* Function: ChangeType
         *      Change the solider type
         * Params:
         *      int type
         *          target type
         * Return -> int
         *      1 Success
         *      0 Failed
         */
        public int ChangeType(int type)
        {
            if(this.type == type)
            {
                return 1;
            }
            if (type == 0)
            {
                // init solider
                initSolider();
                this.type = type;
                GameObject.Find("Terrain").GetComponent<basic>().setType(this.gameObject.name, type);
                return 1;
                
            }
            if(type == 1)
            {
                if(this.score < 10)
                {
                    return 0;
                }else
                {
                    score -= 10;
                    GameObject.Find("Terrain").GetComponent<basic>().setScore(this.gameObject.name, score);
                    // init archer
                    initArcher();
                    this.type = type;
                    GameObject.Find("Terrain").GetComponent<basic>().setType(this.gameObject.name, type);
                    return 1;
                }
            }
            if(type == 2)
            {
                if (this.score < 5)
                {
                    return 0;
                }
                else
                {
                    score -= 5;
                    GameObject.Find("Terrain").GetComponent<basic>().setScore(this.gameObject.name, score);
                    // init lancer
                    initRider();
                    this.type = type;
                    GameObject.Find("Terrain").GetComponent<basic>().setType(this.gameObject.name, type);
                    return 1;
                }
            }
            if(type == 3)
            {
                if (this.score < 5)
                {
                    return 0;
                }
                else
                {
                    score -= 5;
                    GameObject.Find("Terrain").GetComponent<basic>().setScore(this.gameObject.name, score);
                    // init rider
                    initLancer();
                    this.type = type;
                    GameObject.Find("Terrain").GetComponent<basic>().setType(this.gameObject.name, type);
                    return 1;
                }
            }
            return 1;
        }

        public void RestartChangeType(int type)
        {
            if (type == 0)
            {
                // init solider
                initSolider();
                this.type = type;
                GameObject.Find("Terrain").GetComponent<basic>().setType(this.gameObject.name, type);
            }
            if (type == 1)
            {
                GameObject.Find("Terrain").GetComponent<basic>().setScore(this.gameObject.name, score);
                // init archer
                initArcher();
                this.type = type;
                GameObject.Find("Terrain").GetComponent<basic>().setType(this.gameObject.name, type);
            }
            if (type == 2)
            {
                GameObject.Find("Terrain").GetComponent<basic>().setScore(this.gameObject.name, score);
                // init lancer
                initRider();
                this.type = type;
                GameObject.Find("Terrain").GetComponent<basic>().setType(this.gameObject.name, type);
            }
            if (type == 3)
            {
                GameObject.Find("Terrain").GetComponent<basic>().setScore(this.gameObject.name, score);
                // init rider
                initLancer();
                this.type = type;
                GameObject.Find("Terrain").GetComponent<basic>().setType(this.gameObject.name, type);
            }
        }


        //Debug
        public void log(string str)
        {
            GameObject.Find("Terrain").GetComponent<basic>().setT6(str);
        }

        //山贼
        private void initSolider()
        {
            this.CloseAttackRange = 60.0f;
            this.CloseAttackLength = 2f;
            this.Power = 5;
            this.speed = 2;
            // this.gameObject.GetComponent<ConstScript>().hpSet(100);
        }
        //弓箭手
        private void initArcher()
        {
            this.CloseAttackLength = 1.9f;
            this.CloseAttackRange = 50.0f;
            this.Power = 16;
            this.speed = 4;
        }
        //枪兵
        private void initLancer()
        {
            this.CloseAttackLength = 3.5f;
            this.CloseAttackRange = 30.0f;
            this.Power = 20;
            this.speed = 4;
        }
        //骑兵
        private void initRider()
        {
            this.speed = 8;
            this.CloseAttackRange = 60.0f;
            this.CloseAttackLength = 2f;
            this.Power = 10;
        }

        
    }
}

