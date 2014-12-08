using UnityEngine;
using System.Collections;

public class WipeGlass : MonoBehaviour
{
	public GameObject mainTexture;
	UITexture uiTex;
	Texture2D tex;
	int mWidth;
	int mHeight;
	int brushSize;

	void Start ()
	{
		if (mainTexture != null) {
			uiTex = mainTexture.GetComponentInChildren<UITexture> ();
		}
		tex = (Texture2D)uiTex.mainTexture;
		mWidth = tex.width;
		mHeight = tex.height;
		brushSize = 20;
	}

	void Update ()
	{
		if (Input.GetMouseButton (0)) 
			CheckPoint (Input.mousePosition);
	}

	void CheckPoint (Vector3 pScreenPos)
	{
		Vector3 worldPos = UICamera.mainCamera.ScreenToWorldPoint (pScreenPos);
		Vector3 localPos = uiTex.gameObject.transform.InverseTransformPoint (worldPos);
		if (localPos.x > -mWidth / 2 && localPos.x < mWidth / 2 && localPos.y > -mHeight / 2 && localPos.y < mHeight / 2) {

			for (int i = (int)localPos.x - brushSize; i < (int)localPos.x + brushSize; i++) {
				for (int j = (int)localPos.y - brushSize; j < (int)localPos.y + brushSize; j++) {
					if (Mathf.Pow (i - localPos.x, 2) + Mathf.Pow (j - localPos.y, 2) > Mathf.Pow (brushSize, 2))
						continue;
					Color col = tex.GetPixel (i + (int)mWidth / 2, j + (int)mHeight / 2);
					col.a = 0.0f;
					tex.SetPixel (i + (int)mWidth / 2, j + (int)mHeight / 2, col);
				}
			}
			tex.Apply ();
		}
	}
}


