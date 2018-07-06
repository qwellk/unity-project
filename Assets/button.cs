using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour {

    public GameObject hidenobj;
    private bool ison = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(!ison)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(GetComponent<Transform>().position, new Vector2(1f, 0.2f), 0);
            foreach (Collider2D x in colliders)
            {
                print(x.transform.gameObject.layer);
                if (x.transform.gameObject.layer == 1)
                {
                    hidenobj.GetComponent<Renderer>().enabled = true;
                    hidenobj.GetComponent<Collider2D>().enabled = true;
                    ison = true;
                }
            }
        }   
    }
}
