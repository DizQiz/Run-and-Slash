using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngine : MonoBehaviour
{
    Vector2[] enemyPos = new Vector2[5] {new Vector2(65,10.5f), new Vector2(69.5f,9), new Vector2(72.6f,5.6f), new Vector2(69.4f,2.6f), new Vector2(62.3f,-1.3f)};
    public GameObject enemy;
    int randomEnemyPos;
    List<Vector2> usedPositions = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 0; i<2;i++)
        {
            
            randomEnemyPos = Random.Range(0, enemyPos.Length);

            while (usedPositions.Contains(enemyPos[randomEnemyPos]))
            {
                randomEnemyPos = Random.Range(0, enemyPos.Length);
                print("has to make anther random");
            }

            usedPositions.Add(enemyPos[randomEnemyPos]);

            Instantiate(enemy, enemyPos[randomEnemyPos], enemy.transform.rotation);

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "StrongSlash")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
