using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
	public RawImage preview;

	public Vector2Int gridSize;
	public float heightScale;

	private GameObject grid;



	// Update is called once per frame
	void Update()
    {
		if (Input.GetKey(KeyCode.Space)) {
			if (grid != null) {
				Destroy (grid);
			}
			PerlinNoiseGenerator.Instance.GenerateTextureNoise ();
			preview.texture = PerlinNoiseGenerator.Instance.GetPerlinNoise ();

			grid = SimpleGridManager.Instance.CreateGrid (gridSize, PerlinNoiseGenerator.Instance.GetPerlinNoise (), heightScale);
		}
    }
}
