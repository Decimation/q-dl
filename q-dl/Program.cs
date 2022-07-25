using System.Net;
using System.Web;
using AngleSharp.Dom;
using Flurl.Http;
using Kantan.Net.Utilities;
using Novus;

namespace q_dl;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Album
{
	public int    id           { get; set; }
	public string title        { get; set; }
	public string cover        { get; set; }
	public string cover_small  { get; set; }
	public string cover_medium { get; set; }
	public string cover_big    { get; set; }
	public string cover_xl     { get; set; }
	public string md5_image    { get; set; }
	public string tracklist    { get; set; }
	public string type         { get; set; }

	public override string ToString()
	{
		return
			$"{nameof(id)}: {id}, {nameof(title)}: {title}, {nameof(tracklist)}: {tracklist}, {nameof(type)}: {type}";
	}
}

public class Artist
{
	public int    id             { get; set; }
	public string name           { get; set; }
	public string link           { get; set; }
	public string picture        { get; set; }
	public string picture_small  { get; set; }
	public string picture_medium { get; set; }
	public string picture_big    { get; set; }
	public string picture_xl     { get; set; }
	public string tracklist      { get; set; }
	public string type           { get; set; }

	public override string ToString()
	{
		return
			$"{nameof(id)}: {id}, {nameof(name)}: {name}, {nameof(link)}: {link}," +
			$" {nameof(tracklist)}: {tracklist}, {nameof(type)}: {type}";
	}
}

public class Datum
{
	public int    id                      { get; set; }
	public bool   readable                { get; set; }
	public string title                   { get; set; }
	public string title_short             { get; set; }
	public string title_version           { get; set; }
	public string link                    { get; set; }
	public int    duration                { get; set; }
	public int    rank                    { get; set; }
	public bool   explicit_lyrics         { get; set; }
	public int    explicit_content_lyrics { get; set; }
	public int    explicit_content_cover  { get; set; }
	public string preview                 { get; set; }
	public string md5_image               { get; set; }
	public Artist artist                  { get; set; }
	public Album  album                   { get; set; }
	public string type                    { get; set; }

	public override string ToString()
	{
		return
			$"{nameof(id)}: {id}, {nameof(title)}: {title}, {nameof(link)}: {link}, " +
			$"{nameof(duration)}: {duration}, {nameof(artist)}: {artist}, {nameof(album)}: {album}, {nameof(type)}: {type}";
	}
}

public class Root
{
	public List<Datum> data { get; set; }

	public int total { get; set; }
}

public static class Program
{
	public static async Task Main(string[] args)
	{
		Console.WriteLine("Hello, World!");
		Global.Setup();
		Kantan.Net.KantanNetInit.Setup();
		Kantan.KantanInit.Setup();

		var i  = Console.ReadLine();
		var i2 = HttpUtility.UrlPathEncode(i);

		var s = $"https://free-mp3-download.net/search.php?s={i2}";

		var req = await s.WithHeaders(new
		                 {
			                 User_Agent =
				                 "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36"
		                 })
		                 .WithAutoRedirect(true)
		                 .GetAsync();

		var payload = await req.GetJsonAsync<Root>();

		var total = payload.total;
		var data  = payload.data;

		Console.WriteLine($"{total}");

		foreach (Datum datum in data) {
			Console.WriteLine(datum);
		}
	}
}