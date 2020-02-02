using UnityEngine;
using System.Collections;

public class AnimatedTexture : MonoBehaviour {

	public Vector2 speed = Vector2.zero;

	private Vector2 offset = Vector2.zero;
	Vector3 TempPos;
	// Use this for initialization
	void Start () {
		TempPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		offset += speed * Time.deltaTime;

		TempPos.x += offset[0];
		TempPos.y += offset[1];
		transform.position = TempPos;
	}
}
