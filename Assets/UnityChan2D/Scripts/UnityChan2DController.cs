using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(BoxCollider2D))]

//書き足したところとかにコメントを入れていきます。
//このスクリプトはメインとしてはキャラ操作ですが割と他のことも行っています。

public class UnityChan2DController : MonoBehaviour{
    public float maxSpeed = 10f;    //移動最大速度(GUIで設定)
    public float jumpPower = 1000f; //ジャンプ力(GUIで設定)
    public Vector2 backwardForce = new Vector2(-4.5f, 5.4f);

    public LayerMask whatIsGround;

    private Animator m_animator;
    private BoxCollider2D m_boxcollier2D;
    private Rigidbody2D m_rigidbody2D;
    private bool m_isGround;
    private const float m_centerY = 1.5f;

    private State m_state = State.Normal;

    public int hitcount;        //何回敵にあたったかを保管(後述)
    public int life;            //残基数を保管(後述)
    public GameObject se;       //SE鳴らすためのもの

    public bool die = false;    //死んだかの判定

    public bool dieplayed = false;
    public bool goplayed = false;
    
    void Reset(){
        Awake();

        // UnityChan2DController
        maxSpeed = 10f;
        jumpPower = 1000;
        backwardForce = new Vector2(-4.5f, 5.4f);
        whatIsGround = 1 << LayerMask.NameToLayer("Ground");

        // Transform
        transform.localScale = new Vector3(1, 1, 1);

        // Rigidbody2D
        m_rigidbody2D.gravityScale = 3.5f;
        m_rigidbody2D.fixedAngle = true;

        // BoxCollider2D
        m_boxcollier2D.size = new Vector2(1, 2.5f);
        m_boxcollier2D.offset = new Vector2(0, -0.25f);

        // Animator
        m_animator.applyRootMotion = false;
    }

    void Awake(){
        m_animator = GetComponent<Animator>();
        m_boxcollier2D = GetComponent<BoxCollider2D>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();

        hitcount = count.gethit();  //count.csから現在のあたった回数を拾ってきて、前ワールドの状態を引き継ぐ
        life = count.getlife();     //↑の残基版
    }

    void Update(){
        life = count.getlife();     //引き継いではいるんだけど、コイン100枚採ったとき用に...
        hitcount = count.gethit();  //↑+普通にあたったとき用

        if (scoresaver.getcoin() == "100"){ //もし100枚とったら・・・
            life++;                         //残基増やして
            count.setlife(life);            //それを書き込んで
            scoresaver.setcoin("0");        //コイン枚数を0に戻す
        }

        if (m_state != State.Damaged){      //ダメージ受けてない間・・・
            float x = Input.GetAxis("Horizontal");      //ここの挙動はQiitaにあるので省略
            bool jump = Input.GetButtonDown("Jump");    //同上
            if (!die){                                  //まだ死んでなかったら・・・
                Move(x, jump);                          //Move関数使って動かす
            } else {                                    //死んでたら・・・
                Move(0, false);                         //動きを止める(キー操作無効のため)
            }
        }

        //↓ここはUpdateに振り回されないようにする必要があるかもしれない。
        if ((hitcount == 3) || (clearflag.istimeover())){   //もし敵に3回あたったら
            die = true;         //死亡判定ON
            count.sethit(3);    //count.csに3回あたったことを一応記録
            if (count.getlife() == 0){                          //もし残基が0だったら・・・(先にゲームオーバー時の処理)
                if ( (!se.GetComponent<playse>().dieplaying()) && (dieplayed) ){       //かつ死亡ジングルが流れ終わってたら(死亡ジングルはOnTriggerStay2Dで流しています)
                    se.GetComponent<playse>().playgameover();       //ゲームオーバージングルを流す
                    goplayed = true;
                }
                if ( (!se.GetComponent<playse>().gameoverplaying()) && (goplayed) ){  //もしゲームオーバージングルが流れ終わってたら
                    SceneManager.LoadScene ("Result");              //Resultシーンを呼び出します。
                }
            } else {                                            //まだ残基に余裕があったら
                if ( (!se.GetComponent<playse>().dieplaying()) && (dieplayed) ){       //死亡ジングルがなり終わっていたら
                    count.sethit(0);                                //hit数を0に戻して、
                    SceneManager.LoadScene ("Loading " + nowworld.getworld()); //もう一回同じマップを呼び出す。
                }
            }
            
        }
        
    }

    void Move(float move, bool jump){
        if (Mathf.Abs(move) > 0){
            Quaternion rot = transform.rotation;
            transform.rotation = Quaternion.Euler(rot.x, Mathf.Sign(move) == 1 ? 0 : 180, rot.z);
        }

        m_rigidbody2D.velocity = new Vector2(move * maxSpeed, m_rigidbody2D.velocity.y);

        m_animator.SetFloat("Horizontal", move);
        m_animator.SetFloat("Vertical", m_rigidbody2D.velocity.y);
        m_animator.SetBool("isGround", m_isGround);

        if (jump && m_isGround){
            m_animator.SetTrigger("Jump");
            SendMessage("Jump", SendMessageOptions.DontRequireReceiver);
            m_rigidbody2D.AddForce(Vector2.up * jumpPower);
        }
    }

    void FixedUpdate(){
        Vector2 pos = transform.position;
        Vector2 groundCheck = new Vector2(pos.x, pos.y - (m_centerY * transform.localScale.y));
        Vector2 groundArea = new Vector2(m_boxcollier2D.size.x * 0.49f, 0.05f);

        m_isGround = Physics2D.OverlapArea(groundCheck + groundArea, groundCheck - groundArea, whatIsGround);
        m_animator.SetBool("isGround", m_isGround);
    }

    void OnTriggerStay2D(Collider2D other){     //敵にあたったとき
        if (other.tag == "DamageObject" && m_state == State.Normal)
        {
            hitcount++;                 //hitcountを増やす
            count.sethit(hitcount);     //それを記録する
            
            if (hitcount == 3){         //3回あたってたら
                life--;                 //残基減らして
                Debug.Log("Life=" + life);
                count.setlife(life);    //それを記録
                
                // 死亡ジングル音を鳴らす
                se.GetComponent<playse>().playdie();
                dieplayed = true;
            }


            m_state = State.Damaged;
            StartCoroutine(INTERNAL_OnDamage());
        }
    }

    IEnumerator INTERNAL_OnDamage(){
        m_animator.Play(m_isGround ? "Damage" : "AirDamage");
        m_animator.Play("Idle");

        SendMessage("OnDamage", SendMessageOptions.DontRequireReceiver);

        m_rigidbody2D.velocity = new Vector2(transform.right.x * backwardForce.x, transform.up.y * backwardForce.y);

        yield return new WaitForSeconds(.2f);

        while (m_isGround == false)
        {
            yield return new WaitForFixedUpdate();
        }
        m_animator.SetTrigger("Invincible Mode");
        m_state = State.Invincible;
    }

    void OnFinishedInvincibleMode(){
        m_state = State.Normal;
    }

    enum State{
        Normal,
        Damaged,
        Invincible,
    }
}
