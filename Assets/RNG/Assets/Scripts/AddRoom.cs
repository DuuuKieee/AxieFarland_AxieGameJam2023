using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddRoom : MonoBehaviour
{

	private RoomTemplates templates;
	public int roomCount;
	public LayerMask roomLayer;

	void Start()
	{

		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		templates.rooms.Add(this.gameObject);
		roomCount = GameController.instance.room;
		GameController.instance.room++;
		Debug.Log(roomCount);
	}
}
