using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// this script controlles all the things for the player (jump, speed, what happents when the player dies)
/// </summary>
public class Player : MonoBehaviour
{
    public Rigidbody2D rb; 

    private float rotationSpeed = 1;
    private float z_axis = 0;
    protected float timer = 0.0f; 

    
    public float speed = 5.5f;

    private float keepJumping;

    [SerializeField]
    protected float jumpRate = 0.8f;

    [SerializeField]
    protected float targetRot = 0;

    [SerializeField]
    protected int DeathCount = 0;

    [SerializeField]
    protected int jumpCount = 1;

    [SerializeField]
    private LayerMask layers; // wil ik public houden

    public static Player instance; // wil ik public houden

    private float delay = 2.3f;
    
    // Use this for initialization


    void Awake()
    {
        instance = this;

    }
    void Start()
    {
        //gives a jump start at the biginning becauses otherwise the die function registers it as going below 0.01 speed and that triggers the death function
        rb = GetComponent<Rigidbody2D>();
        Vector3 vel = rb.velocity;
        vel.x = speed;
        rb.velocity = vel;

        
    }

    // Update is called once per frame
    void Update()
    {
        //calls the die function when you hit an object or go below velocity 0.01
        Vector3 vel = rb.velocity;
        if (vel.magnitude < 0.01f)
        {           
            timer += Time.deltaTime;
            if (timer > delay)
            {
                Die();
            }
        }
        //vel.x = speed;
        //rb.velocity = vel;



        // gives rotation to the player/block
        timer += Time.deltaTime * rotationSpeed;
        float new_axis = Mathf.LerpAngle(z_axis, targetRot, timer);
        transform.rotation = Quaternion.Euler(0, 0, new_axis);
        //shoots a raycast towards the ground to check if you are on the ground so that you can't keep jumping in the air
        if (Physics2D.Raycast(transform.position, Vector2.down, GetComponent<BoxCollider2D>().size.y / 2 + 0.1f, layers))
        {
            Quaternion rot = transform.rotation;
            transform.rotation = rot;


            


            // lets the player jump when you touch the screen/use your mouse button
            if (Input.GetMouseButton(0) && Time.time > keepJumping)
            {

                //sets a jump rate where the if statement takes the speed from that lets the player jump while holding jump
                Debug.Log("jump");
                keepJumping = Time.time + jumpRate;

                //lets the player jump
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * 55000);

                //sets rotations where the player rotates to so that it lands on a flat side
                timer = 0;
                z_axis = transform.rotation.eulerAngles.z;
                targetRot -= 90;
                targetRot %= 360;

            }

           
        }

    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed);
    }


    /// <summary>
    /// resets level when called
    /// </summary>

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    


}
