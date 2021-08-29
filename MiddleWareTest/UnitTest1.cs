using NUnit.Framework;

namespace MiddleWareTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestMergeLabel()
        {
            const string test = @"    global; int; t0;  ;
    decl_var; int; a;  ;
    Je; 1; 0; label_1;
    =; 1;  ; a;
    decl_var; int; b;  ;
    ==; a; 2; t0;
    Je; t0; 0; label_0;
    label_1; label_3; label_4; label_2; 
    =; 3;  ; b;
label_0:
label_1:
    end;  ;  ;  ;
label_2:
label_3:
label_4:
";
            const string rlt = @"    global; int; t0;  ;
    decl_var; int; a;  ;
    Je; 1; 0; label_0;
    =; 1;  ; a;
    decl_var; int; b;  ;
    ==; a; 2; t0;
    Je; t0; 0; label_0;
    label_0; label_2; label_2; label_2; 
    =; 3;  ; b;
label_0:
    end;  ;  ;  ;
label_2:
";

            Assert.AreEqual(rlt.Trim(), MiddleWares.MergeLabel.Merge(test).Trim());
        }
    }
}