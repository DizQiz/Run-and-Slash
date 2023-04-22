using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerEngine : MonoBehaviour
{
    //movement
    float xDir;
    float yDir;
    public float Speed;
    bool isNowRun;
    public bool istuch;

    //jump
     public bool canJump;
    Rigidbody2D rb;
    int jumpCount;
    
    //fire
    public GameObject slash;
    public GameObject strongSlash;
    public Transform bulletPos;
    
    //animeition
    public Animator PlayerAnime;
    public Animator silverAnim;
    public Animator goldenAnim;

    //health
    public int health;
    public int maxHealth = 100;
    public HealthBar healthBar;

    //paused the game mnue
    public GameObject pauseMenu;
    bool isPause;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);

        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        istuch = false;
        jumpCount = 1;

        canJump = false;

        Speed = 2f ;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Sprint();
        Jump();
        Fire();
        if(istuch)
        {
            StersMovment();
        }
        PauseMenuSetting();
        
        if (health <= 0)
        {
            SceneManager.LoadScene("Death");
        }
    }

    public void Movement()
    {
        if(Input.GetKey(KeyCode.D))
        {
            xDir = 1;
            transform.localScale = new Vector3 (1,transform.localScale.y,transform.localScale.z);
            PlayerAnime.SetBool("Walk", true);
            isNowRun = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            xDir = -1;
            transform.localScale = new Vector3 (-1,transform.localScale.y, transform.localScale.z);
            PlayerAnime.SetBool("Walk", true);
            isNowRun = true;
        }
        else
        {
            xDir = 0;
            PlayerAnime.SetBool("Walk", false);
        }
        transform.position = new Vector2(transform.position.x + xDir * Speed * Time.deltaTime, transform.position.y);
    }

    public void Sprint()
    {
        if(Input.GetKey(KeyCode.LeftShift) && isNowRun == true)
        {
            Speed = 4f;
           
            PlayerAnime.SetBool("Run",true );
           // PlayerAnime.SetBool("Walk", false);
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed = 2.5f;
            PlayerAnime.SetBool("Run", false);
        }
    }

    public void Jump()
    {
        if(canJump)
        {
            if(Input.GetKeyDown(KeyCode.Space) && canJump == true)
            {
                
                if (jumpCount > 0)
                {
                    rb.AddForce(new Vector2(0, 300));
                    PlayerAnime.SetBool("Jump", true);
                   
                    PlayerAnime.SetBool("Walk", false);

                    PlayerAnime.SetBool("Run", false);
                    
                    jumpCount--;
                    if (jumpCount == 0)
                    {
                        canJump = false;
                    }
                }
            } 
        }
    }

    public void Fire()
    {
        
        if (Input.GetMouseButtonDown(0))
        {

            PlayerAnime.SetBool("Attack", true);
            PlayerAnime.SetBool("Walk", false);
            PlayerAnime.SetBool("Run", false);

            Instantiate(slash, bulletPos.transform.position, slash.transform.rotation);
            
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            PlayerAnime.SetBool("Attack", false);
        }

        if (Input.GetMouseButtonDown(1))
        {

            PlayerAnime.SetBool("Attack", true);
            PlayerAnime.SetBool("Walk", false);
            PlayerAnime.SetBool("Run", false);

            Instantiate(strongSlash, bulletPos.transform.position, strongSlash.transform.rotation);
            TakeDamaged(10);

        }
        else if (Input.GetMouseButtonUp(1))
        {
            PlayerAnime.SetBool("Attack", false);
        }



    }
   
    public void StersMovment()
    {
        if(Input.GetKey(KeyCode.W))
        {
            yDir = 1;
            
        }
        else if(Input.GetKey(KeyCode.S))
        {
            yDir = -1;
           
        }
        else
        {
            yDir = 0;
            
        }
        transform.position = new Vector2(transform.position.x ,transform.position.y + yDir * Speed * Time.deltaTime);
    }

    public void TakeDamaged(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;

        Speed = 0f;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
        Speed = 2.5f;
    }

    public void PauseMenuSetting()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                ResumeGame();

                
            }
            else if(!isPause)
            {
                PauseGame();

                
            }
        }
    }

    public IEnumerator AnimeDestorydTime()
    {
        goldenAnim.SetTrigger("GoldenChest");
        yield return new WaitForSeconds(3f);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            PlayerAnime.SetBool("Jump", false);

            canJump = true;
           
            
            jumpCount = 2;
            
        }
        if (collision.gameObject.tag == "SilverChest")
        {
            silverAnim.SetBool("SilverChest", true);
            AnimeDestorydTime();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "GoldenChest")
        {
            goldenAnim.SetTrigger("GoldenChest");
            AnimeDestorydTime();
            // Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag =="Sters")
        {
            istuch= true;
            rb.gravityScale = 0;
            canJump = false;
        }
        if (other.gameObject.tag == "Victory")
        {
            if (TextController.starScore > 1)
            {
                SceneManager.LoadScene("Victory");

            }
            
        }
        if(other.gameObject.tag == "SilverChest")
        {
            silverAnim.SetBool("SilverChest", true);
            AnimeDestorydTime();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "GoldenChest")
        {
            goldenAnim.SetTrigger("GoldenChest");
            AnimeDestorydTime();
           // Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sters")
        {
            istuch = false;
            rb.gravityScale = 1;
            canJump = true;
        }
        if(other.gameObject.tag == "Death")
        {
           
            SceneManager.LoadScene("Death");
        }
    }
}
