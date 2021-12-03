using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Models;

namespace Catalog.Reposotories
{
    public interface IFormRepository
    {
        Task <IEnumerable<Form>> Search(string lärare);
        Task<Form> GetForm(Guid id);
        Task <IEnumerable<Form>> GetForms();
        Task CreateForm(Form form);
        Task UpdateForm(Form form);
        Task DeleteForm(Guid id);
    }
}