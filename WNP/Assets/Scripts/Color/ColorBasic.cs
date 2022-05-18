using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
/// <summary>
/// 색의 기본.
/// 섞기, 컬러코드, 색설명, 색비율 등
/// 이것을 상속하는 클래스는 색을 모두 가짐.
/// 이것을 상속하는 클래스로는 액티브 스킬, 소비품 등이 있을 예정
/// 이것을 상속하는 클래스는 모두 사용 가능함, 고로 Mono가 필요.
/// </summary>
public class ColorBasic : MonoBehaviour,IUsable, IColored
{
	
	//public Dictionary<ColorCodes, string> colorCodeDescPair = new Dictionary<ColorCodes, string>()
	//{
	//	{ColorCodes.White, "다른 색을 더 섞을 수 있습니다." },
	//	{ColorCodes.Red, "" },
	//	{ColorCodes.Green, "" },
	//	{ColorCodes.Blue, "" },
	//	{ColorCodes.Yellow, "" },
	//	{ColorCodes.Purple, "" },
	//	{ColorCodes.Teal, "" },
	//	{ColorCodes.Monza, "" },
	//	{ColorCodes.Lime, "" },
	//	{ColorCodes.Cobalt,"" },
	//	{ColorCodes.Lotus, "" },
	//	{ColorCodes.Goblin, "" },
	//	{ColorCodes.EastBay, "" },
	//	{ColorCodes.Black, "아무 효과도 없습니다." },
	//}; //그냥 해놓은거.
	//public Dictionary<ColorCodes, Vector3> colorCodeRatioPair = new Dictionary<ColorCodes, Vector3>() 
	//{
	//	{ColorCodes.White, new Vector3(0,0,0) },
	//	{ColorCodes.Red, new Vector3(1,0,0) },
	//	{ColorCodes.Green, new Vector3(0,1,0) },
	//	{ColorCodes.Blue, new Vector3(0,0,1) },
	//	{ColorCodes.Yellow, new Vector3(1,1,0) },
	//	{ColorCodes.Purple, new Vector3(1,0,1) },
	//	{ColorCodes.Teal, new Vector3(0,1,1) },
	//	{ColorCodes.Monza, new Vector3(3, 0,1) },
	//	{ColorCodes.Lime, new Vector3(1, 3, 0) },
	//	{ColorCodes.Cobalt, new Vector3(0,1,3) },
	//	{ColorCodes.Lotus, new Vector3(2,1,1) },
	//	{ColorCodes.Goblin, new Vector3(1,2,1) },
	//	{ColorCodes.EastBay, new Vector3(1,1,2) },
	//	{ColorCodes.Black, new Vector3() } // 보류

	//};

	public IColored.ColorCodes MixColor(IColored.ColorCodes baseC, IColored.ColorCodes catalyst)
	{
		return IColored.ColorCodes.Teal;
	}
	public IColored.ColorCodes Replace(IColored.ColorCodes color)
	{
		return color;
	}
	public void Init()
	{

	}


	int GetDimention(Vector3 v)
	{
		return (int)(v.x + v.y + v.z);
	}
	public virtual void Use() { }
}
