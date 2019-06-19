using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Camera))]
public class camera : MonoBehaviour
{
	public string nowLevel;
    public string nextLevel;
    GameObject player;

    public GUIText total;
    // Use this for initialization
    void Start()
    {
        nowLevel = nowworld.getworld();
        this.player = GameObject.Find("DemoUnityChan2D");
        total = total.GetComponent<GUIText> ();
        total.text = scoresaver.getscore();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(-1, playerPos.y + 0.5f, transform.position.z);
		transform.rotation = Quaternion.Euler(0,0,0);

		if (playerPos.y + 0.5f > 89){
			StartCoroutine(INTERNAL_Clear());
            enabled = false;
		}

        // FOR DEBUG: FORCE CLEAR
        if (Input.GetKeyDown(KeyCode.B)) {
            StartCoroutine(INTERNAL_Clear());
        }

    }
	private IEnumerator INTERNAL_Clear()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            player.SendMessage("Clear", SendMessageOptions.DontRequireReceiver);
        }

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
        yield return new WaitForSeconds(3);
		SceneManager.LoadScene ("Loading " + nextLevel);
        // Application.LoadLevel(nextLevel);
    }
}