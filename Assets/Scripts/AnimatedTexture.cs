using UnityEngine;
using System.Collections;

public class AnimatedTexture : MonoBehaviour {

	public Vector2 speed = Vector2.zero;

	private Vector2 offset = Vector2.zero;
	Vector3 TempPos;
	Vector3 TempAlign;

	public Transform repeat;
	public Transform repeat2;
	// Use this for initialization
	void Start () {
		TempPos = transform.position;
		TempAlign = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		offset += speed * Time.deltaTime;


		repeat = this.gameObject.transform.GetChild(0);
		if (repeat.position.x < -140.1336) {
			TempAlign.x = -23.0f;
			repeat.position = new Vector3(TempAlign.x, repeat.position.y, repeat.position.z);
		}

		TempPos.x += offset[0];
		TempPos.y += offset[1];
		transform.position = TempPos;
	}
}
