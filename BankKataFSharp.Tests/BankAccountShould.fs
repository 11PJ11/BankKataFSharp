module BankAccountShould

open NUnit.Framework
open NSubstitute
open Swensen.Unquote
open BankKataFSharp.Source.BankAccount

let clock = Substitute.For<IClock>()
let printer = Substitute.For<IStatementPrinter>()

[<Test>]
let ``print only the statement header given an empty bank account`` () = 
    let bankAccount = BankAccount clock

    bankAccount 
    |> printStatement printer 
    |> ignore 

    printer.Received().printHeader()


