using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrocadoSimulator.BusinessLogic.Processors;
using System.Diagnostics.CodeAnalysis;

namespace TrocadoSimulator.Tests.BusinessLogic {
    /// <summary>
    /// Summary description for FactoryTest
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FactoryTest {
        
        public FactoryTest() {
            //
            // TODO: Add constructor logic here
            //
        }

        [TestMethod]
        public void Processor_BankNoteFactoryTest() {
            AbstractChangeProcessor abstractProcessor = ProcessorFactory.Create(200);
            Assert.IsNotNull(abstractProcessor);
            Assert.IsTrue(abstractProcessor is BankNoteProcessor);
        }
        
        [TestMethod]
        public void Processor_CoinFactoryTest() {
            AbstractChangeProcessor abstractProcessor = ProcessorFactory.Create(199);
            Assert.IsNotNull(abstractProcessor);
            Assert.IsTrue(abstractProcessor is CoinProcessor);
        }

        [TestMethod]
        public void Processor_NegativeChangeAmount() {

            AbstractChangeProcessor abstractProcessor = ProcessorFactory.Create(-3);
            Assert.IsNull(abstractProcessor);
        }
    }
}
