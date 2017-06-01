using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace ConsoleApp.Actors
{
    public class PlaybackActor : UntypedActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating Playback Actor");
        }

        protected override void OnReceive(object message)
        {
            Console.WriteLine(message);
        }
    }
}
