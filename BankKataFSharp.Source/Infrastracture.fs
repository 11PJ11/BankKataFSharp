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
        let HEADER = "DATE | AMOUNT | BALANCE"
        let _display = display

        interface IStatementPrinter with
            member x.printHeader() = 
                _display.show HEADER
            member x.print account = ()