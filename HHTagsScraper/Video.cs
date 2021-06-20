using System.Collections.Generic;

namespace HHTagsScraper
{
    public sealed class Video
    {
        public string Name;
        public IEnumerable<Tag> Tags;
    }

    public sealed class Tag
    {
        public string Title;
    }
}
