using App.Contracts.Object.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Object.Base
{
    public class PersonApp : IPersonApp
    {
        private readonly IPersonRep _ctx;
        public PersonApp(IPersonRep personRep)
        {
            _ctx = personRep;
        }
        public List<PersonView> personViews()
        {
            var persons = _ctx.Get();

            return persons.Select(c => new PersonView
            {
                Id = c.Id,
                Name = c.Name,
                Family = c.Family,
                age = c.age,

            }).ToList();

        }
    }
}
