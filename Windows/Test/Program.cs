using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Azure;
using Azure.AI.Vision.Common;
using Azure.AI.Vision.ImageAnalysis;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;


namespace ComputerVisionQuickstart
{
    class Program
    {
        // Add your Computer Vision key and endpoint
        static string key = Environment.GetEnvironmentVariable("VISION_KEY");
        static string endpoint = Environment.GetEnvironmentVariable("VISION_ENDPOINT");

        // URL image used for analyzing an image (image of puppy)
        private const string ANALYZE_URL_IMAGE = "https://i.etsystatic.com/17878878/r/il/a06832/2628681376/il_1588xN.2628681376_e0og.jpg";

        static void Main(string[] args)
        {
            Console.WriteLine("Azure Cognitive Services Computer Vision - .NET quickstart example");
            Console.WriteLine();

            // Create a client
            //ComputerVisionClient client = Authenticate(endpoint, key);
            var imageSource = VisionSource.FromUrl(ANALYZE_URL_IMAGE);
            var serviceOptions = new VisionServiceOptions(endpoint, new AzureKeyCredential(key));

            // Analyze an image to get features and other properties.
            //AnalyzeImageUrl(client, ANALYZE_URL_IMAGE2).Wait();
            
            AnalyzeImageUrlV4(serviceOptions, imageSource).Wait();
        }

        /*
         * AUTHENTICATE
         * Creates a Computer Vision client used by each example.
         */
        public static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
              { Endpoint = endpoint };
            return client;
        }

        /*

        public static async Task AnalyzeImageUrl(ComputerVisionClient client, string imageUrl)
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("ANALYZE IMAGE - URL");
            Console.WriteLine();

            // Creating a list that defines the features to be extracted from the image. 

            List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
            {
                VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
                VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
                VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
                 VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
                VisualFeatureTypes.Objects
            };

            Console.WriteLine($"Analyzing the image {Path.GetFileName(imageUrl)}...");
            Console.WriteLine();
            // Analyze the URL image 
            ImageAnalysis results = await client.AnalyzeImageAsync(imageUrl, visualFeatures: features);

            // Summarizes the image content.
            Console.WriteLine("Summary:");
            foreach (var caption in results.Description.Captions)
            {
                Console.WriteLine($"{caption.Text} with confidence {caption.Confidence}");
            }
            Console.WriteLine();

            // Display categories the image is divided into.
            Console.WriteLine("Categories:");
            foreach (var category in results.Categories)
            {
                Console.WriteLine($"{category.Name} with confidence {category.Score}");
            }
            Console.WriteLine();

            // Image tags and their confidence score
            Console.WriteLine("Tags:");
            foreach (var tag in results.Tags)
            {
                Console.WriteLine($"{tag.Name} {tag.Confidence}");
            }
            Console.WriteLine();

            // Objects
            Console.WriteLine("Objects:");
            foreach (var obj in results.Objects)
            {
                Console.WriteLine($"{obj.ObjectProperty} with confidence {obj.Confidence} at location {obj.Rectangle.X}, " +
                  $"{obj.Rectangle.X + obj.Rectangle.W}, {obj.Rectangle.Y}, {obj.Rectangle.Y + obj.Rectangle.H}");
            }
            Console.WriteLine();

            // Faces
            Console.WriteLine("Faces:");
            foreach (var face in results.Faces)
            {
                Console.WriteLine($"A {face.Gender} of age {face.Age} at location {face.FaceRectangle.Left}, " +
                  $"{face.FaceRectangle.Left}, {face.FaceRectangle.Top + face.FaceRectangle.Width}, " +
                  $"{face.FaceRectangle.Top + face.FaceRectangle.Height}");
            }
            Console.WriteLine();

            // Adult or racy content, if any.
            Console.WriteLine("Adult:");
            Console.WriteLine($"Has adult content: {results.Adult.IsAdultContent} with confidence {results.Adult.AdultScore}");
            Console.WriteLine($"Has racy content: {results.Adult.IsRacyContent} with confidence {results.Adult.RacyScore}");
            Console.WriteLine($"Has gory content: {results.Adult.IsGoryContent} with confidence {results.Adult.GoreScore}");
            Console.WriteLine();

            // Well-known (or custom, if set) brands.
            Console.WriteLine("Brands:");
            foreach (var brand in results.Brands)
            {
                Console.WriteLine($"Logo of {brand.Name} with confidence {brand.Confidence} at location {brand.Rectangle.X}, " +
                  $"{brand.Rectangle.X + brand.Rectangle.W}, {brand.Rectangle.Y}, {brand.Rectangle.Y + brand.Rectangle.H}");
            }
            Console.WriteLine();

            // Celebrities in image, if any.
            Console.WriteLine("Celebrities:");
            foreach (var category in results.Categories)
            {
                if (category.Detail?.Celebrities != null)
                {
                    foreach (var celeb in category.Detail.Celebrities)
                    {
                        Console.WriteLine($"{celeb.Name} with confidence {celeb.Confidence} at location {celeb.FaceRectangle.Left}, " +
                          $"{celeb.FaceRectangle.Top}, {celeb.FaceRectangle.Height}, {celeb.FaceRectangle.Width}");
                    }
                }
            }
            Console.WriteLine();

            // Popular landmarks in image, if any.
            Console.WriteLine("Landmarks:");
            foreach (var category in results.Categories)
            {
                if (category.Detail?.Landmarks != null)
                {
                    foreach (var landmark in category.Detail.Landmarks)
                    {
                        Console.WriteLine($"{landmark.Name} with confidence {landmark.Confidence}");
                    }
                }
            }
            Console.WriteLine();

            // Identifies the color scheme.
            Console.WriteLine("Color Scheme:");
            Console.WriteLine("Is black and white?: " + results.Color.IsBWImg);
            Console.WriteLine("Accent color: " + results.Color.AccentColor);
            Console.WriteLine("Dominant background color: " + results.Color.DominantColorBackground);
            Console.WriteLine("Dominant foreground color: " + results.Color.DominantColorForeground);
            Console.WriteLine("Dominant colors: " + string.Join(",", results.Color.DominantColors));
            Console.WriteLine();

            // Detects the image types.
            Console.WriteLine("Image Type:");
            Console.WriteLine("Clip Art Type: " + results.ImageType.ClipArtType);
            Console.WriteLine("Line Drawing Type: " + results.ImageType.LineDrawingType);
            Console.WriteLine();
        }
         */
        public static Task AnalyzeImageUrlV4(VisionServiceOptions serviceOptions, VisionSource imageSource)
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("ANALYZE IMAGE V4 - URL");
            Console.WriteLine();

