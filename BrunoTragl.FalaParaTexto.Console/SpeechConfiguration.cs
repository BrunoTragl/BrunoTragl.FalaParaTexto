using System;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System.Configuration;
using System.Threading.Tasks;

namespace BrunoTragl.FalaParaTexto.ConsoleApp
{
    public class SpeechConfiguration
    {
        private SpeechConfig _speechConfig;
        private SpeechRecognizer _speechRecognizer;
        public SpeechConfiguration()
        {
            var chave = ConfigurationManager.AppSettings["chave_azure"];
            var idioma = "pt-BR";
            var uri = new Uri("https://brazilsouth.api.cognitive.microsoft.com/sts/v1.0/issuetoken");
            _speechConfig = SpeechConfig.FromEndpoint(uri, chave);
            _speechConfig.SpeechRecognitionLanguage = idioma;
        }

        public void MicConfiguration()
        {
            try
            {
                var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
                _speechRecognizer = new SpeechRecognizer(_speechConfig, audioConfig);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task StartRecorder()
        {
            var stopRecognition = new TaskCompletionSource<int>();

            _speechRecognizer.Recognizing += (s, e) =>
            {
                Console.WriteLine($"RECOGNIZING: Text={e.Result.Text}");
            };

            _speechRecognizer.Recognized += (s, e) =>
            {
                if (e.Result.Reason == ResultReason.RecognizedSpeech)
                {
                    Console.WriteLine($"RECOGNIZED: Text={e.Result.Text}");
                }
                else if (e.Result.Reason == ResultReason.NoMatch)
                {
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                }


                Console.WriteLine("Aguardando uma tecla..");
                Console.ReadKey();
            };

            _speechRecognizer.Canceled += (s, e) =>
            {
                Console.WriteLine($"CANCELED: Reason={e.Reason}");

                if (e.Reason == CancellationReason.Error)
                {
                    Console.WriteLine($"CANCELED: ErrorCode={e.ErrorCode}");
                    Console.WriteLine($"CANCELED: ErrorDetails={e.ErrorDetails}");
                    Console.WriteLine($"CANCELED: Did you update the subscription info?");
                }

                stopRecognition.TrySetResult(0);

                Console.WriteLine("Aguardando uma tecla..");
                Console.ReadKey();
            };

            _speechRecognizer.SessionStopped += (s, e) =>
            {
                Console.WriteLine("\n    Session stopped event.");
                stopRecognition.TrySetResult(0);

                Console.WriteLine("Aguardando uma tecla..");
                Console.ReadKey();
            };


            Console.WriteLine("Comece a falar..");

            var result = await _speechRecognizer.RecognizeOnceAsync();
            var reason = GetRecognitionResultReason(result);

            Console.WriteLine(reason);

            Console.WriteLine("Aguardando uma tecla..");
            Console.ReadKey();
        }

        private static string GetRecognitionResultReason(SpeechRecognitionResult result)
        {

            switch (result.Reason)
            {
                case ResultReason.NoMatch:
                    return $"Não Reconheceu";
                case ResultReason.Canceled:
                    return $"Estranho hein: {result.Reason}";
                case ResultReason.RecognizedSpeech:
                    return $"Reconheceu {result.Text}";
                default:
                    return $"Muito estranho: {result.Reason}";
            }

        }
    }
}
