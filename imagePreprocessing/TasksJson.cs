using Newtonsoft.Json;
namespace imagePreprocessing
{
    class TasksJson
    {
        [JsonProperty(PropertyName = "hash")]
        public string TaskHash { get; set; }

        [JsonProperty(PropertyName = "is_busy")]
        public int IsBusy { get; set; }

        [JsonProperty(PropertyName = "is_finished")]
        public int IsFinished { get; set; }
    }
}
