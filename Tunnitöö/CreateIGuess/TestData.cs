using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreateIGuess.Data;

namespace CreateIGuess
{
    public class TestData
    {
        public static IList<Invoice> Invoices
        {
            get
            {
                return new List<Invoice>()
                {
                    new Invoice()
                    {
                        Id=1,
                        Date = DateTime.Now.Date,
                        InvoiceNo = "2019/120",
                        Lines = new List<InvoiceLine>()
                        {
                            new InvoiceLine
                            {
                                Id = 1,
                                LineItem = "Eesti juust",
                                Sum = 3.50
                            },
                            new InvoiceLine
                            {
                                 Id = 2,
                                 LineItem = "Ketšup",
                                 Sum = 2.50
                             }
                        }

                     }
                };
            }
        }
    }

    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string InvoiceNo { get; set; }

        public IList<InvoiceLine> Lines { get; set; }

        public Invoice()
        {
            Lines = new List<InvoiceLine>();
        }
    }
    public class InvoiceLine
    {
        public int Id { get; set; }
        public string LineItem { get; set; }
        public double Sum { get; set; }
    }
}
