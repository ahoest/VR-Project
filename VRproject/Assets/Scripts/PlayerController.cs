using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Vector3 velocity = Vector3.zero;

	bool isMoving = false;
	private GameObject hitPainting;
	private Rigidbody rb;

//	public Material selectedObjectMaterial;
//	public Renderer rend;

	private GlowObject m_glowObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (isMoving) {
			transform.position = Vector3.SmoothDamp(transform.position, hitPainting.transform.position, ref velocity, 1.0F);
			float distanceSqr = (transform.position - hitPainting.transform.position).sqrMagnitude;

			if (distanceSqr < 0.1F) {
				isMoving = false;
				transform.position = GameObject.FindGameObjectWithTag (hitPainting.name).transform.position;
			}
		}

	}

	void FixedUpdate () {
		
		RaycastHit hit;

		var cameraCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, Camera.main.nearClipPlane));

		if (!isMoving && Physics.Raycast(cameraCenter, Camera.main.transform.forward, out hit, 10.0F) )
		{
			var obj = hit.transform.gameObject;

			hitPainting = hit.transform.gameObject;

			if (obj.CompareTag ("Painting")) {
				print ("Found a painting: " + obj.transform.position);

				m_glowObject = (GlowObject)obj.GetComponent(typeof(GlowObject));
				m_glowObject.showGlow();
			
//				isMoving = true;
			}


		}

	}
}
