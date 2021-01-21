using Microsoft.CognitiveServices.Speech;
using System;
using System.Threading.Tasks;

namespace BrunoTragl.FalaParaTexto.ConsoleApp
{
    public class ShortSpeech : SpeechConfigurationBase
    {
        public override async Task StartRecorder()
        {
            
            Console.WriteLine("Comece a falar..");

            var result = await _speechRecognizer.RecognizeOnceAsync();
            var reason = GetRecognitionResultReason(result);

            Console.WriteLine(reason);

            Console.WriteLine("Aguardando uma tecla..");
            Console.ReadKey();
        }
    }
}
