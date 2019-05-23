using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions
{
    Up,
    Down,
}

public class launchPad : MonoBehaviour
{
    public Directions directions;
    public float force;

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        //add force to the player (jump extra high)

        switch (directions)
        {
            case Directions.Up:
                Player.instance.rb.velocity = new Vector2(Player.instance.rb.velocity.x,0);
                Player.instance.rb.AddForce(Vector2.up * force);
                break;


            case Directions.Down:
                Player.instance.rb.velocity = new Vector2(Player.instance.rb.velocity.x, 0);
                Player.instance.rb.AddForce(Vector2.down* force);
                break;


            default:
                Debug.LogError("broke stuff send help");
                break;
        }
    }
}
