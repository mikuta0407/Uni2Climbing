using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showFWC : MonoBehaviour {

	public GUIText FWCText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (clearflag.isfwc()){
			FWCText.text = "final world cleared!";
		}
	}
}
