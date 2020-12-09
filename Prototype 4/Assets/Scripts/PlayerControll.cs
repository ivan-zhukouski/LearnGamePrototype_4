using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private Rigidbody playerRB;
    private GameObject focalPoint;
    public float playerSpeed = 10f;
    public bool hasPowerup = false;
    private float powerupStrength = 10f;
    public GameObject powerupIndicator;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }
    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * playerSpeed * verticalInput);
        powerupIndicator.gameObject.transform.position = transform.position - new Vector3(0, 0.5f, 0);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            Rotation.speed = 500f;
            StartCoroutine(PowerupCountdownRoutine());
        }
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerup = false;
        Rotation.speed = 100f;
        powerupIndicator.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            Debug.Log("Collision with: " + other.gameObject.name + " with powerup set to " + hasPowerup);
        }
    }
}
