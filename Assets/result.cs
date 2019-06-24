using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class result : MonoBehaviour {
	public GUIText cong;
	public GUIText cleared;
	public GUIText yourscore;
	
	// Use this for initialization
	void Start () {
		// congratulation
		// you cleared the game 
		if (clearflag.getflag() == 1){
			cong.text = "congratulation!!";
			cleared.text = "YOU cleared the game!!";
		} else {
			cong.text = "GAME OVER";
		cleared.text = "better luck next time";
		}

		yourscore.text = "YOUR SCORE IS " +scoresaver.getscore();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
