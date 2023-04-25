using Ardalis.Specification;
using BancoAPI.Domain.Entities;

namespace BancoAPI.Application.Specifications
{
    public class PagedClientsSpecification : Specification<Client>
    {
        public PagedClientsSpecification(int pageSize, int pageNumber, string name, string lastName)
        {
            Query.Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize);

            if (!string.IsNullOrEmpty(name))
                Query.Search(x => x.Name!, "%" + name + "%");

            if (!string.IsNullOrEmpty(lastName))
                Query.Search(x => x.LastName!, "%" + lastName + "%");
        }
    }
}
