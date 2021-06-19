using HHTagsScraper.DialogManager;
using System.Linq;

namespace HHTagsScraper
{
    class Program
    {
        public static void Main()
        {
            var dialogManager = new ConsoleDialogManager();

            string path = dialogManager.AskLocalPath();

            if(path?.Length < 1)
            {
                dialogManager.ShowInvalidPathMessage();
                return;
            }

            var dirInfoResolver = new LocalDirectoryNameResolver(path);
            using var tagsScraper = new TagsScraper(dialogManager);
            var tagsSaver = new TagsLocalSaver(dialogManager);

            var names = dirInfoResolver.GetFolderNames();

            var tags = tagsScraper.getTags(names).Result;

            tagsSaver.SaveTags(path, tags);
            dialogManager.ShowEndMessage(tags.Count(), names.Count());
        }
    }
}
