using Newtonsoft.Json;

namespace MovieApiMvc.ExternalApi
{
    public class RootObject
    {
        public List<Doc> Docs { get; set; }
        public int Total { get; set; }
        public int Limit { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }
    }

    public class Doc
    {
        public string Url { get; set; }
        public string PreviewUrl { get; set; }
        public int MovieId { get; set; }
    }

    public class ImageInfo
    {
        public List<string> Urls { get; set; }
        public List<string> PreviewUrls { get; set; }
        public int MovieId { get; set; }
    }

    public static class Deserializer
    {
        public static List<ImageInfo> GetMoviesUrlsCovers()
        {
            var json = File.ReadAllText("ExternalApi/posters.json");
            var allItems = JsonConvert.DeserializeObject<List<RootObject>>(json);

            var result = new List<ImageInfo>();
            
            int counter = 0;
            foreach (var item in allItems)
            {
                var imageInfo = new ImageInfo();
                imageInfo.PreviewUrls = new List<string>();
                imageInfo.Urls = new List<string>();

                try
                {
                    counter++;
                    imageInfo.MovieId = item.Docs[0].MovieId;
                }
                catch(IndexOutOfRangeException ex)
                {
                    Console.WriteLine(counter);
                }

        
                foreach (var doc in item.Docs)
                {
                    imageInfo.Urls.Add(doc.Url);
                    imageInfo.PreviewUrls.Add(doc.PreviewUrl);
                }
                result.Add(imageInfo);
            }

            
            return result;

        }


    }


}