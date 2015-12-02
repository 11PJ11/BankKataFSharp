module StatementPrinterShould

    open BankAccount.Source.Infrastructure
    open NUnit.Framework
    open NSubstitute


    let console = Substitute.For<IDisplay>()
    let HEADER = "DATE | AMOUNT | BALANCE"

    [<Test>]
    let ``print the header`` () =
        
        let printer = (StatementPrinter console) :> IStatementPrinter 

        printer.printHeader()

        console.Received().show HEADER