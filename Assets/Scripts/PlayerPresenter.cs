using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;
using UniRx.Triggers;

public class PlayerPresenter : MonoBehaviour
{
	//Player data
	public struct PlayerData
	{
		public int PlayerID;
		public string name;
		public Button _button;
	}
	private PlayerModel model;
	[SerializeField]
	private RectTransform valueRect;
	[SerializeField]
	private Text Player_name;

	[SerializeField]
	private Button button;

	[SerializeField]
	private Text hp_show;

	[SerializeField]
	private Toggle toggle;

	[SerializeField]
	private Slider slider;

	[SerializeField]
	private Image healthImage;

	[SerializeField]
	private Text weapId;

	[SerializeField]
	private Text weapDamagetext;

	[SerializeField]
	private RawImage mainImage;
	public void Initilaize(PlayerModel model)
	{
		//Assert.IsNotNull(model);
		this.model = model;
	}

	private void Start()
	{

		//開始から5s経過
		Observable.Timer(TimeSpan.FromSeconds(5))
		.Subscribe(_ => { Debug.Log("5s 経過"); })
		.AddTo(gameObject);


	}


		public void SetHpChange()
	{
		model.Health.Value -= 5;
	}


	public void SetRate(int max,int value)
	{
		valueRect.localScale = new Vector3(( float )value / max,1,1);
	}
	/// <summary>
	/// リソース準備、データ更新する時に使用
	/// </summary>
	public void Run()
	{
		model.MainTex.Subscribe(tex => mainImage.texture = tex);
		weapId.text = model.WeapId.ToString();

		//体力Barの更新
		//Subscribe
		model.Health
			.Subscribe(
			value => {
				SetRate(model.MaxHp.Value,value);
			})
			.AddTo(gameObject);

		//体力数字の更新
		//SubscribeToText
		model.Health
			.SubscribeToText(hp_show)
			.AddTo(gameObject);

		//ボタン押下処理()
		//Health.Value > 0
		button.OnClickAsObservable()
			.Subscribe(_ => {
				if (model.Health.Value > 0) {
					SetHpChange();
				}
			})
			.AddTo(gameObject);


		//toggle on
		toggle.OnValueChangedAsObservable()
				.Where(on => on)
				.Subscribe(on => {
					{ Debug.Log("toggle is click"); }
				})
				.AddTo(gameObject);

		// if health<=0,isDead = true;
		model.Health
			.Subscribe(_ => {
				if (model.Health.Value <= 0) {
					model.IsDead.Value = true;
				}
			})
			.AddTo(gameObject);

		// if isDead = true,DebugLog"is dead"
		model.IsDead
			.Where(isDead => isDead)
			.Subscribe(_ => {
				Debug.Log("is dead");
			}
			).AddTo(gameObject);



		//UGUIのPlayer_nameはPlayerModelのnameデータへ入れ替える
		model.Name
			.Subscribe(setTxt => Player_name.text = setTxt)
			.AddTo(this);

		//slider is 0 の判断
		slider.OnValueChangedAsObservable().Subscribe(
			s => { ChangeSlider(s); }).AddTo(gameObject);


		//HPを減るにしたがって、health色変換の仕様
		model.Health
			.Subscribe(hp => {
				HpColorChange(hp);
			}).AddTo(gameObject);

		//weapDamage text
		model.WeapDamage
			.SubscribeToText(weapDamagetext)
			.AddTo(gameObject);

		//image update

	}

	public void ChangeSlider(float num)
	{
		if(num<=0) {
			Debug.Log( "slider is 0");
		}
	}

	public void HpColorChange(int hp)
	{
		if(hp <= 50)
		healthImage.color = Color.red;
	}
}