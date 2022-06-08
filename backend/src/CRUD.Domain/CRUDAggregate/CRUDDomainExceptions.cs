using System;

namespace CRUD.Domain.CRUDAggregate
{
    public class InvalidUFExceptions : ArgumentException
    {
        public InvalidUFExceptions(): base("UF cannot be longer than 2 characters.")
        {
        }
    }
}
