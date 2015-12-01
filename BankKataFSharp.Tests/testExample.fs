module Tests 
    open BankKataFSharp.Source.Social
    open NUnit.Framework
    open NodaTime
    open NodaTime.Testing
    open Swensen.Unquote
    open Swensen.Unquote.Assertions
    open FSharpx.State

    [<Measure>]
    type minute =
        static member asDuration value = Duration.FromMinutes(int64 value)

    [<Measure>]
    type hour =
        static member asDuration value = Duration.FromHours(int64 value)

    let advanceMinutes (time: int<minute>) (clock: FakeClock) =
        clock.AdvanceMinutes(int64 time)
    let advanceHours (time: int<hour>) (clock: FakeClock) =
        clock.AdvanceHours(int64 time)

    let assertThat (actual: 'T) (expression: Constraints.IResolveConstraint) = Assert.That (actual, expression)

    [<Test>]
    let ``Alice can publish messages to a personal timeline``() =
        let alice = {Name = "Alice"}

        let clock = FakeClock.FromUtc(2015, 5, 4)
        let network = newSocialNetwork clock

        let postMessage = state { 
                            do! post alice "message"
                            return! timeline alice |> map fst
                          }

        let aliceTimeline = eval postMessage network 

        aliceTimeline =! [{User = alice; Text = "message"; Timestamp = clock.Now}]

    [<Test>]
    let ``Anyone can view Alice’s timeline``() =
        let alice = {Name = "Alice"}
        let bob = {Name = "Bob"}

        let clock = FakeClock.FromUtc(2015, 5, 5, 13, 0, 0)
        let network = exec (state {
            do! post bob "message from bob"
            advanceHours 1<hour> clock
            do! post alice "message from alice" }) (newSocialNetwork clock)

        let quacksByAlice = eval (read alice) network
             
        quacksByAlice =! 
            [{User = alice; Text = "message from alice"; Timestamp = clock.Now}]

    [<Test>]
    let ``Carol can subscribe to Alice’s and Bob’s timelines, and view an aggregated list of all subscriptions``() =
        let alice = {Name = "Alice"}
        let bob = {Name = "Bob"}
        let carol = {Name = "Carol"}
        let dave = {Name = "Dave"}

        let clock = FakeClock.FromUtc(2015, 3, 14)
        let network = exec (state {
            do! post alice "Is anyone home?"
            advanceMinutes 5<minute> clock
            do! post bob "I am. Come say hi!"
            do! post dave "Oh?"
            advanceMinutes 2<minute> clock
            do! post carol "I'm coming!"
            advanceMinutes 1<minute> clock
            do! post dave "I can't make it. :-("
            advanceMinutes 2<minute> clock
            do! post alice "OK, party time." }) (newSocialNetwork clock)

        let timelineForCarol = eval (state {
                do! follows alice bob
                do! follows carol alice
                do! follows carol bob
                return! timelineFor carol }) network
            
        let now = clock.Now
        timelineForCarol =!
            [{User = alice; Text = "OK, party time.";    Timestamp = now}
            ;{User = carol; Text = "I'm coming!";        Timestamp = now - (3<minute> |> minute.asDuration)}
            ;{User = bob;   Text = "I am. Come say hi!"; Timestamp = now - (5<minute> |> minute.asDuration)}
            ;{User = alice; Text = "Is anyone home?";    Timestamp = now - (10<minute> |> minute.asDuration)}]