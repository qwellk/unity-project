using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingblock : MonoBehaviour {

    // Use this for initialization
    public bool onmove = false;
    public float movespeed = 1f;
    public float looptime = 1f;

    private float localtime = 0;
    private bool faceup = false;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!onmove)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(GetComponent<Transform>().position, new Vector2(1f, 0.2f), 0);
            foreach (Collider2D x in colliders)
            {
                if (x.transform.gameObject.layer == 1)
                {
                    onmove = true;
                    GetComponent<Rigidbody2D>().velocity = new Vector3(0, ((faceup)?movespeed:-movespeed), 0);
                }
            }
        }
        else
        {
            if (localtime < looptime)
            {
                localtime += Time.deltaTime;
            }
            else
            {
                localtime = 0;
                faceup = !faceup;
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, ((faceup) ? movespeed : -movespeed), 0);
            }
        }
    }

    void move()
    {
        onmove = true;
        
    }
}
