namespace BankAccount.Source

module Infrastructure =
    open System
    open Model

    type todo = Unit

    type IDisplay =
        abstract member show:string -> Unit

    type IStatementPrinter =    
        abstract member printHeader: Unit -> Unit
        abstract member print: BankAccount -> Unit
        
    type StatementPrinter(display:IDisplay) =
        let _display = display

        interface IStatementPrinter with
            member x.printHeader() = ()
            member x.print account = ()