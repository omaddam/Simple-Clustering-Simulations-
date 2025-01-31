﻿using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

	#region Variables

	[Header("Entities")]

	/// <summary>
	/// References the prefab used to generate entities and display them on the grid.
	/// </summary>
	[Tooltip("References the prefab used to generate entities and display them on the grid.")]
	public GameObject EntityTemplate;

	/// <summary>
	/// References the gameobject that will hold all programmatically generated entities.
	/// </summary>
	[Tooltip("References the gameobject that will hold all programmatically generated entities.")]
	public GameObject EntitiesParent;



	[Header("Paths")]

	/// <summary>
	/// References the prefab used to generate paths and display them on the grid.
	/// </summary>
	[Tooltip("References the prefab used to generate paths and display them on the grid.")]
	public GameObject PathTemplate;

	/// <summary>
	/// References the gameobject that will hold all programmatically generated paths..
	/// </summary>
	[Tooltip("References the gameobject that will hold all programmatically generated paths.")]
	public GameObject PathsParent;

	#endregion

	#region Methods

	/// <summary>
	/// Deletes all entities and paths in the grid.
	/// </summary>
	public void Clear()
	{
		foreach (Transform entity in EntitiesParent.transform)
			GameObject.Destroy(entity.gameObject);
		foreach (Transform path in PathsParent.transform)
			GameObject.Destroy(path.gameObject);
	}

	/// <summary>
	/// Displays entities on the grid.
	/// </summary>
	/// <param name="positions">Position of the entities.</param>
	/// <param name="color">The color applied to the entities. Null = white.</param>
	/// <param name="rotation">The z rotation of the entities.</param>
	public void DisplayEntities(List<Vector2> positions, Color? color = null, bool enableEmission = true, float rotation = 0)
	{
		// For each position, create an entity
		foreach (Vector2 position in positions)
		{
			// Normalize position to fit the grid scale
			Vector2 normalizedPosition = position * 0.5f;

			// Create a new entity instance
			GameObject entity = Instantiate(EntityTemplate, EntitiesParent.transform);
			entity.transform.localPosition = new Vector3(normalizedPosition.x, normalizedPosition.y, 0);
			entity.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, rotation));

			// Extract the script
			GridEntity entityScript = entity.GetComponent<GridEntity>();

			// Set color
			if (color.HasValue)
				entityScript.SetColor(color.Value, enableEmission);
		}
	}

	/// <summary>
	/// Displays paths on the grid.
	/// </summary>
	public void DisplayPaths(List<Vector2> points, Color? color = null)
	{
		// Create a new path instance
		GameObject path = Instantiate(PathTemplate, PathsParent.transform);
		path.transform.localPosition = Vector3.zero;
		path.transform.localRotation = Quaternion.Euler(Vector3.zero);

		// Normalize points
		for (int i = 0; i < points.Count; i++)
			points[i] = points[i] * 0.5f;

		// Extract the script
		GridPath pathScript = path.GetComponent<GridPath>();

		// Set positions
		pathScript.SetPath(points, color);
	}

	/// <summary>
	/// Displays circular boundaries.
	/// </summary>
	public void DisplayCircularBoundary(Vector2 center, float radius, Color color)
	{
		// Create a new path instance
		GameObject path = Instantiate(PathTemplate, PathsParent.transform);
		path.transform.localPosition = Vector3.zero;
		path.transform.localRotation = Quaternion.Euler(Vector3.zero);

		// Extract the script
		GridPath pathScript = path.GetComponent<GridPath>();

		// Set positions
		pathScript.SetCircle(center * 0.5f, radius * 0.55f, color);
	}

	#endregion

}
