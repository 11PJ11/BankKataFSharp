module BankAccountShould

    open NUnit.Framework
    open BankKataFSharp.Source
    open NSubstitute

    let statementPrinter = Substitute.For<IStatementPrinter>()
    let transactions = Substitute.For<ITransactions>()
    let bankAccount = BankAccount(statementPrinter, transactions)

    [<Test>]
    let ``store a deposit`` () =
        bankAccount.deposit 100

        transactions.Received().Add(Arg.Any<Transaction>())

    [<Test>]
    let ``print the header`` () =

        bankAccount.printStatement

        statementPrinter.Received().PrintHeader()

    [<Test>]
    let ``print the transactions`` () =
        bankAccount.printStatement

        statementPrinter.Received().Print(Arg.Any<ITransactions>())