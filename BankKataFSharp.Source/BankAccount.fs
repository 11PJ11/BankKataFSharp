module Bank
    open NodaTime
    open FSharpx.State
    
    type todo = Unit
    type BankAccount = BankAccount of IClock

    let deposit amount = state {
            let! (account:BankAccount) = getState
            return ()
        }

    let withdraw amount = state {
            let! (account:BankAccount) = getState
            return ()
        }

    let printStatement = state {
            let! (account:BankAccount) = getState
            let statements = [""]
            return statements
        }