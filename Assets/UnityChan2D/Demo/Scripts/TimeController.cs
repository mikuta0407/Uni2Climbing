using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class TimeController : MonoBehaviour{
    public int time;
    [SceneName]
    public string nextLevel;

    public GUIText timer;
    
    private int remainingTime;

    private bool stoped = false;

    void Update(){
        remainingTime = time - Mathf.FloorToInt(Time.timeSinceLevelLoad * 2.5f);

        if ( (count.gethit() != 3) && (!clearflag.ismapclear()) && (!stoped) ){    //3回当たってない または クリアしてなければ
            if (0 <= remainingTime){                                //時間に余裕があるとき
                timer.text = remainingTime.ToString("000");         //時間を減らしていく
                timesaver.settime(remainingTime);
            } else {                                                //時間が0になったら
                    clearflag.truetimeover(true);                   //タイムオーバー判定
            }
        } else if (!stoped) {
            Debug.Log("時間を止めます");
            stoped = true;
            timestop(); //固定化
        }
    }

    void timestop(){
        //Debug.Log("時間を止めます");
        remainingTime = timesaver.gettime();
    }

}
