using System.Collections;
using System.Collections.Generic;
using System;
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

        var assembly = RuntimeCompiler.Compile(@"
		        using UnityEngine;

		        public class Test
		        {
		        	public static void Foo()
		        	{
			        	Debug.Log(""Helloooooo, World!"");
			        }
		        }
        ");

        var method = assembly.GetType("Test").GetMethod("Foo");
        var del = (Action) Delegate.CreateDelegate(typeof(Action), method);
        del.Invoke();

        var assemblyT = RuntimeCompiler.Compile(@"
		        using UnityEngine;

		        public class Test
		        {
		        	public static void Foo()
		        	{
			        	Debug.Log(""Seems to work now."");
			        }
		        }
        ");

        var methodT = assemblyT.GetType("Test").GetMethod("Foo");
        var delT = (Action)Delegate.CreateDelegate(typeof(Action), methodT);
        delT.Invoke();
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
