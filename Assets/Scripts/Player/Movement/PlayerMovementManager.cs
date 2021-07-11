using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Menu;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementManager : MonoBehaviour
{
    public float speed = 2.0f;
    public Rigidbody2D rb;
    public AudioManager audioManager;
    private int currentFootstep = 1;

    private void Start()
    {
        if (PlayerStats.instance.initialPosition != null 
            && SceneLoader.instance.currentScene.Equals(PlayerStats.instance.initialPosition.scene)
            && SceneLoader.instance.prevScene == "MainMenu")
        {
            rb.position = PlayerStats.instance.initialPosition.ToVector2();
        }
        audioManager = FindObjectOfType<AudioManager>();
        InvokeRepeating(nameof(PlayFootstep), 0f, 0.25f);
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void PlayFootstep()
    {
        if (rb.velocity == Vector2.zero) return;
        audioManager.Play("Footstep" + currentFootstep);
        currentFootstep++;
        if (currentFootstep > 5) currentFootstep = 1;
    }
    
    private void Move()
    {
        rb.velocity = DirectionWhereIsMoving().normalized * speed;
    }

    private static Vector2 DirectionWhereIsMoving()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        return new Vector2(horizontal, vertical);
    }

    public Vector2 DirectionWhereIsLooking()
    {
        if (!Input.GetMouseButtonDown(0)) DirectionWhereIsMoving();
        Vector2 direction = Input.mousePosition;
        direction = Camera.main.ScreenToWorldPoint(direction);
        direction -= (Vector2) transform.position;
        return direction;
    }

}
