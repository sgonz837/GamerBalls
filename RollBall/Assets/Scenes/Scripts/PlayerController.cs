using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    private int level = 0;
	private Scene curLevel;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
		
		curLevel = SceneManager.GetActiveScene();
		if (curLevel.name == "Level 1") { level = 1; }
		if (curLevel.name == "Level 2") { level = 2; }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            LevelTransition();
            //winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
		if (rb.position.y < -5.0f) {
			rb.position = new Vector3(0f, 0.5f, 0f);
			Debug.Log(rb.position);
			Vector3 bump = new Vector3(0, -10.0f, 0);
			rb.AddForce(bump);
		}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }

    void LevelTransition()
    {
        if(level == 0)
        {
            SceneManager.LoadScene("Level 1");
            level = 1;
            count = 0;
        } else if(level == 1)
        {
            SceneManager.LoadScene("Level 2");
            level = 2;
            count = 0;
        } else if(level == 2)
        {
            winTextObject.SetActive(true);
        }
    }

}
