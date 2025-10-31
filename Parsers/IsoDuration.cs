namespace Mtd.Siri.Core.Parsers
{
	public static class IsoDuration
	{
		public static TimeSpan? TryParse(string? iso8601Duration)
		{
			if (string.IsNullOrWhiteSpace(iso8601Duration))
			{
				return null;
			}

			try
			{
				return System.Xml.XmlConvert.ToTimeSpan(iso8601Duration);
			}
			catch
			{
				return null;
			}
		}
	}
}
