module StatementPrinterTests

    open NUnit.Framework
    open NSubstitute
    open BankKataFSharp.Source
    
    let console = Substitute.For<IDisplay>()
    let statementPrinter = (StatementPrinter console):> IStatementPrinter

    [<Test>]
    let ``print the header`` () =

        statementPrinter.PrintHeader()

        console.Received().Show "DATE | AMOUNT | BALANCE"