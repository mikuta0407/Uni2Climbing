﻿using System.Collections;
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

    public bool s_changestart = false;

    public float maxy = -10;
    
    void Reset(){
        Awake();

        // UnityChan2DController
        maxSpeed = 10f;
        jumpPower = 20;
        //backwardForce = new Vector2(-4.5f, 5.4f);
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

    /// 直前のFixedUpdateフレームでの位置
    Vector2 _preFramePos = new Vector2();

    /// 直前のFixedUpdateフレームで移動操作をしたかどうか
    bool _isMovePreFlame = false;

    /// 床の境目にひっかかったときに上に押し上げる移動量
    public float _deltaUpwardPos = 0.03f;

    /// 前のフレームとのX変位限界 移動量がこの範囲内ならひっかかってるとみなす
    public float _deltaRangeX = 0.001f;

    /// Updateでジャンプ入力を受け付けたフラグ FixedUpdateでこのフラグを見てジャンプ
    bool _jumpFlag = false;

    /// 十字キーによる移動量
    float _deltaX = 0;

    void Update(){
        if (maxy <= transform.position.y){
            maxy = transform.position.y;
        }
        life = count.getlife();     //引き継いではいるんだけど、コイン100枚採ったとき用に...
        hitcount = count.gethit();  //↑+普通にあたったとき用

        if (scoresaver.getcoin() == "100"){ //もし100枚とったら・・・
            life++;                         //残基増やして
            count.setlife(life);            //それを書き込んで
            scoresaver.setcoin("0");        //コイン枚数を0に戻す
        }

        if (m_state != State.Damaged){      //ダメージ受けてない間・・・
            //float x = Input.GetAxis("Horizontal");      //ここの挙動はQiitaにあるので省略
            //bool jump = Input.GetButtonDown("Jump");    //同上
            if ((clearflag.ismapclear()) || (die)){                                  //まだ死んでなかったら・・・
                _deltaX = 0;
                _jumpFlag = false;
            } else {
                _deltaX = Input.GetAxis("Horizontal");
                _jumpFlag = Input.GetButtonDown("Jump");
            }
        }

        //↓ここはUpdateに振り回されないようにする必要があるかもしれない。
        if ((hitcount == 3) || (clearflag.istimeover())){   //もし敵に3回あたったら
            die = true;         //死亡判定ON
            count.sethit(3);    //count.csに3回あたったことを一応記録
            
            if ((clearflag.istimeover()) && (!dieplayed)){
                life--;
                clearflag.truedied(true);
                count.setlife(life);
            }

            if (!dieplayed){
                m_animator.Play("Idle");
                // 死亡ジングル音を鳴らす
                se.GetComponent<playse>().playdie();
                dieplayed = true;
                //Debug
                Debug.Log("死にましたよ");
            }
            
            if (count.getlife() == 0){                          //もし残基が0だったら・・・(先にゲームオーバー時の処理)
                if ( (!se.GetComponent<playse>().dieplaying()) && (dieplayed) && (!goplayed)){       //かつ死亡ジングルが流れ終わってたら(死亡ジングルはOnTriggerStay2Dで流しています)
                    //Debug
                    Debug.Log("ゲームオーバー時の処理に入りました｡");
                    clearflag.truegameover(true);
                    
                    se.GetComponent<playse>().playgameover();       //ゲームオーバージングルを流す
                    goplayed = true;
                    //Debug
                    Debug.Log("ゲームオーバージングルを流しました｡");
                }
                if ( (!se.GetComponent<playse>().gameoverplaying()) && (goplayed) && (dieplayed)){  //もしゲームオーバージングルが流れ終わってたら
                    //Debug
                    Debug.Log("Resultシーンを呼び出します｡");
                    SceneManager.LoadScene ("Result");              //Resultシーンを呼び出します。
                }
            } else {                                            //まだ残基に余裕があったら
                if ( (!se.GetComponent<playse>().dieplaying()) && (dieplayed) && (!s_changestart) ){       //死亡ジングルがなり終わっていたら
                    count.sethit(0);                                //hit数を0に戻して、
                    s_changestart = true;
                    SceneManager.LoadScene ("Loading " + nowworld.getworld()); //もう一回同じマップを呼び出す。
                }
            }
            
        }
        
    }

    
    void Move(float move, bool jump){
        //地上にいるとき
        //前のフレームで移動操作を行っており、かつ前のフレームとのX変位がないorほぼない場合に
        //目の前に障害物があるか確認し、ないならばY位置を微上昇
        //要するにめりこみ段差にひっかかったらちょっと上へ移動させる
        if (_isMovePreFlame && Mathf.Abs(transform.position.x - _preFramePos.x) < _deltaRangeX && m_isGround)
        {
            var pos = transform.position;
            var direction = Mathf.Cos(transform.rotation.y);
            var lineEndPos = new Vector2(pos.x + (m_boxcollier2D.size.x / 2 + 0.1f) * direction, pos.y);

            //進行方向のプレイヤー先端で壁ブロック検知 
            bool isHitForward = Physics2D.Linecast(pos,lineEndPos,whatIsGround);
            
            //めりこみによる段差で床にひっかかったら微上昇 上昇量を増やすとどこで引っかかったかわかりやすい
            if ((isHitForward) && (m_isGround) && (move != 0))
                transform.position = new Vector3(pos.x, pos.y + _deltaUpwardPos, pos.z);
        }
        _preFramePos = transform.position;
        _isMovePreFlame = false;

        if (Mathf.Abs(move) > 0)
        {
            //向き反転
            Quaternion rot = transform.rotation;
            transform.rotation = Quaternion.Euler(rot.x, Mathf.Sign(move) == 1 ? 0 : 180, rot.z);

            _isMovePreFlame = true;
        }
        
        m_animator.SetFloat("Horizontal", move);
        m_animator.SetFloat("Vertical", m_rigidbody2D.velocity.y);
        m_animator.SetBool("isGround", m_isGround);

        if (jump && m_isGround)
        {
            jump = false;
            m_animator.SetTrigger("Jump");
            SendMessage("Jump", SendMessageOptions.DontRequireReceiver);
            m_rigidbody2D.velocity = new Vector2(move * maxSpeed, jumpPower);
        }

        m_rigidbody2D.velocity = new Vector2(move * maxSpeed, m_rigidbody2D.velocity.y);
        /* if (Mathf.Abs(move) > 0){
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
        } */
    }

    void FixedUpdate(){
        Vector2 pos = transform.position;
        Vector2 groundCheck = new Vector2(pos.x, pos.y - (m_centerY * transform.localScale.y));
        Vector2 groundArea = new Vector2(m_boxcollier2D.size.x * 0.49f, 0.05f);

        m_isGround = Physics2D.OverlapArea(groundCheck + groundArea, groundCheck - groundArea, whatIsGround);
        m_animator.SetBool("isGround", m_isGround);
        Move(_deltaX, _jumpFlag);
    }

    void OnTriggerStay2D(Collider2D other){     //敵にあたったとき
        if (other.tag == "DamageObject" && m_state == State.Normal){
            hitcount++;                 //hitcountを増やす
            count.sethit(hitcount);     //それを記録する
            
            if (hitcount == 3){         //3回あたってたら
                life--;                 //残基減らして
                clearflag.truedied(true);
                Debug.Log("Life=" + life);
                count.setlife(life);    //それを記録
            }


            m_state = State.Damaged;
            StartCoroutine(INTERNAL_OnDamage());
        }
    }

    IEnumerator INTERNAL_OnDamage(){
        m_animator.Play(m_isGround ? "Damage" : "AirDamage");
        m_animator.Play("Idle");

        SendMessage("OnDamage", SendMessageOptions.DontRequireReceiver);

        Debug.Log("transform.right.x: " +transform.right.x);
        Debug.Log("transform.up.y: " +transform.up.y);
        m_rigidbody2D.velocity = new Vector2(transform.right.x * -4.5f, transform.up.y * 5.4f);
        Debug.Log("x: " + (transform.right.x * 4.5f) + " y: " + (transform.up.y * 5.4f));
        //m_rigidbody2D.velocity = new Vector2(transform.right.x * backwardForce.x, * backwardForce.y);

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
