using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColored
{
	public enum ColorCodes
	{
		None = -1,
		//扁夯祸 (碍拳祸 绝澜.)
		White,

		Red,
		Green,
		Blue,
		//1瞒
		Yellow, //利踌
		Purple, //利没
		Teal, //没踌
			  //2瞒
		Monza, // 利没 + 利(魂拳枚 祸)
		Lime, //利踌 + 踌
		Cobalt, // 没踌 + 没
				//3瞒
		Lotus, //利踌 + 利没 (哎祸)
		Goblin, //利踌 + 没踌
		EastBay, //利没 + 没踌
				 //八沥
		Black
	}
	public void Init();
	public ColorCodes MixColor(ColorCodes Base, ColorCodes Catalyst);
	public ColorCodes Replace(ColorCodes Catalyst);
}
