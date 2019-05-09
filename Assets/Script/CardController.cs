using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour
{
    List<Sprite> a;
    bool boolClick;
	// Use this for initialization
	void Start () {
        boolClick = true;
        a = GameObject.Find("Controller").GetComponent<ControllerScript>().ListBoBai;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseDown()
    {
        if(boolClick)
        {

            int b = UnityEngine.Random.Range(0, a.Count);
            this.GetComponent<SpriteRenderer>().sprite = a[b];
            string scorePlayerString = a[b].name;
            int scorePlayerInt = Int32.Parse(scorePlayerString.Substring(0, 1));
            ControllerScript.ScorePlayer += scorePlayerInt;
            a.Remove(a[b]);
            boolClick = false;
            ControllerScript.DemGiaTri++;
            
        }
        

    }
}
