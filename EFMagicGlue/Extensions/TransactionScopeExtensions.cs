using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace EFMagicGlue.Extensions
{
    public static class TransactionScopeExtensions
    {
        public static TransactionScope EnsureDistributed(this TransactionScope ts)
        {
            Transaction.Current.EnlistDurable(
                DummyEnlistmentNotification.Id,
                new DummyEnlistmentNotification(),
                EnlistmentOptions.None);

            return ts;
        }

        internal class DummyEnlistmentNotification : IEnlistmentNotification
        {
            internal static readonly Guid Id = Guid.NewGuid(); 

            public void Prepare(PreparingEnlistment preparingEnlistment)
            {
                preparingEnlistment.Prepared();
            }

            public void Commit(Enlistment enlistment)
            {
                enlistment.Done();
            }

            public void Rollback(Enlistment enlistment)
            {
                enlistment.Done();
            }

            public void InDoubt(Enlistment enlistment)
            {
                enlistment.Done();
            }
        }
    }
}
