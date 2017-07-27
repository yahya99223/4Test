using System;

namespace DomainModel
{
    public abstract class PageStateAbstraction
    {
        public static PageStateAbstraction GetState(PageState state, Page context)
        {
            switch (state)
            {
                case PageState.Created:
                    return new PageCreatedState(context);

                case PageState.InProgress:
                    return new PageInProgressState(context);

                case PageState.Finished:
                    return new PageFinishedState(context);
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        protected readonly Page context;

        protected PageStateAbstraction(Page context)
        {
            this.context = context;
        }

        public abstract PageState State { get; }

        public virtual void Proceed()
        {
            throw new Exception($"It's not allowed to change to {PageState.InProgress} from {context.State}");
        }

        public virtual void Finish()
        {
            throw new Exception($"It's not allowed to change to {PageState.Finished} from {context.State}");
        }
    }
}