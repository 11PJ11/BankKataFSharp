module BankAccountShould

    open NUnit.Framework
    open BankKataFSharp.Source
    open NSubstitute

    let statementPrinter = Substitute.For<IStatementPrinter>()
    let transactions = Transactions([])
    let bankAccount = BankAccount(statementPrinter, transactions)

    [<Test>]
    let ``print the header`` () =

        bankAccount.printStatement

        statementPrinter.Received().PrintHeader()

    [<Test>]
    let ``print the transactions`` () =
        bankAccount.printStatement

        statementPrinter.Received().Print(Arg.Any<Transactions>())