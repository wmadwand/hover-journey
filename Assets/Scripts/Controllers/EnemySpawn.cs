using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawn : MonoBehaviour
{
	public Transform[] spawnPoints;
	public GameObject enemyPrefab;
	public GameObject enemyPOIPrefab;

	public void Execute()
	{
		foreach (var item in spawnPoints)
		{
			GameObject theEnemy = Instantiate(enemyPrefab, item.position, item.rotation);

			GameObject theEnemyPOI = Instantiate(enemyPOIPrefab, Game.Instance.canvasTr);

			Enemy enm = theEnemy.GetComponent<Enemy>();
			EnemyHighlighter enmHIgh = theEnemy.GetComponent<EnemyHighlighter>();
			PointOfInterest poi = theEnemyPOI.GetComponent<PointOfInterest>();

			enm.poiGO = theEnemyPOI;
			poi.Init(enm.targetPivotPoint, enm.indicatorPivotPoint);
			enmHIgh.pointOfInterest = poi;
		}

	}
}
