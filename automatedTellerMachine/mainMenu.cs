using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automatedTellerMachine
{
    public class mainMenu
    {
        printUtil p = new printUtil();
        List<bill> atmInventory = new List<bill>();
        int[] dollarAmounts = { 1, 5, 10, 20, 50, 100 };

        #region menuItems

        public void main()
        {
            string input = p.rl("\n\r" + "Enter Command: ", p.wht, p.blue);
            string[] inputData = input.Split('$');
            string command = inputData[0].Trim(' ');
            switch (command)
            {
                case "w":
                case "W":
                    int dollarsRequested;
                    bool pass = int.TryParse(inputData[1], out dollarsRequested);
                    if (pass)
                    {
                        makeWithdrawal(dollarsRequested);
                    }
                    main();
                    return;
                case "i":
                case "I":
                    for(int ii = 1;ii < inputData.Count(); ii++)
                    {
                        int dollarValue;
                        string curDollar = inputData[ii].Trim(' ');
                        bool _pass = int.TryParse(curDollar, out dollarValue);
                        if (_pass)
                        {
                            checkInventory(dollarValue);
                        }
                    }
                    main();
                    return;
                case "r":
                case "R":
                    refillBills();
                    main();
                    return;
                case "q":
                case "Q":
                    break;
                default:
                    main();
                    return;
            }
        }

        #endregion menuItems

        #region utils

        public void checkInventory(int dollarValue)
        {
            int qty = atmInventory.Where(x => x.dollarValue == dollarValue).Count();
            p.write("\n\r" + "$" + dollarValue + " - " + qty.ToString(), p.grn);
        }
        public void makeWithdrawal(int dollarsRequested)
        {
            int[] descendAmounts = { 100, 50, 20, 10, 5, 1};
            List<bill> dollarsToWithdraw = new List<bill>();
            //bill newBill = new bill();
            int currentAmount = dollarsRequested;
            foreach(int amount in descendAmounts)
            {
                while(currentAmount >= 0)
                {
                    //newBill.dollarValue = amount;
                    if(currentAmount - amount >=  0)
                    {
                        if (atmInventory.Where(x => x.dollarValue == amount).Count() > 0)
                        {
                            bill currentBill = atmInventory.Where(x => x.dollarValue == amount).First();
                            atmInventory.Remove(currentBill);
                            dollarsToWithdraw.Add(currentBill);
                            currentAmount -= amount;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if(currentAmount > 0)
            {
                atmInventory.AddRange(dollarsToWithdraw);
                p.write("Failure: Insufficient Funds.", p.red);
            }
            else
            {
                p.write("Success: Dispensed $" + dollarsRequested.ToString(), p.grn);
                foreach (int amount in descendAmounts)
                {
                    bill _bill = new bill();
                    _bill.dollarValue = amount;
                    if (dollarsToWithdraw.Where(x => x.dollarValue == amount).Count() > 0)
                    {
                        p.write("\n\r" + "$" + amount.ToString() + " - " + dollarsToWithdraw.Where(x => x.dollarValue == amount).Count().ToString(), p.grn);
                    }
                }
            }
        }
        public void initializeBills()
        {
            foreach(int amount in dollarAmounts)
            {
                bill newBill = new bill();
                newBill.dollarValue = amount;
                for(int i = 0;i < 10; i++)
                {
                    atmInventory.Add(newBill);
                }
            }
        }
        public void refillBills()
        {
            foreach(int amount in dollarAmounts)
            {
                int billCount = atmInventory.Where(x => x.dollarValue == amount).Count();
                bill newBill = new automatedTellerMachine.bill();
                newBill.dollarValue = amount;
                for(int i = billCount;i < 10; i++)
                {
                    atmInventory.Add(newBill);
                }
            }
        }

        #endregion utils
    }
}
