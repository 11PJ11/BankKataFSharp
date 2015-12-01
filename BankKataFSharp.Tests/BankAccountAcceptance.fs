namespace BankKataFSharp.Tests

module BankAcceptance =
    open Bank
    open NUnit.Framework
    open NodaTime
    open NodaTime.Testing
    open Swensen.Unquote
    open Swensen.Unquote.Assertions
    open FSharpx.State

    [<Measure>]
    type day =
        static member asDuration value = Duration.FromStandardDays(int64 value)

    let advanceDays (days:int<day>) (clock:FakeClock) =
        clock.AdvanceDays(int64 days) 

    [<Test>][<Timeout(500)>]
    let ``it outputs the bank statement of the operations``() =
        let clock = FakeClock.FromUtc(2014, 4, 1)
        let account = BankAccount clock
        let scenario = 
            state {
                //now is the 1/4/2014
                do! deposit 1000
                advanceDays 1<day> clock 
                //now is the 2/4/2014
                do! withdraw 100
                advanceDays 8<day> clock
                //now is the 10/4/2014
                do! deposit 500
                return! printStatement
            }

        let statements = eval scenario account

        statements =! [ "DATE | AMOUNT | BALANCE" 
                      ; "10/04/2014 | 500.00 | 1400.00" 
                      ; "02/04/2014 | -100.00 | 900.00" 
                      ; "01/04/2014 | 1000.00 | 1000.00"]