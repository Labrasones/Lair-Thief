﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackdropScript : MonoBehaviour, BoardPiece {

	public GameObject backgroundPrefab;
	public BoardManager boardManager;
	public int backdropHeight = 4; // In units

	private float widthScale;

	private List<GameObject> backdrops = new List<GameObject>();

	// Use this for initialization
	void Start () {
		boardManager.RegisterPiece (this);
		widthScale = Camera.main.orthographicSize * 2.0f * Camera.main.aspect / 10;
		// Spawn some backdrops
		for (int i = -1; i < 3; i++) {
			AddBackdrop ();
		}
	}

	public void HandleBoardAdvance(int distance){
		for (int i = 0; i < backdrops.Count; i++) {
			GameObject bd = backdrops [i];
			bd.transform.position = bd.transform.position - new Vector3 (0, boardManager.CellToWorld(0,distance).y, 0);

			if (bd.transform.position.y < -backdropHeight*2) {
				backdrops.Remove (bd);
				Destroy (bd);
				i--;

				AddBackdrop();
			}
		}
	}

	private void AddBackdrop(){
		Vector3 position = new Vector3 ();
		position.x = this.transform.position.x - (Camera.main.orthographicSize * Camera.main.aspect);
        if (backdrops.Count > 0)
        {
            position.y = backdrops[backdrops.Count - 1].transform.position.y + boardManager.CellToWorld(0, backdropHeight).y;
        }
        else
        {
            position.y = -boardManager.CellToWorld(0, backdropHeight).y;
        }
		GameObject bd = Instantiate (backgroundPrefab, position, Quaternion.identity, this.transform);
		bd.transform.localScale = new Vector3 (widthScale, 1, 1);
		backdrops.Add (bd);
	}
}
