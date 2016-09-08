using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Newtonsoft.Json;

namespace BeatGrid
{
  public class SQLiteProvider
  {
		private SQLiteConnection _connection;

		public SQLiteProvider(string dbPath)
		{
			_connection = new SQLiteConnection(dbPath);
			// Only creates the table if it does not exist:
			_connection.CreateTable<BeatDbRow>();
			_connection.DeleteAll<BeatDbRow>(); // temp
		}

		#region Public Methods
		public Beat GetBeat(int id)
		{
			string beatJson = _connection.Find<BeatDbRow>(id).Json;
			return JsonConvert.DeserializeObject<Beat>(_connection.Find<BeatDbRow>(id).Json);
		}

		public List<Beat> GetAllBeats()
		{
			//return _connection.Table<BeatDbRow>()
			//	.Select(r => JsonConvert.DeserializeObject<Beat>(r.Json))
			//	.ToList();

			var beatDbRows = _connection.Table<BeatDbRow>().ToList();
			return null;
		}

		public void SaveBeat(Beat beat)
		{
			// Need to figure out case where it is a new insert and get the id
			var beatDbRow = beat.ToDbRow();
			if(beat.Id == -1 || _connection.Update(beatDbRow) == 0)
			{
				// New beat. Need to insert and get the id, then update Beat, serialize and save again.
				//beat.Id = _connection.Insert(beatDbRow);
				beat.Id = _connection.Insert(beatDbRow);
				_connection.Update(beat.ToDbRow());
			}

		}

		public void DeleteBeat(Beat beat)
		{
			_connection.Delete(beat);
		}

		public void DeleteBeat(int id)
		{
			_connection.Delete<BeatDbRow>(id);
		}
		#endregion
	}
}
