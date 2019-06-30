using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showGO : MonoBehaviour {

	public GUIText GOText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (clearflag.isgameover()){
			GOText.text = "GAME OVER";
		}
	}
}
