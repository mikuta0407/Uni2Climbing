using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Camera))]
public class camera : MonoBehaviour
{
	public string nowLevel;
    public string nextLevel;
    public GameObject se;
    public AudioSource bgm;
    GameObject player;

    public int goal = 0;

    
    // Use this for initialization
    void Start()
    {
        goal = 0;
        nowLevel = nowworld.getworld();
        this.player = GameObject.Find("DemoUnityChan2D");
        bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(-1, playerPos.y + 6f, transform.position.z);
		transform.rotation = Quaternion.Euler(0,0,0);

		if ((playerPos.x <= 0) && (playerPos.y >= 91)){
			StartCoroutine(INTERNAL_Clear());
            enabled = false;
		}

        // FOR DEBUG: FORCE CLEAR
        if (Input.GetKeyDown(KeyCode.B)) {
            StartCoroutine(INTERNAL_Clear());
        }

        if ((goal == 1) && (!se.GetComponent<playse>().goalplaying())){
            changeScene();
        }

        if (count.gethit() == 3){
            bgm.Stop();
        }

    }
	private IEnumerator INTERNAL_Clear()
    {
        bgm.Stop();
        se.GetComponent<playse>().playgoal();
        goal = 1;
        Debug.Log("cleared");
        //scoresaver.setscore((int.Parse(scoresaver.getscore()) + (timesaver.gettime()*10)).ToString());
        scoresaver.setscore( ( (Convert.ToInt32(scoresaver.getscore())) + (timesaver.gettime()*10) ) .ToString("0000000") );
        se.GetComponent<playse>().playgoal();
                
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            player.SendMessage("Clear", SendMessageOptions.DontRequireReceiver);
        }

        yield return new WaitForSeconds(0);
    }

    private void changeScene(){
        
        if (nowLevel == "1-1"){
            nextLevel = "1-2";
        } else if (nowLevel == "1-2"){
            nextLevel = "1-3";
        } else if (nowLevel == "1-3"){
            nextLevel = "1-4";
        } else if (nowLevel == "1-4"){
            nextLevel = "1-5";
        } else if (nowLevel == "1-5"){
            clearflag.setflag();
            nextLevel = "result";
        }
        nowworld.setworld(nextLevel);
        if (clearflag.getflag() == 1){
            SceneManager.LoadScene (nextLevel);
        } else {
            SceneManager.LoadScene ("Loading " + nextLevel);
        // Application.LoadLevel(nextLevel);
        }
    }
    
}