using UnityEngine;
using System.Collections;

public class PersonAnimation : MonoBehaviour {

	public Sprite[] spritesStand, spritesWalkFront, spritesJump;
	public float framesPerSecond;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1")) {
			int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
			index = index % spritesWalkFront.Length;
			spriteRenderer.sprite = spritesWalkFront[index];
		} else {
			int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
			index = index % spritesStand.Length;
			spriteRenderer.sprite = spritesStand[index];
		}
	}
}
