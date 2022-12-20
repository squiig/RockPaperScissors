using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject rpsItemPrefab, containerObject;
	public Sprite rockSprite, paperSprite, scissorsSprite;
	public int paperSpawnAmt, rockSpawnAmt, scissorsSpawnAmt;

    // Start is called before the first frame update
    void Start()
    {
		InitialSpawn();
    }

	public void InitialSpawn()
	{
		for (int i = 0; i < rockSpawnAmt; i++)
		{
			Vector3 pos = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
			SpawnRPSItem(RPSType.Rock, pos);
		}
		for (int i = 0; i < paperSpawnAmt; i++)
		{
			Vector3 pos = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
			SpawnRPSItem(RPSType.Paper, pos);
		}
		for (int i = 0; i < scissorsSpawnAmt; i++)
		{
			Vector3 pos = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
			SpawnRPSItem(RPSType.Scissors, pos);
		}
	}

	public RPSItem SpawnRPSItem(RPSType type, Vector3 screenPos)
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint(screenPos);
		pos.z = 0;
		RPSItem item = Instantiate(rpsItemPrefab, pos, Quaternion.identity).GetComponent<RPSItem>();
		switch (type)
		{
			case RPSType.Rock:
				item.SetType(RPSType.Rock);
				item.gameObject.name = "rock" + item.GetHashCode();
				return item;
			case RPSType.Paper:
				item.SetType(RPSType.Paper);
				item.gameObject.name = "paper" + item.GetHashCode();
				return item;
			case RPSType.Scissors:
				item.SetType(RPSType.Scissors);
				item.gameObject.name = "scissors" + item.GetHashCode();
				return item;
			default:
				return null;
		}
	}
}
