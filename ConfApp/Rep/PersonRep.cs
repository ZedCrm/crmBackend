using App.Object.Base;
using Domain.Objects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfApp.Rep
{
    public class PersonRep : BaseRep<Person, int>, IPersonRep
    {
        public PersonRep(MyContext context) : base(context)
        {

        }
    }
}
