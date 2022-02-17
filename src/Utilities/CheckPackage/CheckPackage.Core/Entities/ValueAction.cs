using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Entities
{
    public enum ValueAction
    {
        equals,
        notequals,        
        contains,        
        notcontains,        
        startWith,        
        endWith,
        regexmatch,
        regexnotmatch
    }    
}
