namespace DomainModel
{
    internal class PageFinishedState : PageStateAbstraction
    {
        public PageFinishedState(Page context) : base(context)
        {
        }

        public override PageState State => PageState.Finished;
    }
}