using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using OrderManagement.DbModel;

namespace OrderManagement.ViewModel
{
    public class CreateOrder : ObservableObject
    {
        public ICommand ProcessCommand
        {
            get { return new DelegateCommand(processCommand); }
        }

        private void processCommand()
        {
            var context = new OrderManagementDbContext();
            context.Orders.Add(new Order
            {
                Id = Guid.NewGuid(),              
            });
            context.SaveChanges();
        }
    }
}
