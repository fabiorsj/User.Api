using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace User.Library
{
    public class Result
    {
        HashSet<string> _errors;
        public IEnumerable<string> Errors
        {
            get { return _errors; }
        }
        public bool Success
        {
            get { return Errors.Any() == false; }
        }

        public bool ContainErrors
        {
            get { return Errors.Any(); }
        }


        public Result()
        {
            _errors = new HashSet<string>();
        }

        public void AddError(string erro)
        {
            if (String.IsNullOrWhiteSpace(erro))
            {
                return;
            }
            if (_errors.Contains(erro) == false)
            {
                _errors.Add(erro);
            }
        }

        public void AddErrors(IEnumerable<string> erros)
        {
            foreach (var error in erros)
            {
                AddError(error);
            }
        }

    }
}
