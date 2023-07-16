using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTicketService
{
    public interface ITicketService
    {
        int GetTicketPrice(int ticketId);
    }
    public class TicketNotFoundException : Exception{}
    public class Ticket
    {
        public int Id { get; }
        public string Description { get; }
        public int Price { get; }

        public Ticket(int id, string description, int price)
        {
            this.Id = id;
            this.Description = description;
            this.Price = price;
        }
    }
    public class TicketService : ITicketService
    {
        public int GetTicketPrice(int ticketId)
        {
            var ticket = FakeBaseData.FirstOrDefault(t => t.Id == ticketId);
            return (ticket is null) ?
              throw new TicketNotFoundException() : ticket.Id;
        }

        public Ticket GetTicket(int ticketId)
        {
            var ticket = FakeBaseData.FirstOrDefault(t => t.Id == ticketId);
            return (ticket is null) ?
              throw new TicketNotFoundException() : ticket;
        }

        public void SaveTicket(Ticket ticket)
        {
            FakeBaseData.Add(ticket);
        }

        private void DeleteTicket(Ticket ticket)
        {
            FakeBaseData.Remove(ticket);
        }

        public IEnumerable<Ticket> GetAllTickets()
        {
            return FakeBaseData;
        }

        private List<Ticket> FakeBaseData = new List<Ticket>
  {
    new Ticket(1, "Москва - Санкт-Петербург", 3500),
    new Ticket(2, "Челябинск - Магадан", 3500),
    new Ticket(3, "Норильск - Уфа", 3500)
  };
    }
    public class TicketPrice
    {
        ITicketService ticketService;
        public TicketPrice(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        public int MakeTicketPrice(int ticketId)
        {
            return ticketService.GetTicketPrice(ticketId);
        }
    }
}
