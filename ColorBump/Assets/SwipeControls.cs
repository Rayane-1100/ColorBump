using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwipeControls : MonoBehaviour
{   // Reference to the Rigibody Component
    Rigidbody rb;

    // Used to calculate the direction from the mouse's last to current position
    Vector2 lastMousePos = Vector2.zero;

    // The force with which we can move the ball by swiping
    public float thrust = 100f;

    // Start is called before the first frame update

    // The horizontal ditance from the initial ball position to the edges
    [SerializeField] float wallDistance = 5f;

    // The minimum distance from the Camera to the Ball on the Z Axis
    [SerializeField] float minCamDistance = 4f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 deltaPos = Vector2.zero;

        if (Input.GetMouseButton(0))
        {
            Vector2 currentMousePos = Input.mousePosition;

            if (lastMousePos == Vector2.zero)
                lastMousePos = currentMousePos;

            deltaPos = currentMousePos - lastMousePos;

            lastMousePos = currentMousePos;

            Vector3 force = new Vector3(deltaPos.x, 0, deltaPos.y) * thrust;
            rb.AddForce (force);
        }
        else
        {
            lastMousePos = Vector2.zero;
        }

        
    }
    // LateUpdate is called after Update
    void LateUpdate()
    {
        Vector3 pos = transform.position;

        //If the Camera is behind the Camera
        if (pos.z < Camera.main.transform.position.z + minCamDistance)
        {
            // Block the player's position
            pos.z = Camera.main.transform.position.z + minCamDistance;
        }

        // Reset the position
        transform.position = pos;

        if (pos.x < -wallDistance)
        {
            pos.x = -wallDistance;
        }
        else if (pos.x > wallDistance)
        {
            pos.x = wallDistance;
        }
    }
    // Declare the speed variable
    public float speed = 5f;

    void FixedUpdate()
    {
        //Move the ball forward
        rb.MovePosition(rb.position + Vector3.forward * speed * Time.fixedDeltaTime);

        // Move the Camera forward
        Camera.main.transform.position += Vector3.forward * speed * Time.fixedDeltaTime;
    }
    IEnumerator Die(float delayTime)
    {
        // Do stuff before replaying the level 
        Debug.Log("You're dead");

        // Stop the Player from moving
        speed = 0;
        thrust = 0;

        // Wait some seconds
        yield return new WaitForSeconds(delayTime);

        // Do stuff after waiting some seconds

        //Replay the Level
        SceneManager.LoadScene(0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "evil")
            StartCoroutine(Die(2));
    }
   
    // The UI Panel
    public GameObject winPanel;

    IEnumerator Win(float delayTime)
    {
        // Do stuff before waiting 
        thrust = 0;
        speed = 0;
        rb.velocity = Vector3.zero;

        //Wait some time 
        yield return new WaitForSeconds(delayTime);

        // Do other stuff after waiting

        // Activate the pannel
        winPanel.SetActive(true);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
            StartCoroutine(Win(0.5f));
    }
}
