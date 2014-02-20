using System;

namespace Achamenes.ID3
{
	/// <summary>
	/// This interface sets the minimum functionality required 
	/// of a GenreManager class.
	/// </summary>
	/// <remarks>
	/// An IGenreManager is used to translates the name of genre as
	/// it appears in a tag to something that can be presented to the user 
	/// and also to translate a user entered genre name to something that
	/// can be written in a tag.
	/// <newpara/>
	/// This is normally done by translating the genre name to a genre
	/// code and the opposite. For example, the genre "Alternative" appears 
	/// as a single byte code of 20 in an ID3 v1.1 tag and as either one of the
	/// following text fields in an ID3 v2 tag: "(20)", "(20)Alternative" and "Alternative"
	/// </remarks>
	public interface IGenreManager
	{
		/// <summary>
		/// Returns the genre code of the specified genre name.
		/// </summary>
		/// <remarks>
		/// The search is case-insensitive.
		/// </remarks>
		/// <param name="genreName">The name of the genre.</param>
		/// <returns>
		/// Returns the genre code if recognized,
		/// otherwise it returns an implementation dependent value 
		/// that represents an unrecognized genre.
		/// </returns>
		byte GetGenreCode(string genreName);

		/// <summary>
		/// Returns the name of genre corresponding with the given code.
		/// </summary>
		/// <param name="code">The code of the genre.</param>
		/// <returns>
		/// Returns the name of the genre if recognized. 
		/// If not recognized, an implementation dependent string value 
		/// representing an unrecognized genre is returned.
		/// </returns>
		string GetGenreName(byte code);

		/// <summary>
		/// Translates a genre string as found in an ID3 tag to 
		/// a user friendly representative.
		/// </summary>
		/// <param name="genre">The genre name as found in the ID3 tag.</param>
		/// <returns>A user friendly translation of the given genre information.</returns>
		string TranslateToUserFriendly(string genre);

		/// <summary>
		/// Translates a user entered genre name to a string that can be written to
		/// an ID3 tag.
		/// </summary>
		/// <param name="genre">The string representing the genre as entered by the user.</param>
		/// <returns>Returns a string representing the genre that can be written to the ID3 tag.</returns>
		string TranslateToID3Friendly(string genre);
	}