            // Creating a list that defines the features to be extracted from the image. 

            var analysisOptions = new ImageAnalysisOptions()
            {
                Language = "en",
                Features =
                      ImageAnalysisFeature.CropSuggestions
                    | ImageAnalysisFeature.Caption
                    | ImageAnalysisFeature.DenseCaptions
                    | ImageAnalysisFeature.Objects
                    | ImageAnalysisFeature.People
                    | ImageAnalysisFeature.Text
                    | ImageAnalysisFeature.Tags
                
            };


            Console.WriteLine();
            // Analyze the URL image
            //var imageSource = VisionSource.FromFile(imageUrl);
            using var analyzer = new ImageAnalyzer(serviceOptions, imageSource, analysisOptions);
            var result = analyzer.Analyze();


            if (result.Reason == ImageAnalysisResultReason.Analyzed)
            {
                Console.WriteLine($" Image height = {result.ImageHeight}");
                Console.WriteLine($" Image width = {result.ImageWidth}");
                Console.WriteLine($" Model version = {result.ModelVersion}");

                if (result.Caption != null)
                {
                    Console.WriteLine(" Caption:");
                    Console.WriteLine($"   \"{result.Caption.Content}\", Confidence {result.Caption.Confidence:0.0000}");
                }
                
                if (result.DenseCaptions != null)
                {
                    Console.WriteLine(" Dense Captions:");
                    foreach (var caption in result.DenseCaptions)
                    {
                        Console.WriteLine($"   \"{caption.Content}\", Bounding box {caption.BoundingBox}, Confidence {caption.Confidence:0.0000}");
                    }
                }

                if (result.Objects != null)
                {
                    Console.WriteLine(" Objects:");
                    foreach (var detectedObject in result.Objects)
                    {
                        Console.WriteLine($"   \"{detectedObject.Name}\", Bounding box {detectedObject.BoundingBox}, Confidence {detectedObject.Confidence:0.0000}");
                    }
                }

                if (result.Tags != null)
                {
                    Console.WriteLine($" Tags:");
                    foreach (var tag in result.Tags)
                    {
                        Console.WriteLine($"   \"{tag.Name}\", Confidence {tag.Confidence:0.0000}");
                    }
                }

                if (result.People != null)
                {
                    Console.WriteLine($" People:");
                    foreach (var person in result.People)
                    {
                        Console.WriteLine($"   Bounding box {person.BoundingBox}, Confidence {person.Confidence:0.0000}");
                    }
                }

                if (result.CropSuggestions != null)
                {
                    Console.WriteLine($" Crop Suggestions:");
                    foreach (var cropSuggestion in result.CropSuggestions)
                    {
                        Console.WriteLine($"   Aspect ratio {cropSuggestion.AspectRatio}: "
                            + $"Crop suggestion {cropSuggestion.BoundingBox}");
                    };
                }

                if (result.Text != null)
                {
                    Console.WriteLine($" Text:");
                    foreach (var line in result.Text.Lines)
                    {
                        string pointsToString = "{" + string.Join(',', line.BoundingPolygon.Select(pointsToString => pointsToString.ToString())) + "}";
                        Console.WriteLine($"   Line: '{line.Content}', Bounding polygon {pointsToString}");

                        foreach (var word in line.Words)
                        {
                            pointsToString = "{" + string.Join(',', word.BoundingPolygon.Select(pointsToString => pointsToString.ToString())) + "}";
                            Console.WriteLine($"     Word: '{word.Content}', Bounding polygon {pointsToString}, Confidence {word.Confidence:0.0000}");
                        }
                    }
                }

                var resultDetails = ImageAnalysisResultDetails.FromResult(result);
                Console.WriteLine($" Result details:");
                Console.WriteLine($"   Image ID = {resultDetails.ImageId}");
                Console.WriteLine($"   Result ID = {resultDetails.ResultId}");
                Console.WriteLine($"   Connection URL = {resultDetails.ConnectionUrl}");
                Console.WriteLine($"   JSON result = {resultDetails.JsonResult}");
            }
            else
            {
                var errorDetails = ImageAnalysisErrorDetails.FromResult(result);
                Console.WriteLine(" Analysis failed.");
                Console.WriteLine($"   Error reason : {errorDetails.Reason}");
                Console.WriteLine($"   Error code : {errorDetails.ErrorCode}");
                Console.WriteLine($"   Error message: {errorDetails.Message}");
            }

            return Task.CompletedTask;
        }
    }
}