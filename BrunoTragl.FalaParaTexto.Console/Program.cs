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
            var speechConfig = new SpeechConfiguration();
            speechConfig.MicConfiguration();
            await speechConfig.StartRecorder();
        }
    }
}
