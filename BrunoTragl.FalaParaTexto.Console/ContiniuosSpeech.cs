using Microsoft.CognitiveServices.Speech;
using System;
using System.Threading.Tasks;

namespace BrunoTragl.FalaParaTexto.ConsoleApp
{
    public class ContiniuosSpeech : SpeechConfigurationBase
    {
        public override async Task StartRecorder()
        {
            
            Console.WriteLine("Comece a falar..");

            await _speechRecognizer.StartContinuousRecognitionAsync();

            // Waits for completion. Use Task.WaitAny to keep the task rooted.
            Task.WaitAny(new[] { _stopRecognition.Task });

            //var reason = GetRecognitionResultReason(result);

            //Console.WriteLine(reason);

            //Console.WriteLine("Aguardando uma tecla..");
            //Console.ReadKey();
        }
    }
}
