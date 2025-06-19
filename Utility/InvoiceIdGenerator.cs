using System;
using System.Collections.Generic;

namespace InventoryApp.Utility
{
    class InvoiceIdGenerator
    {
        private readonly HashSet<string> generatedIds = new HashSet<string>();

        public string GenerateInvoiceId()
        {
            string uniqueInvoiceId;

            do
            {
                uniqueInvoiceId = Guid.NewGuid().ToString();
            }
            while (generatedIds.Contains(uniqueInvoiceId));

            generatedIds.Add(uniqueInvoiceId);

            return uniqueInvoiceId;
        }
    }
}
