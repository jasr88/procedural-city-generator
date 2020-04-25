using UnityEngine;

public class SimpleGridManager :Singleton<SimpleGridManager> {
	private GameObject gridUnit;
	private GameObject grid;
	private Vector3 positionOffset = new Vector3 (1f, 0.5f, 1f);

	public GameObject CreateGrid(
		Vector2Int gridSize,                        // Grid Resolution
		Texture2D heightMap,                        // Usually a Perlin Noise Texture
		float heigthScale,                          // the max height that this map can reach
		Vector3 worldPosition = default (Vector3)  // The position of the grid in the world space
	) {
		GameObject gridParentGO = new GameObject ("Grid");
		Transform gridParent = gridParentGO.transform;
		gridParent.position = worldPosition;

		for (int x = 0; x < gridSize.x; x++) {
			for (int y = 0; y < gridSize.y; y++) {
				GameObject gridUnit = GameObject.CreatePrimitive (PrimitiveType.Cube);
				gridUnit.transform.SetPositionAndRotation (SampleNoiseHeight (x, y, gridSize, heightMap, heigthScale) + gridParent.position, gridParent.rotation);
				gridUnit.transform.SetParent (gridParent);
			}
		}
		return gridParentGO;
	}

	public GameObject CreateGrid(
		Vector2Int gridSize,                        // Grid Resolution
		Texture2D heightMap,                        // Usually a Perlin Noise Texture
		float heigthScale,							// the max height that this map can reach
		GameObject cubePrefab,						// The prefab to instatiate every 
		Vector3 worldPosition = default (Vector3)	// The position of the grid in the world space
	) {
		GameObject gridParentGO = new GameObject ("Grid");
		Transform gridParent = gridParentGO.transform;
		gridParent.position = worldPosition;

		for (int x = 0; x < gridSize.x; x++) {
			for (int y = 0; y < gridSize.y; y++) {
				GameObject gridUnit = Instantiate (cubePrefab, SampleNoiseHeight (x, y, gridSize, heightMap, heigthScale) + gridParent.position, gridParent.rotation);
				gridUnit.transform.SetParent (gridParent);
			}
		}

		cubePrefab.SetActive (false);
		return gridParentGO;
	}

	public GameObject CreateFilledGrid(
		Vector2Int gridSize,                        // Grid Resolution
		Texture2D heightMap,                        // Usually a Perlin Noise Texture
		float heigthScale,                          // the max height that this map can reach
		Vector3 worldPosition = default (Vector3)   // The position of the grid in the world space
	) {
		GameObject gridParentGO = new GameObject ("Grid");
		Transform gridParent = gridParentGO.transform;
		gridParent.position = worldPosition;

		for (int x = 0; x < gridSize.x; x++) {
			for (int y = 0; y < gridSize.y; y++) {
				GameObject gridUnit = GameObject.CreatePrimitive (PrimitiveType.Cube);
				gridUnit.transform.SetPositionAndRotation (SampleNoiseHeight (x, y, gridSize, heightMap, heigthScale) + gridParent.position, gridParent.rotation);
				gridUnit.transform.localScale = Vector3.one + Vector3.up * gridUnit.transform.position.y;
				gridUnit.transform.position =  new Vector3(gridUnit.transform.position.x, (gridUnit.transform.localScale.y/2), gridUnit.transform.position.z);
				gridUnit.transform.SetParent (gridParent);
			}
		}
		return gridParentGO;
	}

	private Vector3 SampleNoiseHeight(int x, int y, Vector2Int gridSize, Texture2D heightMap, float heigthScale) {
		int xResample = heightMap.width / gridSize.x;
		int yResample = heightMap.height / gridSize.y;

		float sampledValue = heightMap.GetPixel (
				Mathf.FloorToInt (x * xResample),
				Mathf.FloorToInt (y * yResample)
			).grayscale;

		return new Vector3 (x, sampledValue * heigthScale, y);
	}

}
