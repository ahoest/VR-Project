using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Vector3 velocity = Vector3.zero;

	bool isDraggingPlayer = false;
	private GameObject selectedObject = null;
	private Rigidbody rb;

	bool objectIsSelected = false;

	int time = 0;

	private GlowObject m_glowObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (isDraggingPlayer) {
			transform.position = Vector3.SmoothDamp(transform.position, selectedObject.transform.position, ref velocity, 1.0F);
			float distanceSqr = (transform.position - selectedObject.transform.position).sqrMagnitude;

			if (distanceSqr < 0.1F) {
				isDraggingPlayer = false;
				transform.position = GameObject.FindGameObjectWithTag (selectedObject.name).transform.position;
			}
		}

//		if (objectIsSelected) {
//			time++;
//			print ("Time: " + time);
//
//		}

	}

	//physics
	void FixedUpdate () {
		
		RaycastHit hit;

		var cameraCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, Camera.main.nearClipPlane));

		if (!isDraggingPlayer && Physics.Raycast(cameraCenter, Camera.main.transform.forward, out hit, 10.0F) )
		{
			var obj = hit.transform.gameObject;

			if (objectIsSelected && GameObject.ReferenceEquals(obj, selectedObject)) {
//				print ("Same object selected ");
			} else {
				
				selectedObject = hit.transform.gameObject;						

				if (obj.CompareTag ("Painting")) {
					
					//				int time = 0;

					print ("Found a painting: " + obj.name);

					print ("Time: " + time);

//					if (m_glowObject) {
//						m_glowObject.hideGlow ();
//					}

					m_glowObject = (GlowObject)obj.GetComponent (typeof(GlowObject));
					m_glowObject.showGlow ();
				
									isDraggingPlayer = true;
					objectIsSelected = true;									
				}
			}		

		}

	}
}
