using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeatGrid
{
	public class Beat
	{
		private const int DEFAULT_ROWS_COUNT = 4;
		private const int DEFAULT_COLUMNS_COUNT = 16;

		public Beat() { }
		public Beat(string name) { throw new NotImplementedException(); }
		public Beat(List<Measure> measures)
		{
			Id = Constants.NEW_BEAT_ID;
			Name = "_UNNAMED";
			Measures = measures;
			CurrentMeasureIndex = 0;
			Sounds = new List<Sound>();
		}
		public Beat(List<Sound> sounds, int columnsPerMeasure, int numMeasures)
		{
			Id = Constants.NEW_BEAT_ID;
			Name = "_UNNAMED";
			TimeSignature = null;
			DivisionLevel = NoteType.Unknown;
			CurrentMeasureIndex = 0;
			Measures = new List<Measure>();
			for(int i = 0; i < numMeasures; i++)
			{
				Measures.Add(new Measure(sounds.Count, columnsPerMeasure));
			}

		}

		public string Name { get; set; }
		public int Id { get; set; }
		public int CurrentMeasureIndex { get; set; }
		public Measure CurrentMeasure { get { return Measures[CurrentMeasureIndex]; } }

		public TimeSignature TimeSignature { get; set; }
		public NoteType DivisionLevel { get; set; } // Show 32nd, 16th, or 8th notes
		public List<Measure> Measures { get; set; }

		public List<Sound> Sounds { get; set; }

		public BeatDbRow ToDbRow()
		{
			return new BeatDbRow()
			{
				Id = this.Id,
				Name = this.Name,
				Json = JsonConvert.SerializeObject(this)
			};
		}

		#region Test Methods
		/// <summary>
		/// Returns a beat with 'measureCount' random measures.
		/// </summary>
		/// <param name="measureCount"></param>
		/// <returns></returns>
		public static Beat GetTestBeat(int measureCount = 1)
		{
			var measures = new List<Measure>();
			for(int i = 0; i < measureCount; i++)
			{
				measures.Add(Measure.GetRandomTestMeasure());
			}

			Beat beat = new Beat(measures);
			return beat;
		}

		public static Measure GetEmptyMeasure()
		{
			return new Measure(8, 16);
		}

		public static Measure GetTestMeasure()
		{
			var measure = new Measure(8, 16);

			measure[0, 0].On = true;
			measure[0, 2].On = true;
			measure[0, 4].On = true;
			measure[0, 6].On = true;
			measure[0, 8].On = true;
			measure[0, 10].On = true;
			measure[0, 12].On = true;
			measure[0, 14].On = true;

			measure[1, 4].On = true;
			measure[1, 12].On = true;

			measure[2, 0].On = true;
			measure[2, 8].On = true;

			return measure;
		}

		public static Measure GetRandomTestMeasure()
		{
			var measure = new Measure(8, 16);
			var rand = new Random();

			for (int i = 0; i < measure.Cells.GetLength(0); i++)
			{
				for (int j = 0; j < measure.Cells.GetLength(1); j++)
				{
					measure[i, j].On = rand.Next(0, 6) == 0;
				}
			}

			return measure;
		}
		#endregion

	}
}
