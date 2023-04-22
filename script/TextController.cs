using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    [SerializeField] TMP_Text starText;
    [SerializeField] TMP_Text dimondText;
    [SerializeField] TMP_Text coinText;

    //explenition texet
    [SerializeField] GameObject howToMove;
    [SerializeField] GameObject howToShoot;
    [SerializeField] GameObject howToJump;
    [SerializeField] GameObject howToGoUp;
    [SerializeField] GameObject howToDie;

    int coinScore;
    public static int starScore;
    int dimondScore;

    // Start is called before the first frame update
    void Start()
    {
        howToMove.SetActive(false);
        howToShoot.SetActive(false);
        howToJump.SetActive(false);
        howToGoUp.SetActive(false);
        howToDie.SetActive(false);

        coinScore = 0;
        starScore = 0;
        dimondScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TextStart();
    }
    public void TextStart()
    {
        coinText.text =": " + coinScore.ToString();
        starText.text = ": " + starScore.ToString();
        dimondText.text = ": " + dimondScore.ToString();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Dimond")
        {
            dimondScore += 1;
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.tag == "Star")
        {
            starScore += 1;
            Destroy(collision.gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "HowToMove")
        {
            howToMove.SetActive(true);
        }
        if (other.gameObject.tag == "HowToShoot")
        {
            howToShoot.SetActive(true);
        }
        if (other.gameObject.tag == "HowToJump")
        {
            howToJump.SetActive(true);
        }
        if (other.gameObject.tag == "HowToGoUp")
        {
            howToGoUp.SetActive(true);
        }
        if (other.gameObject.tag == "HowToDie")
        {
            howToDie.SetActive(true);
        }

        if (other.gameObject.tag == "Coin")
        {
            coinScore += 75;
            Destroy(other.gameObject);

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "HowToMove")
        {
            howToMove.SetActive(false);
        }
        if (other.gameObject.tag == "HowToShoot")
        {
            howToShoot.SetActive(false);
        }
        if (other.gameObject.tag == "HowToJump")
        {
            howToJump.SetActive(false);
        }
        if (other.gameObject.tag == "HowToGoUp")
        {
            howToGoUp.SetActive(false);
        }
        if (other.gameObject.tag == "HowToDie")
        {
            howToDie.SetActive(false);
        }
    }
}
