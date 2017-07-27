namespace DomainModel
{
    class PageCreatedState : PageStateAbstraction
    {
        public PageCreatedState(Page context) : base(context)
        {
        }

        public override PageState State => PageState.Created;

        public override void Proceed()
        {
            context.setState(new PageInProgressState(context));
        }
    }
}