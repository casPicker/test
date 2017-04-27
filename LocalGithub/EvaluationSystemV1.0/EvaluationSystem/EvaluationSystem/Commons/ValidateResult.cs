using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluationSystem.Commons
{
    class ValidateResult
    {
        private bool isOk;
        private string message;

        public ValidateResult(bool isOk,string message)
        {
            this.isOk = isOk;
            this.message = message;
        }

        public bool IsOk
        {
            get { return this.isOk; }
            set { this.isOk = value; }
        }

        public string Message
        {
            get { return this.message; }
            set { this.message = value; }
        }
    }
}
