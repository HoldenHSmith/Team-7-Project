using System.Collections.Generic;

public class SortTelegramDispatchTime : IComparer<Telegram>
{

	public int Compare(Telegram x, Telegram y)
	{
		if (x.DispatchTime > y.DispatchTime)
			return 1;
		if (x.DispatchTime < y.DispatchTime)
			return -1;
		else
			return 0;
	}
}
