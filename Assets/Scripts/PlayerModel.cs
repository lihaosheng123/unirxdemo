using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UniRx;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

/// <summary>
/// ビジネスロジック
/// 基本的にデータに対する処理
/// </summary>
public class PlayerModel : MonoBehaviour
{

	//HP
	public ReactiveProperty<int> Health { get; private set; }

	public IReadOnlyReactiveProperty<Texture> MainTex { get { return mainTex; } }
	private readonly ReactiveProperty<Texture> mainTex = new ReactiveProperty<Texture>();

	//Max Hp
	public IReadOnlyReactiveProperty<int> MaxHp { get { return maxHp; } }
	private readonly ReactiveProperty<int> maxHp = new ReactiveProperty<int>();

	//isDead
	public ReactiveProperty<bool> IsDead { get; private set; }

	//name
	public IReadOnlyReactiveProperty<string> Name { get { return _name; } }
	private readonly ReactiveProperty<string> _name = new ReactiveProperty<string>();


	public bool IsUse { get { return isUse; } }
	private readonly bool isUse;

	public int WeapId { get { return weapId; } }
	private readonly int weapId;

	public IReadOnlyReactiveProperty<float> WeapDamage { get { return weapDamage; } }
	private readonly ReactiveProperty<float> weapDamage = new ReactiveProperty<float>();

	private readonly WeaponryData weaponryData;


	public PlayerModel(WeaponryData weaponryData)
	{
		Assert.IsNotNull(weaponryData);
		this.weaponryData = weaponryData;
		weapId = this.weaponryData.WeapId;
		weapDamage.Value = this.weaponryData.Damage;
	}
	// リソースの準備をする
	/// <summary>
	/// リソースロード、データnewとか処理	
	/// </summary>
	public void Prepare()
	{
		Health = new ReactiveProperty<int>(100);
		IsDead = new ReactiveProperty<bool>(false);

		LoadPrefabFromResObservable<Texture>("dog").Subscribe(tex => mainTex.Value = tex);

	}
	 
	/// <summary>
	/// 仮名前設定
	/// </summary>
	public void SetNameTxt()
	{
		int name = Random.Range(1,200);
		_name.Value = "Player_"+ name.ToString();
	}
	/// <summary>
	/// 仮データ
	/// </summary>
	public void SetMaxHp()
	{
		maxHp.Value = 100;
	}
	/// <summary>
	/// run
	/// </summary>
	public void Run()
	{
		SetMaxHp();
		SetNameTxt();
	}
	/// <summary>
	/// Resourcesからデータロード
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="path"></param>
	/// <param name="isAsync"></param>
	/// <returns></returns>
	public IObservable<T> LoadPrefabFromResObservable<T>(string path,bool isAsync = true) where T : Object
	{
		if (isAsync)
			return Resources.LoadAsync<T>(path)
				//ロード
				.AsAsyncOperationObservable()
				.Select(resourceRequest => resourceRequest.asset)
				.OfType<Object,T>();
		return Observable.Start(() => Resources.Load<T>(path),Scheduler.MainThread);
	}
	/// <summary>
	/// データ更新
	/// </summary>
	/// <param name="model"></param>
	public void UpdatePlayerData()
	{
		Run();
		Debug.Log("データ更新");
	}
}