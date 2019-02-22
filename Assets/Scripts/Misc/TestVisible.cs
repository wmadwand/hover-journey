using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestVisible : MonoBehaviour
{
	public Collider2D col2d;
	public Collider col3d;

	public Image image;

	public bool is2D;

	public Transform screenPointTr;
	public RectTransform rect;
	public Transform cubeTr;


	private void Update()
	{
		//if (is2D)
		//{
		//	//if (IsObjectVisible2D(image))
		//	//{
		//	//	Debug.Log("col2d in");
		//	//}
		//	//else
		//	//{
		//	//	Debug.Log("col2d out");
		//	//}
		//}
		//else
		//{
		if (IsObjectVisible3D(col3d))
		{
			Debug.Log("col3d in");
		}
		else
		{
			Debug.Log("col3d out");
		}

		if (IsVisible333())
		{
			Debug.Log("IsVisible333 in");
		}
		else
		{
			Debug.Log("IsVisible333 out");
		}

		if (IsVisible444GOOD(cubeTr.position))
		{



			Debug.Log("IsVisible444 in");
		}
		else
		{
			Debug.Log("IsVisible444 out");
		}
		//}
	}





	bool IsVisible444GOOD(Vector3 point)
	{
		Vector3 viewportPoint = Camera.main.WorldToViewportPoint(point);
		return (viewportPoint.z > 0 && (new Rect(0, 0, 1, 1)).Contains(viewportPoint));
	}



	bool IsVisible333()
	{
		//CalculateRelativeRectTransformBounds	

		return RectTransformUtility.RectangleContainsScreenPoint(rect, screenPointTr.position, Camera.main) ? true : false;
	}




	bool IsObjectVisible2D(Image image /*Collider2D col*/)
	{
		//var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		//var image = GetComponent<Image>();
		var bounds = new Bounds(image.transform.localPosition, image.rectTransform.rect.size);

		var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		if (GeometryUtility.TestPlanesAABB(planes, bounds))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	bool IsObjectVisible3D(Collider col)
	{
		//var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		//var image = GetComponent<Image>();
		//var bounds = new Bounds(image.transform.localPosition, image.rectTransform.rect.size);

		var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		if (GeometryUtility.TestPlanesAABB(planes, col.bounds))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
