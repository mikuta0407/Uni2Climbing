using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingcoin : MonoBehaviour {
	public GUIText coin;
	// Use this for initialization
	void Start () {
		coin.text = scoresaver.getcoin();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
