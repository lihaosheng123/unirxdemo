using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponryData {

	public int WeapId { get { return weapId; } }
	private readonly int weapId;

	public bool IsUse { get { return isUse; } }
	private readonly bool isUse;

	public float Damage { get { return damage; } }
	private readonly float damage;
	public WeaponryData(int weapId,bool isUse,float damage)
	{
		this.weapId = weapId;
		this.isUse = isUse;
		this.damage = damage;
	}
	
}
