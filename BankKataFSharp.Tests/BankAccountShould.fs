module BankAccountShould

open NUnit.Framework
open NSubstitute
open Swensen.Unquote
open BankKataFSharp.Source.BankAccount

let clock = Substitute.For<IClock>()
let printer = Substitute.For<IStatementPrinter>()
let bankAccount = BankAccount clock

[<Test>]
let ``print the statement header`` () = 
    bankAccount 
    |> printStatement printer 
    |> ignore 

    printer.Received().printHeader()
    

[<Test>]
let ``print statement lines`` () = 
    bankAccount 
    |> printStatement printer 
    |> ignore 

    printer.Received().printStatements(Arg.Any<BankAccount>())


