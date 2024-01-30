using System;
namespace Shared.RequestFeatures
{
	public abstract class RequestParameters
	{
		const int maxPagaeSize = 50;
		public int PageNumber { get; set; } = 1;

		private int _pageSize = 10;

		public string? OrderBy { get; set; }

		public int PageSize
		{
			get
			{
				return _pageSize;
			}

			set
			{
				_pageSize = (value > maxPagaeSize) ? maxPagaeSize : value;
			}
		}
	}
}

