using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inputa sahip yani gelen inputları o alıyor ve oyunu yönetiyor, GameHandler a sahip yani game state i yönetiyor
/*
Blokları yaratıyor, konumlandırıyor input alarak kaydırıp yanlış input gelirse oyunu sonlandırıyor
*/

public class Platform : MonoBehaviour
{
    private InputManager ınput;
    public GameHandler game;

    private GameObject block; //kırmızı bloklar
    public GameObject runner;
    public GameObject lines;
	private GameObject road;
	public GameObject background;

    public List<GameObject> platfotmTiles; //blokları barındıran liste

    private float distance; //bi sonraki bloğun gelceği y mesafesi

    public float[] BlockPos;

    private int blockNum; // kaç tane blok olcağı

    public int blockToSlide; // o sırada kaydırılcak blok

    private int exRand = 3;
    private int sameLine = 0;

    public int pushBlockForward; // sıranın en sonuna atılcak blok

    public static Platform instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        instance = this;
    }

    private void Start()
    {
        ınput = new InputManager();

        game = new GameHandler(GameHandler.GameState.On);

		block = GameObject.FindWithTag("Block");
        runner = GameObject.FindWithTag("Runner");
		lines = GameObject.FindWithTag("Lines");
		road = GameObject.FindWithTag("Road");
		background = GameObject.FindWithTag("Background");

        lines.transform.position = new Vector2(0f, runner.transform.position.y + 7);
		road.transform.position = new Vector2(0f, runner.transform.position.y + 7);
		background.transform.position = new Vector2(0f, runner.transform.position.y + 7);

        platfotmTiles = new List<GameObject>();
        platfotmTiles.Add(block);

        BlockPos = new float[] {-1.5f,1.5f};
        distance = 0f;

        platfotmTiles[platfotmTiles.Count - 1].transform.position = new Vector2(0f,distance);
        platfotmTiles.Add((GameObject)Instantiate(block, this.transform));
        distance += 1.5f;
        platfotmTiles[platfotmTiles.Count - 1].transform.position = new Vector2(0f, distance);

        blockNum = 8;

        for (int i = 0; i < blockNum; i++)
        {
            platfotmTiles.Add((GameObject)Instantiate(block, this.transform));
            platfotmTiles[platfotmTiles.Count - 1].transform.position = BlockPositioner(1.5f);
        }
        blockToSlide = 0;
        pushBlockForward = 0;
    }

    // runner bloktan öndeyse bloğu ileri at + lines ı bir ileri taşı
    private void LateUpdate()
    {
        if(runner.transform.position.y >= platfotmTiles[pushBlockForward].transform.position.y + 3f)
        {
            lines.transform.position = new Vector2(0f, runner.transform.position.y + 3);
			road.transform.position = new Vector2(0f, runner.transform.position.y + 3);
			background.transform.position = new Vector2(0f, runner.transform.position.y + 3);

            platfotmTiles[pushBlockForward].transform.position = BlockPositioner(1.5f);
            pushBlockForward = (pushBlockForward + 1 < platfotmTiles.Count) ? pushBlockForward += 1 : pushBlockForward = 0;
        }
    }

    // kaycak bloğa karar ver, input al, input varsa ona göre haraket et
    private void Update()
    {
        if(game.game == GameHandler.GameState.Start)
        {
            if (Mathf.Approximately(platfotmTiles[blockToSlide].transform.position.x, 0f))
            {
                blockToSlide = (blockToSlide + 1 < platfotmTiles.Count) ? blockToSlide += 1 : blockToSlide = 0;
            }

            ınput.directionGetter();

            if (ınput.directions.Count != 0)
            {
                ınput.dirr = ınput.directions.Dequeue();
                if (ınput.dirr == InputManager.direction.right)
                {
                    if (Mathf.Approximately(platfotmTiles[blockToSlide].transform.position.x, BlockPos[1]))
                    {
                        platfotmTiles[blockToSlide].transform.position = new Vector2(platfotmTiles[blockToSlide].transform.position.x - 1.5f, platfotmTiles[blockToSlide].transform.position.y);
                    }
                    else
                    {
                        game.GameOver();
                        StartCoroutine(platfotmTiles[blockToSlide].GetComponent<BlockFallAnimation>().Fall(new Vector2(-1f,0)));
                        var uı = (UIHandler)FindObjectOfType(typeof(UIHandler));
                        uı.GameOver();
                    }
                }
                else if (ınput.dirr == InputManager.direction.left)
                {
                    if (Mathf.Approximately(platfotmTiles[blockToSlide].transform.position.x, BlockPos[0]))
                    {
                        platfotmTiles[blockToSlide].transform.position = new Vector2(platfotmTiles[blockToSlide].transform.position.x + 1.5f, platfotmTiles[blockToSlide].transform.position.y);
                    }
                    else
                    {
                        game.GameOver();
                        StartCoroutine(platfotmTiles[blockToSlide].GetComponent<BlockFallAnimation>().Fall(new Vector2(1f,0)));
                        var uı = (UIHandler)FindObjectOfType(typeof(UIHandler));
                        uı.GameOver();
                    }
                }
            }
        }
    }

    // blokları konumlandıran fonksiyon
    private Vector2 BlockPositioner(float rate)
    {
        int tempEx = exRand;
        exRand = RandomPos.RandomPosition(exRand, sameLine);
        sameLine = (tempEx == exRand) ? sameLine += 1 : sameLine = 0;
        distance += rate;

        return new Vector2(BlockPos[exRand], distance);

    }

}
