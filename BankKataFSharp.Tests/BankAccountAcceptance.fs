namespace BankKataFSharp.Tests

module BankAccountAcceptance =
    open NUnit.Framework
    open BankKataFSharp.Source
    open NSubstitute

    let console = Substitute.For<IDisplay>()
    let statementPrinter = new StatementPrinter()
    let transactions = new Transactions([])
    let bankAccount = new BankAccount(statementPrinter, transactions)


    [<Ignore>][<Test>]
    let ignoreMe() = ()


    [<Test>]
    let ``it outputs the bank statement of the operations``() =
        bankAccount.deposit 1000
        bankAccount.withdraw 100
        bankAccount.deposit 500

        bankAccount.printStatement

        console.Received(1).Show("DATE | AMOUNT | BALANCE")
        console.Received(1).Show("10/04/2014 | 500.00 | 1400.00")
        console.Received(1).Show("02/04/2014 | -100.00 | 900.00")
        console.Received(1).Show("01/04/2014 | 1000.00 | 1000.00")