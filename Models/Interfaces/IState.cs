using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlumniManagement.StateService;

namespace AlumniManagement.Models.Interfaces
{
    public interface IState
    {
        IEnumerable<StateDTO> GetStates();
    }
}
