namespace FlowerSales.API.Models
{
	public class QueryParameters
	{
		const int maxSize = 100;
		private int _pageSize = 50;

		public int Page { get; set; }

		public int Size
		{
			get
			{
				return _pageSize;
			}

			set
			{
				_pageSize = Math.Min(maxSize, value);
			}
		}

		private string _sortOrder = "asc";

		public string SortBy { get; set; } = "Name";

		public string SortOrder
		{
			get
			{
				return _sortOrder;
			}

			set
			{
				if (value == "asc" || value == "desc")
				{
					_sortOrder = value;
				}
			}
		}
	}
}
