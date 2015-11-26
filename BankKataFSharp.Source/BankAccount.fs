namespace BankKataFSharp.Source

    open System

//
//    type Amount = Amount of int
//
//    type Transaction = 
//        | Credit of DateTime * Amount
//        | Debit of DateTime * Amount
//
//    type Transactions = Transactions of Transaction list
    

    type BankAccount() =
        member x.deposit (amount:int) = ()
        member x.withdraw (amount:int) = ()
        member x.printStatement = ()

    type IDisplay =
        abstract member Show: string -> Unit 