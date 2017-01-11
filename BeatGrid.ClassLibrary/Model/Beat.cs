using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeatGrid
{
	public class Beat
	{
		public Beat() : this(SoundHelper.DefaultSounds) { }

		public Beat(List<Sound> sounds)
		{
			Id = Constants.NEW_BEAT_ID;
			Name = "_UNNAMED";
			CurrentMeasureIndex = 0;
			Sounds = sounds;
			TimeSignature = TimeSignature.Default;
			DivisionLevel = Constants.DefaultDivisionLevel;
			SubdivisionsPerBeat = DivisionLevel.GetDenominator() / TimeSignature.NoteType.GetDenominator();
			ColumnsPerMeasure = TimeSignature.NotesPerMeasure *
					(DivisionLevel.GetDenominator() / TimeSignature.NoteType.GetDenominator());
			Tempo = Constants.DEFAULT_TEMPO;

			Measures = new List<Measure>();
			Measures.Add(new Measure(this));
		}

		public Beat(List<Sound> sounds, List<Measure> measures)
		{
			Id = Constants.NEW_BEAT_ID;
			Name = "_UNNAMED";
			Measures = measures;
			CurrentMeasureIndex = 0;
			Sounds = sounds;

			TimeSignature = TimeSignature.Default;
			DivisionLevel = Constants.DefaultDivisionLevel;
			SubdivisionsPerBeat = DivisionLevel.GetDenominator() / TimeSignature.NoteType.GetDenominator();
			ColumnsPerMeasure = TimeSignature.NotesPerMeasure *
					(DivisionLevel.GetDenominator() / TimeSignature.NoteType.GetDenominator());
			Tempo = Constants.DEFAULT_TEMPO;
		}

		public string Name { get; set; }
		public int Id { get; set; }
		public int CurrentMeasureIndex { get; set; }
		public Measure CurrentMeasure { get { return Measures[CurrentMeasureIndex]; } }
		public TimeSignature TimeSignature { get; set; }
		public NoteType DivisionLevel { get; set; } // Show 32nd, 16th, or 8th notes
		public List<Measure> Measures { get; set; }
		public int SubdivisionsPerBeat { get; set; } // Beat as in BPM
		public int PlaybackIntervalMs { get; set; }
		public int ColumnsPerMeasure { get; set; }

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

		// Alternate color every beat for readability.
		public bool CellShouldUseAlternateOffColor(Cell cell)
		{
			return cell.Column % (2 * SubdivisionsPerBeat) >= SubdivisionsPerBeat;
		}

		private void UpdatePlaybackIntervalMs()
		{
			double intervalBetweenBeatsMs = (1.0 / Tempo) * 60 * 1000;
			PlaybackIntervalMs = (int) intervalBetweenBeatsMs / SubdivisionsPerBeat;
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

	}
}
