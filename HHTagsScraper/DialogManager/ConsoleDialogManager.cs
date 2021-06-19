using System;

namespace HHTagsScraper.DialogManager
{
    public class ConsoleDialogManager : IDialogManager
    {
        private DateTime _startTime;
        private DateTime _currentTime { get => DateTime.Now; }

        public void ShowStartRequestsMessage()
        {
            printMessage("Requests started");
            _startTime = _currentTime;
        }

        public void ShowFileSaveMessage(string path, string fileName)
        {
            printMessage($"Scraped tags saved to {path} as {fileName}");
        }

        public void ShowEndMessage(int scrapedTagsCount, int totalTagsCount)
        {
            var diff = _currentTime - _startTime;

            printMessage($"Scraped {scrapedTagsCount}/{totalTagsCount} in {diff}");
            showPressAnyKeyMessage();
        }

        private void showPressAnyKeyMessage()
        {
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }

        public void ShowInvalidPathMessage()
        {
            printMessage("Invalid path");
            showPressAnyKeyMessage();
        }

        public void ShowExceptionMessage(string message)
        {
            printMessage(message);
        }

        public string AskLocalPath()
        {
            Console.Write("Enter folder path: ");
            return Console.ReadLine();
        }

        private void printMessage(string message)
        {
            Console.WriteLine($"[{_currentTime}] {message}");
        }
    }
}
