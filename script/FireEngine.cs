using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEngine : MonoBehaviour
{
    Transform playerScale;
    
    bool faceRight;
    public float timeToVensish;
    
    // Start is called before the first frame update
    void Start()
    {
        timeToVensish = 0.6f;
        playerScale = GameObject.Find("Player").transform;
       
        if (playerScale.localScale.x > 0)
        {
            faceRight = true;
            transform.localScale = new Vector3(0.3052833f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            faceRight = false;
            transform.localScale = new Vector3(-0.3052833f, transform.localScale.y, transform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject,timeToVensish);
        BulletCreator();
    }
    
    public void BulletCreator()
    {
        if (faceRight)
        {
            transform.Translate(0.2f,0,0);
           
        }
        else
        {
            transform.Translate(-0.2f, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

}
