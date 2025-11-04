namespace Jtbd.Domain.ViewModel
{
    public class FinalClusterGroup
    {
        public int ClusterId { get; set; }
        public List<int> StoryIds { get; set; } = new List<int>();
        public List<string> StoryNames { get; set; } = new List<string>();
    }
}
