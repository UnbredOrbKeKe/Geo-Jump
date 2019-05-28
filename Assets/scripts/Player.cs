using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    public ParticleSystem playerParticle;



    public float rotationSpeed = 1;
    private float z_axis = 0;
    protected float timer = 0.0f;
    public float speed = 5.5f;
    protected float keepJumping;
    public float jumpRate = 0.8f;

    [SerializeField]
    protected float targetRot = 0;

    [SerializeField]
    protected int DeathCount = 0;

    [SerializeField]
    protected int jumpCount = 1;

    public LayerMask layers;

    public static Player instance;

    float delay = 2.3f;
    
    // Use this for initialization


    void Awake()
    {
        instance = this;

    }
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        Vector3 vel = rb.velocity;
        vel.x = speed;
        rb.velocity = vel;

        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 vel = rb.velocity;
        if (vel.magnitude < 0.01f)
        {           
            timer += Time.deltaTime;
            if (timer > delay)
            {
                Die();
            }
            
            Debug.Log(Time.time);
            Debug.Log(delay);
        }
        vel.x = speed;
        rb.velocity = vel;



        // gives rotation to the player/block
        timer += Time.deltaTime * rotationSpeed;
        float new_axis = Mathf.LerpAngle(z_axis, targetRot, timer);
        transform.rotation = Quaternion.Euler(0, 0, new_axis);

        if (Physics2D.Raycast(transform.position, Vector2.down, GetComponent<BoxCollider2D>().size.y / 2 + 0.1f, layers))
        {
            Quaternion rot = transform.rotation;
            transform.rotation = rot;


            


            // lets the player jump when you touch the screen/use your mouse button
            if (Input.GetMouseButton(0) && Time.time > keepJumping)
            {
                Debug.Log("jump");
                keepJumping = Time.time + jumpRate;

                //lets the player jump
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * 55000);

                timer = 0;
                z_axis = transform.rotation.eulerAngles.z;
                targetRot -= 90;
                targetRot %= 360;

            }

           
        }

    }


    //resets the level completely
    public void Die()
    {
        Debug.Log("test");
        SceneManager.LoadScene("lvl1");
    }

    


}
