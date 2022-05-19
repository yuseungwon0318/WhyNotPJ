using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColored
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
		Black,

		Max //최대치 용도. 랜덤이나 리스트 만들기에 사용.
	}
	public bool MixColor(ColorCodes baseC, ColorCodes catalyst, out ColorCodes result); //가불가블 반환. 이걸로 조건 판단.
	public ColorCodes Replace(ColorCodes catalyst);
}
