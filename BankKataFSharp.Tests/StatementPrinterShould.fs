module StatementPrinterShould

open NUnit.Framework
open NSubstitute
open BankKataFSharp.Source.BankAccount

let console = Substitute.For<IDisplay>()

[<Test>]
let ``print the header`` () =
    let printer = (StatementPrinter console) :> IStatementPrinter
    
    printer.printHeader()

    console.Received().show "DATE | AMOUNT | BALANCE" 