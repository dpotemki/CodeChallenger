using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExecutionContracts.Models
{
    public class TestCase
    {
        public List<InputParameter> InputParameters { get; set; }
        public InputParameter ExpectedResult { get; set; }
    }
}
