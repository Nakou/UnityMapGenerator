using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapGenerator : MonoBehaviour {
	int[,] map;
	System.Random pRandGen;

	public bool useRandomSeed = false;
	public string seed = "";
	public int chunckLength;
	public int maxAltitude;
	public int minAltitude;

	public int[] firstPoints;

	void generateMap(){
		if (checkLenghtSize ()) {
			this.map = new int[this.chunckLength, this.chunckLength];
		} else {
			Debug.LogError("Incorrect Chunk Length, must be 2^n+1");
		}
		startGeneration ();
		for (int i = 0; i < 3; i++) {
			smoothMap();
		}
	}

	void startGeneration(){
		createDiamondSquareMap ();
	}

	bool checkLenghtSize(){
		if ((this.chunckLength - 1) % 2 != 0) {
			if(this.useRandomSeed){
				this.seed = System.DateTime.Now.ToString();
			}
			pRandGen = new System.Random(this.seed.GetHashCode());
			return false;
		}
		return true;
	}

	void createDiamondSquareMap(){
		if (firstPoints.IsFixedSize) {
			if(firstPoints.Length != 4){
				firstPoints = new int[4];
				firstPoints[0] = pRandGen.Next(minAltitude,maxAltitude);
				firstPoints[1] = pRandGen.Next(minAltitude,maxAltitude);
				firstPoints[2] = pRandGen.Next(minAltitude,maxAltitude);
				firstPoints[3] = pRandGen.Next(minAltitude,maxAltitude);
			}
		}
		int currentSize = this.chunckLength;
		while (currentSize >= 2) {
			/*DIAMOND STEP
			 * a   b *
			 *   e   *
			 * d   d *
			 ********/
			for(int x = 0; x < currentSize
			/*SQUARE STEP
			 * a   b *
			 *   e   *
			 * d   d *
			 ********/
		}
	}

	void smoothMap(){
		for(int x = 0;  x < this.chunckLength; x++){
			for(int y = 0;  y < this.chunckLength; y++){
				map[x,y] = getNeighbourAverage(x,y);
			}
		}
	}

	int getNeighbourAverage(int gridX, int gridY){
		int average = 0;
		int nbrCase = 0;
		for (int localX = gridX - 1; localX <= gridX + 1; localX++) {
			for (int localY = gridY - 1; localY <= gridY + 1; localY++) {
				if(localX >= 0 && localX < this.chunckLength 
				    && localY >= 0 && localY < this.chunckLength
				     && localX != gridX && localY != gridY ){
					average += map[localX,localY];
					nbrCase++;
				}
			}
		}
		return (int)(average / nbrCase);
	}
}
