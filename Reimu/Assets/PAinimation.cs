using UnityEngine;
using System.Collections;

public class PAinimation : MonoBehaviour {
	
	private int state, direction;
	private SpriteRenderer PersonRenderer;

	//Define some values
	public int framesPersecond;
	public Sprite[] Stand, Jump, JumpToward, Dash, DashAir, Walk;
	public Sprite[] Attack0, Attack1, Attack2, Attack3, Attack4, DashAttack;

	//Define state value
	private int STAND_R = 10, STAND_L = 11, WALK_R = 20, WALK_L = 21, JUMP_R = 30, JUMP_L = 31, JUMP_TOWARD_R = 32, JUMP_TOWARD_L = 33,
				DASH_R = 40, DASH_L = 41, DASH_AIR_R = 42, DASH_AIR_L = 43;
	private int ATTACK0_R = 50, ATTACK0_L = 51, ATTACK1_R = 52, ATTACK1_L = 53, ATTACK2_R = 54, ATTACK2_L = 55, ATTACK3_R = 56, ATTACK3_L = 57, ATTACK4_R = 58, ATTACK4_L = 59;
	private int RIGHT = 0, LEFT = 1;

	//Define control value
	private bool isJump = false, isAttack0 = false, isAttack1 = false, isAttack2 = false, isAttack3 = false, isAttack4 = false;
	private float IndepenIndex = 0;
	private float LastAttack = 0; //time
	private int LastState; // last unfree state

	// Use this for initialization
	void Start () {
		PersonRenderer = GetComponent<Renderer>() as SpriteRenderer;
		framesPersecond = 13;
		state = STAND_R;
		LastState = STAND_R;
		direction = RIGHT;
	}

	//Stop action
	void StopState(){
		if (direction == RIGHT){
			state = STAND_R;
		}else {
			state = STAND_L;
		}
	}

	//isFree?
	bool isFree(){
		if (state == STAND_R || state == STAND_L || state == WALK_R || state == WALK_L)
			return true;

		return false;
	}

	//isContinue
	bool isContinue(){
		if (Time.timeSinceLevelLoad - LastAttack < 0.5f && Time.timeSinceLevelLoad - LastAttack > 0.25f)
			return true;

		return false;
	}

	//Attack?
	bool Attacking(){
		if (isAttack0 || isAttack1 || isAttack2 || isAttack3 || isAttack4)
			return true;

		return false;
	}

	//Make attack false
	void FalseAttack(){
		isAttack0 = false;
		isAttack1 = false;
		isAttack2 = false;
		isAttack3 = false;
		isAttack4 = false;
	}

	// Update is called once per frame
	void Update () {
		//State change
		if (Input.GetKey (KeyCode.A) && !isJump && !Attacking()) {
			state = WALK_L;
			direction = LEFT;
			LastAttack = 0;
		} else if (Input.GetKey (KeyCode.D) && !isJump && !Attacking()) {
			state = WALK_R;
			direction = RIGHT;
			LastAttack = 0;
		} else if (Input.GetKeyDown (KeyCode.K) && state == STAND_L) {
			state = JUMP_L;
			IndepenIndex = 0;
			isJump = true;
		} else if (Input.GetKeyDown (KeyCode.K) && state == STAND_R) {
			state = JUMP_R;
			IndepenIndex = 0;
			isJump = true;
		}else if (Input.GetKeyDown (KeyCode.J) && (LastState == ATTACK0_L || LastState == ATTACK0_R) && !isAttack1 && !isJump && isContinue ()) {
			if (direction == RIGHT)
				state = ATTACK1_R;
			else
				state = ATTACK1_L;

			FalseAttack ();
			isAttack1 = true;
			IndepenIndex = 0;
			LastAttack = Time.timeSinceLevelLoad;
			LastState = state;
		} 
		else if (Input.GetKeyDown (KeyCode.J) && !isJump && !Attacking()) {  
			if (direction == RIGHT)
				state = ATTACK0_R;
			else
				state = ATTACK0_L;

			isAttack0 = true;
			IndepenIndex = 0;
			LastAttack = Time.timeSinceLevelLoad;
			LastState = state;
		} else if (isJump || Attacking()) {
		
		} else {
			StopState ();
		}


		//Animation Play
		int index = (int)(Time.timeSinceLevelLoad * framesPersecond);
		if (state == STAND_R) {
			index = index % (Stand.Length / 2);
			PersonRenderer.sprite = Stand [index];
		}
		if (state == STAND_L) {
			index = index % (Stand.Length / 2) + (Stand.Length / 2);
			PersonRenderer.sprite = Stand [index];
		}
		if (state == WALK_R) {
			index = index % (Walk.Length / 2);
			PersonRenderer.sprite = Walk [index];
		}
		if (state == WALK_L) {
			index = index % (Walk.Length / 2) + (Walk.Length / 2);
			PersonRenderer.sprite = Walk [index];
		}
		if (state == JUMP_R) {
			IndepenIndex += Time.deltaTime * framesPersecond;
			PersonRenderer.sprite = Jump [(int)IndepenIndex];
			if (PersonRenderer.sprite == Jump [(Jump.Length / 2) - 1]) {
				StopState ();
				isJump = false;
			}
		}
		if (state == JUMP_L) {
			IndepenIndex += Time.deltaTime * framesPersecond;
			PersonRenderer.sprite = Jump [(int)IndepenIndex + (Jump.Length / 2)];
			if (PersonRenderer.sprite == Jump [Jump.Length - 1]) {
				StopState ();
				isJump = false;
			}
		}
		if (state == ATTACK0_R) {
			IndepenIndex += Time.deltaTime * framesPersecond;
			PersonRenderer.sprite = Attack0 [(int)IndepenIndex];
			if (PersonRenderer.sprite == Attack0 [(Attack0.Length / 2) - 1]) {
				StopState ();
				FalseAttack();
			}
		}
		if (state == ATTACK0_L) {
			IndepenIndex += Time.deltaTime * framesPersecond;
			PersonRenderer.sprite = Attack0 [(int)IndepenIndex + (Attack0.Length / 2)];
			if (PersonRenderer.sprite == Attack0 [Attack0.Length - 1]) {
				StopState ();
				FalseAttack ();
			}
		}
		if (state == ATTACK1_R) {
			IndepenIndex += Time.deltaTime * framesPersecond;
			PersonRenderer.sprite = Attack1 [(int)IndepenIndex];
			if (PersonRenderer.sprite == Attack1 [(Attack1.Length / 2) - 1]) {
				StopState ();
				FalseAttack();
			}
		}
		if (state == ATTACK1_L) {
			IndepenIndex += Time.deltaTime * framesPersecond;
			PersonRenderer.sprite = Attack1 [(int)IndepenIndex + (Attack1.Length / 2)];
			if (PersonRenderer.sprite == Attack1 [Attack1.Length - 1]) {
				StopState ();
				FalseAttack ();
			}
		}
	}
}
