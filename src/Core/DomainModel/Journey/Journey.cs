using System;
using System.Collections.Generic;

namespace Core.DomainModel
{
    public class JourneyEntry
    {
        public JourneyEntry(Guid id, EntryType entryType)
        {
            Id = id;
            EntryType = entryType;
        }

        public Guid Id { get; set; }
        public EntryType EntryType { get; set; }
    }



    public class Journey
    {
        private JourneyState state;
        private string currentStep;

        public Journey(JourneyDefinition journeyDefinition)
        {
            Id = Guid.Empty;
            JourneyDefinition = journeyDefinition;
            state = JourneyState.Initialized;
        }

        public Guid Id { get; set; }
        public JourneyDefinition JourneyDefinition { get; set; }

        public string CurrentStep
        {
            get { return currentStep; }
            set
            {
                currentStep = value;
                Console.WriteLine("Current Step is {0}", value);
                Console.WriteLine();
            }
        }

        public JourneyState State
        {
            get { return state; }
            private set
            {
                switch (value)
                {
                    case JourneyState.Started:
                        if (state != JourneyState.Initialized)
                            throw new AccessViolationException();
                        Console.WriteLine("I have been Started");
                        Console.WriteLine();
                        break;
                    case JourneyState.UnderProcess:
                        if (state != JourneyState.Started && state != JourneyState.UnderProcess)
                            throw new AccessViolationException();
                        Console.WriteLine("I have been UnderProcess");
                        Console.WriteLine();
                        break;
                    case JourneyState.Finished:
                        if (state != JourneyState.Started && state != JourneyState.UnderProcess)
                            throw new AccessViolationException();
                        Console.WriteLine("I have been Finished");
                        Console.WriteLine();
                        break;
                    case JourneyState.Canceled:
                        if (state != JourneyState.Started && state != JourneyState.UnderProcess)
                            throw new AccessViolationException();
                        Console.WriteLine("I have been Canceled");
                        Console.WriteLine();
                        break;
                    default:
                        throw new AccessViolationException();
                }
                state = value;
            }
        }

        public IList<JourneyEntry> Entries { get; set; }


        public void Start()
        {
            State = JourneyState.Started;
            DomainEvents.Raise(new JourneyStarted(this));
        }

        public void Proceed(JourneyInputRequest request)
        {
            State = JourneyState.UnderProcess;
            DomainEvents.Raise(new JourneyIsProceeding(this));

            if (!JourneyDefinition.IsStepAllowed(CurrentStep, request.StepName))
                throw new AccessViolationException();

            CurrentStep = request.StepName;
            if (JourneyDefinition.IsItFinalStep(CurrentStep))
                Finish();
        }

        public void Cancel()
        {
            State = JourneyState.Canceled;
            DomainEvents.Raise(new JourneyIsCanceled(this));
        }

        public void Finish()
        {
            State = JourneyState.Finished;
            DomainEvents.Raise(new JourneyIsFinished(this));
        }
    }
}