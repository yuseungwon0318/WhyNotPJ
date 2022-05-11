using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 색의 기본.
/// 섞기, 컬러코드, 색설명, 색비율 등
/// 이것을 상속하는 클래스는 색을 모두 가짐.
/// 이것을 상속하는 클래스로는 스킬, 소비품 등이 있을 예정
/// </summary>
public class ColorBasic : IColored
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
	public Dictionary<ColorCodes, string> colorCodeDescPair = new Dictionary<ColorCodes, string>()
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
	public Dictionary<ColorCodes, Vector3> colorCodeRatioPair = new Dictionary<ColorCodes, Vector3>() 
	{
		{ColorCodes.White, new Vector3(0,0,0) },
		{ColorCodes.Red, new Vector3(1,0,0) },
		{ColorCodes.Green, new Vector3(0,1,0) },
		{ColorCodes.Blue, new Vector3(0,0,1) },
		{ColorCodes.Yellow, new Vector3(1,1,0) },
		{ColorCodes.Purple, new Vector3(1,0,1) },
		{ColorCodes.Teal, new Vector3(0,1,1) },
		{ColorCodes.Monza, new Vector3(3, 0,1) },
		{ColorCodes.Lime, new Vector3(1, 3, 0) },
		{ColorCodes.Cobalt, new Vector3(0,1,3) },
		{ColorCodes.Lotus, new Vector3(2,1,1) },
		{ColorCodes.Goblin, new Vector3(1,2,1) },
		{ColorCodes.EastBay, new Vector3(1,1,2) },
		{ColorCodes.Black, new Vector3() } // 보류

	};
	public ColorCodes MixColor(ColorCodes Base, ColorCodes Catalyst)
	{
		int baseDimention = GetDimention(colorCodeRatioPair[Base]);
		int catalystDimention = GetDimention(colorCodeRatioPair[Catalyst]);
		if(baseDimention > catalystDimention)
		{
			
		}
		else if(baseDimention< catalystDimention)
		{

		}
		else if(baseDimention == catalystDimention)
		{

		}
		ColorCodes newColorcode = ColorCodes.Black;
		return newColorcode;
	}
	int GetDimention(Vector3 v)
	{
		return (int)(v.x + v.y + v.z);
	}
}
