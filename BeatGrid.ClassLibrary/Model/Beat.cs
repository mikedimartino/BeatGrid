using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeatGrid
{
	public class Beat
	{
		public Beat() { }
		public Beat(List<Sound> sounds, List<Measure> measures)
		{
			Id = Constants.NEW_BEAT_ID;
			Name = "_UNNAMED";
			Measures = measures;
			CurrentMeasureIndex = 0;
			Sounds = sounds;
			Tempo = Constants.DEFAULT_TEMPO;
		}
		//public Beat(List<Measure> measures)
		//{
		//	Id = Constants.NEW_BEAT_ID;
		//	Name = "_UNNAMED";
		//	Measures = measures;
		//	CurrentMeasureIndex = 0;
		//	Sounds = new List<Sound>();
		//}
		//public Beat(List<Sound> sounds, int columnsPerMeasure, int numMeasures)
		//{
		//	Id = Constants.NEW_BEAT_ID;
		//	Name = "_UNNAMED";
		//	TimeSignature = null;
		//	DivisionLevel = NoteType.Unknown;
		//	CurrentMeasureIndex = 0;
		//	Measures = new List<Measure>();
		//	for(int i = 0; i < numMeasures; i++)
		//	{
		//		Measures.Add(new Measure(sounds.Count, columnsPerMeasure));
		//	}
		//	Sounds = sounds;
		//}

		public string Name { get; set; }
		public int Id { get; set; }
		public int CurrentMeasureIndex { get; set; }
		public Measure CurrentMeasure { get { return Measures[CurrentMeasureIndex]; } }
		public TimeSignature TimeSignature { get; set; }
		public NoteType DivisionLevel { get; set; } // Show 32nd, 16th, or 8th notes
		public List<Measure> Measures { get; set; }
		public int SubdivisionsPerBeat { get; set; } // Beat as in BPM
		public int PlaybackIntervalMs { get; set; }

		private int _Tempo = Constants.DEFAULT_TEMPO;
		public int Tempo
		{
			get { return _Tempo; }
			set
			{
				_Tempo = value;
				UpdatePlaybackIntervalMs();
			}
		}

		private void UpdatePlaybackIntervalMs()
		{
			double intervalBetweenBeatsMs = (1.0 / Tempo) * 60 * 1000;
			PlaybackIntervalMs = (int) intervalBetweenBeatsMs / CurrentMeasure.SubdivisionsPerBeat;
		} 

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
			var sounds = SoundHelper.GetDefaultSounds();
			Beat beat = new Beat(sounds, measures);
			return beat;
		}

		public static Beat GetEmptyBeat(int measureCount = 1)
		{
			var measures = new List<Measure>();
			for (int i = 0; i < measureCount; i++)
			{
				measures.Add(Measure.GetEmptyMeasure());
			}
			var sounds = SoundHelper.GetDefaultSounds();
			Beat beat = new Beat(sounds, measures);
			return beat;
		}

		public static Measure GetEmptyMeasure()
		{
			return new Measure(Constants.MAX_ACTIVE_SOUNDS, 16);
		}

		public static Measure GetRandomTestMeasure()
		{
			var measure = new Measure(Constants.MAX_ACTIVE_SOUNDS, 16);
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
