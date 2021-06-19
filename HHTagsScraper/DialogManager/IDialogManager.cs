namespace HHTagsScraper.DialogManager
{
    public interface IDialogManager
    {
        string AskLocalPath();
        void ShowEndMessage(int scrapedTagsCount, int totalTagsCount);
        void ShowFileSaveMessage(string path, string fileName);
        void ShowInvalidPathMessage();
        void ShowStartRequestsMessage();
        void ShowExceptionMessage(string message);
    }
}