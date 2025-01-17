using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class AirBlast : MonoBehaviour {

    bool allowedToShoot = true;
    int blastAnim;
    const float DISPPEAR_TIME = 0.7f;
    const float WAIT_TIME = 8f;

    Animator anim;
    AudioSource shootSound;
    public GameObject airWall;
    Transform myTransform;

    void Start() {
        anim = GameObject.FindWithTag("AirPushHand").GetComponent<Animator>();
        blastAnim = Animator.StringToHash("Push");
        shootSound = GameObject.FindWithTag("AirBlastOrigin").GetComponent<AudioSource>();
        myTransform = transform;
    }

    void FixedUpdate() {
        if (!allowedToShoot)
            return;
        // If the key is pressed create a game object (wall) and then add a velocity
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            anim.SetTrigger(blastAnim);
            float x = myTransform.rotation.eulerAngles.x;
            if (x < 92 && x > 60) AirJump();
            else                  AirPush();
            shootSound.Play();
            StartCoroutine(WaitToShoot());
		}
    }

    void AirPush() {
        Vector3 position = GameObject.FindWithTag("AirBlastOrigin").transform.position;
        Quaternion rotation = GameObject.FindWithTag("MainCamera").transform.rotation;
        GameObject gO = Instantiate(airWall, position, rotation) as GameObject;
        gO.GetComponent<Rigidbody>().AddForce(myTransform.forward * 500000);
        Destroy(gO, DISPPEAR_TIME);
    }

    void AirJump() {
        Rigidbody rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        rb.drag = 0f;
        // force doesnt register when grounded
        rb.velocity = new Vector3(rb.velocity.x, 1f, rb.velocity.z);
        rb.AddForce(new Vector3(0f, 400, 0f), ForceMode.Impulse);
    }

    // You just used a weapon, wait to shoot again.
	IEnumerator WaitToShoot() {
        allowedToShoot = false;
        yield return new WaitForSeconds(WAIT_TIME);
     	allowedToShoot = true;
    }
}
