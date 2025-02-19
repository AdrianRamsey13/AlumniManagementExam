using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniManagement.AlumniService;
using AlumniManagement.Models.Interfaces;
using AlumniManagement.StateService;

namespace AlumniManagement.Models.Repositories
{
    public class State : IState
    {
        private StateServiceClient _stateServiceClient;
        public AlumniDataContext _context;

        public State()
        {
            _stateServiceClient = new StateServiceClient();
        }

        IEnumerable<StateService.StateDTO> IState.GetStates()
        {
            var states = _stateServiceClient.GetStates().ToList();
            //var result = states.Select(s => Mapping.Mapper.Map<DTO.StateDTO>(s));
            return states;
        }
    }
}