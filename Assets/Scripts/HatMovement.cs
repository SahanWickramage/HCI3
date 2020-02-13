using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class HatMovement : MonoBehaviour {

	public int hasKinect;

	//for kinect controls
	public GameObject BodySrcManager;
	public JointType TrackedJoint;
	private BodySourceManager bodyManager;
	private Body[] bodies;
	public float multiplier = 10f;

	//for keyboard controls
	private Rigidbody2D myBody;
	public float speed, xBound;

	// Use this for initialization
	void Start () {
		if (hasKinect == 0) {
			myBody = GetComponent<Rigidbody2D> ();
		} else if (hasKinect == 1) {
			if (BodySrcManager == null) {
				Debug.Log ("Assign body source manager.");
			} else {
				bodyManager = BodySrcManager.GetComponent<BodySourceManager> ();
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//controlls when kinect presents
		if (hasKinect == 0) {
			//Debug.Log ("keyboard");
			float h = Input.GetAxisRaw("Horizontal");

			//print (h);
			if (h > 0) {
				myBody.velocity = Vector2.right * speed;
			} else if (h < 0) {
				myBody.velocity = Vector2.left * speed;
			} else {
				myBody.velocity = Vector2.zero;
			}

			transform.position = new Vector2 (Mathf.Clamp(transform.position.x, -xBound, xBound), transform.position.y);
		}
		//controlls when not nkinect presents
			else if (hasKinect == 1){
			//Debug.Log ("kinect");
			if (bodyManager == null) {
				Debug.Log ("bodyManager is null.");
				return;
			}
			bodies = bodyManager.GetData ();
			if (bodies == null) {
				return;
			}
			Debug.Log ("have bodies");
			foreach (var body in bodies) {
				if (body == null){
					continue;
				}
				if (body.IsTracked){
					Debug.Log ("body is tracked");
					var pos = body.Joints[TrackedJoint].Position;
					//gameObject.transform.position = new Vector3(pos.X * multiplier, pos.Y * multiplier);
					transform.position = new Vector2(Mathf.Clamp(pos.X * multiplier, -xBound, xBound), transform.position.y);
				}
			}
		}
	}
}