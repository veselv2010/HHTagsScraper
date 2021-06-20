using System;
using System.Collections.Generic;
using System.IO;
using HHTagsScraper.DialogManager;
using Newtonsoft.Json;

namespace HHTagsScraper
{
    public class TagsLocalSaver
    {
        private IDialogManager _dialogManager;
        public TagsLocalSaver(IDialogManager dialogManager)
        {
            _dialogManager = dialogManager;
        }

        public void SaveTags(string path, IEnumerable<Video> tags)
        {
            if (!Directory.Exists(path)) throw new IOException();

            string fileName = "tags.json";
            var json = JsonConvert.SerializeObject(tags);
            File.AppendAllText(path + '/' + fileName, json);

            _dialogManager.ShowFileSaveMessage(path, fileName);
        }
    }
}
