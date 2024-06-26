using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthCrystal : MonoBehaviour
{
    [SerializeField] private GameObject Crystal;
    [SerializeField] private GameObject Door;
    [SerializeField] private Animator door_animator;

    private PolygonCollider2D polygon_collider;
    private Animator anim;

    private void Awake()
    {
        polygon_collider = GetComponent<PolygonCollider2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Earth")) // if crystal got hit by earth element
        {
            if (Door != null)
            {
                if (door_animator != null)
                {
                    door_animator.SetTrigger("Open"); // play animation
                    AudioManager.instance.PlaySFX("OpenGate");
                }
                Collider2D door_collider = Door.GetComponent<Collider2D>(); // Take Reference from door's box collider
                if (door_collider != null)
                {
                    door_collider.enabled = false; // disable box collider of the door
                }
            }

            polygon_collider.enabled = false;
            anim.SetTrigger("break"); // the crystal also break
            AudioManager.instance.PlaySFX("CrystalBreak");
        }
    }
    private void CrystalBreak()
    {
        Crystal.SetActive(false);
    }
}
