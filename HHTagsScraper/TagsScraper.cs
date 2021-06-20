using HHTagsScraper.DialogManager;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HHTagsScraper
{
    public sealed class TagsScraper : IDisposable
    {
        private readonly HttpClient _client;
        private const string _userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:89.0) Gecko/20100101 Firefox/89.0";
        private readonly IDialogManager _dialogManager;
        public TagsScraper(IDialogManager dialogManager)
        {
            _client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.UserAgent.Clear();
            _client.DefaultRequestHeaders.UserAgent.ParseAdd(_userAgent);

            _dialogManager = dialogManager;
        }
        public async Task<IEnumerable<Video>> GetTags(
            IEnumerable<string> folderNames)
        {
            var nameWithTags = new List<Video>();
            _dialogManager.ShowStartRequestsMessage();

            foreach(var name in folderNames)
            {
                string url = getUrl(name);
                string htmlText = await getHtmlFromUrl(url);

                if (htmlText == null)
                    continue;

                var tags = getExtractedTags(htmlText);

                if (tags == null || !tags.Any())
                    continue;

                nameWithTags.Add(new Video { Name = name, Tags = tags });
            }

            return nameWithTags;
        }

        private async Task<string> getHtmlFromUrl(string fullUrl)
        {
            try
            {
                var response = await _client.GetStringAsync(fullUrl);
                return response;
            }
            catch(HttpRequestException e)
            {
                _dialogManager.ShowExceptionMessage(fullUrl + ':' + e.Message);
                return null;
            }
        }

        private IEnumerable<Tag> getExtractedTags(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var tags = htmlDoc.DocumentNode.Descendants("span")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("tags-links-single is_exclusive"));

            if (tags.Count() != 1)
            {
                return null;
            }

            var tagsHtml = tags.First().InnerHtml;

            htmlDoc.LoadHtml(tagsHtml);
            return htmlDoc.DocumentNode.Descendants("a")
                .Select((x) => new Tag { Title = x.InnerText });
        }

        private string getUrl(string name)
        {
            string formattedName = name.ToLower()
                .Replace(' ', '-') + "-episode-1";

            return "https://hentaihaven.com/" + formattedName;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
