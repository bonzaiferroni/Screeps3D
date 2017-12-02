using UnityEngine;

namespace Screeps3D.Selection
{
	internal static class SelectionBox
	{
		private static Texture2D _texture;
		private static Texture2D Texture
		{
			get
			{
				if (_texture != null) return _texture; // Early
				_texture = new Texture2D(1, 1);
				_texture.SetPixel(0, 0, Color.white);
				_texture.Apply();
				return _texture;
			}
		}

		private static Bounds _bounds;

		private static void DrawRect(Rect rect, Color color)
		{
			GUI.color = color;
			GUI.DrawTexture(rect, Texture);
			GUI.color = Color.white;
		}

		private static void DrawBorder(Rect rect, float thickness, Color color)
		{
			DrawRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color); // Top
			DrawRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color); // Left
			DrawRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color); // Right
			DrawRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color); // Bottom
		}

		public static void DrawSelectionBox(Vector3 screenPosition1, Vector3 screenPosition2)
		{
			_bounds = GetBounds(Camera.main, screenPosition1, screenPosition2);
			screenPosition1.y = Screen.height - screenPosition1.y;
			screenPosition2.y = Screen.height - screenPosition2.y;
			var topLeft = Vector3.Min(screenPosition1, screenPosition2);
			var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
			var rect = Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
			DrawRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
			DrawBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
		}

		public static bool IsWithinSelectionBox(ObjectView view)
		{
			return _bounds.Contains(Camera.main.WorldToViewportPoint(view.transform.position));
		}

		private static Bounds GetBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2)
		{
			var point1 = Camera.main.ScreenToViewportPoint(screenPosition1);
			var point2 = Camera.main.ScreenToViewportPoint(screenPosition2);
			var min = Vector3.Min(point1, point2);
			var max = Vector3.Max(point1, point2);
			min.z = camera.nearClipPlane;
			max.z = camera.farClipPlane;
			var bounds = new Bounds();
			bounds.SetMinMax(min, max);
			return bounds;
		}
	}
}