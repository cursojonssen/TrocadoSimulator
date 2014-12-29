using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrocadoSimulator.BusinessLogic.Processors {

    public abstract class AbstractChangeProcessor {

        //List<ChangeData> changeCollection;
        //public AbstractChangeProcessor() {
        //    changeCollection = new List<ChangeData>();
        //}

        public abstract int[] AvailableValues();

        public abstract string GetName();

        abstract public List<ChangeData> Calculate(int amountInCents);
    }
}
