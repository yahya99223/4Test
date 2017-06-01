using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using ConsoleApp.Actors;

namespace ConsoleApp
{
    class Program
    {
        private static ActorSystem movieStreamingActorSystem;
        static void Main(string[] args)
        {
            movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor System Created");

            var playbackActorProps = Props.Create<PlaybackActor>();

            var playbackActorRef = movieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor");

            playbackActorRef.Tell("a bug's life");

            Console.ReadLine();
            movieStreamingActorSystem.Terminate().GetAwaiter().GetResult();
        }


    }
}
