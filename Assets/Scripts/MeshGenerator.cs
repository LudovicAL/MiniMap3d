using UnityEngine;

public class MeshGenerator : MonoBehaviour {
	public static MeshGenerator instance { get; private set; }

	[SerializeField]
	private int bellMeshDetailLevel;
	[SerializeField]
	private AnimationCurve bellHeightCurve;

	private void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
		} else {
			instance = this;
		}
	}

	//Generates a bell shaped Mesh
	public Mesh GenerateBellMesh() {
		Vector3[] vertices = new Vector3[(bellMeshDetailLevel + 1) * (bellMeshDetailLevel + 1)];
		float currentX = -0.5f;
		float currentZ = -0.5f;

		for (int i = 0, z = 0; z <= bellMeshDetailLevel; z++) {
			for (int x = 0; x <= bellMeshDetailLevel; x++, i++) {
				vertices[i] = new Vector3(currentX + ((1f / (float)bellMeshDetailLevel) * (float)x), ComputeBellHeightAtCoordinates(x, z), currentZ + ((1f / (float)bellMeshDetailLevel) * (float)z));
			}
		}

		int[] triangles = new int[bellMeshDetailLevel * bellMeshDetailLevel * 6];
		for (int ti = 0, vi = 0, z = 0; z < bellMeshDetailLevel; z++, vi++) {
			for (int x = 0; x < bellMeshDetailLevel; x++, ti += 6, vi++) {
				triangles[ti + 0] = vi;
				triangles[ti + 2] = vi + bellMeshDetailLevel + 1;
				triangles[ti + 1] = vi + 1;

				triangles[ti + 3] = vi + 1;
				triangles[ti + 5] = vi + bellMeshDetailLevel + 1;
				triangles[ti + 4] = vi + bellMeshDetailLevel + 2;
			}
		}
		return BuildMesh(vertices, triangles, GenerateUvs(vertices));
	}

	//Generate the uvs for the passed vertices
	private Vector2[] GenerateUvs(Vector3[] vertices) {
		Vector2[] uvs = new Vector2[vertices.Length];
		for (int i = 0; i < uvs.Length; i++) {
			uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
		}
		return uvs;
	}

	//Builds a Mesh with the passed vertices and triangles
	private Mesh BuildMesh(Vector3[] vertices, int[] triangles, Vector2[] uvs) {
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		mesh.RecalculateBounds();
		mesh.RecalculateTangents();
		mesh.RecalculateNormals();
		return mesh;
	}

	//Computes the height of the bell's Mesh a the passed coordinates
	private float ComputeBellHeightAtCoordinates(int x, int z) {
		if (x % bellMeshDetailLevel != 0 && z % bellMeshDetailLevel != 0) {
			float xWeight = bellHeightCurve.Evaluate((float)x / (float)bellMeshDetailLevel);
			float zWeight = bellHeightCurve.Evaluate((float)z / (float)bellMeshDetailLevel);
			float height = ((xWeight + zWeight) / 2f);
			return height;
		} else {
			return 0;
		}
	}


}
