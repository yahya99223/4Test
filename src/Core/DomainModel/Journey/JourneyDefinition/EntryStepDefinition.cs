namespace Core.DomainModel
{
    public class EntryStepDefinition
    {
        private bool isOptional;

        public EntryStepDefinition(string stepName, bool isOptional)
        {
            StepName = stepName;
            IsOptional = isOptional;
        }


        public string StepName { get; private set; }

        public bool IsOptional
        {
            get { return isOptional; }
            set
            {
                //ToDo Add DomainEvent
                isOptional = value;
            }
        }
    }
}