	/// <summary>
	/// This class implements genre management for the standard and 
	/// extended genre codes as defined in Appendix A of the formal ID3 v2.3
	/// documentation.
	/// </summary>
	public class StandardGenreManager
		: IGenreManager
	{
		private static System.Collections.Hashtable genreDictionary=null;

		private static string[] genresList=
			{
				"Blues","Classic Rock","Country","Dance","Disco","Funk","Grunge","Hip-Hop",
				"Jazz","Metal","New Age","Oldies","Other","Pop","R&B","Rap",
				"Reggae","Rock","Techno","Industrial","Alternative","Ska","Death Metal",
				"Pranks","Soundtrack","Euro-Techno","Ambient","Trip-Hop","Vocal","Jazz+Funk",
				"Fusion","Trance","Classical","Instrumental","Acid","House","Game",
				"Sound Clip","Gospel","Noise","AlternRock","Bass","Soul","Punk",
				"Space","Meditative","Instrumental Pop","Instrumental Rock","Ethnic",
				"Gothic","Darkwave","Techno-Industrial","Electronic","Pop-Folk","Eurodance",
				"Dream","Southern Rock","Comedy","Cult","Gangsta","Top 40","Christian Rap",
				"Pop/Funk","Jungle","Native American","Cabaret","New Wave","Psychadelic",
				"Rave","Showtunes","Trailer","Lo-Fi","Tribal","Acid Punk","Acid Jazz","Polka",
				"Retro","Musical","Rock & Roll","Hard Rock","Folk","Folk-Rock","National Folk",
				"Swing","Fast Fusion","Bebob","Latin","Revival","Celtic","Bluegrass","Avantgarde",
				"Gothic Rock","Progressive Rock","Psychedelic Rock","Symphonic Rock","Slow Rock",
				"Big Band","Chorus","Easy Listening","Acoustic","Humour","Speech","Chanson",
				"Opera","Chamber Music","Sonata","Symphony","Booty Bass","Primus","Porn Groove",
				"Satire","Slow Jam","Club","Tango","Samba","Folklore","Ballad","Power Ballad",
				"Rhythmic Soul","Freestyle","Duet","Punk Rock","Drum Solo","A capella","Euro-House",
				"Dance Hall"};

		/// <summary>
		/// Returns a copy of the list of standard genre names.
		/// </summary>
		/// <returns>A copy of the genre names used in this manager.</returns>
		public static string[] GetGenreList()
		{
			string[] genres=new string[genresList.Length];
			genresList.CopyTo(genres, 0);
			return genres;
		}

		private static void LoadGenereDictionary()
		{
			//Make sure the list's length does not exceed the max sise of a byte
			System.Diagnostics.Debug.Assert(genresList.Length<byte.MaxValue);

			genreDictionary=new System.Collections.Hashtable();
			for(byte i=0;i<genresList.Length;i++)
				genreDictionary.Add(genresList[i].ToLower(), i);
		}

		#region IGenreManager Members
		/// <summary>
		/// Returns the genre code of the specified genre name.
		/// </summary>
		/// <remarks>
		/// The search is case-insensitive.
		/// </remarks>
		/// <param name="genreName">The name of the genre.</param>
		/// <returns>
		/// Returns the genre code if recognized,
		/// otherwise it returns 12 ("Other").
		/// </returns>
		public byte GetGenreCode(string genreName)
		{
			if(genreDictionary==null)
				LoadGenereDictionary();
			if(genreDictionary.Contains(genreName.ToLower()))
			{
				return (byte)genreDictionary[genreName.ToLower()];
			}
			return 12; // Unrecognized genre name
		}

		/// <summary>
		/// Returns the name of genre corresponding with the given code.
		/// </summary>
		/// <param name="code">The code of the genre.</param>
		/// <returns>
		/// Returns the name of the genre if recognized. 
		/// If not recognized, returns "Other".
		/// </returns>
		public string GetGenreName(byte code)
		{
			if(code>=0 && code <genresList.Length)
				return genresList[code];
			return "Other"; // Unrecognized genre code
		}

		/// <summary>
		/// Translates a genre string as found in an ID3 tag to 
		/// a user friendly representative.
		/// </summary>
		/// <param name="genre">The genre name as found in the ID3 tag.</param>
		/// <returns>A user friendly translation of the given genre information.</returns>
		public string TranslateToUserFriendly(string genre)
		{
			if(genre==null || genre=="")
				return "";

			System.Diagnostics.Debug.Assert(genre.Length>0);

			if(genre[0]=='(' && genre[genre.Length-1]==')')
			{
				bool number=true;
				for(int i=1;i<genre.Length-1;i++)
					if(!char.IsDigit(genre[i]))
					{
						number=false;
						break;
					}
				if(number)
				{
					try
					{
						byte n=byte.Parse(genre.Substring(1, genre.Length-2));
						genre=GetGenreName(n);
					}
					catch(Exception)
					{
					}
				}
			}
			else if(genre[0]=='(' && genre.IndexOf(')')!=-1)
			{
				bool number=true;
				for(int i=1;i<genre.IndexOf(')');i++)
					if(!char.IsDigit(genre[i]))
					{
						number=false;
						break;
					}
				if(number)
					genre=genre.Substring(genre.IndexOf(')')+1);
			}

			if(genre.Length>0)
				return genre;
			return "";
		}

		/// <summary>
		/// Translates a user entered genre name to a string that can be written to
		/// an ID3 tag.
		/// </summary>
		/// <param name="genre">The string representing the genre as entered by the user.</param>
		/// <returns>Returns a string representing the genre that can be written to the ID3 tag.</returns>
		public string TranslateToID3Friendly(string genre)
		{
			if(genreDictionary.Contains(genre.ToLower()))
				return "("+genreDictionary[genre.ToLower()].ToString()+")"+genre;
			return genre;
		}

		#endregion
	}
}
