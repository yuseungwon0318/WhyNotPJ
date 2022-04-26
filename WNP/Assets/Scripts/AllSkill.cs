using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
/// <summary>
/// 색조합 메커니즘 구상. 조건 도배하면 간략하지 못함.
/// skillroot 확정시키기.
/// 자료구조 생각해보기.
/// 스킬 사용 구현 대충
/// </summary>
public class AllSkill 
{
    public enum ColorCodes
	{
		None = -1,
		//기본색 (강화색 없음.)
		White,

		Red,
		Green,
		Blue,
		//1차
		Yellow, //적녹
		Purple, //적청
		Teal, //청녹
		//2차
		Monza, // 적청 + 적(산화철 색)
		Lime, //적녹 + 녹
		Cobalt, // 청녹 + 청
		//3차
		Lotus, //적녹 + 적청 (갈색)
		Goblin, //적녹 + 청녹
		EastBay, //적청 + 청녹
		//검정
		Black
	}
	public enum SkillCodes
	{
		None = -1,
		Ball,
		Smash
	}

	private static Dictionary<SkillCodes, string> skillCodeNamePair = new Dictionary<SkillCodes, string>()
	{
		{SkillCodes.Ball, "Ball" },
		{SkillCodes.Smash, "Smash" },
	};
	private static Dictionary<ColorCodes, string> colorCodeDescPair = new Dictionary<ColorCodes, string>()
	{
		{ColorCodes.White, "다른 색을 더 섞을 수 있습니다." },
		{ColorCodes.Red, "" },
		{ColorCodes.Green, "" },
		{ColorCodes.Blue, "" },
		{ColorCodes.Yellow, "" },
		{ColorCodes.Purple, "" },
		{ColorCodes.Teal, "" },
		{ColorCodes.Monza, "" },
		{ColorCodes.Lime, "" },
		{ColorCodes.Cobalt,"" },
		{ColorCodes.Lotus, "" },
		{ColorCodes.Goblin, "" },
		{ColorCodes.EastBay, "" },
		{ColorCodes.Black, "아무 효과도 없습니다." },
	}; //그냥 해놓은거.

	public abstract class SkillRoot
	{
		//추상 프로퍼티. 모든 스킬에는 반드시 존재해야 하는 요소들.
		public abstract string name
		{
			get;
			protected set;
		}
		public abstract SkillCodes skillCode
		{
			get;
			protected set;
		}
		

		public abstract void Init(); //변수 초기화
		public abstract void Use();

		//비 추상 프로퍼티. Init에서 전부 초기화함.
		public ColorCodes colorCode
		{
			get => colorCode;
			protected set => colorCode = value;
		}
		public string colorDesc
		{
			get => colorDesc; protected set => colorDesc = colorCodeDescPair[colorCode];
		} //색 설명 텍스트에 사용될 예정.
		public int manaCost
		{
			get => manaCost;
			set => manaCost = value;
		}
		public float coolDown
		{
			get => coolDown; set => coolDown = value;
		}
		public float skillAtk
		{
			get;
			set;
		}

		public void AddColor(ColorCodes color)
		{
			//색조합에서의 논리. 숫자 사용?
		}
	}
	public class Ball : SkillRoot
	{
		public override string name { get => name; protected set => name = skillCodeNamePair[(int)SkillCodes.Ball]; }
		public override SkillCodes skillCode { get => skillCode; protected set => skillCode = SkillCodes.Ball; }
		public override void Use()
		{
			//발사
		}
		public override void Init()
		{
			colorCode = ColorCodes.White;
			colorDesc = "";
			manaCost = 20;
			coolDown = 2;
			skillAtk = 10;
		}
	}
	public class Smash : SkillRoot
	{
		public override string name { get => name; protected set => name = "Smash"; }
		public override SkillCodes skillCode { get => skillCode; protected set => skillCode = SkillCodes.Smash; }
		public override void Use()
		{
			//찍기, 지진파
		}
		public override void Init()
		{
			colorCode = ColorCodes.White;
			colorDesc = "";
			manaCost = 10;
			coolDown = 3;
			skillAtk = 15;
		}
	}
}
