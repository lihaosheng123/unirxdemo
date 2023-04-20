using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class GroupPresenter : MonoBehaviour
{
	[SerializeField]
	private PlayerPresenter playerPresenter;

	[SerializeField]
	private Transform PlayerParent;

	//private GroupModel GroupModel;

	//	[SerializeField]

	private PlayerPresenter[] playerPresenters;

	private GroupModel model;

	[SerializeField]
	private Button update_button;
	void Awake()
	{
		
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void SetPlayerInfo(PlayerModel model)
	{
		//playerHp.text = "change";
	}

	/// <summary>
	/// リソース準備.
	/// </summary>
	public void Prepare()
	{
		List<PlayerPresenter> presenterList = new List<PlayerPresenter>();

		foreach (PlayerModel pModel in model.playerModels) {
			PlayerPresenter presenter = Instantiate(playerPresenter,PlayerParent);
			presenterList.Add(presenter);
			//この時点でplayerModelが生成しました
			presenter.Initilaize(pModel);
			presenter.Run();
		}
		playerPresenters = presenterList.ToArray();
		Run();
	}

	public void Initialize(GroupModel model)
	{
		//何で生成した前にnullチェック。。。。
		//不明・・
		//Assert.IsNotNull(model);
		this.model = model;
	}
	public void UpdateInfo()
	{
		foreach (PlayerModel pmodel in model.playerModels) {
			pmodel.UpdatePlayerData();
		}
	}
	public void Run()
	{
		//データ更新用
		//監視対象：update_button
		update_button.OnClickAsObservable()
				.Subscribe(_ => UpdateInfo())
				.AddTo(gameObject);
		//model.PlayerDataUpdate
		//	.Subscribe(pModel => UpdateGroupData(pModel))
		//	.AddTo(gameObject);


	}

}