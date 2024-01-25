using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExecutionContracts.Models
{
    public class ExecuteCodeRequest
    {
        public Guid Id { get; set; }
        public string CodeToExecute { get; set; } 
        public string MainMethodName { get; set; }
        public List<TestCase> TestCases { get; set; } = new List<TestCase>();
    }
}
