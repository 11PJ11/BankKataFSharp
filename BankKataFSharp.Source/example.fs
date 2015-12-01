namespace BankKataFSharp.Source

module Social =
    open FSharpx.State
    open NodaTime

    type User = {Name: string}
    type Quack = {User: User; Text: string; Timestamp: Instant}
    type Quacks = Quack list
    type Timeline = Quack list
    type Timelines = Map<User, Quacks * Timeline>
    type Following = Map<User, User list>
    type Network = {
        Timelines: Timelines; 
        Following: Following; 
        Now: unit -> Instant}

    let newSocialNetwork (clock: IClock) = {
        Timelines = Map.empty; 
        Following = Map.empty; 
        Now = (fun () -> clock.Now)}

    let timeline user = state {
        let! (network: Network) = getState
        return defaultArg (Map.tryFind user network.Timelines) ([], []) }

    let read user = state {
        return! timeline user |> map fst }

    let post user message = state {
        let! (network: Network) = getState
        let! quacks, timeline = timeline user
        let newQuack = {User = user; Text = message; Timestamp = network.Now()}
        do! putState { 
            network with 
                Timelines = Map.add user (newQuack :: quacks, newQuack :: timeline) network.Timelines 
            }
        return () }

    let follows follower followee = state {
        let! (network: Network) = getState
        let followees = defaultArg (Map.tryFind follower network.Following) []
        let newFollowing = Map.add follower (followee :: followees) network.Following
        let! followerQuacks, followerTimeline = timeline follower
        let! followeeQuacks = timeline followee |> map fst
        let newTimeline = List.append followeeQuacks followerTimeline |> List.sortWith (fun a b -> Operators.compare b.Timestamp a.Timestamp)
        do! putState { network with Timelines = Map.add follower (followerQuacks, newTimeline) network.Timelines; Following = newFollowing }
        return () }

    let timelineFor user = state {
        return! timeline user |> map snd }

    