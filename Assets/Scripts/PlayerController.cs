/*Cornell Stokes -Date 9/16
 Description. This file is meant to handle the UFO(player), what happens with pickups, and behavior after
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    public Text TimerText;
    public Text WinText;
    private float timeRemaining = 60f;
    private bool gameOver = false;
    public Button restartButton;

    // Reference to the mystery pickup object
    public GameObject mysteryPickup;  

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        WinText.text = "";
        TimerText.text = "Time: " + timeRemaining.ToString("F0");
        StartCoroutine(TimerCountdown());
        restartButton.gameObject.SetActive(false);

        // Ensure the mystery pickup is disabled at the start
        if (mysteryPickup != null)
        {
            mysteryPickup.SetActive(false);  // Disable the entire object at start
        }

        // Enable the mystery pickup after 10 seconds
        StartCoroutine(EnableMysteryPickupAfterTime(10f)); 
    }

    void FixedUpdate()
    {
        if (gameOver) return; // Stop player movement if game is over

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.velocity = movement * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for collision with regular pickups
        if (collision.gameObject.CompareTag("Pickup"))
        {
            GameOver(); // Trigger Game Over when the player collides with a pickup
        }
        // Check for collision with the mystery pickup
        else if (collision.gameObject.CompareTag("MysteryPickup"))
        {
            collision.gameObject.SetActive(false);  // Disable the mystery pickup once collected
            EndGameWithVictory(); // End the game with victory
        }
    }

    public void onRestartButtonPress()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void GameOver()
    {
        if (!gameOver)
        {
            WinText.text = "Game Over";
            gameOver = true;
            restartButton.gameObject.SetActive(true); // Show restart button on game over
        }
    }

    void EndGameWithVictory()
    {
        if (!gameOver)
        {
            WinText.text = "You found it!";  // Display victory message for finding mystery pickup
            gameOver = true;
            restartButton.gameObject.SetActive(true); // Show restart button on victory
        }
    }

    IEnumerator TimerCountdown()
    {
        while (timeRemaining > 0 && !gameOver)
        {
            timeRemaining -= Time.deltaTime;
            TimerText.text = "Time: " + timeRemaining.ToString("F0");
            yield return null; 
        }

        if (!gameOver) // If game is not already over
        {
            WinText.text = "You Win!";
            gameOver = true;
            restartButton.gameObject.SetActive(true);  // Show restart button on winning the timer
        }
    }

    // Coroutine to enable the mystery pickup after a certain time
    IEnumerator EnableMysteryPickupAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        if (mysteryPickup != null)
        {
            mysteryPickup.SetActive(true);  // Enable the object after 10 seconds
        }
    }
}