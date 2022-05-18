using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
/// <summary>
/// ���� �⺻.
/// ����, �÷��ڵ�, ������, ������ ��
/// �̰��� ����ϴ� Ŭ������ ���� ��� ����.
/// �̰��� ����ϴ� Ŭ�����δ� ��Ƽ�� ��ų, �Һ�ǰ ���� ���� ����
/// �̰��� ����ϴ� Ŭ������ ��� ��� ������, ��� Mono�� �ʿ�.
/// </summary>
public class ColorBasic : MonoBehaviour,IUsable, IColored
{
	
	//public Dictionary<ColorCodes, string> colorCodeDescPair = new Dictionary<ColorCodes, string>()
	//{
	//	{ColorCodes.White, "�ٸ� ���� �� ���� �� �ֽ��ϴ�." },
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
	//	{ColorCodes.Black, "�ƹ� ȿ���� �����ϴ�." },
	//}; //�׳� �س�����.
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
	//	{ColorCodes.Black, new Vector3() } // ����

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
