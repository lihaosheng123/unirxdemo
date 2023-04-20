using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class GroupModel
{
	public IObservable<PlayerModel> PlayerDataUpdate { get { return playerDataUpdate; } }
	private readonly Subject<PlayerModel> playerDataUpdate = new Subject<PlayerModel>();

	public IObservable<PlayerModel> OnPlayer { get { return onPlayer; } }
	private readonly Subject<PlayerModel> onPlayer = new Subject<PlayerModel>();
	public List<PlayerModel> playerModels { get; private set; }

	/// <summary>
	/// データの作成
	/// </summary>
	/// <returns></returns>
	private WeaponryData CreateWeaponryData(int weapId,bool isUse,float damage)
	{
		return new WeaponryData(weapId,isUse,damage);
	}
	/// <summary>
	///  リソース準備.
	///  //仮データを使用
	/// </summary>
	public void Prepare()
	{
		//データ生成	
		int random_a = Random.Range(1,200);
		float random_b = Random.Range(1,10);
		WeaponryData _weaponryData = CreateWeaponryData((3+random_a),true,(5.5f+ random_b));

		PlayerModel model = new PlayerModel(_weaponryData);

		//playerModelのリソース準備
		model.Prepare();
		playerModels.Add(model);
		//データ初期化
		Run();
		Debug.Log("playerのリソース準備完了");
	}
	public GroupModel()
	{ 
		playerModels = new List<PlayerModel>(1);
	}
	/// <summary>
	/// データ初期化の場合
	/// データ更新の場合
	/// </summary>
	public void Run()
	{
		//player data update	
		foreach (PlayerModel pModel in playerModels) {
			pModel.Run();
		}
	}

}
