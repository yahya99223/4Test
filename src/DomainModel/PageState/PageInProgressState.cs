using System;

namespace DomainModel
{
    class PageInProgressState : PageStateAbstraction
    {
        public PageInProgressState(Page context) : base(context)
        {
        }

        public override PageState State => PageState.InProgress;

        public override void Proceed()
        {
            if (context.canRetry())
                context.setState(new PageInProgressState(context));
            else
                base.Proceed();
        }

        public override void Finish()
        {
            if (context.canFinish())
                context.setState(new PageFinishedState(context));
            else
                base.Finish();
        }
    }
}