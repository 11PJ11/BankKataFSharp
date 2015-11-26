namespace BankKataFSharp.Tests

module BankAccountAcceptance =
    open NUnit.Framework
    open BankKataFSharp.Source
    open NSubstitute

    let console = Substitute.For<IDisplay>()
    let statementPrinter = StatementPrinter console
    let transactions = Transactions([])
    let bankAccount = BankAccount(statementPrinter, transactions)


    [<Ignore>][<Test>]
    let ignoreMe() = ()


    [<Test>]
    let ``it outputs the bank statement of the operations``() =
        bankAccount.deposit 1000
        bankAccount.withdraw 100
        bankAccount.deposit 500

        bankAccount.printStatement

        console.Received().Show "DATE | AMOUNT | BALANCE" 
        console.Received().Show "10/04/2014 | 500.00 | 1400.00" 
        console.Received().Show "02/04/2014 | -100.00 | 900.00" 
        console.Received().Show "01/04/2014 | 1000.00 | 1000.00" 