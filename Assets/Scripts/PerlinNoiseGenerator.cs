using UnityEngine;

public class PerlinNoiseGenerator :Singleton<PerlinNoiseGenerator> {
	// This prevent that someone intent to instantiate this Singleton from it's constructor
	protected PerlinNoiseGenerator() { }

	// The resolution of the texture we want to create
	public Vector2Int textureResolution;
	// Randomizes the offset of the texture
	public bool isRandomOffset;
	// The actual offset of the texture
	public Vector2Int textureOffset;
	// Represents the "Zoom" for our noise
	public float noiseScale;

	// The result texture 
	private Texture2D perlinNoise;

	public Texture2D GetPerlinNoise() {
		return perlinNoise;
	}

	public void GenerateTextureNoise() {
		// Evaluate if the offset is randon, and if it does, randomized it
		if (isRandomOffset) {
			textureOffset = new Vector2Int (Random.Range (0, 99999), Random.Range (0, 99999));
		}

		perlinNoise = new Texture2D ((int)textureResolution.x, (int)textureResolution.y);

		for (int x = 0; x < textureResolution.x; x++) {
			for (int y = 0; y < textureResolution.y; y++) {
				perlinNoise.SetPixel (x, y, GeneratePerlinPixel (x, y));
			}
		}

		perlinNoise.Apply ();
	}

	private Color GeneratePerlinPixel(int x, int y) {
		float xCoord = (float) x / textureResolution.x * noiseScale + textureOffset.x;
		float yCoord = (float) y / textureResolution.y * noiseScale + textureOffset.y;

		float pixelValue = Mathf.PerlinNoise (xCoord, yCoord);

		return new Color (pixelValue, pixelValue, pixelValue);
	}

}
