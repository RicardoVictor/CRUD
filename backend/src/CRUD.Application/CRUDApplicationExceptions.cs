using System;

namespace CRUD.Application.CRUDAggregate
{
    public class NotFoundException : ArgumentException
    {
        public NotFoundException(string message): base(message)
        {
        }
    }
}
