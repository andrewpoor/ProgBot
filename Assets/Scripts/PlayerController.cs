using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private const int NUM_PICKUPS = 6;

    private Rigidbody rigidBody;
    private int scoreValue = 0;

    public float speed = 1.0f;
    public Text scoreText;
    public Text winMessageText;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        SetScoreText();
        winMessageText.text = "";
    }

	//void FixedUpdate()
 //   {
 //       float moveHorizonal = Input.GetAxis("Horizontal");
 //       float moveVertical = Input.GetAxis("Vertical");

 //       Vector3 movement = new Vector3(moveHorizonal, 0.0f, moveVertical) * speed;

 //       rigidBody.AddForce(movement);
 //   }

    void OnTriggerEnter( Collider other )
    {
        if( other.gameObject.CompareTag("Pickup") )
        {
            other.gameObject.SetActive(false);
            ++scoreValue;
            SetScoreText();
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + scoreValue.ToString();

        if( scoreValue >= NUM_PICKUPS)
        {
            winMessageText.text = "You Win!";
        }
    }
}
