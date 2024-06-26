using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPatrol : MonoBehaviour
{
    [SerializeField] private Transform left_waypoint;
    [SerializeField] private Transform right_waypoint;
    [SerializeField] private Transform enemy;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed;

    // enemy follow player
    public Transform player_location;
    public bool chasing_player;
    public float chasing_distance;
    public float max_distance;

    private Vector3 origin_scale;
    private bool go_left;
    private Rigidbody2D body;

    private void Awake()
    {
        origin_scale = enemy.localScale; // store the current enemy scale
        body = enemy.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Vector2.Distance(enemy.position, player_location.position) > max_distance)
        {
            chasing_player = false;
            anim.SetBool("Walk", false);
        }

        if (chasing_player)
        {
            float tolerance = 0.1f;

            if (Mathf.Abs(enemy.position.x - player_location.position.x) > tolerance)
            {
                if (enemy.position.x > player_location.position.x)
                {
                    anim.SetBool("Walk", true);
                    enemy.localScale = new Vector3(-0.23f, 0.23f, 0.23f);
                    body.velocity = new Vector2(-speed * 1.3f, 1);
                }
                else if (enemy.position.x < player_location.position.x)
                {
                    anim.SetBool("Walk", true);
                    enemy.localScale = new Vector3(0.23f, 0.23f, 0.23f);
                    body.velocity = new Vector2(speed * 1.3f, 1);
                }
            }
            else
            {
                body.velocity = new Vector2(0, 1);
                anim.SetBool("Walk", false);
            }
        }
        else
        {
            if (go_left)
            {
                if (enemy.position.x >= left_waypoint.position.x) // while not touching left waypoint
                {
                    Moving(-1); // go to left
                }
                else // touching waypoint
                {
                    ChangeDirection(); // change direction
                }
            }
            else
            {
                if (enemy.position.x <= right_waypoint.position.x) // while not touching right waypoint
                {
                    Moving(1); // go to right
                }
                else
                {
                    ChangeDirection(); //change direction
                }
            }

            if (Vector2.Distance(enemy.position, player_location.position) < chasing_distance) // if player gets closer to enemy
            {
                chasing_player = true;
            }
        }
    }
    private void ChangeDirection()
    {
        go_left = !go_left;
    }
    private void Moving(int direction_value)
    {
        anim.SetBool("Walk", true);
        // Direction
        enemy.localScale = new Vector3(Mathf.Abs(origin_scale.x) * direction_value, origin_scale.y, origin_scale.z);
        // Move to waypoint
        body.velocity = new Vector2(direction_value * speed * 1, 1);
    }
}
