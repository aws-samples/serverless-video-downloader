using Amazon.Lambda.Core;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace YDownloader
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> FunctionHandler(VideoRequest request, ILambdaContext context)
        {

            Console.WriteLine("Video:" + request.Url);
            var videoUrl = request.Url;

            var youtube = new YoutubeClient();

            //var video = await youtube.Videos.(videoUrl);


            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(
                videoUrl.Trim()
            );

            //var streams = await youtube.Videos.Streams.GetManifestAsync(videoId);
            var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
            if (streamInfo == null)
            {
                Console.Error.WriteLine("This videos has no streams");
                throw new Exception("This videos has no streams");
            }


            var fileName = $"{streamInfo.Url}.{streamInfo.Container.Name}";


            Console.WriteLine($"Downloading stream: {streamInfo.VideoQuality.ToString()} / {streamInfo.Container.Name}... ");
            var videostream = await youtube.Videos.Streams.GetAsync(streamInfo);


            var s3 = new AmazonS3Client();
            HttpClient web = new HttpClient();
            try
            {

                PutObjectRequest putObjectRequest = new PutObjectRequest
                {

                    AutoCloseStream = true,
                    BucketName = request.BucketName,
                    InputStream = videostream,
                    Key = request.VideoTitle + ".mp4"
                };

                Console.WriteLine("Uploding to S3");
                await s3.PutObjectAsync(putObjectRequest);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"error saving : {ex.Message}");

                return new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Content = new StringContent($"Error:{ex.Message}")
                };
            }

            return new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("Uploaded Successfully")
            };
        }
    }
}
