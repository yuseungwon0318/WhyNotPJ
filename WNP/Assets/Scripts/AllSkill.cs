using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// skillroot 확정시키기.
/// 자료구조 생각해보기.
/// 스킬 사용 구현 대충
/// </summary>
public class AllSkill 
{
    enum ColorCodes
	{
		None = -1,
		//기본색 + 2차색 (강화색 없음.)
		White,
		Red,
		Green,
		Blue,
		Yellow,
		Purple,
		Teal,
		//2+차색, 검정 (강화색 포함.)


		Black
	}
	enum SkillCodes
	{
		None = -1,
		Ball,
		Smash
	}
	public abstract class SkillRoot
	{
		public string name
		{
			get;
			protected set;
		}
		public int skillCode
		{
			get;
			protected set;
		}
		public int colorCode
		{
			get{ return colorCode;}
			set{ colorCode = value;}
		}
		public int manaCost
		{
			get{ return manaCost;}
			set { manaCost = value;}
		}
		public float cd
		{
			get; set;
		}
		public abstract void Use();
	}
	class Ball : SkillRoot
	{
		
		public override void Use()
		{
			//발사
		}
	}
	class Smash : SkillRoot
	{
		public override void Use()
		{
			//찍기, 지진파
		}
	}
}
