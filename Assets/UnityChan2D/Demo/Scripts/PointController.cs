using System;
using UnityEngine;

public class PointController : MonoBehaviour
{

    public GUIText total;
    public GUIText coin;
    // public static int score;

    void Start(){
        //scoresaverから情報持ってきて前ワールドの情報を引き継ぐよ
        total.text = scoresaver.getscore();
        coin.text = scoresaver.getcoin();
    }

    private static PointController m_instance;

    public static PointController instance{
        get{
            if (m_instance == false){
                m_instance = FindObjectOfType<PointController>();
            }
            return m_instance;
        }
    }

    public void AddCoin(){  //コイン採ったとき
        total.text = scoresaver.getscore();                                     //まずscoresaverからスコアを改めて取得します
        coin.text = scoresaver.getcoin();                                       //同じくCoinも取得します
        coin.text = (Convert.ToInt32(coin.text) + 1).ToString("00");            //コイン数を一枚増やします(この時点で書き換えしてる)
        total.text = (Convert.ToInt32(total.text) + 100).ToString("0000000");   //スコアに100を足します(この時点で書き換えしてる)
        scoresaver.setscore(total.text);                                        //新しいスコアをscoresaverに書き込みます。
        scoresaver.setcoin(coin.text);                                          //同じくコインも行います。
    }
}
