using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    public int time;
    [SceneName]
    public string nextLevel;

    public GUIText timer;

    void Update(){
        int remainingTime = time - Mathf.FloorToInt(Time.timeSinceLevelLoad * 2.5f);

        if (0 <= remainingTime){
            timer.text = remainingTime.ToString("000");
            timesaver.settime(remainingTime);
        } else {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player){
                count.sethit(3);    //タイムオーバーしたらヒット数を3にする
                remainingTime = (int)timer.text; //固定化(要テスト)
            }
        }
    
    }

}
