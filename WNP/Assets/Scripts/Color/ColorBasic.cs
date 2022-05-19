using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
/// <summary>
/// 색의 기본.
/// 섞기, 컬러코드, 색설명, 색비율 등
/// 색의 정적 클래스임.
/// 이것을 상속하는 클래스는 없음.
/// </summary>
public sealed class ColorBasic
{
	public struct Colors
	{
		public string name { get; private set;}
		public IColored.ColorCodes colorCodes{ get; private set;}
		public string colorDesc { get; private set; }
		public int colorDimention { get; private set; }
		public Vector3 colorRatio { get; private set; }
		public void Init(IColored.ColorCodes code)
		{
			name = code.ToString();
			colorCodes = code;
			colorDesc = colorCodeDescPair[code];
			colorDimention = GetDimention(code);
			colorRatio = colorCodeRatioPair[code];
		}

		public Colors(IColored.ColorCodes code)
		{
			name = code.ToString();
			colorCodes = code;
			colorDesc = colorCodeDescPair[code];
			colorDimention = GetDimention(code);
			colorRatio = colorCodeRatioPair[code];
		}

		
	}
	public static List<Colors> allColors = new List<Colors>()
	{
		new Colors(IColored.ColorCodes.White),

		new Colors(IColored.ColorCodes.Red),
		new Colors(IColored.ColorCodes.Green),
		new Colors(IColored.ColorCodes.Blue),
		new Colors(IColored.ColorCodes.Yellow),
		new Colors(IColored.ColorCodes.Purple),
		new Colors(IColored.ColorCodes.Teal),
		new Colors(IColored.ColorCodes.Monza),
		new Colors(IColored.ColorCodes.Lime),
		new Colors(IColored.ColorCodes.Cobalt),
		new Colors(IColored.ColorCodes.Lotus),
		new Colors(IColored.ColorCodes.Goblin),
		new Colors(IColored.ColorCodes.EastBay),

		new Colors(IColored.ColorCodes.Black)
	};
	static Dictionary<IColored.ColorCodes, string> colorCodeDescPair = new Dictionary<IColored.ColorCodes, string>()
	{
		{IColored.ColorCodes.White, "흰색. 다른 색을 더 섞을 수 있습니다." },
		{IColored.ColorCodes.Red, "붉은색. ~~합니다." },
		{IColored.ColorCodes.Green, "초록색. ~~합니다." },
		{IColored.ColorCodes.Blue, "파란색. ~~합니다." },
		{IColored.ColorCodes.Yellow, "노란색. ~~합니다." },
		{IColored.ColorCodes.Purple, "보라색. ~~합니다." },
		{IColored.ColorCodes.Teal, "청록색. ~~합니다." },
		{IColored.ColorCodes.Monza, "다홍색. ~~합니다." },
		{IColored.ColorCodes.Lime, "라임색. ~~합니다." },
		{IColored.ColorCodes.Cobalt,"코발트. ~~합니다." },
		{IColored.ColorCodes.Lotus, "갈색. ~~합니다." },
		{IColored.ColorCodes.Goblin, "진녹색. ~~합니다." },
		{IColored.ColorCodes.EastBay, "청회색. ~~합니다." },
		{IColored.ColorCodes.Black, "검은색. 아무 효과도 없습니다." },
	}; //그냥 해놓은거.
	static Dictionary<IColored.ColorCodes, Vector3> colorCodeRatioPair = new Dictionary<IColored.ColorCodes, Vector3>()
	   {
	   	{IColored.ColorCodes.White, new Vector3(0,0,0) },
	   	{IColored.ColorCodes.Red, new Vector3(1,0,0) },
	   	{IColored.ColorCodes.Green, new Vector3(0,1,0) },
	   	{IColored.ColorCodes.Blue, new Vector3(0,0,1) },
	   	{IColored.ColorCodes.Yellow, new Vector3(1,1,0) },
	   	{IColored.ColorCodes.Purple, new Vector3(1,0,1) },
	   	{IColored.ColorCodes.Teal, new Vector3(0,1,1) },
	   	{IColored.ColorCodes.Monza, new Vector3(3, 0,1) },
	   	{IColored.ColorCodes.Lime, new Vector3(1, 3, 0) },
	   	{IColored.ColorCodes.Cobalt, new Vector3(0,1,3) },
	   	{IColored.ColorCodes.Lotus, new Vector3(2,1,1) },
	   	{IColored.ColorCodes.Goblin, new Vector3(1,2,1) },
	   	{IColored.ColorCodes.EastBay, new Vector3(1,1,2) },
	   	{IColored.ColorCodes.Black, new Vector3() } // 보류

	};

	public IColored.ColorCodes MixColor(IColored.ColorCodes baseC, IColored.ColorCodes catalyst)
	{
		GetDimention(baseC);
		return IColored.ColorCodes.Teal;
	}
	public IColored.ColorCodes Replace(IColored.ColorCodes color)
	{
		return color;
	}


	static int GetDimention(IColored.ColorCodes v)
	{
		return (int)(colorCodeRatioPair[v].x + colorCodeRatioPair[v].y + colorCodeRatioPair[v].z);
	}
}
