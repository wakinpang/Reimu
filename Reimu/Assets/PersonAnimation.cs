using UnityEngine;
using System.Collections;

public class PersonAnimation : MonoBehaviour
{
	public Sprite[] spritesStand, spritesWalkFront, spritesJump;

	public float framesPerSecond;

	private SpriteRenderer spriteRenderer;

	public int state;//状态

	public int direction;//当前方向

	public float speed;//速度
	
	private float flag;//控制图片？我也不好描述


	//下面定义一些常量，常量用大写好吧；

	private static int REIMU_MOVE = 1;
	
	private static int REIMU_JUMP = 2;
	
	private static int REIMU_STOP = 3;
	
	private static int REIMU_LEFT = 1;
	
	private static int REIMU_RIGHT = 2;

	private static int REIMU_LEFTJUMP = 21;

	private static int REIMU_RIGHTJUMP = 22;

	private static int REIMU_HOMELEFTJUMP = 23;

	private static int REIMU_HOMERIGHTJUMP = 24;

	private const float REIMU_JUMPTIME = 1f;

	void Start () 
	{
		speed = 0.01f;
		state = REIMU_STOP;
		direction = REIMU_RIGHT;

		spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(state == REIMU_STOP)
		{
			if(direction == REIMU_RIGHT)
			{
				int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
				index = index % 10;
				spriteRenderer.sprite = spritesStand[index];
			}
			else if(direction == REIMU_LEFT)
			{
				int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
				index = index % 10 + 10;
				spriteRenderer.sprite = spritesStand[index];
			}
		}
		else if(state == REIMU_MOVE)
		{
			if(direction == REIMU_LEFT)
			{
				transform.Translate(0-speed,0f,0f);
				int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
				index = index % 8;
				spriteRenderer.sprite = spritesWalkFront[index];
			}
			else
			{
				transform.Translate(speed,0f,0f);
				int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
				index = index % 8 +8;
				spriteRenderer.sprite = spritesWalkFront[index];
			}
		}
		else 
		{
			if(state == REIMU_LEFTJUMP)
			{

			}
			else if(state == REIMU_RIGHTJUMP)
			{

			}
			else if(state == REIMU_HOMELEFTJUMP)
			{
				
			}
			else if(state == REIMU_HOMERIGHTJUMP)
			{
				
			}//劳资实在不晓得怎么写了
		}

		if(state == REIMU_STOP || state == REIMU_MOVE) 
		{
			if(Input.GetKeyDown (KeyCode.A))
			{
				state = REIMU_MOVE;
				direction = REIMU_LEFT;
			}
			else if(Input.GetKeyDown(KeyCode.D))
			{
				state = REIMU_MOVE;
				direction = REIMU_RIGHT;
			}
			else if(Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.D))
			{
				state = REIMU_STOP;
			}
			else if(Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.A))
			{
				state = REIMU_STOP;
			}
			else if(Input.GetKey(KeyCode.W))
			{
				if(state == REIMU_LEFT)
				{
					state = REIMU_LEFTJUMP;
					flag = (int)Time.timeSinceLevelLoad;
				}
				else if (state == REIMU_RIGHT)
				{
					state = REIMU_RIGHTJUMP;
					flag = (int)Time.timeSinceLevelLoad;
				}
				else if(state == REIMU_STOP && direction == REIMU_LEFT)
				{
					state = REIMU_HOMELEFTJUMP;
					flag = REIMU_JUMPTIME;
				}
				else if(state == REIMU_STOP && direction == REIMU_RIGHT)
				{
					state = REIMU_HOMERIGHTJUMP;
					flag = (int)Time.timeSinceLevelLoad;
				}
			}
		}

	}
}
