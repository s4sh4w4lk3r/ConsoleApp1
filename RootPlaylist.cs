using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class Item
    {
        [JsonProperty(PropertyName = "track")]
        public Track? Track { get; set; }

        [JsonProperty(PropertyName = "episode")]
        public object? Episode { get; set; }

        [JsonProperty(PropertyName = "PropertyName")]
        public LocalTrack? LocalTrack { get; set; }

        [JsonProperty(PropertyName = "addedDate")]
        public string? AddedDate { get; set; }
    }

    public class LocalTrack
    {
        [JsonProperty(PropertyName = "uri")]
        public string? Uri { get; set; }
    }

    public class Playlist
    {
        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "lastModifiedDate")]
        public string? LastModifiedDate { get; set; }

        [JsonProperty(PropertyName = "items")]
        public List<Item>? Items { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string? Description { get; set; }

        [JsonProperty(PropertyName = "numberOfFollowers")]
        public int NumberOfFollowers { get; set; }
    }

    public class RootPlaylist
    {
        [JsonProperty(PropertyName = "playlists")]
        public List<Playlist>? Playlists { get; set; }
    }
}
