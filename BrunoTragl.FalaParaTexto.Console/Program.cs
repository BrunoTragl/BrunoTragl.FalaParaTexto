using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrunoTragl.FalaParaTexto.ConsoleApp
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            //SpeechConfigurationBase speechConfig = new ShortSpeech();
            SpeechConfigurationBase speechConfig = new ContiniuosSpeech();
            await speechConfig.StartRecorder();

            Console.WriteLine("Aguardando uma tecla..");
            Console.ReadKey();
        }
    }
}
