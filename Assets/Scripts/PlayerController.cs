using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


    public float speed = 1.0f; // Default speed sensitivity
    public ProjectileController projectilePrefab;
	
	// Update is called once per frame
	void Update () {
		/*
	    if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseScreenPos = Input.mousePosition;

            // To fire a projectile towards the mouse position, we need to be able
            // to convert a 2D screen position into a world space position. In
            // order to do this we first have to figure out how far from the camera
            // the game world plane is. Since it's played in the X-Z plane (Y = 0)
            // we can simply take the camera's y-position to be this distance.
            float distanceFromCameraToXZPlane = Camera.main.transform.position.y;

            // Next we can use the camera method ScreenToWorldPoint(). Note that this
            // method expects a Vector3 (not a Vector2), where x and y are the
            // screen pixel coordinates, and z is the world distance from the camera 
            // to project to.
            Vector3 screenPosWithZDistance = (Vector3)mouseScreenPos + (Vector3.forward * distanceFromCameraToXZPlane);
            Vector3 fireToWorldPos = Camera.main.ScreenToWorldPoint(screenPosWithZDistance);

            // Finally we create a new projectile instance, and set its velocity to
            // be the difference between the projected mouse position and the player
            // position so that it effectively travels towards the mouse.
            ProjectileController p = Instantiate<ProjectileController>(projectilePrefab);
            p.transform.position = this.transform.position;
            p.velocity = (fireToWorldPos - this.transform.position).normalized * 10.0f;

        }
        */
    }
}
