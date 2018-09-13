using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    float PUSH_FORCE = 15.0f;
    float MAX_VELOCITY = 7.0f;
    Rigidbody2D cRigidbody;

    void Awake()
    {
        GameManager.instance.OnStartGame += StartGame;
        GameManager.instance.OnFinishGame += EndGame;
    }

    // Use this for initialization
    void Start () {
        cRigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
	}


    // Use this for initialization
    void StartGame()
    {
        TouchManager.instance.OnStartTouch += OnTouch;
        cRigidbody.simulated = true;
    }

    // Use this for initialization
    void EndGame()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        TouchManager.instance.OnStartTouch -= OnTouch;
        cRigidbody.simulated = true;
        Destroy(gameObject);
    }

    void OnTouch(TouchStruct touchStruct)
    {
        float currVelocity = cRigidbody.velocity.y;
        Vector2 newVelocity = new Vector2(0, Mathf.Clamp(currVelocity + PUSH_FORCE, -Mathf.Infinity, MAX_VELOCITY));
        cRigidbody.velocity = newVelocity;
        Achievements.instance.UpgradeJump();
    }

    private void OnDestroy()
    {
        EndGame();
        GameManager.instance.OnStartGame -= StartGame;
        GameManager.instance.OnFinishGame -= EndGame;
    }
}
