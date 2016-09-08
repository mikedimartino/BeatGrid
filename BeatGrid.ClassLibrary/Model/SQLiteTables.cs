using SQLite;

namespace BeatGrid
{
	[Table("Beats")]
	public class BeatDbRow
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Json { get; set; }
	}
}