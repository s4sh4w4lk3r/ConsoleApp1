using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class Album
    {
        [JsonProperty(PropertyName = "artis")]
        public string? Artist { get; set; }

        [JsonProperty(PropertyName = "album")]
        public string? AlbumName { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string? Uri { get; set; }
    }

    public class Artist
    {
        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string? Uri { get; set; }
    }

    public class Episode
    {
        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "show")]
        public string? Show { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string? Uri { get; set; }
    }

    public class YourLibrary
    {
        [JsonProperty(PropertyName = "tracks")]
        public List<Track>? Tracks { get; set; }

        [JsonProperty(PropertyName = "albums")]
        public List<Album>? Albums { get; set; }

        [JsonProperty(PropertyName = "shows")]
        public List<object>? Shows { get; set; }

        [JsonProperty(PropertyName = "episodes")]
        public List<Episode>? Episodes { get; set; }

        [JsonProperty(PropertyName = "bannedTracks")]
        public List<object>? BannedTracks { get; set; }

        [JsonProperty(PropertyName = "artists")]
        public List<Artist>? Artists { get; set; }

        [JsonProperty(PropertyName = "bannedArtists")]
        public List<object>? BannedArtists { get; set; }

        [JsonProperty(PropertyName = "other")]
        public List<string>? Other { get; set; }
    }

    public class Track
    {
        [JsonProperty(PropertyName = "artist")]
        public string? Artist { get; set; }

        [JsonProperty(PropertyName = "album")]
        public string? Album { get; set; }

        [JsonProperty(PropertyName = "track")]
        public string? TrackName { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string? Uri { get; set; }
    }
}
