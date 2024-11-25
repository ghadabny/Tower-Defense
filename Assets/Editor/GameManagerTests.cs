using UnityEngine;
using NUnit.Framework;

[TestFixture]
public class GameManagerTests  {
	private GameManager gameManager;
	private Enemy enemy;

	[Test]
	public void CheckRewardMathTest () {
		gameManager = GameObject.FindObjectOfType<GameManager>();
		Debug.Log("this bitch: " + gameManager.TotalMoney);
		gameManager.TotalMoney = 5;
		var rewardAmt = 2;
		gameManager.addMoney(rewardAmt);
		Assert.That(gameManager.TotalMoney, Is.EqualTo(7).Within(0.000001));
		gameManager.subtractMoney(rewardAmt);
		Assert.That(gameManager.TotalMoney, Is.EqualTo(5).Within(0.000001));
	}

	[Test]
	public void CheckCurrentGameStateTest() {
		gameManager = GameObject.FindObjectOfType<GameManager>();
		gameManager.WaveNumber = 0;	
		gameManager.TotalEscaped = 0;	
		gameManager.setCurrentGameState();
		Assert.That(gameManager.CurrentState, Is.EqualTo(gameStatus.play));

		gameManager.WaveNumber = 1;
		gameManager.TotalEscaped = 0;
		gameManager.setCurrentGameState();
		Assert.That(gameManager.CurrentState, Is.EqualTo(gameStatus.next));
		
		gameManager.WaveNumber = 4;
		gameManager.TotalEscaped = 10;
		gameManager.setCurrentGameState();
		Assert.That(gameManager.CurrentState, Is.EqualTo(gameStatus.gameover));
	
		gameManager.WaveNumber = 10;
		gameManager.TotalEscaped = 5;
		gameManager.setCurrentGameState();
		Assert.That(gameManager.CurrentState, Is.EqualTo(gameStatus.win));	
	}

	[Test]
	public void RegisterEnemiesTest() {
		gameManager = GameObject.FindObjectOfType<GameManager>();
		enemy = Enemy.FindObjectOfType<Enemy>();
		
		gameManager.EnemyList.Clear();
		
		gameManager.RegisterEnemy(enemy);
		Assert.That(gameManager.EnemyList.Count, Is.EqualTo(1).Within(0.000001));

		gameManager.RegisterEnemy(enemy);
		Assert.That(gameManager.EnemyList.Count, Is.EqualTo(2).Within(0.000001));	
	}


}
