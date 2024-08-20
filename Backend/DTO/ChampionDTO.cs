using System.Collections.Generic;

namespace Backend.DTO
{
    public class ChampionApiResponse
    {
        public string Type { get; set; }
        public string Format { get; set; }
        public string Version { get; set; }
        public Dictionary<string, ChampionDTO> Data { get; set; }
    }

    public class ChampionDTO
    {
        public string Version { get; set; }
        public string Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Blurb { get; set; }
        public ImageDTO Image { get; set; }
        public List<string> Tags { get; set; }
        public string Partype { get; set; }
    }
    public class ImageDTO
    {
        public string Full { get; set; }
        public string Sprite { get; set; }
        public string Group { get; set; }
        public string UrlSplash { get; set; }
        public string UrlIcon {get; set; }
    }
}
