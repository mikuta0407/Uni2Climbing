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

    void Update(){
        remainingTime = time - Mathf.FloorToInt(Time.timeSinceLevelLoad * 2.5f);

        if ((count.gethit() != 3) && (!clearflag.ismapclear())){
            if (0 <= remainingTime){
                timer.text = remainingTime.ToString("000");
                timesaver.settime(remainingTime);
            } else {
                    clearflag.truetimeover(true);   //タイムオーバーしたらヒット数を3にする
            }
        } else {
            Debug.Log("時間を止めます");
            timestop(); //固定化
        }
    }

    void timestop(){
        //Debug.Log("時間を止めます");
        remainingTime = timesaver.gettime();
    }

}
