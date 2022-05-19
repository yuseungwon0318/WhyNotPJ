using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
/// <summary>
/// ���� �⺻.
/// ����, �÷��ڵ�, ������, ������ ��
/// ���� ���� Ŭ������.
/// �̰��� ����ϴ� Ŭ������ ����.
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
		{IColored.ColorCodes.White, "���. �ٸ� ���� �� ���� �� �ֽ��ϴ�." },
		{IColored.ColorCodes.Red, "������. ~~�մϴ�." },
		{IColored.ColorCodes.Green, "�ʷϻ�. ~~�մϴ�." },
		{IColored.ColorCodes.Blue, "�Ķ���. ~~�մϴ�." },
		{IColored.ColorCodes.Yellow, "�����. ~~�մϴ�." },
		{IColored.ColorCodes.Purple, "�����. ~~�մϴ�." },
		{IColored.ColorCodes.Teal, "û�ϻ�. ~~�մϴ�." },
		{IColored.ColorCodes.Monza, "��ȫ��. ~~�մϴ�." },
		{IColored.ColorCodes.Lime, "���ӻ�. ~~�մϴ�." },
		{IColored.ColorCodes.Cobalt,"�ڹ�Ʈ. ~~�մϴ�." },
		{IColored.ColorCodes.Lotus, "����. ~~�մϴ�." },
		{IColored.ColorCodes.Goblin, "�����. ~~�մϴ�." },
		{IColored.ColorCodes.EastBay, "ûȸ��. ~~�մϴ�." },
		{IColored.ColorCodes.Black, "������. �ƹ� ȿ���� �����ϴ�." },
	}; //�׳� �س�����.
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
	   	{IColored.ColorCodes.Black, new Vector3() } // ����

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
