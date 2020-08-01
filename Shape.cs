using Newtonsoft.Json;
using System.Collections.Generic;

namespace EntellectUniCupChallenge
{
    public class Shapes
    {

        [JsonProperty("shape_id")]
        public int shape_id { get; set; }

        [JsonProperty("bounding_box")]
        public int bounding_box { get; set; }

        [JsonProperty("capacity")]
        public int capacity { get; set; }

        [JsonProperty("orientations")]
        public List<Rotations> orientations { get; set; }
    }

    public class Rotations
    {
        [JsonProperty("rotation")]
        public int rotation { get; set; }
        [JsonProperty("cells")]
        public List<Coordinate> cells { get; set; }

    }

    public class Coordinate
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}