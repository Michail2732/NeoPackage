using CheckPackage.Core.Checks;
using CheckPackage.Core.Abstract;
using CheckPackage.Tests.Core.Mocks;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using CheckPackage.Core.Regex;

namespace CheckPackage.Tests.Core.Check
{

    //[TestFixture]
    //public class ParameterSDictionaryCheckTests
    //{        
    //    [Test]        
    //    public void Check_AnyArgumentNull_ArgumentNullException() 
    //    {
    //        var instance = CreateInstance();
    //        var context = CreateContext();
    //        var checkInfo = new StubCheckInfo();

    //        Assert.Catch<ArgumentNullException>(() => instance.Check(checkInfo, null));
    //        Assert.Catch<ArgumentNullException>(() => instance.Check(null, context));
    //        Assert.Catch<ArgumentNullException>(() => instance.Check(null, null));
    //    }

    //    //[Test]
    //    //public void Check_AnyArgumentEmpty_false()
    //    //{
    //    //    var instance = CreateInstance();
    //    //    for (int i = 0; i < 16 ; i++)
    //    //    {
    //    //        BitVector32 bitMask = new BitVector32(i);
    //    //        var checkInfo = new Pa(
    //    //            bitMask[1] ? string.Empty : "some value")
    //    //        {
    //    //            Value = bitMask[2] ? string.Empty : "some value",
    //    //            Logic = bitMask[4] ? LogicalOperator.and : LogicalOperator.or,                    
    //    //        };
    //    //        var context = CreateContext(
    //    //         bitMask[8] ? null : new Dictionary<string, SimpleDictionary>
    //    //                 {{ "some key", new SimpleDictionary(new List<string>(), "some name")} });

    //    //        Assert.IsFalse(instance.Check(checkInfo, context).IsSuccess);            
    //    //    }
    //    //}

    //    [Test]
    //    public void Check_EqualWhithout() 
    //    {

    //    }


    //    private ParameterSDictionaryCheck CreateInstance() => new ParameterSDictionaryCheck();

    //    private CheckContext CreateContext(IReadOnlyDictionary<string, SimpleDictionary> dictionaries = null) =>
    //        new CheckContext(                                
    //            Substitute.For<Repository<MatrixDictionary, string>>(),
    //            (dictionaries != null
    //                ? new MockSimpleDictionaryRepository(dictionaries.Select(a => a.Value)) 
    //                : new MockSimpleDictionaryRepository())
    //            );
    //}

}
