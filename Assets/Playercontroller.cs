using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour {

    // Use this for initialization
    public float speed = 1;
    public float ladderspeed = 1;
    public float jumpspeed = 2;
    public float jumptime = 0.5f;
    public float boradthick = 0.5f;
    public float attackdis = 0.5f;
    public int jumpt = 2;

    private float localtime = 0;
    private bool ifjump = false;
    private bool onworld = true;
    private bool onladder = false;
    public int localjumpt = 0;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ladder();
        ground();
        playerinput();

        if (Input.GetKeyDown(KeyCode.X))
        {
            changeworld();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            attack();
        }

        if(Input.GetKey(KeyCode.C))
        {
            attach();
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void playerinput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            float nx = GetComponent<Rigidbody2D>().velocity.x;
            float spx = Mathf.SmoothDamp(nx, speed, ref nx, 0.2f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(spx, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (Input.GetKey(KeyCode.A))
        {
            float nx = GetComponent<Rigidbody2D>().velocity.x;
            float spx = Mathf.SmoothDamp(nx, -speed, ref nx, 0.2f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(spx, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!ifjump)
            {
                float spy = ((onworld)?jumpspeed:-jumpspeed);
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, spy);
                ifjump = true;
                localjumpt += 1;
            }
            else
            {
                if (localjumpt < jumpt)
                {
                    float spy = ((onworld) ? jumpspeed : -jumpspeed);
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, spy);
                    localjumpt += 1;
                }
            }
        }


        if (onladder)
        {
            if (Input.GetKey(KeyCode.W))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, ladderspeed);
            }
        }
    }

    private void changeworld()
    {
        float ny = GetComponent<Transform>().position.y + ((onworld) ? -(boradthick + GetComponent<Renderer>().bounds.size.y) : (boradthick + GetComponent<Renderer>().bounds.size.y));
        GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, ny, GetComponent<Transform>().position.z);
        GetComponent<Rigidbody2D>().gravityScale = -GetComponent<Rigidbody2D>().gravityScale;

        onworld = !onworld;
    }

    private void attack()
    {
        Vector2 attpoint = new Vector2(GetComponent<Transform>().position.x+attackdis* GetComponent<Transform>().forward.x, GetComponent<Transform>().position.y);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(attpoint,new Vector2(1f,0.2f),0);
        foreach (Collider2D x in colliders)
        {
            if (x.transform.gameObject.layer == 8)
            {
                Destroy(x.transform.gameObject);
            }
        }
    }


    private void attach()
    {
        Vector2 attpoint = new Vector2(GetComponent<Transform>().position.x + attackdis * GetComponent<Transform>().forward.x, GetComponent<Transform>().position.y);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(attpoint, new Vector2(1.2f, 0.2f), 0);
        foreach (Collider2D x in colliders)
        {
            if (x.transform.gameObject.layer == 10)
            {
                float deltax = (GetComponent<Renderer>().bounds.size.x + x.GetComponent<Renderer>().bounds.size.x) * ((GetComponent<Transform>().forward.x>0)?1:-1) /2;
                x.GetComponent<Transform>().position = new Vector2(this.GetComponent<Transform>().position.x+deltax, x.GetComponent<Transform>().position.y);
            }
        }
    }

    private void ladder()
    {
        if (onladder) onladder = false;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(GetComponent<Transform>().position, new Vector2(0.8f, 0.8f), 0);
        foreach (Collider2D x in colliders)
        {
            if (x.transform.gameObject.layer == 9)
            {
                onladder = true;
            }
        }
    }

    private void ground()
    {

        Collider2D[] colliders = Physics2D.OverlapBoxAll(GetComponent<Transform>().position, new Vector2(0.1f, GetComponent<Renderer>().bounds.size.y-0.1f), 0);
        if(colliders.Length>1)
        {
            ifjump = false;
            localjumpt = 0;
        }
    }
}
